namespace Hackathon.Service.Users;

public static class Request
{
    public class UpdateProfileRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StudentId { get; set; }
        public string? College { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
    }
}
