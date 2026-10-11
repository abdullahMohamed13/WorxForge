# WorxForge

**The ITWorx AI Assets Hub**

Discover · Contribute · Share · Reuse

WorxForge is an open-source AI assets hub for sharing and discovering reusable prompts,
agents, skills, workflows, and coding standards. The monorepo contains an
ASP.NET Core API and a React web app. It ships with generic lookup data and no
external login accounts, mailboxes, credentials, or organization knowledge assets.

## Guest access

Login is skipped by default. Open the web app and navigate Home, Explore, My
Library, My Assets, Publish Asset, Profile, and Notifications without signing in.
The API uses a synthetic **Community Guest** workspace identity with no email
address or credentials. All visitors share its profile, assets, bookmarks,
reactions, and notifications. Use a fresh database containing public/demo data.

**TODO: Login is temporarily skipped until an authentication provider is configured
and account flows are completed. Email notifications are skipped until an SMTP
account is configured.**

Email delivery is registered as a successful no-op: no SMTP network connection,
sender address, or password is required. In-app notifications remain stored in
SQL Server and delivered over SignalR. Email-change/verification endpoints return
`status: "skipped"` without changing or falsely verifying an email address.

Guest access provides ordinary user features. Admin authorization remains
restricted, and unfinished admin/account operations are listed in
[feature status](docs/feature-status.md).

## Stack and source layout

| Component | Location | Stack |
|---|---|---|
| API | `src/WorxForge.API` | ASP.NET Core 10, minimal APIs, SignalR |
| Application | `src/WorxForge.Core` | Handlers, validators, contracts |
| Domain | `src/WorxForge.Domain` | Entities, DTOs, commands and queries |
| Infrastructure | `src/WorxForge.Infrastructure` | EF Core 10, SQL Server, repositories |
| Web | `src/WorxForge.UI` | React 19, TypeScript, Vite 8, Tailwind CSS, TanStack Query |
| Tests | `src/Tests` | xUnit, Moq, FluentAssertions |

See [architecture](docs/architecture.md) for the data flow.

## Prerequisites

- .NET SDK matching `global.json` (10.0.302, or a compatible patch).
- Node.js 24 and npm, matching `.nvmrc`.
- A fresh SQL Server database (Developer, Express, LocalDB, or a hosted instance).
- PowerShell on Windows, or Bash on macOS/Linux.

## Quick start

Run `./setup.ps1` on Windows or `bash setup.sh` on macOS/Linux. Both scripts restore
and build the backend, offer database migration setup, and install the frontend.
`-BackendOnly` / `--backend-only` and `-FrontendOnly` / `--frontend-only` are supported.
No Google or SMTP account is requested for guest mode.

The API uses a new `knowledge-hub-local-development` User Secrets namespace, so
configuration from the original application is not reused. For manual setup,
configure your own local connection string with User Secrets:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your fresh database connection string>" --project src/WorxForge.API
dotnet tool restore
dotnet ef database update --project src/WorxForge.Infrastructure --startup-project src/WorxForge.API --context WorxForge.Infrastructure.Configuration.WorxForgeDbContext
dotnet run --project src/WorxForge.API
```

The local tool manifest includes EF Core 10 migration tooling and NSwag. Lookup
data and the synthetic guest row are seeded after migrations, when the API starts.

The Development configuration allows `http://localhost:5173` through CORS.
For a deployed API, configure `Cors:AllowedOrigins:0` with your frontend origin.
The API listens at `https://localhost:7235` / `http://localhost:5222`, with Swagger
available in Development at `/swagger`.

In a second shell:

```powershell
cd src/WorxForge.UI
Copy-Item .env.example .env.local
npm ci
npm run dev
```

Open `http://localhost:5173`. The Vite proxy forwards both `/api` and `/hubs` to
`VITE_API_PROXY_TARGET`. For separate production origins, set `VITE_API_BASE_URL`
to your API URL before building. Google client configuration is optional and unused
while guest access is enabled.

## Authentication configuration

`GuestAccess:Enabled` defaults to `true` in every environment. The frontend reads
`GET /api/authentication/access-mode` to follow the API setting. Guest mode ignores
incoming bearer tokens, clears a previous browser login token, and does not expose
Google/debug login endpoints. It does not require any JWT signing key.

To explicitly use the retained Google/JWT flow, set `GuestAccess:Enabled=false`
and configure `Authentication:Google:ClientId`, frontend `VITE_GOOGLE_CLIENT_ID`,
and `JwtAccessToken:Issuer`, `Audience`, `SigningKey`, and `ExpirationMinutes`
using environment variables or User Secrets. Generate your own signing key of
at least 32 bytes. Debug login is available only in Development with guest mode
disabled and separately configured `developmentGoogleSubject` / `developmentEmail`.
Email delivery remains skipped; the account/email/password features require further
implementation before enabling those flows.

Never commit real credentials, `.env.local`, database content, or private documents.
Environment variables use `__` for nested keys, for example
`GuestAccess__Enabled=false`.

## Validation

```powershell
dotnet test src/WorxForge.slnx
cd src/WorxForge.UI
npm run lint
npm test -- --maxWorkers=1
npm run build
```

Backend and frontend CI workflows run build/tests for changes to their source.
The release workflow is manually dispatched from `main`. No GitHub publication
or hosting is triggered by setup.

## Public source handoff

Use the sanitized source archive described in [publication notes](docs/publication.md)
when creating a new public repository. Removing a file from the current tree does
not remove it from old Git commits or branches. The original clone retains its
history for private reference; do not make that history public without a separate
review and sanitization.

## License and attribution

Copyright (c) 2026 ITWORX. Proposed license: [MIT](LICENSE).
Reuse, modification, distribution, and commercial use are permitted provided the
copyright and permission notices are retained in copies or substantial portions.
Please retain the license file when cloning, forking, or redistributing the project.
