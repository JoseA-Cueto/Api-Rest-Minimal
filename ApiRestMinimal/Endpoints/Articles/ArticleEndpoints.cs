using ApiRestMinimal.Common.Interfaces.Articles;
using ApiRestMinimal.Contracts.Requests;

namespace ApiRestMinimal.Endpoints.Articles;

public static class ArticleEndpoints
{
    public static void MapArticleEndpoints(this IEndpointRouteBuilder app)
    {
        // GET all articles
        app.MapGet("/api/articles", async (IArticleService articleService) =>
        {
            var articles = await articleService.GetAllArticlesAsync();
            return Results.Ok(articles);
        });

        // GET an article by ID
        app.MapGet("/api/articles/{id:guid}", async (Guid id, IArticleService articleService) =>
        {
            var article = await articleService.GetArticleByIdAsync(id);
            if (article is null)
                return Results.NotFound(new { Message = "Article not found" });

            return Results.Ok(article);
        });

        // POST a new article
        app.MapPost("/api/articles", async (CreateArticleRequest articleDto, IArticleService articleService) =>
        {
            if (string.IsNullOrWhiteSpace(articleDto.Title) || string.IsNullOrWhiteSpace(articleDto.Content))
                return Results.BadRequest(new { Message = "Title and Content cannot be empty." });

            await articleService.CreateArticleAsync(articleDto);
            return Results.Created($"/api/articles/{articleDto.Id}", articleDto);
        });

        // PUT (update) an article
        app.MapPut("/api/articles/{id:guid}",
            async (Guid id, UpdateArticleRequest updatedArticleDto, IArticleService articleService) =>
            {
                if (string.IsNullOrWhiteSpace(updatedArticleDto.Title) ||
                    string.IsNullOrWhiteSpace(updatedArticleDto.Content))
                    return Results.BadRequest(new { Message = "Title and Content cannot be empty." });

                await articleService.UpdateArticleAsync(id, updatedArticleDto);

                return Results.Ok(updatedArticleDto);
            });

        // DELETE an article
        app.MapDelete("/api/articles/{id:guid}", async (Guid id, IArticleService articleService) =>
        {
            var article = await articleService.GetArticleByIdAsync(id);
            if (article is null)
                return Results.NotFound(new { Message = "Article not found" });

            await articleService.DeleteArticleAsync(id);
            return Results.Ok(new { Message = "Article deleted successfully" });
        });
    }
}