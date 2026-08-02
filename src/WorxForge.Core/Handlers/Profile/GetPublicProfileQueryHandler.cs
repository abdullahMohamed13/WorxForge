using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Profile;

namespace WorxForge.Core.Handlers.Profile;

public sealed class GetPublicProfileQueryHandler : IQueryHandler<GetPublicProfileQuery, WorxForge.Domain.Dtos.Profile.PublicProfileDto>
{
    public Task<WorxForge.Domain.Dtos.Profile.PublicProfileDto> HandleAsync(GetPublicProfileQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
