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
    }

    public class ApprovedTeamResponse
    {
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public Guid? TrackId { get; set; }
        public string? TrackTitle { get; set; }
        public Guid? TopicId { get; set; }
        public string? TopicTitle { get; set; }
        public List<ApprovedTeamMemberResponse> Members { get; set; } = new();
        public bool IsBanned { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class ApprovedTeamMemberResponse
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string StudentId { get; set; } = null!;
        public bool IsLeader { get; set; }
    }

    public class TrackTeamCountResponse
    {
        public Guid TrackId { get; set; }
        public Guid EventId { get; set; }
        public string Title { get; set; } = null!;
        public int? MaxTeam { get; set; }
        public int CurrentTeamCount { get; set; }
    }
}
