using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Service.Common.Constants;
using Service.DTOs;

namespace Service.Validators;

public class CreateOwnerValidator : RegisterDtoValidator<CreateOwnerDto>
{
    public CreateOwnerValidator(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager) : base(userManager)
    // TODO: Find a way to inject OwnerDtoValidator input ruleset here
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;
        
        RuleSet("input", () =>
        {
            RuleFor(x => x.FirstName)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.Required, nameof(OwnerDto.FirstName)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(OwnerDto.FirstName)))
                .MaximumLength(50).WithMessage(string.Format(CommonValidationErrorMessages.StringLength, nameof(OwnerDto.FirstName), 50));

            RuleFor(x => x.LastName)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.Required, nameof(OwnerDto.LastName)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(OwnerDto.LastName)))
                .MaximumLength(50).WithMessage(string.Format(CommonValidationErrorMessages.StringLength, nameof(OwnerDto.LastName), 50));

            RuleFor(x => x.Gym)
                .MaximumLength(100).WithMessage(string.Format(CommonValidationErrorMessages.StringLength, nameof(OwnerDto.Gym), 100));

            RuleFor(x => x.CountryId)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.Required, nameof(OwnerDto.CountryId)))
                .GreaterThan(0).WithMessage(string.Format(CommonValidationErrorMessages.InvalidId));
        });

        RuleSet("CreateBusiness", () =>
        {
            // check country exists
            RuleFor(x => x.CountryId)
                .MustAsync(async (id, cancellation) => await unitOfWork.CountryRepository.ExistsAsync((int)id))
                .WithMessage(x => string.Format(CommonValidationErrorMessages.DoesNotExist, "Country", x.CountryId));
        });
    }
}
