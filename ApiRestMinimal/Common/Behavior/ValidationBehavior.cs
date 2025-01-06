using FluentValidation;

namespace ApiRestMinimal.Common.Behavior;

public static class ValidationBehavior
{
    public static IResult ValidateRequest<T>(T request, IValidator<T> validator)
    {
        var validationResult = validator.Validate(request);
        
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray()
                );

            return Results.ValidationProblem(errors);
        }

        return null;
    }
}