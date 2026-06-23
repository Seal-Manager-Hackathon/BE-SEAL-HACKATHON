namespace Hackathon.Service.AssignEvents.Response;

public class AssignEventResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid EventRoleId { get; set; }
    public Guid EventId { get; set; }
}
