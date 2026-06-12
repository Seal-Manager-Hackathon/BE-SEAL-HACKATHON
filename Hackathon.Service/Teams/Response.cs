namespace Hackathon.Service.Teams;

public static class Response
{
    public class TeamMemberResponse
    {
        public Guid UserId { get; set; }
        public bool IsLeader { get; set; }
        public string? Status { get; set; }
    }

    public class TeamResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool CanEdit { get; set; }
        public List<TeamMemberResponse> Members { get; set; } = new();
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class MessageResponse
    {
        public string Message { get; set; } = null!;
    }

    public class CreateTeamResponse : TeamResponse
    {
        public string Message { get; set; } = null!;
    }

    public class InvitationResponse
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? LimitTime { get; set; }
        public string Message { get; set; } = null!;
    }
}
