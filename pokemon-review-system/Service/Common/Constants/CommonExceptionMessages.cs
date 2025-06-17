namespace Service.Common.Constants;

using Shared.ErrorAndResults;

public static class ErrorMessages
{
    public static readonly Error NotFound = new("NotFound", "The requested resource was not found.");
    public static readonly Error InvalidInput = new("InvalidInput", "The input provided is invalid.");
    public static readonly Error Unauthorized = new("Unauthorized", "You are not authorized to perform this action.");
    public static readonly Error InternalServerError = new("InternalServerError", "An internal server error occurred. Please try again later.");
    public static readonly Error ValidationError = new("ValidationError", "There was a validation error with the provided data.");
    
    // auth
    
}
