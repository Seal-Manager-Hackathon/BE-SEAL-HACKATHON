using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MentorsService = Hackathon.Service.Mentors;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/mentor")]
public class MentorsController : ControllerBase
{
    private readonly MentorsService.IService _mentorsService;

    public MentorsController(MentorsService.IService mentorsService)
    {
        _mentorsService = mentorsService;
    }

    [HttpGet("events")]
    public async Task<IActionResult> GetMentorEvents([FromQuery] MentorsService.Request.GetMentorEventsRequest request)
    {
        var result = await _mentorsService.GetMentorEvents(request);
        result.TraceId = HttpContext.TraceIdentifier;
        return Ok(result);
    }

    [HttpGet("tracks")]
    public async Task<IActionResult> GetMentorTracks([FromQuery] Guid? eventId)
    {
        var result = await _mentorsService.GetMentorTracks(eventId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("tracks/{trackId:guid}/teams")]
    public async Task<IActionResult> GetMentorTrackTeams(Guid trackId, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _mentorsService.GetMentorTrackTeams(trackId, paginationRequest);
        result.TraceId = HttpContext.TraceIdentifier;
        return Ok(result);
    }

    [HttpPost("tracks/{trackId:guid}/notifications")]
    public async Task<IActionResult> SendTrackNotification(Guid trackId, [FromBody] MentorsService.Request.SendNotificationRequest request)
    {
        var result = await _mentorsService.SendTrackNotification(trackId, request);
        return Ok(ApiResponseFactory.Base(result, 200, result.Message, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("teams/{teamId:guid}/notifications")]
    public async Task<IActionResult> SendTeamNotification(Guid teamId, [FromQuery] Guid? trackId, [FromBody] MentorsService.Request.SendNotificationRequest request)
    {
        var result = await _mentorsService.SendTeamNotification(teamId, trackId, request);
        return Ok(ApiResponseFactory.Base(result, 200, result.Message, traceId: HttpContext.TraceIdentifier));
    }
}
