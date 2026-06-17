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
    public async Task<IActionResult> GetMyInvitations([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _invitationsService.GetMyInvitations(pageIndex, pageSize);
        return Ok(result);
    }
}
