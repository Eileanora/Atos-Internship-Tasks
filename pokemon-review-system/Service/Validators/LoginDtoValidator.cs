using FluentValidation;
using Service.DTOs;
using Service.Common.Constants;

namespace Service.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleSet("input", () => {
            RuleFor(x => x.Email)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(LoginDto.Email)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(LoginDto.Email)))
                .EmailAddress().WithMessage(AuthValidationMessages.InvalidEmail);

            RuleFor(x => x.Password)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(LoginDto.Password)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(LoginDto.Password)));
        });
    }
}

