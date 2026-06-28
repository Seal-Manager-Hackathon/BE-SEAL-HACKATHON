namespace Hackathon.Service.Judges;

public interface IService
{
    Task<List<Response.JudgeTrackResponse>> GetMyTracks();
    Task<List<Response.JudgeTrackSubmissionResponse>> GetTrackSubmissions(Guid trackId);
    Task<Response.SubmissionCriteriaResponse> GetSubmissionCriteria(Guid submissionId);
    Task<Response.JudgeSubmissionScoreResponse?> GetMySubmissionScore(Guid submissionId);
    Task<Response.JudgeScoreDashboardResponse> GetMyScores();
    Task<Response.JudgeSubmissionScoreResponse> SubmitScore(Guid submissionId, Request.SubmitScoreRequest request);
    Task<Response.JudgeSubmissionScoreResponse> SubmitMockScore(Guid submissionId, Request.SubmitScoreRequest request);
    Task<Response.JudgeSubmissionScoreResponse> UpdateScore(Guid scoreId, Request.SubmitScoreRequest request);
    Task<string> FinalizeScore(Guid scoreId);
    Task<Response.JudgeSubmissionScoreResponse> SubmitRetakeScore(Guid scoreId, Request.SubmitScoreRequest request);
    Task<(List<Response.JudgeTrackTeamResponse> Data, string Message)> GetJudgeTeamsByEvent(Guid eventId, Guid? roundId);
}
