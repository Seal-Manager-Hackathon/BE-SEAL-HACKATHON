namespace Hackathon.Service.Rounds;

public static class Response
{
    public class RoundResponse
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? RoundNo { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public DateTimeOffset? StartSubmission { get; set; }
        public DateTimeOffset? EndSubmission { get; set; }
        public int? LimitTeam { get; set; }
        public bool IsDisable { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class SubmitAssignmentResponse
    {
        public Guid SubmissionId { get; set; }
        public Guid TeamId { get; set; }
        public string? Url { get; set; }
        public DateTimeOffset SubmittedAt { get; set; }
    }

    public class MyRoundResponse
    {
        public Guid RoundId { get; set; }
        public Guid EventId { get; set; }
        public string RoundName { get; set; } = null!;
        public string EventName { get; set; } = null!;
        public int? RoundNo { get; set; }
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public Guid RegisterTeamId { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public DateTimeOffset? StartSubmission { get; set; }
        public DateTimeOffset? EndSubmission { get; set; }
    }

    public class MyRoundDetailResponse
    {
        public Guid RoundId { get; set; }
        public Guid EventId { get; set; }
        public string RoundName { get; set; } = null!;
        public string EventName { get; set; } = null!;
        public int? RoundNo { get; set; }
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public Guid RegisterTeamId { get; set; }
        public Guid? TrackId { get; set; }
        public string? TrackTitle { get; set; }
        public Guid? TopicId { get; set; }
        public string? TopicTitle { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public DateTimeOffset? StartSubmission { get; set; }
        public DateTimeOffset? EndSubmission { get; set; }
    }

    public class SubmissionResponse
    {
        public Guid SubmissionId { get; set; }
        public string? Url { get; set; }
        public DateTimeOffset? SubmittedAt { get; set; }
        public string? Status { get; set; }
        public decimal? TotalScore { get; set; }
    }

    public class StaffRoundSubmissionResponse
    {
        public Guid? SubmissionId { get; set; }
        public Guid RoundDetailId { get; set; }
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public Guid? TrackId { get; set; }
        public string? TrackTitle { get; set; }
        public Guid? TopicId { get; set; }
        public string? TopicTitle { get; set; }
        public string? Url { get; set; }
        public string? Description { get; set; }
        public string SubmissionStatus { get; set; } = null!;
        public DateTimeOffset? SubmittedAt { get; set; }
        public string? GradingStatus { get; set; }
        public List<AssignedJudgeResponse> AssignedJudges { get; set; } = new();
        public decimal? AverageScore { get; set; }
        public decimal? MinScore { get; set; }
        public decimal? MaxScore { get; set; }
    }

    public class AssignedJudgeResponse
    {
        public Guid JudgeId { get; set; }
        public string JudgeName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool HasScored { get; set; }
        public decimal? TotalScore { get; set; }
        public bool IsFinalized { get; set; }
    }

    public class AssignJudgesToSubmissionResponse
    {
        public Guid SubmissionId { get; set; }
        public List<AssignedJudgeResponse> AssignedJudges { get; set; } = new();
    }

    public class EndRoundResponse
    {
        public Guid ClosedRoundId { get; set; }
        public Guid? NextRoundId { get; set; }
        public int TotalTeamsAdvanced { get; set; }
    }
}
