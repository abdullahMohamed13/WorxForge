import { Anvil } from "lucide-react";
import { APP_NAME } from "../../constants/app.constants";

interface LogoProps {
  className?: string;
}

export function Logo({ className = "size-8" }: LogoProps) {
  return <Anvil className={className} role="img" aria-label={`${APP_NAME} logo`} strokeWidth={2} />;
}
