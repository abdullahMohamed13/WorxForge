import { FaBookmark, FaRegBookmark, FaRegComment, FaRegThumbsUp } from "react-icons/fa";

interface IconProps {
  className?: string;
}

export function ReactionIcon({ className }: IconProps) {
  return <FaRegThumbsUp aria-hidden="true" className={className} />;
}

export function CommentIcon({ className }: IconProps) {
  return <FaRegComment aria-hidden="true" className={className} />;
}

interface BookmarkIconProps extends IconProps {
  filled: boolean;
}

export function BookmarkIcon({ className, filled }: BookmarkIconProps) {
  const Icon = filled ? FaBookmark : FaRegBookmark;
  return <Icon aria-hidden="true" className={className} />;
}
