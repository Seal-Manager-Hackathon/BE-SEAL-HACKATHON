using Hackathon.Api.Extention;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EventsService = Hackathon.Service.Events;
using TracksService = Hackathon.Service.Tracks;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/v1/events")]
public class EventsController(EventsService.IService eventsService, TracksService.IService tracksService) : ControllerBase
{
    private readonly EventsService.IService _eventsService = eventsService;
    private readonly TracksService.IService _tracksService = tracksService;

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
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPatch("/api/v1/admin/events/{eventId:guid}")]
    public async Task<IActionResult> UpdateEvent(Guid eventId, EventsService.Request.UpdateEventRequest request)
    {
        var result = await _eventsService.UpdateEvent(eventId, request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpDelete("/api/v1/admin/events/{eventId:guid}")]
    public async Task<IActionResult> DeleteEvent(Guid eventId)
    {
        var result = await _eventsService.DeleteEvent(eventId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPatch("/api/v1/admin/events/{eventId:guid}/publish")]
    public async Task<IActionResult> PublishEvent(Guid eventId)
    {
        var result = await _eventsService.PublishEvent(eventId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpGet("/api/v1/admin/events")]
    public async Task<IActionResult> GetEventsForAdmin([FromQuery] EventsService.Request.GetEventsForAdminRequest request)
    {
        var result = await _eventsService.GetEventsForAdmin(request);
        return Ok(result);
    }

    [HttpGet("most-participants")]
    public async Task<IActionResult> GetMostParticipants([FromQuery] int? limit, [FromQuery] bool? isDisable)
    {
        var result = await _eventsService.GetMostParticipants(limit, isDisable);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("{eventId:guid}")]
    public async Task<IActionResult> GetEvent(Guid eventId)
    {
        var result = await _eventsService.GetEvent(eventId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
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
