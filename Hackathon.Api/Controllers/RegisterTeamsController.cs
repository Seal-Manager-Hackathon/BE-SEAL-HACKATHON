using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegisterTeamsService = Hackathon.Service.RegisterTeams;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/register-teams")]
public class RegisterTeamsController : ControllerBase
{
    private readonly RegisterTeamsService.IService _registerTeamsService;

    public RegisterTeamsController(RegisterTeamsService.IService registerTeamsService)
    {
        _registerTeamsService = registerTeamsService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterTeamForEvent(RegisterTeamsService.Request.RegisterTeamRequest request)
    {
        var result = await _registerTeamsService.RegisterTeamForEvent(request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("{registerTeamId:guid}/status")]
    public async Task<IActionResult> GetMyRegistrationStatus(Guid registerTeamId)
    {
        var result = await _registerTeamsService.GetMyRegistrationStatus(registerTeamId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
