using FluentValidation;
using Service.Common.Constants;
using Service.DTOs;

namespace Service.Validators;

public class AddRoleRequestValidator : AbstractValidator<AddRoleRequest>
{
    public AddRoleRequestValidator()
    {
        RuleSet("input", () =>{
            RuleFor(x => x.Email)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(AddRoleRequest.Email)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(AddRoleRequest.Email)))
                .EmailAddress().WithMessage(AuthValidationMessages.InvalidEmail);

            RuleFor(x => x.Role)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(AddRoleRequest.Role)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(AddRoleRequest.Role)));
        });
    }
    
}
