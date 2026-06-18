using Hackathon.Repository.Enum;

namespace Hackathon.Service.Users;

public static class Reponse
{
    public class UserProfileDetailResponse
    {
        public required string Email { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTimeOffset DateOfBirth { get; set; } = DateTimeOffset.MinValue;
        public string StudentId { get; set; } = string.Empty;
        public string College { get; set; } = string.Empty;
        public string? ImgUrl { get; set; }
        public string? LinkUrl { get; set; }
        public UserStatusEnum? Status { get; set; }
        public string? BanReason { get; set; }
    }
}