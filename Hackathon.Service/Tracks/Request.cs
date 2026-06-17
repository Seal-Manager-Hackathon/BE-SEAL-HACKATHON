namespace Hackathon.Service.Tracks;

public static class Request
{
    public class AssignTrackToTeamRequest
    {
        public Guid TrackId { get; set; }
    }

    public class AssignTopicToTeamRequest
    {
        public Guid TopicId { get; set; }
    }
}
