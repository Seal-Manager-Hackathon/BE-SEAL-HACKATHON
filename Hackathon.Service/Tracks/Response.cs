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
}
