import { useAuthContext } from "../../contexts/AuthContext";
import { APP_NAME } from "../../constants/app.constants";

export function Footer() {
  const { user, isGuestAccess } = useAuthContext();

  return (
    <footer className="border-t border-border bg-background px-4 py-4 text-center text-sm text-muted-foreground sm:px-6">
      {isGuestAccess ? `Guest workspace · ${APP_NAME}` : user ? `Signed in as ${user.displayName} · ${APP_NAME}` : APP_NAME}
    </footer>
  );
}
