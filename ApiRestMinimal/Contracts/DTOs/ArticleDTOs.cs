namespace ApiRestMinimal.Contracts.DTOs;

public class ArticleDTOs
{
    public Guid Id { get; init; }
    public string Title { get; init; } = default!;
    public string Content { get; init; } = default!;
    public Guid CategoryId { get; init; }
}