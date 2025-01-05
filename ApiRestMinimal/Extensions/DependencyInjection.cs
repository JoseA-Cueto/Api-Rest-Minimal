using ApiRestMinimal.Repository.Interfaces;
using ApiRestMinimal.Services.Interfaces;
using ApiRestMinimal.Services.Serv;

namespace ApiRestMinimal.Extensions;

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