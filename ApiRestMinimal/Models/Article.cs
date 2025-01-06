using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApiMinimal.Models;

public class Article
{
    public Guid Id { get; init; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    [Required] public Guid CategoryId { get; set; }
    [ForeignKey("CategoryId")] public virtual Category? Category { get; init; }
}