using Hackathon.Service.Models;
using Microsoft.AspNetCore.Mvc;
using CriticalService = Hackathon.Service.Criticals;

namespace Hackathon.Api.Controllers;

[ApiController]
public class CriticalController : ControllerBase
{
    private readonly CriticalService.IService _criticalService;

    public CriticalController(CriticalService.IService criticalService)
    {
        _criticalService = criticalService;
    }

    [HttpGet("api/v1/rounds/{roundId}/criteria")]
    public async Task<IActionResult> GetCriteriaByRound(Guid roundId)
    {
        var result = await _criticalService.GetCriteriaByRound(roundId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("api/v1/events/{eventId}/criteria")]
    public async Task<IActionResult> GetCriteriaByEvent(Guid eventId)
    {
        var result = await _criticalService.GetCriteriaByEvent(eventId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
