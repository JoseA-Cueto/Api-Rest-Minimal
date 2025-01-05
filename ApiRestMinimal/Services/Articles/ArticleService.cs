using ApiRestMinimal.Common.Interfaces.Articles;
using ApiRestMinimal.Contracts.DTOs;
using ApiRestMinimal.Services.Interfaces;
using AutoMapper;
using MiApiMinimal.Models;

namespace ApiRestMinimal.Services.Articles;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public ArticleService(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<List<ArticleDTOs>> GetAllArticlesAsync()
    {
        var articles = await _articleRepository.GetAllAsync();
        return _mapper.Map<List<ArticleDTOs>>(articles);
    }

    public async Task<ArticleDTOs> GetArticleByIdAsync(Guid id)
    {
        var article = await _articleRepository.GetByIdAsync(id);
        return _mapper.Map<ArticleDTOs>(article);
    }

    public async Task CreateArticleAsync(ArticleDTOs articleDto)
    {
        var article = _mapper.Map<Article>(articleDto);
        await _articleRepository.AddAsync(article);
    }

    public async Task UpdateArticleAsync(ArticleDTOs articleDto)
    {
        var article = _mapper.Map<Article>(articleDto);
        await _articleRepository.UpdateAsync(article);
    }

    public async Task DeleteArticleAsync(Guid id)
    {
        await _articleRepository.DeleteAsync(id);
    }
}