import type { AssetSummary } from "../../assets/models/assets.models";
import type { PagedResult } from "../../../models/common.models";

export interface LookupItem {
  id: string;
  name: string;
}

export interface ExploreFilters {
  domains: LookupItem[];
  assetTypes: LookupItem[];
  useCases: LookupItem[];
  tools: LookupItem[];
  tags: string[];
}

export interface AssetSearchRequest {
  keywords?: string;
  domainIds?: string[];
  assetTypeIds?: string[];
  useCaseIds?: string[];
  toolIds?: string[];
  tags?: string[];
  sortBy?: "relevance" | "recency" | "popularity";
  page: number;
  pageSize: number;
}

export type AssetSearchResult = PagedResult<AssetSummary>;
