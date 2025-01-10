using ApiRestMinimal.Common.Interfaces.ImageFile;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestMinimal.Endpoints.ImageFiles
{
    public static class ImageEndpoints
    {
        public static void MapImageEndpoints(this IEndpointRouteBuilder app)
        {
            // Endpoint para subir una imagen
            app.MapPost("/api/articles/{articleId}/upload-image", async (Guid articleId, IFormFile file, IImageService imageService, IWebHostEnvironment env) =>
            {
                try
                {
                    var imageDto = await imageService.UploadImageAsync(articleId, file, env);
                    return Results.Ok(new { Message = "Image uploaded successfully", Image = imageDto });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { Message = ex.Message });
                }
            })
            .DisableAntiforgery();

            // Endpoint para obtener la imagen asociada a un artículo
            app.MapGet("/api/articles/{articleId}/image", async (Guid articleId, IImageService imageService) =>
            {
                try
                {
                    var imageDto = await imageService.GetImageByArticleIdAsync(articleId);
                    return Results.Ok(imageDto);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(new { Message = ex.Message });
                }
            });

            // Endpoint para eliminar una imagen
            app.MapDelete("/api/images/{imageId}", async (Guid imageId, IImageService imageService) =>
            {
                try
                {
                    await imageService.DeleteImageAsync(imageId);
                    return Results.Ok(new { Message = "Image deleted successfully" });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { Message = ex.Message });
                }
            });
        }
    }

}
