using ApiRestMinimal.Contracts.DTOs;
using ApiRestMinimal.Contracts.Requests;
using ApiRestMinimal.Contracts.Responses;
using ApiRestMinimal.Models;
using AutoMapper;


namespace ApiRestMinimal.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Article, ArticleDTOs>().ReverseMap();
        CreateMap<Category, CategoryDTOs>().ReverseMap();
        CreateMap<Article, CreateArticleRequest>().ReverseMap();
        CreateMap<Article, UpdateArticleRequest>().ReverseMap();
        CreateMap<Article, ArticleResponse>().ReverseMap();
        CreateMap<Image, ImageDTOs>().ReverseMap();

    }
}