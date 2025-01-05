using MiApiMinimal.Models;

namespace ApiRestMinimal.DTOs;

public class CategoryDTOs
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Article> Articles { get; set; }
}