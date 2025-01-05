using ApiRestMinimal.Common.Interfaces.Articles;
using ApiRestMinimal.Persistence.Articles;
using ApiRestMinimal.Services.Articles;

namespace ApiRestMinimal.Common.Extensions;

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