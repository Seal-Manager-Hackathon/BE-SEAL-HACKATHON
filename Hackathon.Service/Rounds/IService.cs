using Hackathon.Service.Models;

namespace Hackathon.Service.Rounds;

public interface IService
{
    Task<List<Response.RoundResponse>> GetRounds(Guid eventId);
    Task<Response.RoundDetailResponse> GetRound(Guid roundId);
    Task<List<Response.MyRoundResponse>> GetMyRounds(Guid? eventId, Guid teamId);
    Task<Response.MyRoundDetailResponse> GetMyRoundDetail(Guid registerTeamId);
    Task<Response.CreateSubmissionResponse> CreateSubmission(Guid roundId, Request.CreateSubmissionRequest request);
    Task<BasePaginationResponse> GetRoundSubmissions(Guid roundId, Request.GetSubmissionsQuery query);
    Task<BasePaginationResponse> GetMyRoundSubmissions(Guid roundId, Request.GetSubmissionsQuery query);
    Task<BasePaginationResponse> GetRoundRanking(Guid roundId, Request.GetSubmissionsQuery query);
    Task<Response.MyRoundScoreResponse> GetMyRoundScore(Guid roundId);
    Task<BasePaginationResponse> GetStaffRoundSubmissions(Guid roundId, Request.GetStaffRoundSubmissionsQuery query);
    Task<Response.AssignJudgesToSubmissionResponse> AssignJudgesToSubmission(Guid submissionId, Request.AssignJudgesToSubmissionRequest request);
    Task<(Response.EndRoundResponse Data, string Message)> EndRound(Guid roundId);
}
