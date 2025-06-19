using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Internal;
using Service.Common.Constants;
using Service.DTOs;
using Service.Interfaces;
using Service.Mappers;
using Shared.ErrorAndResults;
using Shared.Helpers;
using Shared.ResourceParameters;

namespace Service.Services;

public class PokemonService(
    IUnitOfWork unitOfWork,
    IValidator<PokemonDto> pokemonValidator) : IPokemonService
{
    public async Task<Result<PagedList<PokemonDto>>> GetAllAsync(
        PokemonResourceParameters resourceParameters)
    {
        var pokemons = await unitOfWork.PokemonRepository.GetAllAsync(resourceParameters);
        return Result<PagedList<PokemonDto>>.Success(pokemons.ToListDto());
    }

    public async Task<Result<PokemonDto>> GetByIdAsync(int id)
    {
        var pokemon = await unitOfWork.PokemonRepository.GetByIdAsync(id, true);
        if (pokemon == null)
            return Result<PokemonDto>.Failure(ErrorMessages.NotFound);
        return Result<PokemonDto>.Success(pokemon.ToDetailDto());
    }

    public async Task<Result<PokemonDto>> AddAsync(PokemonDto pokemon)
    {
        var validationResult = await ValidationHelper.ValidateAndReportAsync(pokemonValidator, pokemon, "CreateBusiness");
        if (!validationResult.IsSuccess)
        {
            return Result<PokemonDto>.Failure(validationResult.Error);
        }
        var newPokemon = pokemon.ToEntity();
        await unitOfWork.PokemonRepository.AddAsync(newPokemon);

        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<PokemonDto>.Failure(ErrorMessages.InternalServerError);

        return Result<PokemonDto>.Success(newPokemon.ToCreatedDto(pokemon.CategoriesId));
    }

    public async Task<Result<PokemonDto>> UpdateAsync(PokemonDto pokemon)
    {
        var validationResult = await ValidationHelper.ValidateAndReportAsync(
            pokemonValidator,
            pokemon,
            ctx => { ctx.RootContextData["pokemonId"] = pokemon.Id; },
            "UpdateBusiness");
        if (!validationResult.IsSuccess)
        {
            return Result<PokemonDto>.Failure(validationResult.Error);
        }
        var pokemonEntity = await unitOfWork.PokemonRepository.FindByIdAsync((int)pokemon.Id);
        pokemonEntity.UpdateEntityFromDto(pokemon);
        unitOfWork.PokemonRepository.UpdateAsync(pokemonEntity);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<PokemonDto>.Failure(ErrorMessages.InternalServerError);
        return Result<PokemonDto>.Success(pokemon);
    }

    public async Task<Result> DeleteAsync(PokemonDto pokemon)
    {
        var pokemonExists = await unitOfWork.PokemonRepository.FindByIdAsync((int)pokemon.Id);
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
