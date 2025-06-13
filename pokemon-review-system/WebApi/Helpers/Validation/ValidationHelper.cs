using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Service.Common.ErrorAndResults;
using WebApi.Helpers.Extensions;

namespace WebApi.Helpers.Validation;

public static class ValidationHelper
{
    public static async Task<Result> ValidateAsync<T>(IValidator<T> validator, T dto, string ruleSet = "Input")
    {
        var validationResult = await validator.ValidateAsync(
            dto,
            options => options.IncludeRuleSets(ruleSet)
        );
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Result.Failure(new Error("ValidationError", errorMessage));
        }
        return Result.Success();
    }

    public static async Task<IActionResult?> ValidateAndReportAsync<T>(this ControllerBase controller, IValidator<T> validator, T dto, string ruleSet = "Input")
    {
        var validationResult = await validator.ValidateAsync(
            dto,
            options => options.IncludeRuleSets(ruleSet)
        );
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(controller.ModelState);
            return controller.ValidationProblem(controller.ModelState);
        }
        return null;
    }
}



