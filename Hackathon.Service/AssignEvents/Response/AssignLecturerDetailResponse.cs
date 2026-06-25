namespace Hackathon.Service.AssignEvents.Response;

public class AssignLecturerDetailResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid? EventRoleId { get; set; }
    public Hackathon.Repository.Enum.EventRoleEnum? EventRoleName { get; set; }
    public bool IsDisable { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
