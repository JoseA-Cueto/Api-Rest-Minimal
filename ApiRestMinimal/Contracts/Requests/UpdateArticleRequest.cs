namespace ApiRestMinimal.Contracts.Requests;

public class UpdateArticleRequest
{
    public string Title { get; init; } = default!;
    public string Content { get; init; } = default!;
    public Guid CategoryId { get; init; }
}