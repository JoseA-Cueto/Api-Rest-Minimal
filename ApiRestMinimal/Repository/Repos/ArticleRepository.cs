
using ApiRestMinimal.Repository.Interfaces;
using MiApiMinimal.Data;
using MiApiMinimal.Models;

public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
{
    public ArticleRepository(ApplicationDbContext context) : base(context) { }
}
