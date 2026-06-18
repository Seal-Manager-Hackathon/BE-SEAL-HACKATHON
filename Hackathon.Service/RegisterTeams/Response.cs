namespace Hackathon.Service.RegisterTeams;

public static class Response
{
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
