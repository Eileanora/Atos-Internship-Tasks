using System.Runtime.CompilerServices;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Shared.ErrorAndResults;

namespace WebApi.Helpers.Validation
{
    public static class PatchHelper
    {
        public static (TDto PatchedDto, Result ValidationResult) HandlePatch<TDto>(
            this ControllerBase controller,
            TDto originalDto,
            JsonPatchDocument<TDto> patchDoc)
            where TDto : BaseDto, new()
        {
            patchDoc.ApplyTo(originalDto, controller.ModelState);
            if (!controller.ModelState.IsValid)
            {
                return (originalDto, Result.Failure(Service.Common.Constants.ErrorMessages.ValidationError));
            }
            // var validationResult = await validator.ValidateAsync(originalDto, options => options.IncludeRuleSets(ruleSet));
            // if (!validationResult.IsValid)
            //     return (originalDto, Result.Failure(Service.Common.Constants.ErrorMessages.ValidationError));
            return (originalDto, Result.Success());
        }
    }
}
