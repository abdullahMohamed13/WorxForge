import { createContext, useContext } from "react";
import type { PropsWithChildren } from "react";

type AuthContextValue = {
  isAuthenticated: boolean;
};

const AuthContext = createContext<AuthContextValue>({ isAuthenticated: false });

export function AuthProvider({ children }: PropsWithChildren) {
  // TODO: Implementation
  return <AuthContext.Provider value={{ isAuthenticated: false }}>{children}</AuthContext.Provider>;
}

export function useAuthContext(): AuthContextValue {
  // TODO: Implementation
  return useContext(AuthContext);
}

