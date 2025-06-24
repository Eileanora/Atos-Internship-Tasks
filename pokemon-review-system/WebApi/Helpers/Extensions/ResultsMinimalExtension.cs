using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Constants;
using Shared.ErrorAndResults;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace WebApi.Helpers.Extensions;

public static class ResultsMinimalExtension
{
    public static IResult ToResults(this Result result)
    {
        if (result.IsSuccess)
            return  Results.Ok();
        
        return result.Error.MapErrorResultMinimal();
    } 
    public static IResult ToResults<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return Results.Ok(result.Value);
        return result.Error.MapErrorResultMinimal();
    }
    private static IResult MapErrorResultMinimal(this Error error)
    {
        return error.Code switch
        {
            nameof(ErrorMessages.NotFound) or "NotFound" => Results.NotFound(error),
            nameof(ErrorMessages.Unauthorized) or "Unauthorized" => Results.Unauthorized(),
            nameof(ErrorMessages.InvalidInput) or "InvalidInput" => Results.BadRequest(error),
            nameof(ErrorMessages.ValidationError) or "ValidationError" =>
                error.Description == CommonValidationErrorMessages.ResourceNotFound
                    ? Results.NotFound(error)
                    : Results.BadRequest(error),
            nameof(ErrorMessages.InternalServerError) or "InternalServerError" => Results.Problem(error.Description, statusCode: 500),
            nameof(ErrorMessages.WrongCredentials) or "WrongCredentials" => Results.Unauthorized(),
            nameof(ErrorMessages.CannotGenerateToken) or "CannotGenerateToken" => Results.Problem(error.Description, statusCode: 500),
            nameof(ErrorMessages.InvalidRefreshToken) or "InvalidRefreshToken" => Results.Unauthorized(),
            _ => Results.Problem(error.Description, statusCode: 500)
        };
    }
}
