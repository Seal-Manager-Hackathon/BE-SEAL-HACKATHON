using System.ComponentModel.DataAnnotations;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Mvc;
using RoundsService = Hackathon.Service.Rounds;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/v1/rounds")]
public class RoundsController : ControllerBase
{
    private readonly RoundsService.IService _roundsService;

    public RoundsController(RoundsService.IService roundsService)
    {
        _roundsService = roundsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRounds([FromQuery, Required] Guid eventId)
    {
        var result = await _roundsService.GetRounds(eventId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
