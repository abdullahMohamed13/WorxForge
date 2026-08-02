using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Admin;

namespace WorxForge.Core.Validators.Admin;

public sealed class GetAdminReportsQueryValidator : IRequestValidator<GetAdminReportsQuery>
{
    public Task ValidateAsync(GetAdminReportsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
