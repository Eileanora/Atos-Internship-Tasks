using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Helpers.Validation
{
    public static class PatchHelper
    {
        public static async Task<IActionResult?> HandlePatchAsync<TDto>(
            this ControllerBase controller,
            IValidator<TDto> validator,
            TDto originalDto,
            JsonPatchDocument<TDto> patchDoc,
            string ruleSet = "Input")
            where TDto : class, new()
        {
            patchDoc.ApplyTo(originalDto, controller.ModelState);
            var validationProblem = await controller.ValidateAndReportAsync(validator, originalDto, ruleSet);
            if (validationProblem != null)
            {
                return validationProblem;
            }
            return null;
        }
    }
}
