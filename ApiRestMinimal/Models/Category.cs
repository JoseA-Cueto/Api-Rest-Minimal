namespace ApiRestMinimal.Models;

public class Category
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public virtual ICollection<Article>? Articles { get; set; }
}