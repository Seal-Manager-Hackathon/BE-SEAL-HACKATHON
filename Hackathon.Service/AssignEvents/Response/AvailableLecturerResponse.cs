using Hackathon.Repository.Enum;

namespace Hackathon.Service.AssignEvents.Response;

public class AvailableLecturerResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public RoleEnum Role { get; set; }
    public bool IsAlreadyAssignedToEvent { get; set; }
    public EventRoleEnum? AssignedEventRole { get; set; }
}
