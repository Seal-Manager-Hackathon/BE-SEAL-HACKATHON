using Hackathon.Service.Users;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService = Hackathon.Service.Users;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly UserService.IService _userService;

    public UserController(UserService.IService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var result = await _userService.GetUserById(userId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("students")]
    [Authorize]
    public async Task<IActionResult> SearchStudents([FromQuery] Request.SearchStudentsRequest request)
    {
        var result = await _userService.SearchStudents(request);
        result.TraceId = HttpContext.TraceIdentifier;
        return Ok(result);
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfileUser()
    {
        var result = await _userService.GetProfileUser();
        return Ok(ApiResponseFactory.Base(result,200,"SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpPatch("profile")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] Request.UpdateProfileRequest requestBody)
    {
        var message = await _userService.UpdateProfile(requestBody);
        return Ok(ApiResponseFactory.Base(null,200,message, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("system-report")]
    [Authorize]
    public async Task<IActionResult> CreateSystemReport(Request.CreateSystemReportRequest requestBody)
    {
        var message = await _userService.CreateSystemReport(requestBody);
        return Ok(ApiResponseFactory.Base(null,201,message, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("/api/v1/me/assignments")]
    [Authorize]
    public async Task<IActionResult> GetMyAssignments()
    {
        var result = await _userService.GetMyAssignments();
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("reports/me")]
    [Authorize]
    public async Task<IActionResult> GetMyReports([FromQuery] Request.GetMyReportsRequest request)
    {
        var result = await _userService.GetMyReports(request);
        return Ok(result);
    }

    [HttpGet("reports/{reportId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetMyReportById(Guid reportId)
    {
        var result = await _userService.GetMyReportById(reportId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpPatch("me/avatar")]
    [Authorize]
    public async Task<IActionResult> UpdateAvatar([FromForm] Request.UpdateAvatarRequest requestBody)
    {
        var message = await _userService.UpdateAvatar(requestBody);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }
}
