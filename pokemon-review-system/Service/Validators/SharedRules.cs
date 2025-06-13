using FluentValidation;

namespace Service.Validators;

public static class SharedRules 
{
    public static IRuleBuilderOptions<T, string> FullNameValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Full name is required.")
            .Length(2, 50).WithMessage("Full name must be more than 2 and less than 50 characters.");
    }
    
    public static IRuleBuilderOptions<T, string> FirstNameValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("First name is required.")
            .Length(2, 50).WithMessage("First name must be more than 2 and less than 50 characters.");
    }
    
    public static IRuleBuilderOptions<T, string> LastNameValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Last name is required.")
            .Length(2, 50).WithMessage("Last name must be more than 2 and less than 50 characters.");
    }
    
    public static IRuleBuilderOptions<T, string> BiographyValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Biography is required.")
            .Length(2, 500).WithMessage("Biography must be more than 2 and less than 500 characters.");
    }
}