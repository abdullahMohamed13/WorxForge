# WorxForge web app

The ITWorx AI Assets Hub — Discover · Contribute · Share · Reuse.

React 19 + TypeScript + Vite 8. The API selects guest mode through
`/api/authentication/access-mode`; no Google client ID is required by default.

From this directory, copy `.env.example` to `.env.local` once and start the API
using the root README instructions before starting Vite.

```sh
npm ci
npm run dev
npm run lint
npm test -- --maxWorkers=1
npm run build
```

Vite forwards `/api` and `/hubs` to the configured proxy target, which defaults to
`https://localhost:7235`. Use the API HTTPS launch profile, or change
`VITE_API_PROXY_TARGET` to match your local API. These development proxies are
not part of the production build. See the [root README](../../README.md) for
API/database setup and hosting guidance.
