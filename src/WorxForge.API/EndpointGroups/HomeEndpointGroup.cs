using WorxForge.Domain.Queries.Home;

namespace WorxForge.API.EndpointGroups;

public static class HomeEndpointGroup
{
    public static RouteGroupBuilder MapHomeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/home");
        group.MapGet("/dashboard", GetHomeDashboardAsync);
        return group;
    }

    private static Task<IResult> GetHomeDashboardAsync([AsParameters] GetHomeDashboardQuery query, CancellationToken cancellationToken) => throw new NotImplementedException();
}
