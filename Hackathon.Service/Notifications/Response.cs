namespace Hackathon.Service.Notifications;

public static class Response
{
    public class NotificationResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TeamId { get; set; }
        public string? Title { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
