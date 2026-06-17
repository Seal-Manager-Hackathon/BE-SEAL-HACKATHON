namespace Hackathon.Service.Tracks;

public static class Response
{
    public class TrackResponse
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? MaxTeam { get; set; }
        public bool IsDisable { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class TopicResponse
    {
        public Guid Id { get; set; }
        public Guid TrackId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsDisable { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class TeamTrackAssignmentResponse
    {
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public Guid EventId { get; set; }
        public Guid TrackId { get; set; }
        public string TrackTitle { get; set; } = null!;
        public string Message { get; set; } = null!;
    }

    public class TeamTopicAssignmentResponse
    {
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public Guid EventId { get; set; }
        public Guid TrackId { get; set; }
        public string TrackTitle { get; set; } = null!;
        public Guid TopicId { get; set; }
        public string TopicTitle { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
