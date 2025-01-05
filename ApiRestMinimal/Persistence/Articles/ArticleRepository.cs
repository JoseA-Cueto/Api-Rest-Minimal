using ApiRestMinimal.Common.Interfaces.Articles;
using ApiRestMinimal.Persistence.Base;
using MiApiMinimal.Data;
using MiApiMinimal.Models;

namespace ApiRestMinimal.Persistence.Articles;

public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
{
    public ArticleRepository(ApplicationDbContext context) : base(context)
    {
    }
}