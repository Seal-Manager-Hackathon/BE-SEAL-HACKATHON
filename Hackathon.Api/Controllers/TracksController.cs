using Hackathon.Api.Extention;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicsService = Hackathon.Service.Topics;
using TracksService = Hackathon.Service.Tracks;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/v1/tracks")]
public class TracksController : ControllerBase
{
    private readonly TracksService.IService _tracksService;
    private readonly TopicsService.IService _topicsService;

    public TracksController(TracksService.IService tracksService, TopicsService.IService topicsService)
    {
        _tracksService = tracksService;
        _topicsService = topicsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTracks([FromQuery] Guid? eventId, [FromQuery] string? keyword, [FromQuery] bool? isDisable, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _tracksService.GetTracks(eventId, keyword, isDisable, paginationRequest);
        return Ok(result);
    }

    [HttpGet("{trackId:guid}")]
    public async Task<IActionResult> GetTrack(Guid trackId)
    {
        var result = await _tracksService.GetTrack(trackId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("my-assignment")]
    [Authorize]
    public async Task<IActionResult> GetMyEventAssignment([FromQuery] Guid eventId, [FromQuery] string? role)
    {
        Hackathon.Repository.Enum.EventRoleEnum? eventRole = null;
        if (!string.IsNullOrWhiteSpace(role) && System.Enum.TryParse<Hackathon.Repository.Enum.EventRoleEnum>(role, true, out var parsed))
        {
            eventRole = parsed;
        }

        var result = await _tracksService.GetMyEventAssignment(eventId, eventRole);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

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

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPost("/api/v1/admin/events/{eventId:guid}/tracks")]
    public async Task<IActionResult> CreateTrack(Guid eventId, [FromBody] TracksService.Request.CreateTrackRequest request)
    {
        var result = await _tracksService.CreateTrack(eventId, request);
        var data = new { id = result.Id };
        return Created("", ApiResponseFactory.Base(data, 201, "TRACK_CREATED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    [HttpGet("/api/v1/admin/tracks/{trackId:guid}/topics")]
    public async Task<IActionResult> GetAdminTopicsByTrack(Guid trackId, [FromQuery] string? keyword, [FromQuery] bool? isDisable, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _tracksService.GetAdminTopicsByTrack(trackId, keyword, isDisable, paginationRequest);
        return Ok(result);
    }

    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    [HttpPost("/api/v1/admin/tracks/{trackId:guid}/topics")]
    public async Task<IActionResult> CreateTopic(Guid trackId, [FromBody] TopicsService.Request.CreateTopicRequest request)
    {
        var result = await _topicsService.CreateTopic(trackId, request);
        var data = new { id = result.Id };
        return Created("", ApiResponseFactory.Base(data, 201, "TOPIC_CREATED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPatch("/api/v1/admin/tracks/{trackId:guid}")]
    public async Task<IActionResult> UpdateTrack(Guid trackId, [FromBody] TracksService.Request.UpdateTrackRequest request)
    {
        var result = await _tracksService.UpdateTrack(trackId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "TRACK_UPDATED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    [HttpPatch("/api/v1/admin/tracks/{trackId:guid}/visibility")]
    public async Task<IActionResult> UpdateTrackVisibility(Guid trackId, [FromBody] TracksService.Request.UpdateTrackVisibilityRequest request)
    {
        var result = await _tracksService.UpdateTrackVisibility(trackId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "TRACK_VISIBILITY_UPDATED", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpDelete("/api/v1/admin/tracks/{trackId:guid}")]
    public async Task<IActionResult> DeleteTrack(Guid trackId)
    {
        var result = await _tracksService.DeleteTrack(trackId);
        return Ok(ApiResponseFactory.Base(result, 200, "TRACK_DELETED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }
}
