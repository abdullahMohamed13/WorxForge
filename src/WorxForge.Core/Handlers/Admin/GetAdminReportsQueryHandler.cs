using WorxForge.Core.Abstractions;
using WorxForge.Domain.Queries.Admin;

namespace WorxForge.Core.Handlers.Admin;

public sealed class GetAdminReportsQueryHandler : IQueryHandler<GetAdminReportsQuery, WorxForge.Domain.Dtos.Admin.AdminReportDto>
{
    public Task<WorxForge.Domain.Dtos.Admin.AdminReportDto> HandleAsync(GetAdminReportsQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
