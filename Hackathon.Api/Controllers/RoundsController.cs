using System.ComponentModel.DataAnnotations;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("teams/{teamId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetMyRounds(Guid teamId, [FromQuery] Guid? eventId)
    {
        var result = await _roundsService.GetMyRounds(eventId, teamId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("register-teams/{registerTeamId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetMyRoundDetail(Guid registerTeamId)
    {
        var result = await _roundsService.GetMyRoundDetail(registerTeamId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("{roundId:guid}/submit-assignment")]
    [Authorize]
    public async Task<IActionResult> SubmitAssignment(Guid roundId, [FromBody] RoundsService.Request.SubmitAssignmentRequest request)
    {
        var result = await _roundsService.SubmitAssignment(roundId, request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("{roundId:guid}/submissions")]
    [Authorize]
    public async Task<IActionResult> GetRoundSubmissions(Guid roundId, [FromQuery] RoundsService.Request.GetSubmissionsQuery query)
    {
        var result = await _roundsService.GetRoundSubmissions(roundId, query);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}