# WorxForge — Frontend

The web frontend for [WorxForge](https://github.com/itworx/WorxForge), a platform for creating, discovering, and sharing reusable AI knowledge assets (prompts, skills, and agents) across teams.

> **Backend repository:** [itworx/WorxForge-BE](https://github.com/itworx/WorxForge-BE)

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | React 19 + TypeScript (strict mode) |
| Build tool | Vite 8 |
| Styling | Tailwind CSS v4 |
| Server state | TanStack Query v5 |
| Forms | React Hook Form + Zod |
| Real-time | `@microsoft/signalr` (notifications hub) |
| Auth | Google Identity Services (GIS) |
| Router | React Router v7 |
| Testing | Vitest + Testing Library |
| Linting | oxlint |

---

## Architecture

The application lives in `worxforge-web/` with a feature-based folder structure:

```
worxforge-web/src/
  features/           # One folder per domain (assets, auth, explore, home, ...)
    <feature>/
      components/     # UI components scoped to this feature
      hooks/          # React Query hooks
      models/         # TypeScript types
      pages/          # Route-level components
      services/       # HTTP service functions
  layouts/            # AuthLayout, MainLayout (authenticated shell + sidebar)
  routes/             # appRouter.tsx — all route definitions
  navigation/         # Centralized sidebar navigation config
  contexts/           # AuthContext, NotificationContext, SessionContext
  api/                # NSwag-generated HTTP client + custom fetch wrapper
  components/         # Shared UI components (Pagination, SearchBar, AppSidebar)
```

The dev server proxies `/api/*` requests to the backend via Vite's built-in proxy
(configured with `VITE_API_PROXY_TARGET`). SignalR connects to
`{VITE_API_BASE_URL}/hubs/notifications` using the access token.

---

## Prerequisites

| Requirement | Version |
|---|---|
| Node.js | v24 (see `.nvmrc`) |
| npm | bundled with Node.js |
| Git | any recent version |
| Google Cloud project | for OAuth Client ID |

Install Node.js from [nodejs.org](https://nodejs.org) or use [nvm](https://github.com/nvm-sh/nvm) / [nvm-windows](https://github.com/coreybutler/nvm-windows):

```bash
nvm install   # reads .nvmrc automatically
nvm use
```

---

## Quick Start

```powershell
# Windows
git clone https://github.com/itworx/WorxForge-FE.git
cd WorxForge-FE
.\setup.ps1
```

```bash
# Linux / macOS
git clone https://github.com/itworx/WorxForge-FE.git
cd WorxForge-FE
chmod +x setup.sh
./setup.sh
```

The setup script:
1. Verifies Node.js and npm versions
2. Prompts for required environment variables and writes them to `worxforge-web/.env.local`
3. Installs dependencies with `npm ci`
4. Verifies the production build
5. Optionally runs the linter

For CI or non-interactive environments:

```powershell
.\setup.ps1 -NonInteractive
```
```bash
./setup.sh --non-interactive
```

---

## Environment Variables

All variables live in `worxforge-web/.env.local` (git-ignored). Copy `worxforge-web/.env.example` to get started.

| Variable | Description | Example | Required |
|---|---|---|---|
| `VITE_API_BASE_URL` | Backend API base URL used by the browser at runtime. **Leave empty** in local dev to use the Vite proxy. | `https://api.example.com` | No (empty = use proxy) |
| `VITE_API_PROXY_TARGET` | Vite dev proxy target — `/api` requests are forwarded here. Also used as the SignalR hub base. | `https://localhost:7235` | Yes (local dev) |
| `VITE_GOOGLE_CLIENT_ID` | Google OAuth 2.0 Client ID from Google Cloud Console. | `123456789-abc.apps.googleusercontent.com` | Yes |

> **SignalR:** The notifications hub connects automatically to `{VITE_API_BASE_URL}/hubs/notifications` — no separate variable is needed.

---

## Google OAuth Setup

The same Google Cloud Client ID is shared with the backend.

1. Open [Google Cloud Console](https://console.cloud.google.com) → **APIs & Services** → **Credentials**.
2. Create an **OAuth 2.0 Client ID** of type **Web application**.
3. Under **Authorized JavaScript origins**, add:
   - `http://localhost:5173` (local dev)
   - Your production domain (e.g. `https://worxforge.example.com`)
4. Copy the **Client ID** and set it as `VITE_GOOGLE_CLIENT_ID` in `.env.local`.

See the [backend README](https://github.com/itworx/WorxForge-BE) for the corresponding backend configuration.

---

## Connecting to the Backend

For local development, set `VITE_API_PROXY_TARGET` to wherever the backend is running:

```env
VITE_API_PROXY_TARGET=https://localhost:7235
```

Vite will forward all `/api/*` requests from the frontend dev server to that URL, so you
do not need CORS configuration for local development.

Follow the [backend README](https://github.com/itworx/WorxForge-BE) to run the backend locally.

---

## Running in Development

```bash
cd worxforge-web
npm run dev
```

Opens at **http://localhost:5173**.

---

## Building for Production

```bash
cd worxforge-web
npm run build
```

Output is written to `worxforge-web/dist/`.

---

## Running Tests

```bash
cd worxforge-web
npm test
```

Watch mode:

```bash
npm run test:watch
```

---

## Contributing

1. Fork the repository.
2. Create a feature branch off `dev`:
   ```bash
   git checkout dev
   git checkout -b feature/your-feature-name
   ```
3. Commit using [Conventional Commits](https://www.conventionalcommits.org):
   ```
   feat: add bookmark toggle to asset card
   fix: correct pagination offset on explore page
   chore: update dependency versions
   ```
4. Open a pull request targeting the `dev` branch.