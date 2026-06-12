using Hackathon.Api.Extention;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthService = Hackathon.Service.Auth;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService.IService _authService;

    public AuthController(AuthService.IService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(AuthService.Request.RegisterRequest request)
    {
        var result = await _authService.Register(request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("tokens/refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        var result = await _authService.RefreshToken();
        Response.WriteAuthCookies(result.AccessToken!, result.RefreshToken!);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("email-verifications")]
    public async Task<IActionResult> VerifyEmail(AuthService.Request.VerifyEmailRequest request)
    {
        var result = await _authService.VerifyEmail(request);
        if (!string.IsNullOrEmpty(result?.AccessToken) && !string.IsNullOrEmpty(result.RefreshToken))
        {
            Response.WriteAuthCookies(result.AccessToken, result.RefreshToken);
        }

        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize]
    [HttpGet("/api/users/me")]
    public async Task<IActionResult> GetMe()
    {
        var result = await _authService.GetMe();
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var result = await _authService.Logout();
        Response.DeleteAuthCookies();
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize]
    [HttpPatch("change-password")]
    public async Task<IActionResult> ChangePassword(AuthService.Request.ChangePasswordRequest request)
    {
        var result = await _authService.ChangePassword(request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(AuthService.Request.ForgotPasswordRequest request)
    {
        var result = await _authService.ForgotPassword(request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(AuthService.Request.LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        Response.WriteAuthCookies(result.AccessToken!, result.RefreshToken!);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
