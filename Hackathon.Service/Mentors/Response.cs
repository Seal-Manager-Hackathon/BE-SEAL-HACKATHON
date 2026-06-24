namespace Hackathon.Service.Mentors;

public static class Response
{
    public class MentorEventResponse
    {
        public Guid AssignEventId { get; set; }
        public Guid EventId { get; set; }
        public string EventName { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
