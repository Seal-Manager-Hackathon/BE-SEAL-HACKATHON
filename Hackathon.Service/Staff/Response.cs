using Hackathon.Repository.Enum;

namespace Hackathon.Service.Staff;

public static class Response
{
    public class StaffEventResponse
    {
        public Guid AssignEventId { get; set; }
        public Guid EventId { get; set; }
        public string EventName { get; set; } = null!;
        public string? Season { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public EventRoleEnum? Role { get; set; }
        public EventStatusEnum? EventStatus { get; set; }
    }
}
