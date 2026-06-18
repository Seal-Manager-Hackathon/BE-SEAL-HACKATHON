namespace Hackathon.Service.Teams;

public static class Response
{
    public class TeamMemberResponse
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTimeOffset DateOfBirth { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string College { get; set; } = string.Empty;
        public bool IsLeader { get; set; }
        public string? Status { get; set; }
    }

    public class CreateTeamResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool CanEdit { get; set; }
        public List<TeamMemberResponse> Members { get; set; } = new();
        public DateTimeOffset CreatedAt { get; set; }
        public string Message { get; set; } = null!;
    }

    public class MessageResponse
    {
        public string Message { get; set; } = null!;
    }

    public class MyTeamResponse
    {
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public bool CanEdit { get; set; }
        public bool IsLeader { get; set; }
        public string? MemberStatus { get; set; }
        public DateTimeOffset JoinedAt { get; set; }
    }

    public class TeamDetailResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool CanEdit { get; set; }
        public List<TeamMemberResponse> Members { get; set; } = new();
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class RegisterEventResponse
    {
        public Guid RegisterId { get; set; }
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public Guid EventId { get; set; }
        public string EventName { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Message { get; set; } = null!;
    }

    public class RegisteredEventItemResponse
    {
        public Guid RegisterId { get; set; }
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public Guid EventId { get; set; }
        public string EventName { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class RejectionReasonResponse
    {
        public Guid RegisterId { get; set; }
        public string Status { get; set; } = null!;
        public string? RejectionReason { get; set; }
    }
}
