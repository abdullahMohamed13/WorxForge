using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Profile;

namespace WorxForge.Core.Validators.Profile;

public sealed class GetMyProfileStatisticsQueryValidator : IRequestValidator<GetMyProfileStatisticsQuery>
{
    public Task ValidateAsync(GetMyProfileStatisticsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
