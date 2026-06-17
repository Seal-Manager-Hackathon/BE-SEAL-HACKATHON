using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegisterTeamsService = Hackathon.Service.RegisterTeams;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/admin/register-teams")]
public class StaffRegisterTeamsController : ControllerBase
{
    private readonly RegisterTeamsService.IService _registerTeamsService;

    public StaffRegisterTeamsController(RegisterTeamsService.IService registerTeamsService)
    {
        _registerTeamsService = registerTeamsService;
    }

    [HttpGet("events")]
    public async Task<IActionResult> GetAssignedEvents()
    {
        var result = await _registerTeamsService.GetAssignedEvents();
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("events/{eventId:guid}/pending")]
    public async Task<IActionResult> GetPendingTeamsByEvent(Guid eventId)
    {
        var result = await _registerTeamsService.GetPendingTeamsByEvent(eventId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("{registerTeamId:guid}")]
    public async Task<IActionResult> GetRegistrationDetailForReview(Guid registerTeamId)
    {
        var result = await _registerTeamsService.GetRegistrationDetailForReview(registerTeamId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPatch("{registerTeamId:guid}/approve")]
    public async Task<IActionResult> ApproveRegistration(Guid registerTeamId)
    {
        var result = await _registerTeamsService.ApproveRegistration(registerTeamId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPatch("{registerTeamId:guid}/reject")]
    public async Task<IActionResult> RejectRegistration(Guid registerTeamId, RegisterTeamsService.Request.RejectRegistrationRequest request)
    {
        var result = await _registerTeamsService.RejectRegistration(registerTeamId, request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
