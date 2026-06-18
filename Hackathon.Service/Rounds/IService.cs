namespace Hackathon.Service.Rounds;

public interface IService
{
    Task<List<Response.RoundResponse>> GetRounds(Guid eventId);
    Task<List<Response.MyRoundResponse>> GetMyRounds(Guid? eventId, Guid teamId);
    Task<Response.MyRoundDetailResponse> GetMyRoundDetail(Guid roundId, Guid registerTeamId);
}
