using MiApiMinimal.Models;

namespace ApiRestMinimal.Contracts.DTOs;

public class CategoryDTOs
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public virtual ICollection<Article>? Articles { get; init; }
}