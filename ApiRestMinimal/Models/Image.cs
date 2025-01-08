using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRestMinimal.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        [Required]
        public Guid ArticleId { get; set; }
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }
    }
}
