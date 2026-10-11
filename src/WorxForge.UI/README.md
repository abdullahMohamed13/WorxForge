# WorxForge web app

The ITWorx AI Assets Hub — Discover · Contribute · Share · Reuse.

React 19 + TypeScript + Vite 8. The API selects guest mode through
`/api/authentication/access-mode`; no Google client ID is required by default.

```sh
npm ci
npm run dev
npm run lint
npm test -- --maxWorkers=1
npm run build
```

Copy `.env.example` to `.env.local`. Vite forwards `/api` and `/hubs` to the configured
proxy target. See the [root README](../../README.md) for API/database setup.
