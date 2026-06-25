using System;
using Hackathon.Repository.Enum;

namespace Hackathon.Service.Lecturers;

public static class Response
{
    public class LecturerEventResponse
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
