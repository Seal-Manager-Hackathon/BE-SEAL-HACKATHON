using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EventsService = Hackathon.Service.Events;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly EventsService.IService _eventsService;

    public EventsController(EventsService.IService eventsService)
    {
        _eventsService = eventsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetEvents([FromQuery] int? year, [FromQuery] bool? isDisable)
    {
        var result = await _eventsService.GetEvents(year, isDisable);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchEvents([FromQuery] string? keyword, [FromQuery] int? year, [FromQuery] string? status, [FromQuery] bool? isDisable, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _eventsService.SearchEvents(keyword, year, status, isDisable, pageIndex, pageSize);
        return Ok(result);
    }

    [HttpGet("most-participants")]
    public async Task<IActionResult> GetMostParticipants([FromQuery] int? limit, [FromQuery] bool? isDisable)
    {
        var result = await _eventsService.GetMostParticipants(limit, isDisable);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("{eventId:guid}")]
    public async Task<IActionResult> GetEvent(Guid eventId, [FromQuery] bool? isDisable)
    {
        var result = await _eventsService.GetEvent(eventId, isDisable);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize]
    [HttpGet("/api/me/events/joined")]
    public async Task<IActionResult> GetJoinedEvents([FromQuery] int? year, [FromQuery] string? status, [FromQuery] bool? isDisable)
    {
        var result = await _eventsService.GetJoinedEvents(year, status, isDisable);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
