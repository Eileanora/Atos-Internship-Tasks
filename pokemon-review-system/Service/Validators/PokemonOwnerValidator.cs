using Domain.Models;
using FluentValidation;
using Service.Common.Constants;
using Service.Interfaces;

namespace Service.Validators;

public class PokemonOwnerValidator : AbstractValidator<PokemonOwner>
{
    public PokemonOwnerValidator(IUnitOfWork unitOfWork)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleSet("Input", () =>
        {
            RuleFor(po => po.OwnerId)
                .MustAsync(async (ownerId, cancellation) =>
                    await unitOfWork.OwnerRepository.ExistsAsync(ownerId))
                .WithMessage(po => string.Format(CommonValidationErrorMessages.DoesNotExist, "Owner", po.OwnerId));

            RuleFor(po => po.PokemonId)
                .MustAsync(async (pokemonId, cancellation) =>
                    await unitOfWork.PokemonRepository.ExistsAsync(pokemonId))
                .WithMessage(po => string.Format(CommonValidationErrorMessages.DoesNotExist, "Pokemon", po.PokemonId));
        });
        RuleSet("CreateBusiness", () =>
        {
            RuleFor(po => po)
                .MustAsync(async (po, cancellation) =>
                    !await unitOfWork.PokemonOwnerRepository.OwnerPokemonExistsAsync(po.OwnerId, po.PokemonId))
                .WithMessage(OwnerValidationErrorMessages.OwnerAlreadyHasPokemon);
        });
        RuleSet("DeleteBusiness", () =>
        {
            RuleFor(po => po)
                .MustAsync(async (po, cancellation) =>
                    await unitOfWork.PokemonOwnerRepository.OwnerPokemonExistsAsync(po.OwnerId, po.PokemonId))
                .WithMessage(OwnerValidationErrorMessages.OwnerDoesNotHavePokemon);
        });

    }
}
