using ApiRestMinimal.DTOs;

namespace ApiRestMinimal.Services.Interfaces;

public interface IArticleService
{
    Task<List<ArticleDTOs>> GetAllArticlesAsync();
    Task<ArticleDTOs> GetArticleByIdAsync(Guid id);
    Task CreateArticleAsync(ArticleDTOs article);
    Task UpdateArticleAsync(ArticleDTOs article);
    Task DeleteArticleAsync(Guid id);
}