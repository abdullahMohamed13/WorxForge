import { createContext, useCallback, useContext, useEffect, useMemo, useState } from "react";
import type { PropsWithChildren } from "react";
import { createAuthenticationService } from "../features/authentication/services/authenticationService";
import { clearAccessToken, getAccessToken } from "../services/tokenService";
import type { UserRole } from "../types/app.types";

export interface AuthenticatedUser {
  userId: string;
  displayName: string;
  avatarUrl?: string;
  role: UserRole;
}

type AuthContextValue = {
  isAuthenticated: boolean;
  isGuestAccess?: boolean;
  isLoading: boolean;
  user: AuthenticatedUser | null;
  setAuthenticatedUser: (nextUser: AuthenticatedUser) => void;
  signOut: () => Promise<void>;
};

const AuthContext = createContext<AuthContextValue>({
  isAuthenticated: false,
  isGuestAccess: true,
  isLoading: true,
  user: null,
  setAuthenticatedUser: () => undefined,
  signOut: async () => undefined
});

const authenticationService = createAuthenticationService();

function normalizeRole(role: UserRole | string | number): UserRole {
  if (role === "Admin" || role === 2) {
    return "Admin";
  }

  return "User";
}

export function AuthProvider({ children }: PropsWithChildren) {
  const [isLoading, setIsLoading] = useState(true);
  const [isGuestAccess, setIsGuestAccess] = useState(true);
  const [user, setUser] = useState<AuthenticatedUser | null>(null);

  const setAuthenticatedUser = useCallback((nextUser: AuthenticatedUser) => {
    setUser({
      ...nextUser,
      role: normalizeRole(nextUser.role)
    });
  }, []);

  const signOut = useCallback(async () => {
    if (isGuestAccess) return;
    try {
      await authenticationService.logout();
    } catch {
      // Best effort sign out; local token is cleared either way.
    } finally {
      clearAccessToken();
      setUser(null);
    }
  }, [isGuestAccess]);

  useEffect(() => {
    const controller = new AbortController();
    const restoreSession = async () => {
      try {
        const mode = await authenticationService.getAccessMode(controller.signal);
        setIsGuestAccess(mode.guestAccessEnabled);
        // TODO: Login is skipped pending adding an SMTP account and completing account flows.
        if (mode.guestAccessEnabled) clearAccessToken();
        if (!mode.guestAccessEnabled && !getAccessToken()) return;

        const session = await authenticationService.getCurrentUser(controller.signal);
        setUser({
          userId: session.userId,
          displayName: session.displayName,
          avatarUrl: session.avatarUrl,
          role: normalizeRole(session.role)
        });
      } catch {
        if (controller.signal.aborted) return;
        clearAccessToken();
        setUser(null);
      } finally {
        if (!controller.signal.aborted) setIsLoading(false);
      }
    };

    void restoreSession();
    return () => controller.abort();
  }, []);

  const value = useMemo<AuthContextValue>(() => ({
    isAuthenticated: user !== null,
    isGuestAccess,
    isLoading,
    user,
    setAuthenticatedUser,
    signOut
  }), [isLoading, isGuestAccess, signOut, user, setAuthenticatedUser]);

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuthContext(): AuthContextValue {
  return useContext(AuthContext);
}
