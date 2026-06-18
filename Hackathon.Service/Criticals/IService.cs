namespace Hackathon.Service.Criticals;

public interface IService
{
    Task<Response.RoundCriteriaResponse> GetCriteriaByRound(Guid roundId);
    Task<List<Response.RoundCriteriaResponse>> GetCriteriaByEvent(Guid eventId);
}
