using Microsoft.EntityFrameworkCore;
using MiApiMinimal.Models;

namespace MiApiMinimal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
    }
}
