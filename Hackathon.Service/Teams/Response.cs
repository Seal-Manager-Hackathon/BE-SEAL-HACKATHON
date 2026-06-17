namespace Hackathon.Service.Teams;

public static class Response
{
    public class TeamMemberResponse
    {
        public Guid UserId { get; set; }
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
}
