namespace Hackathon.Service.MentorNotifications;

public static class Response
{
    public class MentorNotificationResponse
    {
        public Guid Id { get; set; }
        public Guid AssignTrackId { get; set; }
        public Guid TrackId { get; set; }
        public Guid EventId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
