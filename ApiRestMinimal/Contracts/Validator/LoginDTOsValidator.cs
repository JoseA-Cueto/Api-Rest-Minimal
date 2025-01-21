using FluentValidation;
using ApiRestMinimal.Contracts.DTOs;

public class LoginDTOsValidator : AbstractValidator<LoginDTOs>
{
    public LoginDTOsValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email format");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
    }
}

