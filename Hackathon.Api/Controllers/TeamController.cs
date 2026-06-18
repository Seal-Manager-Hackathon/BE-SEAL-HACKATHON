using Hackathon.Repository.Enum;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamsService = Hackathon.Service.Teams;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/teams")]
public class TeamController : ControllerBase
{
    private readonly TeamsService.IService _teamService;

    public TeamController(TeamsService.IService teamService)
    {
        _teamService = teamService;
    }

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
}
