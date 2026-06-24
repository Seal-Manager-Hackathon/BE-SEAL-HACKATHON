using System;

namespace Hackathon.Service.Tracks;

public static class Request
{
    public class AssignTrackToTeamRequest
    {
        public Guid EventId { get; set; }
        public Guid TrackId { get; set; }
    }

    public class AssignTopicToTeamRequest
    {
        public Guid EventId { get; set; }
        public Guid TopicId { get; set; }
    }
}
