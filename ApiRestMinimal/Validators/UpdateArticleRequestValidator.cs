using ApiRestMinimal.Contracts.Requests;
using FluentValidation;

namespace ApiRestMinimal.Validators;

public class UpdateArticleRequestValidator : AbstractValidator<UpdateArticleRequest>
{
    public UpdateArticleRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Content).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}