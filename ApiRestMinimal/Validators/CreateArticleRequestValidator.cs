using ApiRestMinimal.Contracts.Requests;
using FluentValidation;

namespace ApiRestMinimal.Validators;

public class CreateArticleRequestValidator : AbstractValidator<CreateArticleRequest>
{
    public CreateArticleRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Content).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}