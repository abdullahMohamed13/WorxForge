using WorxForge.Domain.Commands.Settings;

namespace WorxForge.API.EndpointGroups;

public static class SettingsEndpointGroup
{
    public static RouteGroupBuilder MapSettingsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/settings");
        group.MapPut("/email", UpdateEmailAddressAsync);
        group.MapPost("/verify-email", VerifyUpdatedEmailAsync);
        group.MapPut("/password", ChangePasswordAsync);
        group.MapDelete("/account", DeleteAccountAsync);
        return group;
    }

    private static Task<IResult> UpdateEmailAddressAsync(UpdateEmailAddressCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> VerifyUpdatedEmailAsync(VerifyUpdatedEmailCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> ChangePasswordAsync(ChangePasswordCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    private static Task<IResult> DeleteAccountAsync(DeleteAccountCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
}
