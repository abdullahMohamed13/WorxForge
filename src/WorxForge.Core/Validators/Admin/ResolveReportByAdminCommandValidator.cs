using WorxForge.Core.Abstractions;
using WorxForge.Domain.Commands.Admin;

namespace WorxForge.Core.Validators.Admin;

public sealed class ResolveReportByAdminCommandValidator : IRequestValidator<ResolveReportByAdminCommand>
{
    public Task ValidateAsync(ResolveReportByAdminCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
