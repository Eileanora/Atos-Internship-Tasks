using FluentValidation;
using Service.Common.Constants;
using Shared.DTOs;
using Service.Interfaces;

namespace Service.Validators;

public class PokemonDtoValidator : AbstractValidator<PokemonDto>
{
    public PokemonDtoValidator(
        IUnitOfWork unitOfWork)
    {
        Func<IEnumerable<int>, CancellationToken, Task<bool>> categoriesExistRule = async (categoriesId, _) =>
        {
            return await unitOfWork.CategoryRepository.CheckCategoriesExistAsync(categoriesId);
        };
        RuleSet("Input", () =>
        {
            RuleFor(p => p.Name)
                .NotNull();
            RuleFor(p => p.BirthDate)
                .NotNull();
            RuleFor(p => p.CategoriesId)
                .NotNull();
            
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.Required, "Name"))
                .MaximumLength(100).WithMessage(string.Format(CommonValidationErrorMessages.StringLength, "Name", 100)); 
            
            RuleFor(p => p.BirthDate)
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.Required, "BirthDate"))
                .LessThanOrEqualTo(DateTime.Today).WithMessage(CommonValidationErrorMessages.BirthDateFuture);

            RuleFor(p => p.CategoriesId)
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.Required, "CategoriesId"));
        });
        
        RuleSet("CreateBusiness", () =>
        {
            RuleFor(p => p)
                .MustAsync(async (x, _, _) =>
                {
                    var nameExists = await unitOfWork.PokemonRepository.CheckNameUniqueAsync(x.Name);
                    return nameExists;
                })
                .WithMessage(p => string.Format(CommonValidationErrorMessages.NameExists, p.Name));
            
            RuleFor(p => p.CategoriesId)
                .MustAsync(async (categoriesId, _) =>
                {
                    return await unitOfWork.CategoryRepository.CheckCategoriesExistAsync(categoriesId);
                })
                .WithMessage(p => string.Format(CommonValidationErrorMessages.InvalidId));
        });
        
        RuleSet("UpdateBusiness", () =>
        {
            RuleFor(p => p)
                .MustAsync(async (x, _, context, _) =>
                {
                    var nameExists = await unitOfWork.PokemonRepository.GetByNameAsync(x.Name);
                    var pokemonId = (int)context.RootContextData["pokemonId"];
                    return nameExists == null || nameExists.Id == pokemonId;
                })
                .WithMessage(p => string.Format(CommonValidationErrorMessages.NameExists, p.Name));
            RuleFor(p => p.CategoriesId)
                .MustAsync(async (categoriesId, _) =>
                {
                    return await unitOfWork.CategoryRepository.CheckCategoriesExistAsync(categoriesId);
                })
                .WithMessage(p => string.Format(CommonValidationErrorMessages.InvalidId));
        });
    }
}
