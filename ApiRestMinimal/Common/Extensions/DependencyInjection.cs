using ApiRestMinimal.Common.Interfaces.Articles;
using ApiRestMinimal.Common.Interfaces.ImageFile;
using ApiRestMinimal.Persistence.Articles;
using ApiRestMinimal.Persistence.ImageFile;
using ApiRestMinimal.Services.Articles;

namespace ApiRestMinimal.Common.Extensions;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Repositorios
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        // Servicios
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IImageService, ImageService>();
    }
}