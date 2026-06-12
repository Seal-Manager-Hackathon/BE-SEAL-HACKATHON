namespace Hackathon.Service.Users;

public static class Response
{
    public class ProfileResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StudentId { get; set; }
        public string? College { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public string? Status { get; set; }
        public bool? IsVerified { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
