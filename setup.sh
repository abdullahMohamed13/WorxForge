#!/usr/bin/env bash
# Restores and builds the WorxForge backend solution.
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
SOLUTION_FILE="$SCRIPT_DIR/src/WorxForge.slnx"

GREEN='\033[0;32m'
CYAN='\033[0;36m'
RED='\033[0;31m'
RESET='\033[0m'

step()  { echo -e "${CYAN}  --> $*${RESET}"; }
ok()    { echo -e "${GREEN}  [OK] $*${RESET}"; }
fail()  { echo -e "${RED}  [FAIL] $*${RESET}"; }

echo ""
echo -e "${CYAN}==============================${RESET}"
echo -e "${CYAN}  WorxForge Backend Setup     ${RESET}"
echo -e "${CYAN}==============================${RESET}"
echo ""

# Verify .NET SDK
step "Checking .NET SDK..."
if ! command -v dotnet &>/dev/null; then
    fail ".NET SDK not found. Install from https://dotnet.microsoft.com/download"
    exit 1
fi
DOTNET_VERSION=$(dotnet --version)
ok ".NET SDK $DOTNET_VERSION detected."

# Verify solution file
if [[ ! -f "$SOLUTION_FILE" ]]; then
    fail "Solution file not found: $SOLUTION_FILE"
    exit 1
fi

# Restore
step "Restoring NuGet packages..."
dotnet restore "$SOLUTION_FILE"
ok "NuGet restore succeeded."

# Build
step "Building backend solution..."
dotnet build "$SOLUTION_FILE" --no-restore --configuration Debug
ok "Backend build succeeded."

echo ""
echo -e "${GREEN}  Backend setup complete.${RESET}"
echo ""
