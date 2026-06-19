using Hackathon.Repository.Enum;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegisterTeamsService = Hackathon.Service.RegisterTeams;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/register-teams")]
public class RegisterTeamController(RegisterTeamsService.IService registerTeamService) : ControllerBase
{
    private readonly RegisterTeamsService.IService _registerTeamService = registerTeamService;

    [HttpPost]
    public async Task<IActionResult> RegisterEvent([FromBody] RegisterTeamsService.Request.RegisterEventRequest request)
    {
        var result = await _registerTeamService.RegisterEvent(request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyRegisteredEvents([FromQuery] RegisterTeamsService.Request.GetMyRegisteredEventsRequest request, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _registerTeamService.GetMyRegisteredEvents(request, paginationRequest);
        return Ok(result);
    }

    [HttpGet("{registerId:guid}")]
    public async Task<IActionResult> GetRegisterTeamDetailForStudent(Guid registerId)
    {
        var result = await _registerTeamService.GetRegisterTeamDetailForStudent(registerId);
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
        var result = await _registerTeamService.AcceptRegisterTeam(registerId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPut("staff/{registerId:guid}/reject")]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> RejectRegistration(Guid registerId, [FromBody] RegisterTeamsService.Request.RejectRegisterTeamRequest request)
    {
        var result = await _registerTeamService.RejectRegisterTeam(registerId, request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
