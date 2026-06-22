namespace Hackathon.Service.Auths;

public class Response
{
    public class VerifyEmailResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }

    public class AuthResponse : VerifyEmailResponse;

    public class GetMeResponse
    {
        public Guid Id { get; set; }
        public string Role { get; set; } = null!;
        public required String FirstName { get; set; }
        public required String LastName { get; set; }
        public required String Email { get; set; }
        public String? Avatar { get; set; }
    }

    public class LoginResponse
    {
        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }
    }
}