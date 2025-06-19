using Domain.Interfaces;
using Domain.Models;
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

public class OwnerService(
    IUnitOfWork unitOfWork,
    IValidator<OwnerDto> ownerValidator,
    IValidator<CreateOwnerDto> createOwnerValidator,
    IValidator<Domain.Models.PokemonOwner> pokemonOwnerValidator,
    IAuthService authService,
    IUserContext userContext)
    : IOwnerService
{
    public async Task<Result<PagedList<OwnerDto>>> GetAllAsync(OwnerResourceParameters resourceParameters)
    {
        var owners = await unitOfWork.OwnerRepository.GetAllAsync(resourceParameters);
        return Result<PagedList<OwnerDto>>.Success(owners.ToListDto());
    }

    public async Task<Result<OwnerDto>> GetByIdAsync(string id)
    {
        var owner = await unitOfWork.OwnerRepository.GetByIdAsyncWithInclude(id);
        if (owner == null)
            return Result<OwnerDto>.Failure(ErrorMessages.NotFound);
        return Result<OwnerDto>.Success(owner.ToDetailDto());
    }
    
    public async Task<Result<OwnerDto>> GetByIdForPatchAsync(string id)
    {
        var owner = await unitOfWork.OwnerRepository.GetByUserIdAsync(id);
        if (owner == null)
            return Result<OwnerDto>.Failure(ErrorMessages.NotFound);
        return Result<OwnerDto>.Success(owner.ToUpdateDto());
    }

    public async Task<Result<OwnerDto>> AddAsync(CreateOwnerDto owner)
    {
        var validationResult = await ValidationHelper.ValidateAndReportAsync(createOwnerValidator, owner, "CreateBusiness");
        if(!validationResult.IsSuccess)
            return Result<OwnerDto>.Failure(validationResult.Error);

        var createUserResult = await authService.RegisterAsync(owner.OwnerToRegisterDto());
        if (!createUserResult.IsSuccess)
            return Result<OwnerDto>.Failure(createUserResult.Error);
        
        var newOwner = owner.ToEntity();
        newOwner.UserId = createUserResult.Value.Id;
        await unitOfWork.OwnerRepository.AddAsync(newOwner);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<OwnerDto>.Failure(ErrorMessages.InternalServerError);
        
        await authService.AddToRoleAsync("Owner", createUserResult.Value, null);
        
        return Result<OwnerDto>.Success(newOwner.ToCreatedDto());
    }

    public async Task<Result<OwnerDto>> UpdateAsync(OwnerDto owner)
    {
        var validationResult = await ValidationHelper.ValidateAndReportAsync(ownerValidator,
            owner,
            ctx => { ctx.RootContextData["ownerId"] = owner.HiddenId; },
            "Business");
        if (!validationResult.IsSuccess)
            return Result<OwnerDto>.Failure(validationResult.Error);
        
        var ownerEntity = await unitOfWork.OwnerRepository.FindByIdAsync((int)owner.HiddenId);
        ownerEntity.UpdateEntityFromDto(owner);
        
        unitOfWork.OwnerRepository.UpdateAsync(ownerEntity);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<OwnerDto>.Failure(ErrorMessages.InternalServerError);
        return Result<OwnerDto>.Success(owner);
    }

    public async Task<Result> DeleteAsync(OwnerDto owner)
    {
        var ownerEntity = await unitOfWork.OwnerRepository.GetByUserIdAsync(owner.Id);
        if (ownerEntity == null)
            return Result.Failure(new Error("NotFound", "Owner not found"));
        
        unitOfWork.OwnerRepository.DeleteAsync(ownerEntity);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result.Failure(ErrorMessages.InternalServerError);
        return Result.Success();
    }
    
    public async Task<Result> AddPokemonToOwnerAsync(string ownerId, int pokemonId)
    {
        var id = await unitOfWork.OwnerRepository.GetOwnerIdByUserIdAsync(ownerId);
        if (id <= 0)
            return Result.Failure(ErrorMessages.NotFound);
        
        var pokemonOwner = new PokemonOwner { OwnerId = id, PokemonId = pokemonId };
        var validationResult = await ValidationHelper.ValidateAndReportAsync(pokemonOwnerValidator, pokemonOwner, "Input,CreateBusiness");
        if (!validationResult.IsSuccess)
        {
            return Result.Failure(validationResult.Error);
        }
        
        await unitOfWork.PokemonOwnerRepository.AddAsync(pokemonOwner);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result.Failure(ErrorMessages.InternalServerError);
        return Result.Success();
    }
    
    public async Task<Result> RemovePokemonFromOwnerAsync(string ownerId, int pokemonId)
    {
        var id = await unitOfWork.OwnerRepository.GetOwnerIdByUserIdAsync(ownerId);
        if (id <= 0)
            return Result.Failure(ErrorMessages.NotFound);
        
        var pokemonOwner = new PokemonOwner { OwnerId = id, PokemonId = pokemonId };
        var validationResult = await ValidationHelper.ValidateAndReportAsync(pokemonOwnerValidator, pokemonOwner, "Input,DeleteBusiness");
        if (!validationResult.IsSuccess)
        {
            return Result.Failure(validationResult.Error);
        }
        
        unitOfWork.PokemonOwnerRepository.DeleteAsync(pokemonOwner);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result.Failure(ErrorMessages.InternalServerError);
        return Result.Success();
    }
    
    public async Task<Result> ValidateOwnerAuthentication(string ownerId)
    {
        // if (userContext.IsAdmin)
        //     return Result.Success();
        //
        // var isValid = await unitOfWork.OwnerRepository.OwnerIdIsUserIdAsync(ownerId, userContext.Id.ToString());
        //
        // if (!isValid)
        //     return Result.Failure(ErrorMessages.Unauthorized);
        return Result.Success();
    }
}
