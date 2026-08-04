import type { PropsWithChildren } from "react";
import { Navigate } from "react-router-dom";

export function AdminRoute({ children }: PropsWithChildren) {
  // TODO: Implementation
  const isAdmin = false;
  return isAdmin ? <>{children}</> : <Navigate to="/unauthorized" replace />;
}

