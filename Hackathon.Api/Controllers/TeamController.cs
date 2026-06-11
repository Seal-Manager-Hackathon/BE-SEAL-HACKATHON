using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamsService = Hackathon.Service.Teams;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/teams")]
public class TeamController : ControllerBase
{
    private readonly TeamsService.IService _teamService;

    public TeamController(TeamsService.IService teamService)
    {
        _teamService = teamService;
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

    [HttpPost("invitations/{invitationId:guid}/response")]
    public async Task<IActionResult> RespondInvitation(Guid invitationId, TeamsService.Request.RespondInvitationRequest request)
    {
        var result = await _teamService.RespondInvitation(invitationId, request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
