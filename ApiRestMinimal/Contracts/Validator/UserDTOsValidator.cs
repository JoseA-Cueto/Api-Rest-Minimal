using FluentValidation;
using ApiRestMinimal.Contracts.DTOs;

public class UserDTOsValidator : AbstractValidator<UserDTOs>
{
    public UserDTOsValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email format");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
    }
}
