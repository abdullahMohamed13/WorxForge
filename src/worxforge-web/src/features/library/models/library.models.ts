import type { AssetSummary } from "../../assets/models/assets.models";

export interface LibraryItem {
  asset: AssetSummary;
  lastViewedAt: string;
}
