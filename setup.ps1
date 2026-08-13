#Requires -Version 5.1
<#
.SYNOPSIS
    Bootstraps the WorxForge frontend project for local development.

.DESCRIPTION
    Validates prerequisites, creates .env.local from .env.example with interactive
    prompts for required environment variables, installs dependencies, verifies the
    build, and optionally runs lint.

.PARAMETER NonInteractive
    Skip all interactive prompts. Copies .env.example to .env.local as-is without
    filling in any values. Intended for CI environments.

.PARAMETER SkipBuild
    Skip the build verification step.

.PARAMETER StartDevServer
    Start the Vite development server after setup completes.
#>
[CmdletBinding()]
param(
    [switch]$NonInteractive,
    [switch]$SkipBuild,
    [switch]$StartDevServer
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Write-Step  { Write-Host "  --> $args" -ForegroundColor Cyan   }
function Write-Ok    { Write-Host "  [OK] $args" -ForegroundColor Green  }
function Write-Warn  { Write-Host "  [WARN] $args" -ForegroundColor Yellow }
function Write-Fail  { Write-Host "  [FAIL] $args" -ForegroundColor Red  }

$RepoRoot    = $PSScriptRoot
$FrontendDir = Join-Path $PSScriptRoot 'worxforge-web'
$NvmrcPath   = Join-Path $RepoRoot '.nvmrc'

Write-Host ''
Write-Host '==============================' -ForegroundColor DarkCyan
Write-Host '  WorxForge Frontend Setup    ' -ForegroundColor DarkCyan
Write-Host '==============================' -ForegroundColor DarkCyan
Write-Host ''

# -- Node.js ------------------------------------------------------------------

Write-Step 'Checking Node.js...'
if (-not (Get-Command node -ErrorAction SilentlyContinue)) {
    Write-Fail 'Node.js not found. Install from https://nodejs.org'
    exit 1
}
$nodeVersion = node --version
Write-Ok "Node.js $nodeVersion detected."

if (Test-Path $NvmrcPath) {
    $requiredNodeVersion = (Get-Content $NvmrcPath -Raw).Trim()
    if ($requiredNodeVersion) {
        $currentMajor  = ($nodeVersion        -replace '^v', '').Split('.')[0]
        $requiredMajor = ($requiredNodeVersion -replace '^v', '').Split('.')[0]
        if ($currentMajor -ne $requiredMajor) {
            Write-Warn "Recommended Node.js is $requiredNodeVersion (.nvmrc), current is $nodeVersion."
        } else {
            Write-Ok "Node.js major version matches .nvmrc ($requiredNodeVersion)."
        }
    }
}

# -- npm ----------------------------------------------------------------------

Write-Step 'Checking npm...'
if (-not (Get-Command npm -ErrorAction SilentlyContinue)) {
    Write-Fail 'npm not found. Reinstall Node.js from https://nodejs.org'
    exit 1
}
$npmVersion = npm --version
Write-Ok "npm $npmVersion detected."

# -- Frontend directory -------------------------------------------------------

if (-not (Test-Path $FrontendDir)) {
    Write-Fail "Frontend directory not found: $FrontendDir"
    exit 1
}
if (-not (Test-Path (Join-Path $FrontendDir 'package.json'))) {
    Write-Fail "package.json not found in $FrontendDir"
    exit 1
}

Push-Location $FrontendDir
try {

    # -- .env.local -----------------------------------------------------------

    $envExamplePath = Join-Path $FrontendDir '.env.example'
    $envLocalPath   = Join-Path $FrontendDir '.env.local'

    if (Test-Path $envLocalPath) {
        Write-Ok '.env.local already exists -- skipping env setup.'
    } elseif (-not (Test-Path $envExamplePath)) {
        Write-Warn '.env.example not found -- skipping env setup.'
    } elseif ($NonInteractive) {
        Write-Step 'Non-interactive mode: copying .env.example to .env.local...'
        Copy-Item -Path $envExamplePath -Destination $envLocalPath
        Write-Ok 'Created .env.local (fill in the required values before running npm run dev).'
    } else {
        Write-Step 'Configuring environment variables...'
        Write-Host ''
        Write-Host '  The following values will be written to .env.local.' -ForegroundColor DarkGray
        Write-Host '  Press Enter to accept the shown default, or type a new value.' -ForegroundColor DarkGray
        Write-Host ''

        $envContent = Get-Content $envExamplePath -Raw

        Write-Host '  VITE_API_BASE_URL' -ForegroundColor White
        Write-Host '    Backend API base URL used at runtime by the browser.' -ForegroundColor DarkGray
        Write-Host '    Leave empty (default) to use the Vite /api proxy in dev --' -ForegroundColor DarkGray
        Write-Host '    the proxy target is set separately via VITE_API_PROXY_TARGET.' -ForegroundColor DarkGray
        Write-Host '    Example: https://localhost:7235  (or empty for local dev)' -ForegroundColor DarkGray
        $apiBaseUrl = Read-Host '    Value [leave empty for proxy mode]'

        Write-Host ''
        Write-Host '  VITE_API_PROXY_TARGET' -ForegroundColor White
        Write-Host '    Vite dev proxy target -- the actual backend URL that /api' -ForegroundColor DarkGray
        Write-Host '    requests are forwarded to when VITE_API_BASE_URL is empty.' -ForegroundColor DarkGray
        Write-Host '    Example: https://localhost:7235' -ForegroundColor DarkGray
        $proxyTarget = Read-Host '    Value [https://localhost:7235]'
        if ([string]::IsNullOrWhiteSpace($proxyTarget)) { $proxyTarget = 'https://localhost:7235' }

        Write-Host ''
        Write-Host '  VITE_GOOGLE_CLIENT_ID' -ForegroundColor White
        Write-Host '    Google OAuth 2.0 Client ID from Google Cloud Console.' -ForegroundColor DarkGray
        Write-Host '    Required for the Google Sign-In button to render.' -ForegroundColor DarkGray
        Write-Host '    Example: 123456789-abc.apps.googleusercontent.com' -ForegroundColor DarkGray
        $googleClientId = Read-Host '    Value'

        $envContent = $envContent -replace '(?m)^(VITE_API_BASE_URL=).*$',     "VITE_API_BASE_URL=$apiBaseUrl"
        $envContent = $envContent -replace '(?m)^(VITE_API_PROXY_TARGET=).*$', "VITE_API_PROXY_TARGET=$proxyTarget"
        $envContent = $envContent -replace '(?m)^(VITE_GOOGLE_CLIENT_ID=).*$', "VITE_GOOGLE_CLIENT_ID=$googleClientId"

        [System.IO.File]::WriteAllText($envLocalPath, $envContent, [System.Text.Encoding]::UTF8)
        Write-Ok 'Created .env.local.'
        Write-Host ''
    }

    # -- Dependencies ---------------------------------------------------------

    Write-Step 'Installing npm dependencies...'
    $lockFilePath = Join-Path $FrontendDir 'package-lock.json'
    if (Test-Path $lockFilePath) {
        npm ci
    } else {
        npm install
    }
    Write-Ok 'npm dependency install succeeded.'

    # -- Build ----------------------------------------------------------------

    if (-not $SkipBuild) {
        Write-Step 'Verifying frontend build...'
        npm run build
        Write-Ok 'Frontend build succeeded.'
    } else {
        Write-Ok 'Skipping build verification by request.'
    }

    # -- Lint (interactive prompt) --------------------------------------------

    if (-not $NonInteractive) {
        Write-Host ''
        $runLint = Read-Host '  Run linter now? [y/N]'
        if ($runLint -match '^[Yy]') {
            Write-Step 'Running linter...'
            npm run lint
            Write-Ok 'Lint succeeded.'
        }
    }

    # -- Dev server -----------------------------------------------------------

    if ($StartDevServer) {
        Write-Step 'Starting Vite development server...'
        npm run dev
    }

} finally {
    Pop-Location
}

Write-Host ''
Write-Host '  Frontend setup complete.' -ForegroundColor Green
if (-not $StartDevServer) {
    Write-Host '  Next steps:' -ForegroundColor DarkGray
    Write-Host '    cd worxforge-web' -ForegroundColor DarkGray
    Write-Host '    npm run dev' -ForegroundColor DarkGray
    Write-Host '  Then open: http://localhost:5173' -ForegroundColor DarkGray
}
Write-Host ''