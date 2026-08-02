using WorxForge.Core.Interfaces.Repositories;
using WorxForge.Domain.Dtos.Admin;
using WorxForge.Domain.Entities;
using WorxForge.Domain.Enums;

namespace WorxForge.Infrastructure.Repositories;

public sealed class ReportRepository : IReportRepository
{
    public Task<Report?> GetByIdAsync(Guid reportId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<(IReadOnlyList<AdminReportDto> Items, int Total)> GetAllAsync(ReportStatus? status, string? sortBy, int page, int pageSize, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task AddAsync(Report report, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task UpdateAsync(Report report, CancellationToken cancellationToken) => throw new NotImplementedException();
}
