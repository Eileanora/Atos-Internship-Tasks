using FluentValidation;
using Shared.ErrorAndResults;

namespace Shared.Helpers;

public static class ValidationHelper
{
    public static async Task<Result> ValidateAndReportAsync<T>(IValidator<T> validator, T dto, string ruleSet = "Input")
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
}



