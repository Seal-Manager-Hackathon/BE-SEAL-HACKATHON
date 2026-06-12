namespace Hackathon.Service.LeaderBoards;

public interface IService
{
    Task<List<Response.LeaderBoardItemResponse>> GetEventLeaderBoard(Guid eventId);
    Task<(List<Response.LeaderBoardItemResponse> Items, int TotalCount)> GetYearLeaderBoard(int? year, int pageIndex, int pageSize);
}
