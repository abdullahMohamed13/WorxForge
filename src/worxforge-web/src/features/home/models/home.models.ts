import type { AssetSummary } from "../../assets/models/assets.models";

export interface HomeDashboard {
  // TODO: Recommendation algorithm TBD.
  recommendations: AssetSummary[];
  featured: AssetSummary[];
  trending: AssetSummary[];
  recentlyAccessed: AssetSummary[];
  announcements: string[];
}
