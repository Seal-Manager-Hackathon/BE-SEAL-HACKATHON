using Hackathon.Service.Models;
using Microsoft.AspNetCore.Mvc;
using LeaderBoardsService = Hackathon.Service.LeaderBoards;

namespace Hackathon.Api.Controllers;

[ApiController]
public class LeaderBoardsController : ControllerBase
{
    private readonly LeaderBoardsService.IService _leaderBoardsService;

    public LeaderBoardsController(LeaderBoardsService.IService leaderBoardsService)
    {
        _leaderBoardsService = leaderBoardsService;
    }

    [HttpGet("api/v1/events/{eventId:guid}/leaderboard")]
    public async Task<IActionResult> GetEventLeaderBoard(Guid eventId)
    {
        var result = await _leaderBoardsService.GetEventLeaderBoard(eventId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("api/v1/leaderboards/year")]
    public async Task<IActionResult> GetYearLeaderBoard([FromQuery] int? year, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _leaderBoardsService.GetYearLeaderBoard(year, pageIndex, pageSize);
        return Ok(ApiResponseFactory.BasePagination(result.Items, pageIndex, pageSize, result.TotalCount));
    }
}
