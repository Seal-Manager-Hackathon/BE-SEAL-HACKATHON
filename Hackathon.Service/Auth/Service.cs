using System.Security.Claims;
using System.Text.RegularExpressions;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.MailService;
using Hackathon.Service.Models;
using Hackathon.Service.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IService = Hackathon.Service.MailService.IService;

namespace Hackathon.Service.Auth;

public class Service : IService
{
    private readonly AppDbContext _dbContext;
    private readonly SecurityOption _securityOptions = new();
    private readonly JwtService.IService _jwtService;
    private readonly MailService.IService _mailService;
    private readonly IHttpContextAccessor _httpContext;

    public Service(AppDbContext dbContext, IConfiguration configuration,
        MailService.IService mailService, IHttpContextAccessor httpContextAccessor,
        JwtService.IService jwtService)
    {
        _dbContext = dbContext;
        configuration.GetSection(nameof(SecurityOption)).Bind(_securityOptions);
        _jwtService = jwtService;
        _mailService = mailService;
        _httpContext = httpContextAccessor;
    }
    
    private bool CheckExpiredAccessToken()
    {
        var check = _httpContext.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Expiration);
        if (check != null)
        {
            if (long.TryParse(check.Value, out long expUnixTime))
            {
                var expirationTime = DateTimeOffset.FromUnixTimeSeconds(expUnixTime);
                if (expirationTime > DateTimeOffset.UtcNow)
                {
                    // Token VẪN CÒN HẠN
                    Console.WriteLine("Token vẫn còn hiệu lực.");
                    return false;
                }
                return true;
            }
        }
        return true;
    }

    private string CheckRefreshToken()
    {
        _httpContext.HttpContext!.Request.Cookies.TryGetValue("Refresh-Token", out var check);
        if (check == null)
        {
            throw new MissingAccessTokenException();
        }

        return check;
    }

    public async Task<string> Register(Request.RegisterRequest request)
    {
        if (request.Password != request.ConfirmPassword)
        {
            throw new BadRequestException("PASSWORD_CONFIRMATION_NOT_MATCH");
        }

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var isExistEmail = await _dbContext.Users
                .AnyAsync(x => x.Email.ToLower() == request.Email.ToLower() );
            if (isExistEmail)
            {
                throw new ConflictException("EMAIL_ALREADY_EXISTS");            }

            var pepperPassword = request.Password + _securityOptions.Pepper;
            //haha
            var hashedPassword = global::BCrypt.Net.BCrypt.EnhancedHashPassword(pepperPassword, hashType: global::BCrypt.Net.HashType.SHA256);

            var newUser = new Hackathon.Repository.Entity.Users()
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                HashPassword = hashedPassword,
                Role = RoleEnum.Student,
                IsVerified = false
            };
            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            var newClaims = new List<Claim>()
            {
                new Claim("UserId", newUser.Id.ToString()),
                new Claim("Role", RoleEnum.Student.ToString()),
                new Claim("IsVerified", newUser.IsVerified.ToString().ToLower()),
            };
            var emailToken = _jwtService.GenerateEmailVerificationToken(newClaims, 2);

            await _mailService.SendMail(new MailContent
            {
                To = request.Email,
                Subject = "Hoa Theo Mua",
                Body = MailTemplate.EmailContainToken(emailToken),
            });


            await transaction.CommitAsync();
            return "Đăng ký thành công. Vui lòng kiểm tra email để xác thực tài khoản.";
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Response.AuthResponse> RefreshToken()
    {
        // Trả về false tức là: CÒN ACCESS VÀ CÒN HẠN
        bool isMissingAccessToken = CheckExpiredAccessToken();

        if (!isMissingAccessToken)
        {// Nếu trả về true: Nghĩa là KHÔNG CÓ ACCESS TOKEN -> Luồng tự động trôi xuống bước 2
            throw new BadRequestException("ACCESS_TOKEN_STILL_VALID");        } 

        var rawRefreshToken = CheckRefreshToken();

        var storedToken = await _dbContext.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.RefreshTokenHash == rawRefreshToken);

        if (storedToken == null)
        {
            throw new ExpiredRefreshTokenException();
        }

        bool isActive = storedToken.RevokedAt == null && storedToken.ExpiredAt > DateTimeOffset.UtcNow;

        if (!isActive)
        {
            throw new ExpiredRefreshTokenException();
        }

        storedToken.RevokedAt = DateTimeOffset.UtcNow;
        await _dbContext.SaveChangesAsync();

        string newRawRefreshToken = _jwtService.GenerateRefreshToken();

        var newRefreshTokenEntity = new Hackathon.Repository.Entity.RefreshTokens()
        {
            RefreshTokenHash = newRawRefreshToken, // Lưu chuỗi thuần trực tiếp theo cấu hình hệ thống
            UserId = storedToken.User.Id,
            IpAddress = _httpContext.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
            UserAgent = _httpContext.HttpContext?.Request.Headers["User-Agent"].ToString() ?? "Unknown",
            DeviceLabel = storedToken.DeviceLabel,
            ExpiredAt = DateTimeOffset.UtcNow.AddDays(7),
            CreatedAt = DateTimeOffset.UtcNow,
        };
        _dbContext.RefreshTokens.Add(newRefreshTokenEntity);
        await _dbContext.SaveChangesAsync();

        var claimsForNewToken = new List<Claim>
        {
            new Claim("UserId", storedToken.User.Id.ToString()),
            new Claim("Role", storedToken.User.Role.ToString()),
            new Claim("IsVerified", storedToken.User.IsVerified.ToString().ToLower()),
            new Claim(ClaimTypes.Role, storedToken.User.Role.ToString()),
        };
        string newAccessToken = _jwtService.GenerateAccessToken(claimsForNewToken);

        var result = new Response.AuthResponse()
        {
            RefreshToken = newRawRefreshToken,
            AccessToken = newAccessToken,
        };

        return result;
    }

    public async Task<Response.VerifyEmailResponse?> VerifyEmail(Request.VerifyEmailRequest request)
    {
        var validateToken = _jwtService.ValidateToken(request.Token);
        if (validateToken == null)
        {
            throw new BadRequestException("INVALID_OR_EXPIRED_EMAIL_VERIFICATION_TOKEN");
        }

        var userIdStr = validateToken.FindFirst("UserId")?.Value;
        var userId = Guid.Parse(userIdStr!);
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
        {
            throw new NotFoundException("USER_NOT_FOUND");        }

        if (user.IsVerified == true)
        {
            return new Response.VerifyEmailResponse
            {
                AccessToken = null!,
                RefreshToken = null!,
                Message = "USER_ALREADY_VERIFIED"
            };
        }

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            user.IsVerified = true;
            _dbContext.Users.Update(user);
            user.UpdatedAt = DateTimeOffset.UtcNow;

            var authClaims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("IsVerified", user.IsVerified.ToString().ToLower()),
            };

            var accessToken = _jwtService.GenerateAccessToken(authClaims);
            var refreshToken = _jwtService.GenerateRefreshToken();

            var httpContext = _httpContext.HttpContext;
            var ipAddress = httpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP";
            var userAgent = httpContext?.Request?.Headers["User-Agent"].ToString() ?? "Unknown Device";

            var refreshTokenEntity = new Hackathon.Repository.Entity.RefreshTokens()
            {
                Id = Guid.NewGuid(),
                RefreshTokenHash = refreshToken,
                IpAddress = ipAddress,
                UserAgent = userAgent,
                RevokedAt = null,
                DeviceLabel = "",
                UserId = user.Id,
                CreatedAt = DateTimeOffset.UtcNow,
                ExpiredAt = DateTimeOffset.UtcNow.AddDays(7),
            };
            await _dbContext.RefreshTokens.AddAsync(refreshTokenEntity);


            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            return new Response.VerifyEmailResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Message = "EMAIL_VERIFICATION_SUCCESSFUL"
            };
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private Guid CheckAccessToken()
    {
        var accessToken = _httpContext.HttpContext?.User.FindFirst("UserId")?.Value;
        if (accessToken != null)
        {
            return Guid.Parse(accessToken);
        }

        return Guid.Empty;
    }

    private static bool IsValidPassword(string password)
    {
        return Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d).{8,}$");
    }

    public async Task<Response.GetMeResponse> GetMe()
    {
        var userId = CheckAccessToken();
        if (userId == Guid.Empty)
        {
            throw new MissingAccessTokenException();
        }

        var query = await _dbContext.Users
            .Where(x => x.Id == userId)
            .Select(y => new Response.GetMeResponse()
            {
                Email = y.Email,
                FirstName = y.FirstName,
                LastName = y.LastName,
                Avatar = y.AvatarUrl,
            }).FirstOrDefaultAsync();

        if (query == null)
        {
            throw new NotFoundException("USER_NOT_FOUND");
        }

        return query;
    }

    public async Task<Response.LogoutResponse> Logout()
    {
        var rtInCookie = CheckRefreshToken();

        var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x =>
            x.RefreshTokenHash == rtInCookie);

        if (refreshToken == null)
        {
            throw new UnauthorizedException("INVALID_REFRESH_TOKEN");
        }

        if (refreshToken.RevokedAt != null)
        {
            throw new UnauthorizedException("USER_ALREADY_LOGGED_OUT");
        }

        refreshToken.RevokedAt = DateTimeOffset.UtcNow;
        _dbContext.RefreshTokens.Update(refreshToken);
        await _dbContext.SaveChangesAsync();

        return new Response.LogoutResponse()
        {
            Message = "LOGOUT_SUCCESSFUL",
        };
    }
    
    public async Task<Response.LoginResponse> LoginAsync(
        Request.LoginRequest request)
    {
        var httpContext = _httpContext.HttpContext;
        var ipAddress = httpContext?.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
        var userAgent = httpContext?.Request.Headers.UserAgent.ToString() ?? "Unknown Device";
        var email = request.Email.Trim();

        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Email == email && !x.IsDisable);

        if (user == null)
        {
            throw new UnauthorizedException("INVALID_EMAIL_OR_PASSWORD");
        }

        var pepperPassword = request.Password + _securityOptions.Pepper;

        var isPasswordValid = global::BCrypt.Net.BCrypt.EnhancedVerify(
            pepperPassword,
            user.HashPassword,
            hashType: global::BCrypt.Net.HashType.SHA256
        );

        if (!isPasswordValid)
        {
            throw new UnauthorizedException("INVALID_EMAIL_OR_PASSWORD");
        }

        var claims = new List<Claim>
        {
            new Claim("UserId", user.Id.ToString()),
            new Claim("Role", user.Role.ToString()),
            new Claim("IsVerified", user.IsVerified.ToString().ToLower()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var accessToken = _jwtService.GenerateAccessToken(claims);

        var refreshToken = _jwtService.GenerateRefreshToken();

        var refreshTokenEntity = new Hackathon.Repository.Entity.RefreshTokens
        {
            Id = Guid.NewGuid(),
            RefreshTokenHash = refreshToken,
            IpAddress = ipAddress,
            UserAgent = userAgent,
            DeviceLabel = "",
            ExpiredAt = DateTimeOffset.UtcNow.AddDays(7),
            UserId = user.Id,
            CreatedAt = DateTimeOffset.UtcNow,
        };

        await _dbContext.RefreshTokens.AddAsync(refreshTokenEntity);
        await _dbContext.SaveChangesAsync();

        var result = new Response.LoginResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Message = "LOGIN_SUCCESSFUL",
            
        };
        return result;

    }

    public async Task<Response.MessageResponse> ChangePassword(Request.ChangePasswordRequest request)
    {
        var userId = CheckAccessToken();
        if (userId == Guid.Empty)
        {
            throw new MissingAccessTokenException();
        }

        if (request.NewPassword != request.ConfirmPassword)
        {
            throw new BadRequestException("PASSWORD_CONFIRMATION_NOT_MATCH");
        }

        if (!IsValidPassword(request.NewPassword))
        {
            throw new BadRequestException("INVALID_PASSWORD_FORMAT");
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null)
        {
            throw new NotFoundException("USER_NOT_FOUND");
        }

        var currentPepperPassword = request.CurrentPassword + _securityOptions.Pepper;
        var isPasswordValid = global::BCrypt.Net.BCrypt.EnhancedVerify(
            currentPepperPassword,
            user.HashPassword,
            hashType: global::BCrypt.Net.HashType.SHA256
        );

        if (!isPasswordValid)
        {
            throw new BadRequestException("CURRENT_PASSWORD_INVALID");
        }

        var newPepperPassword = request.NewPassword + _securityOptions.Pepper;
        user.HashPassword = global::BCrypt.Net.BCrypt.EnhancedHashPassword(newPepperPassword, hashType: global::BCrypt.Net.HashType.SHA256);
        user.UpdatedAt = DateTimeOffset.UtcNow;
        await _dbContext.SaveChangesAsync();

        return new Response.MessageResponse { Message = "PASSWORD_CHANGED_SUCCESSFULLY" };
    }

    public async Task<Response.MessageResponse> ForgotPassword(Request.ForgotPasswordRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            throw new BadRequestException("EMAIL_REQUIRED");
        }

        var email = request.Email.Trim();
        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            throw new BadRequestException("INVALID_EMAIL_FORMAT");
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && !x.IsDisable);
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
            };
            var resetToken = _jwtService.GenerateEmailVerificationToken(claims, 1);
            await _mailService.SendMail(new MailContent
            {
                To = email,
                Subject = "Reset password",
                Body = MailTemplate.EmailContainToken(resetToken),
            });
        }

        return new Response.MessageResponse { Message = "FORGOT_PASSWORD_REQUEST_ACCEPTED" };
    }
}