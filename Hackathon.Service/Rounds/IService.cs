using Hackathon.Service.Models;

namespace Hackathon.Service.Rounds;

public interface IService
{
    Task<List<Response.RoundResponse>> GetRounds(Guid eventId);
    Task<List<Response.MyRoundResponse>> GetMyRounds(Guid? eventId);
    Task<Response.SubmitAssignmentResponse> SubmitAssignment(Guid roundId, Request.SubmitAssignmentRequest request);
    Task<BasePaginationResponse> GetRoundSubmissions(Guid roundId, Request.GetSubmissionsQuery query);
    Task<Response.EndRoundResponse> EndRound(Guid roundId);
}
