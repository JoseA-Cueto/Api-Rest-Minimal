namespace MiApiMinimal.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Article> Articles { get; set; } = new();
    }
}

