using Hackathon.Service.Models;
using Microsoft.AspNetCore.Mvc;
using RoundsService = Hackathon.Service.Rounds;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/rounds")]
public class RoundsController : ControllerBase
{
    private readonly RoundsService.IService _roundsService;

    public RoundsController(RoundsService.IService roundsService)
    {
        _roundsService = roundsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRounds([FromQuery] Guid? eventId, [FromQuery] bool? isDisable)
    {
        var result = await _roundsService.GetRounds(eventId, isDisable);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
