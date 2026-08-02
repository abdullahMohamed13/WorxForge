using WorxForge.Domain.Queries.Explore;

namespace WorxForge.API.EndpointGroups;

public static class ExploreEndpointGroup
{
    public static RouteGroupBuilder MapExploreEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/explore");
        group.MapGet("/search", SearchAssetsAsync);
        group.MapGet("/browse", BrowseAssetsAsync);
        group.MapGet("/filters", GetExploreFiltersAsync);
        return group;
    }

    private static Task<IResult> SearchAssetsAsync([AsParameters] SearchAssetsQuery query, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> BrowseAssetsAsync([AsParameters] BrowseAssetsQuery query, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> GetExploreFiltersAsync([AsParameters] GetExploreFiltersQuery query, CancellationToken cancellationToken) => throw new NotImplementedException();
}
