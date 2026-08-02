using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Library;

namespace WorxForge.Core.Handlers.Library;

public sealed class GetSavedAssetsQueryHandler : IQueryHandler<GetSavedAssetsQuery, WorxForge.Domain.Dtos.Library.LibrarySummaryDto>
{
    public Task<WorxForge.Domain.Dtos.Library.LibrarySummaryDto> HandleAsync(GetSavedAssetsQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
