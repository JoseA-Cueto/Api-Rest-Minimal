using ApiRestMinimal.Common.Interfaces.BaseRepo;
using ApiRestMinimal.Models;

namespace ApiRestMinimal.Common.Interfaces.ImageFile
{
    public interface IImageRepository : IRepositoryBase<Image>
    {
        Task<Image> GetImageByArticleIdAsync(Guid articleId);
    }
}
