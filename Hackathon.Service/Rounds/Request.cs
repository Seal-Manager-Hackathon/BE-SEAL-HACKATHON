using System.ComponentModel.DataAnnotations;
using Hackathon.Service.Models;

namespace Hackathon.Service.Rounds;

public static class Request
{
    public class SubmitAssignmentRequest
    {
        public string? Url { get; set; }
        public string? Description { get; set; }
    }

    public class GetSubmissionsQuery : PaginationRequest
    {
    }

    public class GetStaffRoundSubmissionsQuery : PaginationRequest
    {
        public Guid? TrackId { get; set; }
        public Guid? TopicId { get; set; }
        public string? SubmissionStatus { get; set; }
        public string? GradingStatus { get; set; }
        public string? Keyword { get; set; }
    }

    public class AssignJudgesToSubmissionRequest
    {
        public List<Guid> JudgeIds { get; set; } = new();
    }
}
