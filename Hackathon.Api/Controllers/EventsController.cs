using Hackathon.Api.Extention;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EventsService = Hackathon.Service.Events;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/v1/events")]
public class EventsController : ControllerBase
{
    private readonly EventsService.IService _eventsService;

    public EventsController(EventsService.IService eventsService)
    {
        _eventsService = eventsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetEvents([FromQuery] EventsService.Request.GetEventsRequest request)
    {
        var result = await _eventsService.GetEvents(request);
        return Ok(result);
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

    [Authorize]
    [HttpGet("events/joined")]
    public async Task<IActionResult> GetJoinedEvents([FromQuery] EventsService.Request.GetJoinedEventsRequest request)
    {
        var result = await _eventsService.GetJoinedEvents(request);
        return Ok(result);
    }
}
