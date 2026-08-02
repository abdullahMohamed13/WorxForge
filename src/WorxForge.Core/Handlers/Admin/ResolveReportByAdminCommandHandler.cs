using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Admin;

namespace WorxForge.Core.Handlers.Admin;

public sealed class ResolveReportByAdminCommandHandler : ICommandHandler<ResolveReportByAdminCommand>
{
    public Task HandleAsync(ResolveReportByAdminCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
