using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Service.Common.Constants;

namespace Service.Validators;

public class PokemonOwnerValidator : AbstractValidator<PokemonOwner>
{
    public PokemonOwnerValidator(IUnitOfWork unitOfWork)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleSet("Input", () =>
        {
            RuleFor(po => po.OwnerId)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.Required,
                    nameof(PokemonOwner.OwnerId)));

            RuleFor(po => po.PokemonId)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.Required,
                    nameof(PokemonOwner.PokemonId)));
        });
        
        // TODO : Get rid of duplicate code by using a common rule set for both Create and Delete
        RuleSet("CreateBusiness", () =>
        {
            RuleFor(po => po.PokemonId)
                .MustAsync(async (pokemonId, cancellation) =>
                    await unitOfWork.PokemonRepository.ExistsAsync(pokemonId))
                .WithMessage(po => string.Format(CommonValidationErrorMessages.DoesNotExist, "Pokemon", po.PokemonId));
            
            RuleFor(po => po)
                .MustAsync(async (po, cancellation) =>
                    !await unitOfWork.PokemonOwnerRepository.OwnerPokemonExistsAsync(po.OwnerId, po.PokemonId))
                .WithMessage(OwnerValidationErrorMessages.OwnerAlreadyHasPokemon);
        });
        RuleSet("DeleteBusiness", () =>
        {
            RuleFor(po => po.PokemonId)
                .MustAsync(async (pokemonId, cancellation) =>
                    await unitOfWork.PokemonRepository.ExistsAsync(pokemonId))
                .WithMessage(po => string.Format(CommonValidationErrorMessages.DoesNotExist, "Pokemon", po.PokemonId));
            
            RuleFor(po => po)
                .MustAsync(async (po, cancellation) =>
                    await unitOfWork.PokemonOwnerRepository.OwnerPokemonExistsAsync(po.OwnerId, po.PokemonId))
                .WithMessage(OwnerValidationErrorMessages.OwnerDoesNotHavePokemon);
        });

    }
}
