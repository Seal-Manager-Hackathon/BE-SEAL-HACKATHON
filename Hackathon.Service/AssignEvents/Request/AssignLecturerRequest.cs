using Hackathon.Repository.Enum;

namespace Hackathon.Service.AssignEvents.Request;

public class AssignLecturerRequest
{
    public Guid LecturerId { get; set; }
    public EventRoleEnum EventRole { get; set; }
}
