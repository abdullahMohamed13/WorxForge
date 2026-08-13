#!/usr/bin/env bash
# Installs dependencies and configures the WorxForge frontend project for local development.
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
FRONTEND_DIR="$SCRIPT_DIR/worxforge-web"
NON_INTERACTIVE=false

for arg in "$@"; do
  case "$arg" in
    --non-interactive) NON_INTERACTIVE=true ;;
  esac
done

GREEN='\033[0;32m'
CYAN='\033[0;36m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
GRAY='\033[0;37m'
WHITE='\033[1;37m'
RESET='\033[0m'

step()  { echo -e "${CYAN}  --> $*${RESET}"; }
ok()    { echo -e "${GREEN}  [OK] $*${RESET}"; }
warn()  { echo -e "${YELLOW}  [WARN] $*${RESET}"; }
fail()  { echo -e "${RED}  [FAIL] $*${RESET}"; }
gray()  { echo -e "${GRAY}    $*${RESET}"; }

echo ""
echo -e "${CYAN}==============================${RESET}"
echo -e "${CYAN}  WorxForge Frontend Setup    ${RESET}"
echo -e "${CYAN}==============================${RESET}"
echo ""

# -- Node.js ------------------------------------------------------------------

step "Checking Node.js..."
if ! command -v node &>/dev/null; then
    fail "Node.js not found. Install from https://nodejs.org"
    exit 1
fi
NODE_VERSION=$(node --version)
ok "Node.js $NODE_VERSION detected."

NVMRC="$SCRIPT_DIR/.nvmrc"
if [[ -f "$NVMRC" ]]; then
    REQUIRED=$(cat "$NVMRC" | tr -d '[:space:]')
    if [[ -n "$REQUIRED" ]]; then
        CURRENT_MAJOR=$(echo "$NODE_VERSION" | sed 's/^v//' | cut -d. -f1)
        REQUIRED_MAJOR=$(echo "$REQUIRED"    | sed 's/^v//' | cut -d. -f1)
        if [[ "$CURRENT_MAJOR" != "$REQUIRED_MAJOR" ]]; then
            warn "Recommended Node.js is $REQUIRED (.nvmrc), current is $NODE_VERSION."
        else
            ok "Node.js major version matches .nvmrc ($REQUIRED)."
        fi
    fi
fi

# -- npm ----------------------------------------------------------------------

step "Checking npm..."
if ! command -v npm &>/dev/null; then
    fail "npm not found. Reinstall Node.js from https://nodejs.org"
    exit 1
fi
NPM_VERSION=$(npm --version)
ok "npm $NPM_VERSION detected."

# -- Frontend directory -------------------------------------------------------

if [[ ! -d "$FRONTEND_DIR" ]]; then
    fail "Frontend directory not found: $FRONTEND_DIR"
    exit 1
fi
if [[ ! -f "$FRONTEND_DIR/package.json" ]]; then
    fail "package.json not found in $FRONTEND_DIR"
    exit 1
fi

cd "$FRONTEND_DIR"

# -- .env.local ---------------------------------------------------------------

ENV_EXAMPLE="$FRONTEND_DIR/.env.example"
ENV_LOCAL="$FRONTEND_DIR/.env.local"

if [[ -f "$ENV_LOCAL" ]]; then
    ok ".env.local already exists -- skipping env setup."
elif [[ ! -f "$ENV_EXAMPLE" ]]; then
    warn ".env.example not found -- skipping env setup."
elif [[ "$NON_INTERACTIVE" == "true" ]]; then
    step "Non-interactive mode: copying .env.example to .env.local..."
    cp "$ENV_EXAMPLE" "$ENV_LOCAL"
    ok "Created .env.local (fill in the required values before running npm run dev)."
else
    step "Configuring environment variables..."
    echo ""
    gray "The following values will be written to .env.local."
    gray "Press Enter to accept the shown default, or type a new value."
    echo ""

    # Copy the template to start from
    cp "$ENV_EXAMPLE" "$ENV_LOCAL"

    # -- VITE_API_BASE_URL ----------------------------------------------------
    echo -e "${WHITE}  VITE_API_BASE_URL${RESET}"
    gray "Backend API base URL used at runtime by the browser."
    gray "Leave empty (default) to use the Vite /api proxy in dev --"
    gray "the proxy target is set separately via VITE_API_PROXY_TARGET."
    gray "Example: https://localhost:7235  (or empty for local dev)"
    read -r -p "    Value [leave empty for proxy mode]: " API_BASE_URL

    # -- VITE_API_PROXY_TARGET ------------------------------------------------
    echo ""
    echo -e "${WHITE}  VITE_API_PROXY_TARGET${RESET}"
    gray "Vite dev proxy target -- the actual backend URL that /api"
    gray "requests are forwarded to when VITE_API_BASE_URL is empty."
    gray "Example: https://localhost:7235"
    read -r -p "    Value [https://localhost:7235]: " PROXY_TARGET
    PROXY_TARGET="${PROXY_TARGET:-https://localhost:7235}"

    # -- VITE_GOOGLE_CLIENT_ID ------------------------------------------------
    echo ""
    echo -e "${WHITE}  VITE_GOOGLE_CLIENT_ID${RESET}"
    gray "Google OAuth 2.0 Client ID from Google Cloud Console."
    gray "Required for the Google Sign-In button to render."
    gray "Example: 123456789-abc.apps.googleusercontent.com"
    read -r -p "    Value: " GOOGLE_CLIENT_ID

    # Write values into .env.local using sed in-place
    sed -i "s|^VITE_API_BASE_URL=.*|VITE_API_BASE_URL=${API_BASE_URL}|"     "$ENV_LOCAL"
    sed -i "s|^VITE_API_PROXY_TARGET=.*|VITE_API_PROXY_TARGET=${PROXY_TARGET}|" "$ENV_LOCAL"
    sed -i "s|^VITE_GOOGLE_CLIENT_ID=.*|VITE_GOOGLE_CLIENT_ID=${GOOGLE_CLIENT_ID}|" "$ENV_LOCAL"

    ok "Created .env.local."
    echo ""
fi

# -- Dependencies -------------------------------------------------------------

step "Installing npm dependencies..."
if [[ -f "$FRONTEND_DIR/package-lock.json" ]]; then
    npm ci
else
    npm install
fi
ok "npm dependency install succeeded."

# -- Build --------------------------------------------------------------------

step "Verifying frontend build..."
npm run build
ok "Frontend build succeeded."

# -- Lint (interactive prompt) ------------------------------------------------

if [[ "$NON_INTERACTIVE" != "true" ]]; then
    echo ""
    read -r -p "  Run linter now? [y/N]: " RUN_LINT
    if [[ "$RUN_LINT" =~ ^[Yy]$ ]]; then
        step "Running linter..."
        npm run lint
        ok "Lint succeeded."
    fi
fi

echo ""
echo -e "${GREEN}  Frontend setup complete.${RESET}"
echo -e "${GRAY}  Next steps:${RESET}"
echo -e "${GRAY}    cd worxforge-web${RESET}"
echo -e "${GRAY}    npm run dev${RESET}"
echo -e "${GRAY}  Then open: http://localhost:5173${RESET}"
echo ""