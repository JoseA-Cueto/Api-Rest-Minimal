using ApiRestMinimal.DTOs;
using AutoMapper;
using MiApiMinimal.Models;

namespace MiApiMinimal.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
          
            CreateMap<Article, ArticleDTOs>().ReverseMap();            
            CreateMap<Category, CategoryDTOs>().ReverseMap();
        }
    }
}
