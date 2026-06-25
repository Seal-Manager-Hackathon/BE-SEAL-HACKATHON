namespace Hackathon.Service.Mentors;

public static class Response
{
    public class MentorEventResponse
    {
        public Guid AssignEventId { get; set; }
        public Guid EventId { get; set; }
        public string EventName { get; set; } = null!;
        public Hackathon.Repository.Enum.EventRoleEnum? Role { get; set; }
    }
}
