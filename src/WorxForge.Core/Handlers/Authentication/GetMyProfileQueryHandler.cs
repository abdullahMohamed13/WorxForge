using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Authentication;

namespace WorxForge.Core.Handlers.Authentication;

public sealed class GetMyProfileQueryHandler : IQueryHandler<GetMyProfileQuery, WorxForge.Domain.Dtos.Profile.MyProfileDto>
{
    public Task<WorxForge.Domain.Dtos.Profile.MyProfileDto> HandleAsync(GetMyProfileQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
