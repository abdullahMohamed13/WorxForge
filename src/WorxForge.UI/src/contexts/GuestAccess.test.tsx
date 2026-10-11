import { render, screen, waitFor } from "@testing-library/react";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import { afterEach, describe, expect, it, vi } from "vitest";
import { AuthProvider, useAuthContext } from "./AuthContext";
import { ProtectedRoute } from "../routes/ProtectedRoute";
import { AdminRoute } from "../routes/AdminRoute";
import { LoginPage } from "../features/authentication/pages/LoginPage";
import { getAccessToken, setAccessToken } from "../services/tokenService";
import { ROUTES } from "../constants/routes.constants";

afterEach(() => {
  vi.unstubAllGlobals();
  localStorage.clear();
  sessionStorage.clear();
});

function mockMode(guestAccessEnabled: boolean) {
  const fetchMock = vi.fn(async (url: RequestInfo | URL) => {
    const path = String(url);
    const body = path.endsWith("/access-mode") ? { guestAccessEnabled } : {
      userId: "guest-id",
      displayName: "Community Guest",
      role: 1
    };
    return new Response(JSON.stringify(body), { headers: { "Content-Type": "application/json" } });
  });
  vi.stubGlobal("fetch", fetchMock);
  return fetchMock;
}

function GuestProfile() {
  const { user, isLoading } = useAuthContext();
  return <div>{isLoading ? "Loading identity" : `${user?.displayName}:${user?.role}`}</div>;
}

describe("guest access", () => {
  it("opens a protected feature without login, clears old tokens, and retains the ordinary user role", async () => {
    setAccessToken("old-login-token");
    const fetchMock = mockMode(true);
    render(<MemoryRouter><AuthProvider><ProtectedRoute><GuestProfile /></ProtectedRoute></AuthProvider></MemoryRouter>);
    expect(await screen.findByText("Community Guest:User")).not.toBeNull();
    expect(getAccessToken()).toBeNull();
    expect(fetchMock.mock.calls.some(([url]) => String(url).endsWith("/login"))).toBe(false);
  });

  it("redirects a login deep link to the app in guest mode", async () => {
    mockMode(true);
    render(<MemoryRouter initialEntries={[ROUTES.AUTH.LOGIN]}><AuthProvider><Routes>
      <Route path={ROUTES.AUTH.LOGIN} element={<LoginPage />} />
      <Route path={ROUTES.HOME} element={<div>Guest home</div>} />
    </Routes></AuthProvider></MemoryRouter>);
    expect(await screen.findByText("Guest home")).not.toBeNull();
    expect(screen.queryByText("Sign in or create your account with Google.")).toBeNull();
  });

  it("preserves the login guard when the API disables guest mode", async () => {
    mockMode(false);
    render(<MemoryRouter initialEntries={[ROUTES.EXPLORE]}><AuthProvider><Routes>
      <Route path={ROUTES.EXPLORE} element={<ProtectedRoute><div>Explore feature</div></ProtectedRoute>} />
      <Route path={ROUTES.AUTH.LOGIN} element={<div>Login required</div>} />
    </Routes></AuthProvider></MemoryRouter>);
    expect(await screen.findByText("Login required")).not.toBeNull();
  });

  it("does not grant administrative access to the shared guest", async () => {
    mockMode(true);
    render(<MemoryRouter initialEntries={[ROUTES.ADMIN.USERS]}><AuthProvider><Routes>
      <Route path={ROUTES.ADMIN.USERS} element={<AdminRoute><div>Administration</div></AdminRoute>} />
      <Route path={ROUTES.UNAUTHORIZED} element={<div>Restricted administration</div>} />
    </Routes></AuthProvider></MemoryRouter>);
    await waitFor(() => expect(screen.getByText("Restricted administration")).not.toBeNull());
    expect(screen.queryByText("Administration")).toBeNull();
  });
});
