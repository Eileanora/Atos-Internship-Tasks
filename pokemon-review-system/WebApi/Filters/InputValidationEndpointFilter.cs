using FluentValidation;
using Shared.Helpers;
using WebApi.Helpers.Extensions;

namespace WebApi.Filters;

public class InputValidationEndpointFilter<TModel>
    (IValidator<TModel> validator) 
    : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var model = context.GetArgument<TModel>(0);
        
        if (context.HttpContext.Request.Method is not ("POST" or "PUT"))
            return next(context);

        var validationResult = await ValidationHelper
            .ValidateAndReportAsync(validator, model, "input");
  

        if (!validationResult.IsSuccess)
            return validationResult.ToResults();
        
        return next(context);
        
    }
}
