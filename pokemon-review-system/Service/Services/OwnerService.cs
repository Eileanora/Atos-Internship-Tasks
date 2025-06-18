using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Internal;
using Service.Common.Constants;
using Service.Interfaces;
using Service.Mappers;
using Shared.DTOs;
using Shared.ErrorAndResults;
using Shared.Helpers;
using Shared.ResourceParameters;

namespace Service.Services;

public class OwnerService(
    IUnitOfWork unitOfWork,
    IValidator<OwnerDto> ownerValidator,
    IValidator<CreateOwnerDto> createOwnerValidator,
    IValidator<Domain.Models.PokemonOwner> pokemonOwnerValidator,
    IAuthManager authManager,
    IUserContext userContext)
    : IOwnerManager
{
    public async Task<Result<PagedList<OwnerDto>>> GetAllAsync(OwnerResourceParameters resourceParameters)
    {
        var owners = await unitOfWork.OwnerRepository.GetAllAsync(resourceParameters);
        return Result<PagedList<OwnerDto>>.Success(owners.ToListDto());
    }

    public async Task<Result<OwnerDto>> GetByIdAsync(int id)
    {
        var owner = await unitOfWork.OwnerRepository.GetByIdAsyncWithInclude(id);
        if (owner == null)
            return Result<OwnerDto>.Failure(ErrorMessages.NotFound);
        return Result<OwnerDto>.Success(owner.ToDetailDto());
    }

    public async Task<Result<int>> AddAsync(CreateOwnerDto owner)
    {
        var validationResult = await ValidationHelper.ValidateAndReportAsync(createOwnerValidator, owner, "CreateBusiness");
        if(!validationResult.IsSuccess)
            return Result<int>.Failure(validationResult.Error);

        var createUserResult = await authManager.RegisterAsync(owner.OwnerToRegisterDto());
        if (!createUserResult.IsSuccess)
            return Result<int>.Failure(createUserResult.Error);
        
        var newOwner = owner.ToEntity();
        newOwner.UserId = createUserResult.Value.Id;
        await unitOfWork.OwnerRepository.AddAsync(newOwner);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<int>.Failure(ErrorMessages.InternalServerError);
        
        await authManager.AddToRoleAsync("Owner", createUserResult.Value, null);
        
        return Result<int>.Success(newOwner.Id);
    }

    public async Task<Result<OwnerDto>> UpdateAsync(OwnerDto owner)
    {
        var context = new ValidationContext<OwnerDto>(
            owner,
            new PropertyChain(),
            new RulesetValidatorSelector(new[] { "Business" }))
        {
            RootContextData = { ["ownerId"] = owner.Id }
        };
        var validationResult = await ownerValidator.ValidateAsync(context);
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
            return Result<OwnerDto>.Failure(new Error("ValidationError", errorMessage));
        }
        
        var ownerEntity = await unitOfWork.OwnerRepository.FindByIdAsync((int)owner.Id);
        ownerEntity.UpdateEntityFromDto(owner);
        
        unitOfWork.OwnerRepository.UpdateAsync(ownerEntity);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<OwnerDto>.Failure(ErrorMessages.InternalServerError);
        return Result<OwnerDto>.Success(owner);
    }

    public async Task<Result> DeleteAsync(OwnerDto owner)
    {
        var ownerEntity = await unitOfWork.OwnerRepository.FindByIdAsync((int)owner.Id);
        if (ownerEntity == null)
            return Result.Failure(new Error("NotFound", "Owner not found"));
        unitOfWork.OwnerRepository.DeleteAsync(ownerEntity);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result.Failure(ErrorMessages.InternalServerError);
        return Result.Success();
    }
    
    public async Task<Result> AddPokemonToOwnerAsync(int ownerId, int pokemonId)
    {
        var pokemonOwner = new Domain.Models.PokemonOwner { OwnerId = ownerId, PokemonId = pokemonId };
        var validationResult = await pokemonOwnerValidator.ValidateAsync(pokemonOwner, options => options.IncludeRuleSets("Input", "CreateBusiness"));
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
            return Result.Failure(new Error("ValidationError", errorMessage));
        }
        
        await unitOfWork.PokemonOwnerRepository.AddAsync(pokemonOwner);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result.Failure(ErrorMessages.InternalServerError);
        return Result.Success();
    }
    
    public async Task<Result> RemovePokemonFromOwnerAsync(int ownerId, int pokemonId)
    {
        var pokemonOwner = new Domain.Models.PokemonOwner { OwnerId = ownerId, PokemonId = pokemonId };
        var validationResult = await pokemonOwnerValidator.ValidateAsync(pokemonOwner, options => options.IncludeRuleSets("Input", "DeleteBusiness"));
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
            return Result.Failure(new Error("ValidationError", errorMessage));
        }
        
        unitOfWork.PokemonOwnerRepository.DeleteAsync(pokemonOwner);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result.Failure(ErrorMessages.InternalServerError);
        return Result.Success();
    }
    
    public async Task<Result> ValidateOwnerAuthentication(int ownerId)
    {
        if (userContext.IsAdmin)
            return Result.Success();

        var isValid = await unitOfWork.OwnerRepository.OwnerIdIsUserIdAsync(ownerId, userContext.UserId.ToString());
        
        if (!isValid)
            return Result.Failure(ErrorMessages.Unauthorized);
        return Result.Success();
    }
}
