using Hackathon.Repository.Enum;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamsService = Hackathon.Service.Teams;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/register-teams")]
public class RegisterTeamController : ControllerBase
{
    private readonly TeamsService.IService _teamService;

    public RegisterTeamController(TeamsService.IService teamService)
    {
        _teamService = teamService;
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

    [HttpPut("{registerId:guid}/approve")]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> ApproveRegistration(Guid registerId)
    {
        var result = await _teamService.ApproveRegistration(registerId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPut("{registerId:guid}/reject")]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> RejectRegistration(Guid registerId, [FromBody] TeamsService.Request.RejectTeamRequest request)
    {
        var result = await _teamService.RejectRegistration(registerId, request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
