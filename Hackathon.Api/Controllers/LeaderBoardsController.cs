using System;
using System.Threading.Tasks;
using Hackathon.Api.Extention;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LeaderBoardsService = Hackathon.Service.LeaderBoards;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/v1/leaderboards")]
public class LeaderBoardsController(LeaderBoardsService.IService leaderBoardsService) : ControllerBase
{
    private readonly LeaderBoardsService.IService _leaderBoardsService = leaderBoardsService;

    [HttpGet("year/{year:int}")]
    public async Task<IActionResult> GetYearLeaderboard(int year)
    {
        var result = await _leaderBoardsService.GetYearLeaderboard(year);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    [HttpPatch("/api/v1/admin/leaderboards/{leaderBoardId:guid}/details/{teamId:guid}")]
    public async Task<IActionResult> AssignAward(Guid leaderBoardId, Guid teamId, [FromBody] LeaderBoardsService.Request.AssignAwardRequest request)
    {
        var result = await _leaderBoardsService.AssignAward(leaderBoardId, teamId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "AWARD_ASSIGNED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }
}
