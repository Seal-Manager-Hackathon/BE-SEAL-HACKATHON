using Hackathon.Api.Extention;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EventsService = Hackathon.Service.Events;
using TracksService = Hackathon.Service.Tracks;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/v1/events")]
public class EventsController : ControllerBase
{
    private readonly EventsService.IService _eventsService ;
    private readonly TracksService.IService _tracksService ;

    public EventsController(EventsService.IService eventsService, TracksService.IService tracksService)
    {
        _eventsService = eventsService;
        _tracksService = tracksService;
    }

    [HttpGet]
    public async Task<IActionResult> GetEvents([FromQuery] EventsService.Request.GetEventsRequest request)
    {
        var result = await _eventsService.GetEvents(request);
        return Ok(result);
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPost("/api/v1/admin/events")]
    public async Task<IActionResult> CreateEvent(EventsService.Request.CreateEventRequest request)
    {
        var result = await _eventsService.CreateEvent(request);
        return Ok(ApiResponseFactory.Base(result,201,"EVENT_CREATED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPost("/api/v1/admin/events/{eventId:guid}/staff")]
    public async Task<IActionResult> AssignStaffToEvent(Guid eventId, EventsService.Request.AssignStaffToEventRequest request)
    {
        var result = await _eventsService.AssignStaffToEvent(eventId, request);
        return Ok(ApiResponseFactory.Base(result,201,"STAFF_ASSIGNED_TO_EVENT_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPost("/api/v1/admin/events/{eventId:guid}/awards")]
    public async Task<IActionResult> CreateAward(Guid eventId, EventsService.Request.CreateAwardRequest request)
    {
        var result = await _eventsService.CreateAward(eventId, request);
        return Ok(ApiResponseFactory.Base(result, 201, "AWARD_CREATED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    [HttpPost("/api/v1/admin/assign-events/{id:guid}/tracks")]
    public async Task<IActionResult> AssignEventToTrack(Guid id, EventsService.Request.AssignEventToTrackRequest request)
    {
        var result = await _eventsService.AssignEventToTrack(id, request);
        return Ok(ApiResponseFactory.Base(result, 200, "TRACK_ASSIGNED", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    [HttpPost("/api/v1/admin/events/{eventId:guid}/leaderboard/recalculate")]
    public async Task<IActionResult> RecalculateLeaderboard(Guid eventId)
    {
        var message = await _eventsService.RecalculateLeaderboard(eventId);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    [HttpPatch("/api/v1/admin/events/{eventId:guid}/leaderboard/lock")]
    public async Task<IActionResult> LockLeaderboard(Guid eventId)
    {
        var message = await _eventsService.LockLeaderboard(eventId);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    [HttpPatch("/api/v1/admin/events/{eventId:guid}/leaderboard/publish")]
    public async Task<IActionResult> PublishLeaderboard(Guid eventId)
    {
        var message = await _eventsService.PublishLeaderboard(eventId);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPatch("/api/v1/admin/events/{eventId:guid}")]
    public async Task<IActionResult> UpdateEvent(Guid eventId, EventsService.Request.UpdateEventRequest request)
    {
        var message = await _eventsService.UpdateEvent(eventId, request);
        return Ok(ApiResponseFactory.Base(null,200,message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpDelete("/api/v1/admin/events/{eventId:guid}")]
    public async Task<IActionResult> DeleteEvent(Guid eventId)
    {
        var message = await _eventsService.DeleteEvent(eventId);
        return Ok(ApiResponseFactory.Base(null,200,message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpDelete("/api/v1/admin/awards/{id:guid}")]
    public async Task<IActionResult> DeleteAward(Guid id)
    {
        var message = await _eventsService.DeleteAward(id);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    [HttpDelete("/api/v1/admin/assign-tracks/{id:guid}")]
    public async Task<IActionResult> RemoveTrackAssignment(Guid id)
    {
        var result = await _eventsService.RemoveTrackAssignment(id);
        return Ok(ApiResponseFactory.Base(new { id = result }, 200, "TRACK_ASSIGNMENT_REMOVED", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPatch("/api/v1/admin/events/{eventId:guid}/publish")]
    public async Task<IActionResult> PublishEvent(Guid eventId)
    {
        var message = await _eventsService.PublishEvent(eventId);
        return Ok(ApiResponseFactory.Base(null,200,message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPatch("/api/v1/admin/awards/{id:guid}")]
    public async Task<IActionResult> UpdateAward(Guid id, EventsService.Request.UpdateAwardRequest request)
    {
        var message = await _eventsService.UpdateAward(id, request);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPatch("/api/v1/admin/events/{eventId:guid}/cancel")]
    public async Task<IActionResult> CancelEvent(Guid eventId)
    {
        var message = await _eventsService.CancelEvent(eventId);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPatch("/api/v1/admin/events/{eventId:guid}/close")]
    public async Task<IActionResult> CloseEvent(Guid eventId)
    {
        var message = await _eventsService.CloseEvent(eventId);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPatch("/api/v1/admin/events/{eventId:guid}/restore")]
    public async Task<IActionResult> RestoreEvent(Guid eventId)
    {
        var message = await _eventsService.RestoreEvent(eventId);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPatch("/api/v1/admin/events/{eventId:guid}/unpublish")]
    public async Task<IActionResult> UnpublishEvent(Guid eventId)
    {
        var message = await _eventsService.UnpublishEvent(eventId);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPatch("/api/v1/admin/assign-events/{id:guid}/role")]
    public async Task<IActionResult> UpdateLecturerRole(Guid id, EventsService.Request.UpdateLecturerRoleRequest request)
    {
        var message = await _eventsService.UpdateLecturerRole(id, request);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpGet("/api/v1/admin/events")]
    public async Task<IActionResult> GetEventsForAdmin([FromQuery] EventsService.Request.GetEventsForAdminRequest request)
    {
        var result = await _eventsService.GetEventsForAdmin(request);
        return Ok(result);
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpGet("/api/v1/admin/events/{eventId:guid}/assignments")]
    public async Task<IActionResult> GetEventAssignments(Guid eventId)
    {
        var result = await _eventsService.GetEventAssignments(eventId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    [HttpGet("/api/v1/admin/events/{eventId:guid}/setup-status")]
    public async Task<IActionResult> GetSetupStatus(Guid eventId)
    {
        var result = await _eventsService.GetSetupStatus(eventId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("{eventId:guid}/awards")]
    public async Task<IActionResult> GetAwards(Guid eventId)
    {
        var result = await _eventsService.GetAwards(eventId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("{eventId:guid}/leaderboard")]
    public async Task<IActionResult> GetLeaderboard(Guid eventId)
    {
        var result = await _eventsService.GetLeaderboard(eventId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("{eventId:guid}/summary")]
    public async Task<IActionResult> GetSummary(Guid eventId)
    {
        var result = await _eventsService.GetSummary(eventId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("{eventId:guid}/teams/{teamId:guid}/scores")]
    public async Task<IActionResult> GetTeamScores(Guid eventId, Guid teamId)
    {
        var result = await _eventsService.GetTeamScores(eventId, teamId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("most-participants")]
    public async Task<IActionResult> GetMostParticipants([FromQuery] int? limit, [FromQuery] bool? isDisable)
    {
        var result = await _eventsService.GetMostParticipants(limit, isDisable);
        return Ok(ApiResponseFactory.Base(result,200,"SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("{eventId:guid}")]
    public async Task<IActionResult> GetEvent(Guid eventId)
    {
        var result = await _eventsService.GetEvent(eventId);
        return Ok(ApiResponseFactory.Base(result,200,"SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("{eventId:guid}/tracks")]
    public async Task<IActionResult> GetTracksByEvent(Guid eventId, [FromQuery] string? keyword, [FromQuery] bool? isDisable, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _tracksService.GetTracks(eventId, keyword, isDisable, paginationRequest);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("events/joined")]
    public async Task<IActionResult> GetJoinedEvents([FromQuery] EventsService.Request.GetJoinedEventsRequest request)
    {
        var result = await _eventsService.GetJoinedEvents(request);
        return Ok(result);
    }
}
