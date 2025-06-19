using Domain.Interfaces;
using FluentValidation;
using Service.Common.Constants;
using Service.DTOs;

namespace Service.Validators;

public class ReviewDtoValidator : AbstractValidator<ReviewDto>
{
    public ReviewDtoValidator(IUnitOfWork unitOfWork)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;
        
        RuleSet("Input", () =>
        {
            RuleFor(x => x.Title)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.Required, nameof(ReviewDto.Title)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(ReviewDto.Title)))
                .MaximumLength(100).WithMessage(string.Format(CommonValidationErrorMessages.StringLength, nameof(ReviewDto.Title), 100));

            RuleFor(x => x.Content)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.Required, nameof(ReviewDto.Content)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(ReviewDto.Content)))
                .MaximumLength(1000).WithMessage(string.Format(CommonValidationErrorMessages.StringLength, nameof(ReviewDto.Content), 1000));

            RuleFor(x => x.Rating)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.Required, nameof(ReviewDto.Rating)))
                .InclusiveBetween(1, 5).WithMessage(ReviewValidationErrorMessages.RatingMustBeBetween1And5);

            RuleFor(x => x.ReviewerId)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.Required, nameof(ReviewDto.ReviewerId)));

            RuleFor(x => x.PokemonId)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.Required, nameof(ReviewDto.PokemonId)));
        });

        RuleSet("CreateBusiness", () =>
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.PokemonId)
                .MustAsync(async (x, _, _) =>
                {
                    return await unitOfWork.PokemonRepository.ExistsAsync((int)x.PokemonId);
                })
                .WithMessage(x => string.Format(CommonValidationErrorMessages.DoesNotExist, "Pokemon", x.PokemonId));

            RuleFor(x => x.ReviewerId)
                .MustAsync(async (x, _, _) =>
                {
                    return await unitOfWork.ReviewerRepository.ExistsAsync((int)x.ReviewerId);
                })
                .WithMessage(x => string.Format(CommonValidationErrorMessages.DoesNotExist, "Reviewer", x.ReviewerId));
            
            RuleFor(x => x)
                .MustAsync(async (dto, _, _) =>
                {
                    var exists = await unitOfWork.ReviewRepository.ExistsAsync(dto.ReviewerId ?? 0, dto.PokemonId ?? 0);
                    return !exists;
                })
                .WithMessage(x => ReviewValidationErrorMessages.ReviewAlreadyExists);
        });
    }
}
