using System.Security.Claims;
using System.Text;
using Hackathon.Service.JwtService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Hackathon.Api.Extention;

public static class JwtExtensions
{
    public const string AdminPolicy = "AdminPolicy";
    public const string StaffPolicy = "StaffPolicy";
    public const string LecturerPolicy = "LecturerPolicy";
    public const string StudentPolicy = "StudentPolicy";
    public const string StaffOrAdminPolicy = "StaffOrAdminPolicy";
    public const string UserVerifiedPolicy = "UserVerifiedPolicy";
    
    public static void AddJwtServices(this IServiceCollection services, IConfiguration configuration)
    {
        JwtOption jwtOption = new JwtOption();
        configuration.GetSection("JwtOptions").Bind(jwtOption);
        var key = Encoding.UTF8.GetBytes(jwtOption.SecretKey);
    
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,//dung signature, dung issure nua, dung server
                    ValidateAudience = true, // 
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOption.Issuer,
                    ValidAudience = jwtOption.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    NameClaimType = ClaimTypes.NameIdentifier,
                    RoleClaimType = ClaimTypes.Role,
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        
                        if (context.Request.Cookies.TryGetValue(CookieExtensions.AccessTokenCookieName, out var token))
                        {
                            context.Token = token; 
                        }

                        
                        return Task.CompletedTask;
                    }
                };
            });
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AdminPolicy, policy =>
                policy.RequireRole("Admin"));
            // [Authorize(Policy = JwtExtensions.AdminPolicy)]
        
            options.AddPolicy(StaffPolicy, policy =>
                policy.RequireRole("Staff"));
            // [Authorize(Policy = JwtExtensions.StaffPolicy)]
        
            options.AddPolicy(StudentPolicy, policy =>
                policy.RequireRole("Student"));
            
            options.AddPolicy(LecturerPolicy, policy =>
                policy.RequireRole("Lecturer"));
        
            options.AddPolicy(StaffOrAdminPolicy, policy =>
                policy.RequireRole("Staff", "Admin"));
            // [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
            
            options.AddPolicy(UserVerifiedPolicy, policy =>
                policy.RequireRole("Student")
                    .RequireClaim("IsVerified", "true"));
        });
    }
}