# WorxForge Backend

Backend API for the WorxForge platform, built with ASP.NET Core following Clean Architecture principles.

---

## Repository Structure

```
/
├── docs/
│   ├── architecture/      Architecture notes and decisions
│   ├── requirements/      Product requirements and specifications
│   └── archive/           Historical phase handoffs
├── src/
│   ├── WorxForge.API/         Presentation layer — Minimal API endpoints and middleware
│   ├── WorxForge.Core/        Application layer — Handlers, validators, and interfaces
│   ├── WorxForge.Domain/      Domain layer — Entities, commands, queries, and DTOs
│   ├── WorxForge.Infrastructure/  Infrastructure layer — Repository and service implementations
│   └── Tests/
│       ├── UnitTests/
│       ├── IntegrationTests/
│       └── SystemTests/
├── setup.ps1              Windows setup script
└── setup.sh               Linux/macOS setup script
```

---

## Prerequisites

- [.NET SDK 10.0.302](https://dotnet.microsoft.com/download) or later in the `10.0.x` range
- Git

---

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

The setup scripts verify that the .NET SDK is installed, restore all NuGet packages, and build the solution.

---

## Build

```bash
dotnet build src/WorxForge.slnx
```

---

## Documentation

| Location | Contents |
|---|---|
| [`docs/requirements/`](docs/requirements/) | Software Requirements Specification, Business Requirements Document, Database Design |
| [`docs/architecture/`](docs/architecture/) | Architecture notes and open decisions |
| [`docs/archive/`](docs/archive/) | Historical phase handoff documents |
