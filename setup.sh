#!/usr/bin/env bash
# Installs dependencies and verifies the WorxForge frontend project.
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
FRONTEND_DIR="$SCRIPT_DIR/worxforge-web"

GREEN='\033[0;32m'
CYAN='\033[0;36m'
RED='\033[0;31m'
RESET='\033[0m'

step()  { echo -e "${CYAN}  --> $*${RESET}"; }
ok()    { echo -e "${GREEN}  [OK] $*${RESET}"; }
fail()  { echo -e "${RED}  [FAIL] $*${RESET}"; }

echo ""
echo -e "${CYAN}==============================${RESET}"
echo -e "${CYAN}  WorxForge Frontend Setup    ${RESET}"
echo -e "${CYAN}==============================${RESET}"
echo ""

# Verify Node.js
step "Checking Node.js..."
if ! command -v node &>/dev/null; then
    fail "Node.js not found. Install from https://nodejs.org"
    exit 1
fi
NODE_VERSION=$(node --version)
ok "Node.js $NODE_VERSION detected."

# Verify npm
step "Checking npm..."
if ! command -v npm &>/dev/null; then
    fail "npm not found. Reinstall Node.js from https://nodejs.org"
    exit 1
fi
NPM_VERSION=$(npm --version)
ok "npm $NPM_VERSION detected."

# Verify frontend directory
if [[ ! -d "$FRONTEND_DIR" ]]; then
    fail "Frontend directory not found: $FRONTEND_DIR"
    exit 1
fi

cd "$FRONTEND_DIR"

# Install dependencies
step "Installing npm dependencies from lock file..."
npm ci
ok "npm dependency install succeeded."

# Verify build
step "Verifying frontend build..."
npm run build
ok "Frontend build succeeded."

echo ""
echo -e "${GREEN}  Frontend setup complete.${RESET}"
echo ""
