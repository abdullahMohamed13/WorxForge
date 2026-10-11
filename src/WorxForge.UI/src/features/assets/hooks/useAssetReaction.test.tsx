import { act, renderHook, waitFor } from "@testing-library/react";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import type { PropsWithChildren } from "react";
import { beforeEach, describe, expect, it, vi } from "vitest";
import { httpClient } from "../../../api/httpClient";
import { useAssetReaction } from "./useAssetReaction";

vi.mock("../../../api/httpClient", () => ({ httpClient: { post: vi.fn() } }));

function setup() {
  const queryClient = new QueryClient({ defaultOptions: { queries: { retry: false }, mutations: { retry: false } } });
  queryClient.setQueryData(["assets", "details", "asset-1"], { assetId: "asset-1", likeCount: 4 });
  const invalidate = vi.spyOn(queryClient, "invalidateQueries");
  const wrapper = ({ children }: PropsWithChildren) => <QueryClientProvider client={queryClient}>{children}</QueryClientProvider>;
  return { ...renderHook(() => useAssetReaction(), { wrapper }), queryClient, invalidate };
}

describe("Useful feedback cache consistency", () => {
  beforeEach(() => vi.clearAllMocks());

  it("uses the server count and refreshes every related screen", async () => {
    vi.mocked(httpClient.post).mockResolvedValue({ assetId: "asset-1", likeCount: 17, totalCount: 17, currentUserReaction: "Like" });
    const { result, queryClient, invalidate } = setup();
    await act(async () => { await result.current.mutateAsync({ assetId: "asset-1", reactionType: "Like" }); });
    expect(httpClient.post).toHaveBeenCalledWith("/api/assets/asset-1/reaction", { reactionType: "Like" });
    expect(queryClient.getQueryData(["assets", "details", "asset-1"])).toMatchObject({ likeCount: 17, currentUserReaction: "Like" });
    for (const queryKey of [["home", "dashboard"], ["explore", "assets"], ["library", "saved"], ["assets", "my-assets"], ["profile"], ["assets", "editor", "detail", "asset-1"]])
      expect(invalidate).toHaveBeenCalledWith({ queryKey });
    await waitFor(() => expect(result.current.isPending).toBe(false));
  });

  it("clears selection when the server removes Useful feedback", async () => {
    vi.mocked(httpClient.post).mockResolvedValue({ assetId: "asset-1", likeCount: 0, totalCount: 0, currentUserReaction: null });
    const { result, queryClient } = setup();
    await act(async () => { await result.current.mutateAsync({ assetId: "asset-1", reactionType: "Like" }); });
    expect(queryClient.getQueryData(["assets", "details", "asset-1"])).toMatchObject({ likeCount: 0, currentUserReaction: undefined });
  });

  it("leaves confirmed data unchanged after a failed request", async () => {
    vi.mocked(httpClient.post).mockRejectedValue(new Error("Unavailable"));
    const { result, queryClient, invalidate } = setup();
    await act(async () => { await expect(result.current.mutateAsync({ assetId: "asset-1", reactionType: "Like" })).rejects.toThrow("Unavailable"); });
    expect(queryClient.getQueryData(["assets", "details", "asset-1"])).toMatchObject({ likeCount: 4 });
    expect(invalidate).not.toHaveBeenCalled();
  });
});
