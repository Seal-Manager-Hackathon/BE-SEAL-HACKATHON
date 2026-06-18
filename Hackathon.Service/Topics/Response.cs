namespace Hackathon.Service.Topics;

public static class Response
{
    public class AssignedTopicResponse
    {
        public Guid RegisterTeamId { get; set; }
        public Guid EventId { get; set; }
        public Guid? TrackId { get; set; }
        public string? TrackTitle { get; set; }
        public string? TrackDescription { get; set; }
        public Guid? TopicId { get; set; }
        public string? TopicTitle { get; set; }
        public string? TopicDescription { get; set; }
    }
}
