using Hackathon.Repository.Enum;
using Hackathon.Service.Models;

namespace Hackathon.Service.AssignEvents;

public static class Request
{
    public class AssignLecturerRequest
    {
        public Guid LecturerId { get; set; }
        public EventRoleEnum EventRole { get; set; }
    }

    public class GetAvailableLecturersRequest : PaginationRequest
    {
        public Guid EventRoleId { get; set; }
        public string? Keyword { get; set; }
    }
}
