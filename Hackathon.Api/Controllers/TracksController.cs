using Hackathon.Service.Models;
using Microsoft.AspNetCore.Mvc;
using TracksService = Hackathon.Service.Tracks;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/v1/tracks")]
public class TracksController(TracksService.IService tracksService) : ControllerBase
{
    private readonly TracksService.IService _tracksService = tracksService;

    [HttpGet("{trackId:guid}/teams/count")]
    public async Task<IActionResult> GetTrackTeamCount(Guid trackId)
    {
        var result = await _tracksService.GetTrackTeamCount(trackId);
        return Ok(ApiResponseFactory.Base(result,200,"SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("{trackId:guid}/topics")]
    public async Task<IActionResult> GetTopicsByTrack(Guid trackId, [FromQuery] string? keyword, [FromQuery] bool? isDisable, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _tracksService.GetTopicsByTrack(trackId, keyword, isDisable, paginationRequest);
        return Ok(result);
    }
}
