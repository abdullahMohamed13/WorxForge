# WorxForge Frontend

Frontend application for the WorxForge platform, built with React, TypeScript, and Vite.

## Repository Structure

```
/
├── .editorconfig
├── .gitignore
├── .nvmrc
├── README.md
├── setup.ps1
├── setup.sh
├── docs/
│   ├── requirements/
│   └── archive/
└── src/
    └── worxforge-web/
```

## Prerequisites

- Node.js `v24.18.1` (see `.nvmrc`)
- npm (comes with Node.js)
- Git

## Setup

### Windows (PowerShell)

```powershell
.\setup.ps1
```

### Linux / macOS (Bash)

```bash
chmod +x setup.sh
./setup.sh
```

The setup scripts:

- verify Node.js and npm
- install dependencies with `npm ci`
- verify the frontend build

## Run locally

```powershell
cd .\src\worxforge-web
npm run dev
```

## Build

```powershell
cd .\src\worxforge-web
npm run build
```

## Documentation

- `docs/requirements/` — SRS and BRD
- `docs/archive/` — historical handoff
