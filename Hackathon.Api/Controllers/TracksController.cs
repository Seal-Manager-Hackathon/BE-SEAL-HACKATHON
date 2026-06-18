using Hackathon.Service.Models;
using Microsoft.AspNetCore.Mvc;
using TracksService = Hackathon.Service.Tracks;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/v1/tracks")]
public class TracksController : ControllerBase
{
    private readonly TracksService.IService _tracksService;

    public TracksController(TracksService.IService tracksService)
    {
        _tracksService = tracksService;
    }

    [HttpGet("{trackId:guid}/teams/count")]
    public async Task<IActionResult> GetTrackTeamCount(Guid trackId)
    {
        var result = await _tracksService.GetTrackTeamCount(trackId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
