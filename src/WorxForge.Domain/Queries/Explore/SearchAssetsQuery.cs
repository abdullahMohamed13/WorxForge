namespace WorxForge.Domain.Queries.Explore;

public sealed class SearchAssetsQuery
{
    public Guid RequestingUserId { get; init; }
    public string? Keywords { get; init; }
    public IReadOnlyList<Guid> DomainIds { get; init; } = [];
    public IReadOnlyList<Guid> AssetTypeIds { get; init; } = [];
    public IReadOnlyList<Guid> UseCaseIds { get; init; } = [];
    public IReadOnlyList<Guid> ToolIds { get; init; } = [];
    public IReadOnlyList<string> Tags { get; init; } = [];
    public string? SortBy { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}

