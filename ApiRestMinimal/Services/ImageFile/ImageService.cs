using ApiRestMinimal.Common.Interfaces.Articles;
using ApiRestMinimal.Common.Interfaces.ImageFile;
using ApiRestMinimal.Contracts.DTOs;
using ApiRestMinimal.Models;

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly IArticleRepository _articleRepository;

    public ImageService(IImageRepository imageRepository, IArticleRepository articleRepository)
    {
        _imageRepository = imageRepository;
        _articleRepository = articleRepository;
    }

    // Método para subir imagen
    public async Task<ImageDTOs> UploadImageAsync(Guid articleId, IFormFile file, IWebHostEnvironment env)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("No file uploaded.");

        var article = await _articleRepository.GetByIdAsync(articleId);
        if (article == null)
            throw new ArgumentException("Article not found.");

        // Crear la carpeta 'UploadedImages' si no existe
        var uploadsDirectory = Path.Combine(env.ContentRootPath, "UploadedImages");

        if (!Directory.Exists(uploadsDirectory))
        {
            Directory.CreateDirectory(uploadsDirectory);
        }

        var filePath = Path.Combine(uploadsDirectory, Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var image = new Image
        {
            Id = Guid.NewGuid(),
            FileName = file.FileName,
            FilePath = filePath,
            ArticleId = articleId
        };

        await _imageRepository.AddAsync(image);

        // Mapear a DTO
        var imageDto = new ImageDTOs
        {
            FileName = image.FileName,
            FilePath = image.FilePath,
            ArticleId = image.ArticleId
        };

        return imageDto;
    }

    // Método para obtener la imagen por ID de artículo
    public async Task<ImageDTOs> GetImageByArticleIdAsync(Guid articleId)
    {
        var image = await _imageRepository.GetImageByArticleIdAsync(articleId);
        if (image == null)
            throw new ArgumentException("Image not found for the given article.");

        return new ImageDTOs
        {
            FileName = image.FileName,
            FilePath = image.FilePath,
            ArticleId = image.ArticleId
        };
    }

    // Método para borrar la imagen
    public async Task DeleteImageAsync(Guid imageId)
    {
        var image = await _imageRepository.GetByIdAsync(imageId);
        if (image == null)
            throw new ArgumentException("Image not found.");

        File.Delete(image.FilePath);  // Eliminar el archivo del sistema

        await _imageRepository.DeleteAsync(imageId);  // Eliminar la imagen de la base de datos
    }

   
}
