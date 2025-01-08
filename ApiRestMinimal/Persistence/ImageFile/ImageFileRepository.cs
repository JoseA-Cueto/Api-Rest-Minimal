using ApiRestMinimal.Common.Interfaces.Articles;
using ApiRestMinimal.Data;
using ApiRestMinimal.Models;
using ApiRestMinimal.Persistence.Base;
using MiApiMinimal.Models;

namespace ApiRestMinimal.Persistence.ImageFile
{
    public class ImageFileRepository : RepositoryBase<ImageFile>, IImageFileRepository
    {
        public ImageFileRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
