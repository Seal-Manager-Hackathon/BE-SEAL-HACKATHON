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

    [HttpPost]
    public async Task<IActionResult> CreateTeam(TeamsService.Request.CreateTeamRequest request)
    {
        var result = await _teamService.CreateTeam(request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
