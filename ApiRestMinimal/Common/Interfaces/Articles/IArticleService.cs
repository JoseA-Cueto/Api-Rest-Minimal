using ApiRestMinimal.Contracts.DTOs;
using ApiRestMinimal.Contracts.Requests;

namespace ApiRestMinimal.Common.Interfaces.Articles;

public interface IArticleService
{
    Task<List<ArticleDTOs>> GetAllArticlesAsync();
    Task<ArticleDTOs> GetArticleByIdAsync(Guid id);
    Task CreateArticleAsync(CreateArticleRequest article);
    Task UpdateArticleAsync(Guid Id, UpdateArticleRequest article);
    Task DeleteArticleAsync(Guid id);
}