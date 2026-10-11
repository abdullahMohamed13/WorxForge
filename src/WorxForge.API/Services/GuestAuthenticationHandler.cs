using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using WorxForge.Domain.Constants;
using WorxForge.Domain.Enums;

namespace WorxForge.API.Services;

// TODO: Login is skipped pending adding an SMTP account and completing account flows.
// No token is issued and incoming bearer tokens cannot select another user's identity.
public sealed class GuestAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string SchemeName = "GuestWorkspace";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[]
        {
            new Claim(ClaimConstants.UserId, GuestWorkspaceConstants.UserId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, GuestWorkspaceConstants.UserId.ToString()),
            new Claim(ClaimTypes.Name, GuestWorkspaceConstants.DisplayName),
            new Claim(ClaimConstants.Role, UserRole.User.ToString())
        };
        var identity = new ClaimsIdentity(claims, SchemeName, ClaimTypes.Name, ClaimConstants.Role);
        return Task.FromResult(AuthenticateResult.Success(
            new AuthenticationTicket(new ClaimsPrincipal(identity), SchemeName)));
    }
}
