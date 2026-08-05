import { CommentIcon, ReactionIcon } from "./AssetCardIcons";

interface AssetCardStatsProps {
  reactionCount: number;
  commentCount: number;
}

export function AssetCardStats({ reactionCount, commentCount }: AssetCardStatsProps) {
  return (
    <ul className="flex items-center gap-3 text-xs text-text/60">
      <li className="flex items-center gap-1">
        <ReactionIcon className="size-4" />
        <span>
          {reactionCount}
          <span className="sr-only"> reactions</span>
        </span>
      </li>
      <li className="flex items-center gap-1">
        <CommentIcon className="size-4" />
        <span>
          {commentCount}
          <span className="sr-only"> comments</span>
        </span>
      </li>
    </ul>
  );
}
