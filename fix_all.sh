cat << 'INNER_EOF' > D:/dotNet/Hackathon/Hackathon.Api/Controllers/RegisterTeamController.cs
using Hackathon.Repository.Enum;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamsService = Hackathon.Service.Teams;
using RegisterTeamsService = Hackathon.Service.RegisterTeams;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/register-teams")]
public class RegisterTeamController : ControllerBase
{
    private readonly TeamsService.IService _teamService;
    private readonly RegisterTeamsService.IService _registerTeamService;

    public RegisterTeamController(TeamsService.IService teamService, RegisterTeamsService.IService registerTeamService)
    {
        _teamService = teamService;
        _registerTeamService = registerTeamService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterEvent([FromBody] TeamsService.Request.RegisterEventRequest request)
    {
        var result = await _teamService.RegisterEvent(request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyRegisteredEvents([FromQuery] TeamsService.Request.GetMyRegisteredEventsRequest request, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _teamService.GetMyRegisteredEvents(request, paginationRequest);
        return Ok(result);
    }

    [HttpGet("{registerId:guid}/rejection-reason")]
    public async Task<IActionResult> GetRejectionReason(Guid registerId)
    {
        var result = await _teamService.GetRejectionReason(registerId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("staff/events/{eventId:guid}")]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> GetRegisterTeamsByEvent(Guid eventId, [FromQuery] string? keyword, [FromQuery] RegisterTeamStatusEnum? status, [FromQuery] bool? isDisable, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _registerTeamService.GetRegisterTeamsByEvent(eventId, keyword, status, isDisable, paginationRequest);
        return Ok(result);
    }

    [HttpGet("staff/{registerTeamId:guid}")]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> GetRegisterTeamDetail(Guid registerTeamId)
    {
        var result = await _registerTeamService.GetRegisterTeamDetail(registerTeamId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPut("staff/{registerId:guid}/approve")]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> ApproveRegistration(Guid registerId)
    {
        var result = await _teamService.ApproveRegistration(registerId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPut("staff/{registerId:guid}/reject")]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> RejectRegistration(Guid registerId, [FromBody] TeamsService.Request.RejectTeamRequest request)
    {
        var result = await _teamService.RejectRegistration(registerId, request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
INNER_EOF
