import type { LucideIcon } from "lucide-react";
import { Search, FilePlus2, Share2, Repeat2 } from "lucide-react";
import { useNavigate } from "react-router-dom";
import { SearchBar } from "../../../components/SearchBar";
import { Logo } from "../../../layouts/components/Logo";
import { APP_DESCRIPTION, APP_NAME, APP_TAGLINE } from "../../../constants/app.constants";
import { ROUTES } from "../../../constants/routes.constants";

interface HeroFeature {
  icon: LucideIcon;
  title: string;
  description: string;
}

const HERO_FEATURES: HeroFeature[] = [
  { icon: Search, title: "Discover", description: "Find prompts, agents, skills, and standards shared by the community." },
  { icon: FilePlus2, title: "Contribute", description: "Publish your own AI assets to help others build and learn." },
  { icon: Share2, title: "Share", description: "Share useful assets and insights with your team and the community." },
  { icon: Repeat2, title: "Reuse", description: "Reuse proven prompts and workflows to accelerate your next project." }
];

export function HeroSection() {
  const navigate = useNavigate();

  function handleSearch(query: string) {
    if (!query) return;
    navigate(`${ROUTES.EXPLORE}?q=${encodeURIComponent(query)}`);
  }

  return (
    <section className="mx-auto w-full max-w-3xl text-center">
      <div className="rounded-2xl border border-foreground/10 bg-card px-4 py-7 sm:px-6 sm:py-8">
        <div className="flex items-center justify-center gap-3 text-foreground">
          <Logo className="size-8 shrink-0 sm:size-10" />
          <h1 className="text-4xl font-semibold tracking-tight sm:text-5xl">{APP_NAME}</h1>
        </div>
        <p className="mt-5 text-lg font-medium text-foreground sm:text-xl">{APP_DESCRIPTION}</p>
        <p className="mt-3 text-base text-neutral-500 sm:text-lg">{APP_TAGLINE}</p>
      </div>

      <SearchBar
        className="mt-6"
        placeholder='Try: "prompt for generating xUnit tests" or "API security review"'
        onSearch={handleSearch}
      />

      <div className="mt-8 grid grid-cols-2 gap-3 md:grid-cols-4">
        {HERO_FEATURES.map(({ icon: Icon, title, description }) => (
          <div key={title} className="rounded-xl border border-secondary/30 bg-card p-4 text-left shadow-sm">
            <Icon aria-hidden="true" className="h-5 w-5 text-primary" />
            <div className="mt-2 text-sm font-semibold">{title}</div>
            <div className="mt-0.5 text-xs text-muted-foreground">{description}</div>
          </div>
        ))}
      </div>
    </section>
  );
}
