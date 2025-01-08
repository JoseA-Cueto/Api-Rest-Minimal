using ApiRestMinimal.Contracts.DTOs;

namespace ApiRestMinimal.Common.Interfaces.ImageFile
{
    public interface IImageService
    {
        Task<ImageDTOs> UploadImageAsync(Guid articleId, IFormFile file, IWebHostEnvironment env);
        Task<ImageDTOs> GetImageByArticleIdAsync(Guid articleId);
        Task DeleteImageAsync(Guid imageId);
    }
}
