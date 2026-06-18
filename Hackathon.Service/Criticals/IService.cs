namespace Hackathon.Service.Criticals;

public interface IService
{
    Task<Response.RoundCriteriaResponse> GetCriteriaByRound(Guid roundId, bool? isDisable);
}
