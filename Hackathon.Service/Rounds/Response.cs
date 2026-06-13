namespace Hackathon.Service.Rounds;

public static class Response
{
    public class RoundResponse
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public DateTimeOffset? StartSubmission { get; set; }
        public DateTimeOffset? EndSubmission { get; set; }
        public int? LimitTeam { get; set; }
        public bool IsDisable { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
