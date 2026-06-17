namespace Hackathon.Service.Critical;

public interface IService
{
    Task<Response.RoundCriteriaResponse> GetCriteriaByRound(Guid roundId, bool? isDisable);
}
