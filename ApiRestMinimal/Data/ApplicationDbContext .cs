using ApiRestMinimal.Models;

using Microsoft.EntityFrameworkCore;

namespace ApiRestMinimal.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; } 
    public DbSet<Category> Categories { get; set; } 


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>()
            .HasOne(a => a.Category)
            .WithMany(c => c.Articles)
            .HasForeignKey(a => a.CategoryId);

        base.OnModelCreating(modelBuilder);
    }
}