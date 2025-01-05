using ApiRestMinimal.Repository;
using ApiRestMinimal.Repository.Interfaces;
using ApiRestMinimal.Services.Interfaces;
using ApiRestMinimal.Services.Serv;
using Microsoft.Extensions.DependencyInjection;
using MiApiMinimal.Models;

namespace ApiRestMinimal.Extensions
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Repositorios
            services.AddScoped<IArticleRepository, ArticleRepository>();


            // Servicios
            services.AddScoped<IArticleService, ArticleService>();
  

           
        }
    }
}
