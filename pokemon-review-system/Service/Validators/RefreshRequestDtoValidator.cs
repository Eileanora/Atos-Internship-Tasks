using FluentValidation;
using Service.DTOs;
using Service.Common.Constants;

namespace Service.Validators;

public class RefreshRequestDtoValidator : AbstractValidator<RefreshRequestDto>
{
    public RefreshRequestDtoValidator()
    {
        RuleSet("input", () => {
            RuleFor(x => x.AccessToken)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(RefreshRequestDto.AccessToken)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(RefreshRequestDto.AccessToken)));

            RuleFor(x => x.RefreshToken)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(RefreshRequestDto.RefreshToken)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(RefreshRequestDto.RefreshToken)));
        });
    }
}

