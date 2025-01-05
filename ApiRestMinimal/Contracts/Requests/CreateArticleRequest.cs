namespace ApiRestMinimal.Contracts.Requests;

public class CreateArticleRequest
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; init; } = default!;
    public string Content { get; init; } = default!;
    public Guid CategoryId { get; init; }
}