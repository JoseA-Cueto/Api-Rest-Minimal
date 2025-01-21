using ApiRestMinimal.Common.Behavior;
using ApiRestMinimal.Common.Interfaces.Articles;
using ApiRestMinimal.Contracts.Requests;
using ApiRestMinimal.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace ApiRestMinimal.Endpoints.Articles;

public static class ArticleEndpoints
{
    public static void MapArticleEndpoints(this IEndpointRouteBuilder app)
    {
        // GET all articles
        app.MapGet("/api/articles",  async (IArticleService articleService) =>
        {
            var articles = await articleService.GetAllArticlesAsync();
            return Results.Ok(articles);
        });

        // GET an article by ID
        app.MapGet("/api/articles/{id:guid}",  async (Guid id, IArticleService articleService) =>
        {
            var article = await articleService.GetArticleByIdAsync(id);
            
            if (article is null)
                return Results.NotFound(new { Message = "Article not found" });

            return Results.Ok(article);
        });

        // POST a new article
        app.MapPost("/api/articles",  async (
            CreateArticleRequest createArticleRquest, 
            IArticleService articleService,
            IValidator<CreateArticleRequest> validator) =>
        {
            var validationResult = ValidationBehavior.ValidateRequest<CreateArticleRequest>(createArticleRquest, validator);
            
            if (validationResult != null)
                return validationResult;
            
            await articleService.CreateArticleAsync(createArticleRquest);
            return Results.Created($"/api/articles/{createArticleRquest.Id}", createArticleRquest);
        });

        // PUT (update) an article
        app.MapPut("/api/articles/{id:guid}",  async (
            Guid id, 
            UpdateArticleRequest UpdateArticleRequest, 
            IArticleService articleService,
            IValidator<UpdateArticleRequest> validator) =>
            {
                var validationResult = ValidationBehavior.ValidateRequest<UpdateArticleRequest>(UpdateArticleRequest, validator);
            
                if (validationResult != null)
                    return validationResult;
                
                await articleService.UpdateArticleAsync(id, UpdateArticleRequest);
                return Results.Ok(UpdateArticleRequest);
            });

        // DELETE an article
        app.MapDelete("/api/articles/{id:guid}",  async (Guid id, IArticleService articleService) =>
        {
            var article = await articleService.GetArticleByIdAsync(id);
            if (article is null)
                return Results.NotFound(new { Message = "Article not found" });

            await articleService.DeleteArticleAsync(id);
            return Results.Ok(new { Message = "Article deleted successfully" });
        });
    }
}