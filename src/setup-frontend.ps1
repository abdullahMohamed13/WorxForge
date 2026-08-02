#Requires -Version 5.1
<#
.SYNOPSIS
    Installs dependencies and verifies the WorxForge frontend project.
#>
[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Write-Step  { Write-Host "  --> $args" -ForegroundColor Cyan   }
function Write-Ok    { Write-Host "  [OK] $args" -ForegroundColor Green  }
function Write-Fail  { Write-Host "  [FAIL] $args" -ForegroundColor Red  }

$FrontendDir = Join-Path $PSScriptRoot 'worxforge-web'

Write-Host ''
Write-Host '==============================' -ForegroundColor DarkCyan
Write-Host '  WorxForge Frontend Setup    ' -ForegroundColor DarkCyan
Write-Host '==============================' -ForegroundColor DarkCyan
Write-Host ''

# Verify Node.js
Write-Step 'Checking Node.js...'
if (-not (Get-Command node -ErrorAction SilentlyContinue)) {
    Write-Fail 'Node.js not found. Install from https://nodejs.org'
    exit 1
}
$nodeVersion = node --version
Write-Ok "Node.js $nodeVersion detected."

# Verify npm
Write-Step 'Checking npm...'
if (-not (Get-Command npm -ErrorAction SilentlyContinue)) {
    Write-Fail 'npm not found. Reinstall Node.js from https://nodejs.org'
    exit 1
}
$npmVersion = npm --version
Write-Ok "npm $npmVersion detected."

# Verify frontend directory
if (-not (Test-Path $FrontendDir)) {
    Write-Fail "Frontend directory not found: $FrontendDir"
    exit 1
}

Push-Location $FrontendDir
try {
    # Install dependencies
    Write-Step 'Installing npm dependencies...'
    npm install
    if ($LASTEXITCODE -ne 0) {
        Write-Fail 'npm install failed.'
        exit 1
    }
    Write-Ok 'npm install succeeded.'

    # Verify build
    Write-Step 'Verifying frontend build...'
    npm run build
    if ($LASTEXITCODE -ne 0) {
        Write-Fail 'Frontend build failed.'
        exit 1
    }
    Write-Ok 'Frontend build succeeded.'
}
finally {
    Pop-Location
}

Write-Host ''
Write-Host '  Frontend setup complete.' -ForegroundColor Green
Write-Host ''
