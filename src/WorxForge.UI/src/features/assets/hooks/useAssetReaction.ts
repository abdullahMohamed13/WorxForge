import { useMutation, useQueryClient } from "@tanstack/react-query";
import { httpClient } from "../../../api/httpClient";
import type { AssetDetails } from "../models/assets.models";

// Like is the compatibility value for the public Useful action.
export type ReactionType = "Like";

export interface AssetReactionSummary {
  assetId: string;
  likeCount: number;
  totalCount: number;
  currentUserReaction: ReactionType | null;
}

interface ToggleReactionParams {
  assetId: string;
  reactionType: ReactionType;
}

export function useAssetReaction() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ assetId, reactionType }: ToggleReactionParams) =>
      httpClient.post<AssetReactionSummary>(`/api/assets/${assetId}/reaction`, { reactionType }),
    onSuccess: async (summary, { assetId }) => {
      // Use the server count rather than guessing from a potentially stale guest workspace.
      queryClient.setQueryData<AssetDetails>(["assets", "details", assetId], (asset) => asset && ({
        ...asset,
        likeCount: summary.likeCount,
        currentUserReaction: summary.currentUserReaction ?? undefined
      }));
      await Promise.all([
        ["assets", "details", assetId],
        ["assets", "editor", "detail", assetId],
        ["assets", "my-assets"],
        ["home", "dashboard"],
        ["explore", "assets"],
        ["library", "saved"],
        ["profile"]
      ].map((queryKey) => queryClient.invalidateQueries({ queryKey })));
    }
  });
}
