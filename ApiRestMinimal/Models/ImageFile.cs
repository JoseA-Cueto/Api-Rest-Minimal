using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MiApiMinimal.Models;

namespace ApiRestMinimal.Models
{
    public class ImageFile
    {
        
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ContentType { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public string PhysicalPath { get; set; }
        public int Size { get; set; }
        public DateTime CreateDate { get; set; }
        public int ArticleId { get; set; }
        [ForeignKey("ArticleId")]
        public Article Article { get; set; }
    }
}
