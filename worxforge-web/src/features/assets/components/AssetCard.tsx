import { Link } from "react-router-dom";
import type { AssetSummary } from "../models/assets.models";
import { ROUTES } from "../../../constants/routes.constants";
import { AssetBadge } from "./AssetBadge";
import { AssetToolTags } from "./AssetToolTags";
import { AssetCardMeta } from "./AssetCardMeta";
import { AssetCardStats } from "./AssetCardStats";
import { BookmarkButton } from "./BookmarkButton";

interface AssetCardProps {
  asset: AssetSummary;
  onToggleBookmark?: (assetId: string, isBookmarked: boolean) => void;
  className?: string;
}

export function AssetCard({ asset, onToggleBookmark, className = "" }: AssetCardProps) {
  const titleId = `asset-card-title-${asset.assetId}`;

  return (
    <article
      aria-labelledby={titleId}
      className={`flex h-full flex-col gap-3 rounded-2xl border border-secondary/30 bg-background p-5 shadow-sm transition-shadow hover:shadow-md ${className}`}
    >
      <header className="flex items-start justify-between gap-2">
        <AssetBadge label={asset.assetTypeName} variant="primary" />
        <BookmarkButton
          assetId={asset.assetId}
          isBookmarked={asset.isBookmarked}
          onToggle={onToggleBookmark}
        />
      </header>

      <h3 id={titleId} className="text-lg leading-snug font-semibold text-text">
        <Link
          to={ROUTES.ASSET(asset.assetId)}
          className="rounded-sm hover:text-primary focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-primary"
        >
          <span className="line-clamp-2">{asset.title}</span>
        </Link>
      </h3>

      {asset.description && (
        <p className="line-clamp-2 text-sm text-text/70">{asset.description}</p>
      )}

      <div className="flex flex-wrap items-center gap-1.5">
        <AssetBadge label={asset.domainName} variant="secondary" />
        <AssetToolTags tools={asset.tools} />
      </div>

      <div className="mt-auto flex flex-col gap-3 pt-2">
        <div className="flex items-end justify-between gap-3">
          <AssetCardMeta authorDisplayName={asset.authorDisplayName} updatedAt={asset.updatedAt} />
          <AssetCardStats reactionCount={asset.reactionCount} commentCount={asset.commentCount} />
        </div>

        <Link
          to={ROUTES.ASSET(asset.assetId)}
          className="mt-4 inline-flex items-center justify-center rounded-lg border border-secondary/30 bg-gray-100 px-4 py-2 text-sm font-medium text-black transition-colors hover:bg-primary/10 hover:text-primary focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-primary"
        >
          Open asset
        </Link>
      </div>
    </article>
  );
}
