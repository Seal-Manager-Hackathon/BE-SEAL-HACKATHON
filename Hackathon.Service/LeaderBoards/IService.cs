using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.Service.LeaderBoards;

public interface IService
{
    Task<List<Response.YearLeaderboardResponse>> GetYearLeaderboard(int year);
    Task<string> AssignAward(Guid leaderBoardId, Guid teamId, Request.AssignAwardRequest request);
}
