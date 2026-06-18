using Hackathon.Repository.Enum;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamsService = Hackathon.Service.Teams;
using RegisterTeamsService = Hackathon.Service.RegisterTeams;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/teams")]
public class TeamController(TeamsService.IService teamService) : ControllerBase
{
    private readonly TeamsService.IService _teamService = teamService;

    [HttpGet("me")]
    public async Task<IActionResult> GetMyTeams([FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _teamService.GetMyTeams(paginationRequest);
        return Ok(result);
    }

    [HttpGet("{teamId:guid}")]
    public async Task<IActionResult> GetTeamDetail(Guid teamId)
    {
        var result = await _teamService.GetTeamDetail(teamId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeam(TeamsService.Request.CreateTeamRequest request)
    {
        var result = await _teamService.CreateTeam(request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("{teamId:guid}/invitations")]
    public async Task<IActionResult> InviteMember(Guid teamId, TeamsService.Request.InviteMemberRequest request)
    {
        var result = await _teamService.InviteMember(teamId, request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPut("{teamId:guid}")]
    public async Task<IActionResult> UpdateTeam(Guid teamId, TeamsService.Request.UpdateTeamRequest request)
    {
        var result = await _teamService.UpdateTeam(teamId, request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpDelete("{teamId:guid}/members")]
    public async Task<IActionResult> RemoveMembers(Guid teamId, TeamsService.Request.RemoveMembersRequest request)
    {
        var result = await _teamService.RemoveMembers(teamId, request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPut("{teamId:guid}/leader")]
    public async Task<IActionResult> TransferLeader(Guid teamId, TeamsService.Request.TransferLeaderRequest request)
    {
        var result = await _teamService.TransferLeader(teamId, request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("{teamId:guid}/events")]
    public async Task<IActionResult> GetTeamRegisteredEvents(Guid teamId, [FromQuery] RegisterTeamsService.Request.GetTeamRegisteredEventsRequest request, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _teamService.GetTeamRegisteredEvents(teamId, request, paginationRequest);
        return Ok(result);
    }

    [HttpGet("{teamId:guid}/events/approved-count")]
    public async Task<IActionResult> GetApprovedEventsCount(Guid teamId)
    {
        var result = await _teamService.GetApprovedEventsCount(teamId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("{teamId:guid}/events/latest")]
    public async Task<IActionResult> GetLatestRegisteredEvent(Guid teamId)
    {
        var result = await _teamService.GetLatestRegisteredEvent(teamId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
