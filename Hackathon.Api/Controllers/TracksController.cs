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

    [HttpGet]
    public async Task<IActionResult> GetTracks([FromQuery] Guid? eventId, [FromQuery] string? keyword, [FromQuery] bool? isDisable, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _tracksService.GetTracks(eventId, keyword, isDisable, pageIndex, pageSize);
        return Ok(result);
    }
}
