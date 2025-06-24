using FluentValidation;
using Service.DTOs;
using Service.Common.Constants;

namespace Service.Validators;

public class LogoutRequestValidator : AbstractValidator<LogoutRequest>
{
    public LogoutRequestValidator()
    {
        RuleSet("input", () => {
            RuleFor(x => x.RefreshToken)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(LogoutRequest.RefreshToken)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(LogoutRequest.RefreshToken)));
        });
    }
}

