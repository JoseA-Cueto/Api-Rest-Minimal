using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApiMinimal.Models;

public class Article
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    [Required] public Guid CategoryId { get; set; }
    [ForeignKey("CategoryId")] public virtual Category Category { get; set; }
}