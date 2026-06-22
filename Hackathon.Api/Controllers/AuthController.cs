using Hackathon.Api.Extention;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthsService = Hackathon.Service.Auths;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthsService.IService _authService;

    public AuthController(AuthsService.IService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(AuthsService.Request.RegisterRequest request)
    {
        var result = await _authService.Register(request);
        return Ok(ApiResponseFactory.Base(null,200,result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("tokens/refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        var result = await _authService.RefreshToken();
        Response.WriteAuthCookies(result.AccessToken!, result.RefreshToken!);
        return Ok(ApiResponseFactory.Base(result,200,"SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("email-verifications")]
    public async Task<IActionResult> VerifyEmail(AuthsService.Request.VerifyEmailRequest request)
    {
        var result = await _authService.VerifyEmail(request);
        if (!string.IsNullOrEmpty(result?.AccessToken) && !string.IsNullOrEmpty(result.RefreshToken))
        {
            Response.WriteAuthCookies(result.AccessToken, result.RefreshToken);
        }

        var message = !string.IsNullOrEmpty(result?.AccessToken)
            ? "EMAIL_VERIFICATION_SUCCESSFUL"
            : "USER_ALREADY_VERIFIED";
        return Ok(ApiResponseFactory.Base(result,200,message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize]
    [HttpGet("/api/v1/auth/me")]
    public async Task<IActionResult> GetMe()
    {
        var result = await _authService.GetMe();
        return Ok(ApiResponseFactory.Base(result,200,"SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var message = await _authService.Logout();
        Response.DeleteAuthCookies();
        return Ok(ApiResponseFactory.Base(null,200,message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize]
    [HttpPatch("change-password")]
    public async Task<IActionResult> ChangePassword(AuthsService.Request.ChangePasswordRequest request)
    {
        var message = await _authService.ChangePassword(request);
        return Ok(ApiResponseFactory.Base(null,200,message, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(AuthsService.Request.ForgotPasswordRequest request)
    {
        var message = await _authService.ForgotPassword(request);
        return Ok(ApiResponseFactory.Base(null,200,message, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(AuthsService.Request.ResetPasswordRequest request)
    {
        var message = await _authService.ResetPassword(request);
        return Ok(ApiResponseFactory.Base(null,200,message, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("email-verifications/resend")]
    public async Task<IActionResult> ResendEmailVerification(AuthsService.Request.ResendEmailVerificationRequest request)
    {
        var message = await _authService.ResendEmailVerification(request);
        return Ok(ApiResponseFactory.Base(null,200,message, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(AuthsService.Request.LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        Response.WriteAuthCookies(result.AccessToken!, result.RefreshToken!);
        return Ok(ApiResponseFactory.Base(result,200,"LOGIN_SUCCESSFUL", traceId: HttpContext.TraceIdentifier));
    }
}
