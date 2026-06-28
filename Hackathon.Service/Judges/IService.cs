using Hackathon.Service.Models;

namespace Hackathon.Service.Judges;

public interface IService
{
    Task<List<Response.JudgeTrackResponse>> GetMyTracks();
    Task<BasePaginationResponse> GetTrackSubmissions(Guid trackId, PaginationRequest paginationRequest);
    Task<BasePaginationResponse> GetEventSubmissions(Guid eventId, Guid? trackId, Guid? roundId, PaginationRequest paginationRequest);
    Task<BasePaginationResponse> GetPendingSubmissions(Guid eventId, Guid? trackId, Guid? roundId, bool? isGraded, PaginationRequest paginationRequest);
    Task<BasePaginationResponse> GetCurrentEventPendingSubmissions(Guid? trackId, Guid? roundId, PaginationRequest paginationRequest);
    Task<BasePaginationResponse> SearchSubmissions(Guid eventId, Guid? trackId, string? keyword, PaginationRequest paginationRequest);
    Task<Response.SubmissionCriteriaResponse> GetSubmissionCriteria(Guid submissionId);
    Task<Response.JudgeSubmissionScoreResponse?> GetMySubmissionScore(Guid submissionId);
    Task<BasePaginationResponse> GetMyScores(Guid eventId, Guid? trackId, bool? isGraded, PaginationRequest paginationRequest);
    Task<Response.JudgeSubmissionScoreResponse> SubmitScore(Guid submissionId, Request.SubmitScoreRequest request);
    Task<Response.JudgeSubmissionScoreResponse> SubmitMockScore(Guid submissionId, Request.SubmitScoreRequest request);
    Task<Response.JudgeSubmissionScoreResponse> UpdateScore(Guid scoreId, Request.SubmitScoreRequest request);
    Task<string> FinalizeScore(Guid scoreId);
    Task<Response.JudgeSubmissionScoreResponse> SubmitRetakeScore(Guid scoreId, Request.SubmitScoreRequest request);
    Task<(List<Response.JudgeTrackTeamResponse> Data, string Message)> GetJudgeTeamsByEvent(Guid eventId, Guid? roundId);
}
