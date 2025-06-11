namespace Service.Helpers.Constants;

public static class ErrorMessages
{
    public const string NotFound = "The requested resource was not found.";
    public const string InvalidInput = "The input provided is invalid.";
    public const string Unauthorized = "You are not authorized to perform this action.";
    public const string InternalServerError = "An internal server error occurred. Please try again later.";
    public const string ValidationError = "There was a validation error with the provided data.";
    
    // put them in a common array
    public static readonly string[] CommonErrorMessages = 
    {
        NotFound,
        InvalidInput,
        Unauthorized,
        InternalServerError,
        ValidationError
    };
}
