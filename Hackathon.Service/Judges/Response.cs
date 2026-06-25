using Hackathon.Repository.Enum;

namespace Hackathon.Service.Judges;

public static class Response
{
    public class JudgeTrackResponse
    {
        public Guid AssignTrackId { get; set; }
        public Guid TrackId { get; set; }
        public string TrackTitle { get; set; } = string.Empty;
        public string? TrackDescription { get; set; }
        public Guid EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public int SubmissionCount { get; set; }
        public int GradedSubmissionCount { get; set; }
    }

    public class JudgeTrackSubmissionResponse
    {
        public Guid SubmissionId { get; set; }
        public Guid RoundDetailId { get; set; }
        public Guid RoundId { get; set; }
        public string RoundName { get; set; } = string.Empty;
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string? Description { get; set; }
        public SubmissionStatusEnum? Status { get; set; }
        public DateTimeOffset? SubmittedAt { get; set; }
        public bool IsGraded { get; set; }
        public Guid? ScoreId { get; set; }
        public decimal? TotalScore { get; set; }
    }

    public class SubmissionCriteriaResponse
    {
        public Guid SubmissionId { get; set; }
        public Guid RoundId { get; set; }
        public Guid? TemplateId { get; set; }
        public string? TemplateTitle { get; set; }
        public List<CriteriaItemResponse> CriteriaItems { get; set; } = [];
    }

    public class CriteriaItemResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal MaxScore { get; set; }
    }

    public class JudgeSubmissionScoreResponse
    {
        public Guid ScoreId { get; set; }
        public Guid SubmissionId { get; set; }
        public Guid AssignTrackId { get; set; }
        public decimal? TotalScore { get; set; }
        public bool IsRetake { get; set; }
        public bool IsMock { get; set; }
        public List<JudgeScoreItemResponse> ScoreItems { get; set; } = [];
    }

    public class JudgeScoreItemResponse
    {
        public Guid CriteriaItemId { get; set; }
        public string CriteriaItemName { get; set; } = string.Empty;
        public decimal? Score { get; set; }
        public string? Comment { get; set; }
    }

    public class JudgeScoreDashboardResponse
    {
        public int TotalAssignedSubmissions { get; set; }
        public int TotalGradedSubmissions { get; set; }
        public int TotalPendingSubmissions { get; set; }
        public decimal GradedPercentage { get; set; }
    }
}
