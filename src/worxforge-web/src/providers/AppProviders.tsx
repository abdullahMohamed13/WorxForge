import type { PropsWithChildren } from "react";
import { AuthProvider } from "../contexts/AuthContext";
import { SessionProvider } from "../contexts/SessionContext";
import { NotificationProvider } from "../contexts/NotificationContext";

export function AppProviders({ children }: PropsWithChildren) {
  // TODO: Implementation
  return (
    <AuthProvider>
      <SessionProvider>
        <NotificationProvider>{children}</NotificationProvider>
      </SessionProvider>
    </AuthProvider>
  );
}

