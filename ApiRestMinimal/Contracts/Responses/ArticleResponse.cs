namespace ApiRestMinimal.Contracts.Responses;

public class ArticleResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; } = default!;
    public string Content { get; init; } = default!;
    public Guid CategoryId { get; init; }
}