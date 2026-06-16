using System.ComponentModel.DataAnnotations;

namespace Hackathon.Service.Auth;

public class Request
{
    public class RegisterRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
    public class VerifyEmailRequest
    {
        public required string Token { get; set; }
        
    }
    
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]

        public required string Password { get; set; }
    }

    public class ChangePasswordRequest
    {
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
        public required string ConfirmPassword { get; set; }
    }

    public class ForgotPasswordRequest
    {
        [Required(ErrorMessage = "EMAIL_REQUIRED")]
        [EmailAddress(ErrorMessage = "INVALID_EMAIL_FORMAT")]
        public required string Email { get; set; }
    }

    public class ResetPasswordRequest : IValidatableObject
    {
        [Required(ErrorMessage = "Token không được để trống")]
        public required string Token { get; set; }

        [Required(ErrorMessage = "Mật khẩu mới không được để trống")]
        public required string NewPassword { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        public required string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NewPassword != ConfirmPassword)
            {
                yield return new ValidationResult(
                    "PASSWORD_CONFIRMATION_NOT_MATCH",
                    new[] { nameof(ConfirmPassword) });
            }
        }
    }
}