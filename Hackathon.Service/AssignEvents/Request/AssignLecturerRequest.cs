namespace Hackathon.Service.AssignEvents.Request;

public class AssignLecturerRequest
{
    public Guid LecturerId { get; set; }
    public Guid EventRoleId { get; set; }
}
