using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InvitationsService = Hackathon.Service.Invitations;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/invitations")]
public class InvitationsController : ControllerBase
{
    private readonly InvitationsService.IService _invitationsService;

    public InvitationsController(InvitationsService.IService invitationsService)
    {
        _invitationsService = invitationsService;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyInvitations([FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _invitationsService.GetMyInvitations(paginationRequest);
        return Ok(result);
    }

    [HttpPost("{invitationId:guid}/accept")]
    public async Task<IActionResult> AcceptInvitation(Guid invitationId)
    {
        var result = await _invitationsService.AcceptInvitation(invitationId);
        return Ok(ApiResponseFactory.Base(result,200,"SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("{invitationId:guid}/reject")]
    public async Task<IActionResult> RejectInvitation(Guid invitationId)
    {
        var result = await _invitationsService.RejectInvitation(invitationId);
        return Ok(ApiResponseFactory.Base(result,200,"SUCCESS", traceId: HttpContext.TraceIdentifier));
    }
}
