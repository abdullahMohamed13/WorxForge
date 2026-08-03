using WorxForge.Domain.Commands.Assets;
using WorxForge.Domain.Commands.Comments;
using WorxForge.Domain.Commands.Library;
using WorxForge.Domain.Queries.Assets;
using Microsoft.AspNetCore.Mvc;

namespace WorxForge.API.EndpointGroups;

public static class AssetsEndpointGroup
{
    public static RouteGroupBuilder MapAssetsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/assets");
        group.MapPost("/", CreateAssetAsync);
        group.MapPut("/{assetId:guid}", UpdateAssetAsync);
        group.MapDelete("/{assetId:guid}", DeleteAssetAsync);
        group.MapPost("/preview", PreviewAssetAsync);
        group.MapGet("/{assetId:guid}", GetAssetDetailsAsync);
        group.MapGet("/{assetId:guid}/comments", GetAssetCommentsAsync);
        group.MapPost("/{assetId:guid}/comments", CreateAssetCommentAsync);
        group.MapDelete("/{assetId:guid}/comments/{commentId:guid}", DeleteAssetCommentAsync);
        group.MapPost("/{assetId:guid}/bookmark", BookmarkAssetAsync);
        group.MapDelete("/{assetId:guid}/bookmark", RemoveBookmarkAsync);
        group.MapPost("/{assetId:guid}/follow", FollowAssetAsync);
        group.MapDelete("/{assetId:guid}/follow", UnfollowAssetAsync);
        group.MapPost("/{assetId:guid}/reaction", ReactToAssetAsync);
        return group;
    }

    private static Task<IResult> CreateAssetAsync(CreateAssetCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> UpdateAssetAsync(UpdateAssetCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> DeleteAssetAsync([FromBody] DeleteAssetCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> PreviewAssetAsync(PreviewAssetCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> GetAssetDetailsAsync([AsParameters] GetAssetDetailsQuery query, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> GetAssetCommentsAsync([AsParameters] GetAssetCommentsQuery query, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> CreateAssetCommentAsync(CreateAssetCommentCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> DeleteAssetCommentAsync([FromBody] DeleteAssetCommentCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> BookmarkAssetAsync(BookmarkAssetCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> RemoveBookmarkAsync([FromBody] RemoveBookmarkCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> FollowAssetAsync(FollowAssetCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> UnfollowAssetAsync([FromBody] UnfollowAssetCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> ReactToAssetAsync(ReactToAssetCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
}
