namespace Hackathon.Service.Events;

public static class Response
{
    public class EventResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public DateTimeOffset? RegisterLimitTime { get; set; }
        public int? LimitTeam { get; set; }
        public int? MinMember { get; set; }
        public int? MaxMember { get; set; }
        public string? Status { get; set; }
        public int? NumberRound { get; set; }
        public string? Season { get; set; }
        public bool IsDisable { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class CreateEventResponse
    {
        public Guid Id { get; set; }
    }

    public class AssignStaffToEventResponse
    {
        public Guid Id { get; set; }
    }

    public class EventParticipantResponse : EventResponse
    {
        public int TeamCount { get; set; }
        public int ParticipantCount { get; set; }
    }

    public class StudentEventResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public string? Status { get; set; }
        public string? Season { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class AdminEventResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public string? Status { get; set; }
        public string? Season { get; set; }
        public bool IsDisable { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
