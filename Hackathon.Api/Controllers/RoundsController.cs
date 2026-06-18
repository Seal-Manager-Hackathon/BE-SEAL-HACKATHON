using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoundsService = Hackathon.Service.Rounds;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/rounds")]
public class RoundsController : ControllerBase
{
    private readonly RoundsService.IService _roundsService;

    public RoundsController(RoundsService.IService roundsService)
    {
        _roundsService = roundsService;
    }

    [HttpPost("{roundId:guid}/submit-assignment")]
    public async Task<IActionResult> SubmitAssignment(Guid roundId, [FromBody] RoundsService.Request.SubmitAssignmentRequest request)
    {
        var result = await _roundsService.SubmitAssignment(roundId, request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
