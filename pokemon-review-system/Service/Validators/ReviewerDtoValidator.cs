using Domain.Interfaces;
using FluentValidation;
using Service.Common.Constants;
using Shared.DTOs;

namespace Service.Validators;

public class ReviewerDtoValidator : AbstractValidator<ReviewerDto>
{
    public ReviewerDtoValidator(IUnitOfWork unitOfWork)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;
        
        RuleSet("Input", () =>
        {
            RuleFor(x => x.FirstName)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.Required, nameof(ReviewerDto.FirstName)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(ReviewerDto.FirstName)))
                .MaximumLength(50).WithMessage(string.Format(CommonValidationErrorMessages.StringLength, nameof(ReviewerDto.FirstName), 50));

            RuleFor(x => x.LastName)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.Required, nameof(ReviewerDto.LastName)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(ReviewerDto.LastName)))
                .MaximumLength(50).WithMessage(string.Format(CommonValidationErrorMessages.StringLength, nameof(ReviewerDto.LastName), 50));
        });

        RuleSet("CreateBusiness", () =>
        {
            RuleFor(x => x)
                .MustAsync(async (dto, _, _) =>
                {
                    var exists = await unitOfWork.ReviewerRepository.NameExistsAsync(dto.FirstName, dto.LastName);
                    return !exists;
                })
                .WithMessage(x => string.Format(CommonValidationErrorMessages.NameExists, $"{x.FirstName} {x.LastName}"));
        });

        RuleSet("UpdateBusiness", () =>
        {
            RuleFor(x => x)
                .MustAsync(async (dto, _, context, _) =>
                {
                    var reviewerId = (int?)context.RootContextData["reviewerId"] ?? 0;
                    var reviewer = await unitOfWork.ReviewerRepository.GetByNameAsync(dto.FirstName, dto.LastName);
                    return reviewer == null || reviewer.Id == reviewerId;
                })
                .WithMessage(x => string.Format(CommonValidationErrorMessages.NameExists, $"{x.FirstName} {x.LastName}"));
        });
    }
}
