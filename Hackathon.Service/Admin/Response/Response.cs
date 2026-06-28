using Hackathon.Repository.Enum;

namespace Hackathon.Service.Admin.Response;

public class AdminUserResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public string? StudentId { get; set; }
    public string? College { get; set; }
    public RoleEnum Role { get; set; }
    public UserStatusEnum? Status { get; set; }
    public bool? IsVerified { get; set; }
    public bool IsDisable { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
