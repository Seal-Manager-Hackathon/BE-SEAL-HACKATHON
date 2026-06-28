using System;
using Hackathon.Repository.Enum;

namespace Hackathon.Service.AssignEvents.Response;

public class AssignLecturerDetailResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid? EventRoleId { get; set; }
    public EventRoleEnum? EventRole { get; set; }
    public RoleEnum Role { get; set; }
    public bool IsDisable { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
