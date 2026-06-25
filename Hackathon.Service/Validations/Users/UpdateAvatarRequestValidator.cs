using FluentValidation;
using Hackathon.Service.Users;

namespace Hackathon.Service.Validations.Users;

public class UpdateAvatarRequestValidator : AbstractValidator<Request.UpdateAvatarRequest>
{
    public UpdateAvatarRequestValidator()
    {
        RuleFor(x => x.AvatarUrl)
            .NotEmpty().WithMessage("AVATAR_URL_REQUIRED")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage("AVATAR_URL_INVALID");
    }
}
