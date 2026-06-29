using Hackathon.Api.Extention;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LecturersService = Hackathon.Service.Lecturers;
using RoundsService = Hackathon.Service.Rounds;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize(Policy = JwtExtensions.LecturerPolicy)]
[Route("api/v1/lecturers")]
public class LecturersController : ControllerBase
{
    private readonly LecturersService.IService _lecturersService;
    private readonly RoundsService.IService _roundsService;

    public LecturersController(LecturersService.IService lecturersService, RoundsService.IService roundsService)
    {
        _lecturersService = lecturersService;
        _roundsService = roundsService;
    }

    [HttpGet("events")]
    public async Task<IActionResult> GetLecturerEvents([FromQuery] PaginationRequest request)
    {
        var result = await _lecturersService.GetLecturerEvents(request);
        return Ok(result);
    }

    [HttpGet("events/search")]
    public async Task<IActionResult> SearchLecturerEvents([FromQuery] LecturersService.Request.SearchLecturerEventsRequest request)
    {
        var result = await _lecturersService.SearchLecturerEvents(request);
        return Ok(result);
    }

    [HttpGet("events/current")]
    public async Task<IActionResult> GetCurrentLecturerEvents()
    {
        var result = await _lecturersService.GetCurrentLecturerEvents();
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("rounds/{roundId:guid}/submissions")]
    public async Task<IActionResult> GetRoundSubmissions(Guid roundId, [FromQuery] RoundsService.Request.GetSubmissionsQuery query)
    {
        var result = await _roundsService.GetLecturerRoundSubmissions(roundId, query);
        return Ok(result);
    }
}
