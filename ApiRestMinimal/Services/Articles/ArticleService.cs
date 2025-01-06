using ApiRestMinimal.Common.Exceptions;
using ApiRestMinimal.Common.Interfaces.Articles;
using ApiRestMinimal.Contracts.DTOs;
using ApiRestMinimal.Contracts.Requests;
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

    public async Task DeleteArticleAsync(Guid id)
    {
        await _articleRepository.DeleteAsync(id);
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

    public async Task CreateArticleAsync(CreateArticleRequest articleDto)
    {
        var article = _mapper.Map<Article>(articleDto);
        await _articleRepository.AddAsync(article);
    }

    public async Task UpdateArticleAsync(Guid Id, UpdateArticleRequest articleDto)
    {
        var existingArticle = await _articleRepository.GetByIdAsync(Id);
        
        if (existingArticle is null)
            throw new NotFoundException($"the article with Id: {Id} doesn't exist");
        
        existingArticle.Title = articleDto.Title;
        existingArticle.Content = articleDto.Content;
        existingArticle.CategoryId = articleDto.CategoryId;
        
        await _articleRepository.UpdateAsync(existingArticle);
    }
}