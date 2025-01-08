using ApiRestMinimal.Common.Interfaces.Articles;
using ApiRestMinimal.Data;
using ApiRestMinimal.Models;
using ApiRestMinimal.Persistence.Base;


namespace ApiRestMinimal.Persistence.Articles;

public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
{
    public ArticleRepository(ApplicationDbContext context) : base(context)
    {
    }
}