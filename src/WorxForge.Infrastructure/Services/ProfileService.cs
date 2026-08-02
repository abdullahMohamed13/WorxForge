using WorxForge.Core.Interfaces.Services;
using WorxForge.Domain.Dtos.Profile;

namespace WorxForge.Infrastructure.Services;

public sealed class ProfileService : IProfileService
{
    public Task<MyProfileDto> GetMyProfileAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<PublicProfileDto> GetPublicProfileAsync(Guid targetUserId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<ProfileStatisticsDto> GetStatisticsAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task UpdateProfileAsync(Guid userId, string displayName, string username, string? avatarUrl, CancellationToken cancellationToken) => throw new NotImplementedException();
}
