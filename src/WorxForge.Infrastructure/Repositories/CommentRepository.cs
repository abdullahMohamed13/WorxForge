using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Dtos.Assets;
using WorxForge.Domain.Entities;

namespace WorxForge.Infrastructure.Repositories;

public sealed class CommentRepository : ICommentRepository
{
    public Task<Comment?> GetByIdAsync(Guid commentId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<(IReadOnlyList<AssetCommentDto> Items, int Total)> GetByAssetAsync(Guid assetId, int page, int pageSize, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<(IReadOnlyList<AssetCommentDto> Items, int Total)> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task AddAsync(Comment comment, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task SoftDeleteAsync(Guid commentId, CancellationToken cancellationToken) => throw new NotImplementedException();
}
