using System.ComponentModel.DataAnnotations;

namespace MiApiMinimal.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } 
        public virtual ICollection<Article> Articles { get; set; }
    }
}

