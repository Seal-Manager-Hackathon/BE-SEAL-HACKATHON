using Hackathon.Api.Extention;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TracksService = Hackathon.Service.Tracks;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize(Policy = JwtExtensions.StaffPolicy)]
[Route("api/v1/staff")]
public class StaffTracksController : ControllerBase
{
    private readonly TracksService.IService _tracksService;

    public StaffTracksController(TracksService.IService tracksService)
    {
        _tracksService = tracksService;
    }

    [HttpGet("events/{eventId:guid}/tracks")]
    public async Task<IActionResult> GetTracksByEvent(Guid eventId, [FromQuery] string? keyword, [FromQuery] bool? isDisable, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _tracksService.GetTracksByEvent(eventId, keyword, isDisable, paginationRequest);
        return Ok(result);
    }

    [HttpGet("tracks/{trackId:guid}/topics")]
    public async Task<IActionResult> GetTopicsByTrack(Guid trackId, [FromQuery] string? keyword, [FromQuery] bool? isDisable, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _tracksService.GetTopicsByTrack(trackId, keyword, isDisable, paginationRequest);
        return Ok(result);
    }

    [HttpPatch("teams/{teamId:guid}/track")]
    public async Task<IActionResult> AssignTrackToTeam(Guid teamId, TracksService.Request.AssignTrackToTeamRequest request)
    {
        var result = await _tracksService.AssignTrackToTeam(teamId, request);
        return Ok(ApiResponseFactory.Base(result, true,"", HttpContext.TraceIdentifier));
    }

    [HttpPatch("teams/{teamId:guid}/topic")]
    public async Task<IActionResult> AssignTopicToTeam(Guid teamId, TracksService.Request.AssignTopicToTeamRequest request)
    {
        var result = await _tracksService.AssignTopicToTeam(teamId, request);
        return Ok(ApiResponseFactory.Base(result, true,"", HttpContext.TraceIdentifier));
    }
}
