using FluentValidation;
using FluentValidation.Internal;
using Service.Common.Constants;
using Service.Common.ErrorAndResults;
using Shared.DTOs;
using Service.Interfaces;
using Service.Mappers;

namespace Service.Managers.PokemonManager;

public class PokemonManager(
    IUnitOfWork unitOfWork,
    IValidator<PokemonDto> pokemonValidator) : IPokemonManager
{
    public async Task<Result<IEnumerable<PokemonDto>>> GetAllAsync()
    {
        var pokemons = await unitOfWork.PokemonRepository.GetAllAsync();
        return Result<IEnumerable<PokemonDto>>.Success(pokemons.Select(p => p.ToListDto()));
    }

    public async Task<Result<PokemonDto>> GetByIdAsync(int id)
    {
        var pokemon = await unitOfWork.PokemonRepository.GetByIdAsync(id);
        if (pokemon == null)
            return Result<PokemonDto>.Failure(ErrorMessages.NotFound);
        return Result<PokemonDto>.Success(pokemon.ToDetailDto());
    }

    public async Task<Result<PokemonDto>> AddAsync(PokemonDto pokemon)
    {
        var validationResult = await pokemonValidator.ValidateAsync(pokemon,
            options => options.IncludeRuleSets("input", "CreateBusiness"));
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Result<PokemonDto>.Failure(new Error("ValidationError", errorMessage));
        }
        
        var newPokemon = pokemon.ToEntity();
        await unitOfWork.PokemonRepository.AddAsync(newPokemon);

        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<PokemonDto>.Failure(ErrorMessages.InternalServerError);

        return Result<PokemonDto>.Success(newPokemon.ToDetailDto());
    }

    public async Task<Result<PokemonDto>> UpdateAsync(PokemonDto pokemon)
    {
        var context= new ValidationContext<PokemonDto>(
            pokemon,
            new PropertyChain(),
            new RulesetValidatorSelector(new [] {"UpdateBusiness"}))
        {
            RootContextData = {["pokemonId"] = pokemon.Id }
        };

        var validationResult = await pokemonValidator.ValidateAsync(context);

        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Result<PokemonDto>.Failure(new Error("ValidationError", errorMessage));
        }
        
        var pokemonEntity = await unitOfWork.PokemonRepository.GetByIdAsync((int)pokemon.Id);
        pokemonEntity.UpdateEntityFromDto(pokemon);
        
        unitOfWork.PokemonRepository.UpdateAsync(pokemonEntity); 

        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<PokemonDto>.Failure(ErrorMessages.InternalServerError);

        return Result<PokemonDto>.Success(pokemon);
    }

    public async Task<Result> DeleteAsync(PokemonDto pokemon)
    {
        var pokemonExists = await unitOfWork.PokemonRepository.GetByIdAsync((int)pokemon.Id);
        if (pokemonExists == null)
            return Result.Failure(ErrorMessages.NotFound);

        unitOfWork.PokemonRepository.DeleteAsync(pokemonExists);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result.Failure(ErrorMessages.InternalServerError);
        return Result.Success();
    }

    public async Task<Result<decimal>> GetPokemonRatingAsync(int pokemonId)
    {
        var pokemonExists = await unitOfWork.PokemonRepository.ExistsAsync(pokemonId);
        if (!pokemonExists)
            return Result<decimal>.Failure(ErrorMessages.NotFound);
        
        var rating = await unitOfWork.PokemonRepository.GetPokemonRatingAsync(pokemonId);
        return Result<decimal>.Success(rating);
    }
}
