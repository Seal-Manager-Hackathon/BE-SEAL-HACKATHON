using Hackathon.Service.Models;
using Microsoft.AspNetCore.Mvc;
using CriticalService = Hackathon.Service.Criticals;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/rounds")]
public class CriticalController : ControllerBase
{
    private readonly CriticalService.IService _criticalService;

    public CriticalController(CriticalService.IService criticalService)
    {
        _criticalService = criticalService;
    }

    [HttpGet("{roundId}/criteria")]
    public async Task<IActionResult> GetCriteriaByRound(Guid roundId, [FromQuery] bool? isDisable)
    {
        var result = await _criticalService.GetCriteriaByRound(roundId, isDisable);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
