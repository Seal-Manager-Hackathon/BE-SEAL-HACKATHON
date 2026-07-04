using Hackathon.Api.Extention;
using Hackathon.Repository.Enum;
using Hackathon.Service.Models;
using Hackathon.Service.Rounds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdminService = Hackathon.Service.Admin;
using EventsService = Hackathon.Service.Events;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize(Policy = JwtExtensions.AdminPolicy)]
[Route("api/v1/admin")]
public class AdminController : ControllerBase
{
    private readonly AdminService.IService _adminService;
    private readonly EventsService.IService _eventsService;

    public AdminController(AdminService.IService adminService, EventsService.IService eventsService)
    {
        _adminService = adminService;
        _eventsService = eventsService;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers([FromQuery] RoleEnum? role, [FromQuery] string? keyword, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _adminService.GetAllUsers(role, keyword, paginationRequest);
        return Ok(result);
    }

    [HttpGet("users/search")]
    public async Task<IActionResult> SearchUsers([FromQuery] AdminService.GetUsersQuery query)
    {
        var result = await _adminService.SearchUsers(query);
        return Ok(result);
    }

    [HttpGet("events/{eventId:guid}/rounds")]
    public async Task<IActionResult> GetRounds(Guid eventId, [FromQuery] AdminService.GetAdminRoundsRequest request)
    {
        var result = await _adminService.GetRounds(eventId, request);
        result.TraceId = HttpContext.TraceIdentifier;
        return Ok(result);
    }

    [HttpPost("events/{eventId:guid}/rounds")]
    public async Task<IActionResult> CreateRound(Guid eventId, AdminService.CreateRoundRequest request)
    {
        var result = await _adminService.CreateRound(eventId, request);
        return StatusCode(201, ApiResponseFactory.Base(result, 201, "ROUND_CREATED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [HttpPatch("rounds/{roundId:guid}")]
    public async Task<IActionResult> UpdateRound(Guid roundId, AdminService.UpdateRoundRequest request)
    {
        await _adminService.UpdateRound(roundId, request);
        return Ok(ApiResponseFactory.Base(null, 200, "ROUND_UPDATED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [HttpDelete("rounds/{roundId:guid}")]
    public async Task<IActionResult> DeleteRound(Guid roundId)
    {
        await _adminService.DeleteRound(roundId);
        return Ok(ApiResponseFactory.Base(null, 200, "ROUND_DELETED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [HttpPatch("rounds/{roundId:guid}/restore")]
    public async Task<IActionResult> RestoreRound(Guid roundId)
    {
        var message = await _adminService.RestoreRound(roundId);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("notifications")]
    public async Task<IActionResult> SendSystemNotification(AdminService.SendSystemNotificationRequest request)
    {
        var result = await _adminService.SendSystemNotification(request);
        return Ok(ApiResponseFactory.Base(result, 200, "SYSTEM_NOTIFICATION_SENT", traceId: HttpContext.TraceIdentifier));
    }

    [HttpDelete("assign-events/{id:guid}")]
    public async Task<IActionResult> RemoveStaffAssignment(Guid id)
    {
        var message = await _eventsService.RemoveStaffAssignment(id);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPatch("users/{userId:guid}/role")]
    public async Task<IActionResult> ChangeUserRole(Guid userId, [FromBody] AdminService.ChangeUserRoleRequest request)
    {
        var result = await _adminService.ChangeUserRole(userId, request);
        return Ok(ApiResponseFactory.Base(null, 200, result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("rounds/{roundId:guid}/submissions")]
    public async Task<IActionResult> GetRoundSubmissions(Guid roundId, [FromQuery] Request.GetStaffRoundSubmissionsQuery query)
    {
        var result = await _adminService.GetRoundSubmissions(roundId, query);
        result.TraceId = HttpContext.TraceIdentifier;
        return Ok(result);
    }
}
