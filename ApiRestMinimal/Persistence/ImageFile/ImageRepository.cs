using ApiRestMinimal.Common.Interfaces.ImageFile;
using ApiRestMinimal.Data;
using ApiRestMinimal.Models;
using ApiRestMinimal.Persistence.Base;
using Microsoft.EntityFrameworkCore;

namespace ApiRestMinimal.Persistence.ImageFile
{
    public class ImageRepository : RepositoryBase<Image>, IImageRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ImageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Image> GetImageByArticleIdAsync(Guid articleId)
        {
            return await _dbContext.Images
                .FirstOrDefaultAsync(i => i.ArticleId == articleId);    
        }

    }
}
