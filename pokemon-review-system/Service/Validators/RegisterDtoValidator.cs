using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Common.Constants;
using Service.DTOs;

namespace Service.Validators;

public class RegisterDtoValidator<T> : AbstractValidator<T> where T : RegisterDto
{
    public RegisterDtoValidator(UserManager<User> userManager)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        // ClassLevelCascadeMode = CascadeMode.Stop;
        
        RuleSet("Input", () =>
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(RegisterDto.Email)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(RegisterDto.Email)))
                .EmailAddress().WithMessage(AuthValidationMessages.InvalidEmail);

            RuleFor(x => x.Password)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull,
                    nameof(RegisterDto.Password)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty,
                    nameof(RegisterDto.Password)))
                .MinimumLength(6).WithMessage(string.Format(AuthValidationMessages.InvalidLength, 6))
                .Matches(@"[A-Z]").WithMessage(AuthValidationMessages.UpperCaseRequired)
                .Matches(@"[0-9]").WithMessage(AuthValidationMessages.DigitRequired)
                .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage(AuthValidationMessages.SpecialCharacterRequired);
            
            RuleFor(x => x.ConfirmPassword)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(RegisterDto.ConfirmPassword)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(RegisterDto.ConfirmPassword)))
                .Equal(x => x.Password).WithMessage(AuthValidationMessages.PasswordsDoNotMatch);
        });
        
        RuleSet("CreateBusiness", () =>
        {
            RuleFor(x => x.Email)
                .MustAsync(async (email, cancellation) =>
                {
                    return !await userManager.Users.AnyAsync(u => u.Email == email, cancellation);
                }).WithMessage(AuthValidationMessages.UserAlreadyExists);
        });

    }
}
