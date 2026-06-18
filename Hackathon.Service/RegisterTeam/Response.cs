using Hackathon.Repository.Enum;

namespace Hackathon.Service.RegisterTeam;

public static class Response
{
    public class RegisterTeamResponse
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public Guid EventId { get; set; }
        public Guid? TrackId { get; set; }
        public string? TrackTitle { get; set; }
        public Guid? TopicId { get; set; }
        public string? TopicTitle { get; set; }
        public string? Description { get; set; }
        public string? RejectionReason { get; set; }
        public RegisterTeamStatusEnum? Status { get; set; }
        public bool IsBanned { get; set; }
        public bool IsDisable { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class RegisterTeamDetailResponse
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public Guid EventId { get; set; }
        public string EventName { get; set; } = null!;
        public Guid? TrackId { get; set; }
        public string? TrackTitle { get; set; }
        public Guid? TopicId { get; set; }
        public string? TopicTitle { get; set; }
        public string? Description { get; set; }
        public string? RejectionReason { get; set; }
        public RegisterTeamStatusEnum Status { get; set; }
        public bool IsBanned { get; set; }
        public bool IsDisable { get; set; }
        public List<RegisterTeamMemberResponse> Members { get; set; } = new();
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }

    public class RegisterTeamMemberResponse
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string StudentId { get; set; } = null!;
        public bool IsLeader { get; set; }
    }
}
