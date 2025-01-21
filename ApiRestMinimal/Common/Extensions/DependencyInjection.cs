using ApiRestMinimal.Common.Interfaces.Articles;
using ApiRestMinimal.Common.Interfaces.ImageFile;
using ApiRestMinimal.Common.Interfaces.Users;
using ApiRestMinimal.Persistence.Articles;
using ApiRestMinimal.Persistence.ImageFile;
using ApiRestMinimal.Persistence.Users;
using ApiRestMinimal.Services.Articles;
using ApiRestMinimal.Services.Users;

namespace ApiRestMinimal.Common.Extensions;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Repositorios
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        // Servicios
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IUserService, UserService>();

    }
}