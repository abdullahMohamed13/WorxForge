#Requires -Version 5.1
<#
.SYNOPSIS
    Restores and builds the WorxForge backend solution.
#>
[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Write-Step  { Write-Host "  --> $args" -ForegroundColor Cyan   }
function Write-Ok    { Write-Host "  [OK] $args" -ForegroundColor Green  }
function Write-Fail  { Write-Host "  [FAIL] $args" -ForegroundColor Red  }

$SolutionFile = Join-Path $PSScriptRoot 'src\WorxForge.slnx'

Write-Host ''
Write-Host '==============================' -ForegroundColor DarkCyan
Write-Host '  WorxForge Backend Setup     ' -ForegroundColor DarkCyan
Write-Host '==============================' -ForegroundColor DarkCyan
Write-Host ''

# Verify .NET SDK
Write-Step 'Checking .NET SDK...'
if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Fail '.NET SDK not found. Install from https://dotnet.microsoft.com/download'
    exit 1
}
$dotnetVersion = dotnet --version
Write-Ok ".NET SDK $dotnetVersion detected."

# Verify solution file exists
if (-not (Test-Path $SolutionFile)) {
    Write-Fail "Solution file not found: $SolutionFile"
    exit 1
}

# Restore
Write-Step 'Restoring NuGet packages...'
dotnet restore $SolutionFile
if ($LASTEXITCODE -ne 0) {
    Write-Fail 'NuGet restore failed.'
    exit 1
}
Write-Ok 'NuGet restore succeeded.'

# Build
Write-Step 'Building backend solution...'
dotnet build $SolutionFile --no-restore --configuration Debug
if ($LASTEXITCODE -ne 0) {
    Write-Fail 'Backend build failed.'
    exit 1
}
Write-Ok 'Backend build succeeded.'

Write-Host ''
Write-Host '  Backend setup complete.' -ForegroundColor Green
Write-Host ''
