namespace Hackathon.Service.RegisterTeams;

public static class Response
{
    public class RegisterTeamResponse
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public string? TeamName { get; set; }
        public Guid EventId { get; set; }
        public string? EventName { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? RejectionReason { get; set; }
        public bool IsBanned { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string Message { get; set; } = null!;
    }

    public class AssignedEventResponse
    {
        public Guid EventId { get; set; }
        public string EventName { get; set; } = null!;
        public string? EventStatus { get; set; }
        public string EventRole { get; set; } = null!;
        public DateTimeOffset? RegisterLimitTime { get; set; }
    }

    public class PendingRegisterTeamResponse
    {
        public Guid RegisterTeamId { get; set; }
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public int MemberCount { get; set; }
        public string? Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class RegisterTeamDetailResponse : RegisterTeamResponse
    {
        public List<TeamMemberDetailResponse> Members { get; set; } = new();
    }

    public class TeamMemberDetailResponse
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StudentId { get; set; }
        public string? College { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public bool IsLeader { get; set; }
        public string? Status { get; set; }
    }
}
