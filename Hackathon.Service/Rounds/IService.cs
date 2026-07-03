using Hackathon.Service.Models;

namespace Hackathon.Service.Rounds;

public interface IService
{
    Task<List<Response.RoundResponse>> GetRounds(Guid eventId);
    Task<List<Response.MyRoundResponse>> GetMyRounds(Guid? eventId, Guid teamId);
    Task<Response.MyRoundDetailResponse> GetMyRoundDetail(Guid registerTeamId);
    Task<Response.SubmitAssignmentResponse> SubmitAssignment(Guid roundId, Request.SubmitAssignmentRequest request);
    Task<BasePaginationResponse> GetRoundSubmissions(Guid roundId, Request.GetSubmissionsQuery query);
    Task<BasePaginationResponse> GetStaffRoundSubmissions(Guid roundId, Request.GetStaffRoundSubmissionsQuery query);
    Task<Response.AssignJudgesToSubmissionResponse> AssignJudgesToSubmission(Guid submissionId, Request.AssignJudgesToSubmissionRequest request);
    Task<(Response.EndRoundResponse Data, string Message)> EndRound(Guid roundId);
}
