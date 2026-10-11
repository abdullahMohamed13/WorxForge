import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import { beforeEach, describe, expect, it, vi } from "vitest";
import { AssetDetailsPage } from "./AssetDetailsPage";
import { useAssetDetails } from "../hooks/useAssetDetails";
import { useBookmark } from "../hooks/useBookmark";
import { useAssetReaction } from "../hooks/useAssetReaction";
import { toast } from "sonner";

vi.mock("../hooks/useAssetDetails", () => ({
  useAssetDetails: vi.fn()
}));

vi.mock("../hooks/useBookmark", () => ({
  useBookmark: vi.fn()
}));

vi.mock("../hooks/useAssetReaction", () => ({
  useAssetReaction: vi.fn()
}));

vi.mock("../../comments/components/AssetCommentsSection", () => ({
  AssetCommentsSection: ({ assetId }: { assetId: string }) => (
    <div data-testid="comments-section">Comments section for {assetId}</div>
  )
}));

const assetFixture = {
  assetId: "asset-1",
  title: "Asset title",
  description: "Asset description",
  assetTypeName: "Prompt",
  domainName: "Engineering",
  tools: ["Copilot"],
  authorDisplayName: "Author",
  createdAt: "2026-08-01T00:00:00.000Z",
  updatedAt: "2026-08-02T00:00:00.000Z",
  commentCount: 3,
  isBookmarked: false,
  likeCount: 4,
  markdownContent: "# Header",
  useCaseName: "Review",
  compatibleTools: ["Copilot"],
  authorId: "user-1",
  viewCount: 10,
  isFollowing: false
};

function renderPage() {
  return render(
    <MemoryRouter initialEntries={["/assets/asset-1"]}>
      <Routes>
        <Route path="/assets/:assetId" element={<AssetDetailsPage />} />
      </Routes>
    </MemoryRouter>
  );
}

describe("AssetDetailsPage", () => {
  beforeEach(() => {
    vi.clearAllMocks();
    vi.mocked(useAssetDetails).mockReturnValue({ data: assetFixture, isLoading: false, isError: false } as never);
    vi.mocked(useBookmark).mockReturnValue({ mutate: vi.fn(), isPending: false } as never);
    vi.mocked(useAssetReaction).mockReturnValue({ mutate: vi.fn(), isPending: false } as never);
  });

  it("renders existing content and the Discussion tab", async () => {
    renderPage();
    expect(screen.getByRole("heading", { name: "Asset title" })).not.toBeNull();
    await userEvent.click(screen.getByRole("button", { name: "Discussion (3)" }));
    expect(screen.getByTestId("comments-section").textContent).toContain("asset-1");
  });

  it("shows one Useful action and count without negative feedback", async () => {
    const mutate = vi.fn();
    vi.mocked(useAssetReaction).mockReturnValue({ mutate, isPending: false } as never);
    renderPage();
    const useful = screen.getByRole("button", { name: "Useful" });
    expect(useful.getAttribute("aria-pressed")).toBe("false");
    expect(screen.getByText("4", { exact: true })).not.toBeNull();
    expect(screen.queryByRole("button", { name: /^(Like|Dislike)$/ })).toBeNull();
    expect(screen.queryByText(/not useful|helpful/i)).toBeNull();
    await userEvent.click(useful);
    expect(mutate).toHaveBeenCalledWith({ assetId: "asset-1", reactionType: "Like" }, expect.any(Object));
  });

  it("shows existing positive feedback as selected and allows removal", async () => {
    const mutate = vi.fn();
    vi.mocked(useAssetDetails).mockReturnValue({ data: { ...assetFixture, currentUserReaction: "Like" }, isLoading: false } as never);
    vi.mocked(useAssetReaction).mockReturnValue({ mutate, isPending: false } as never);
    renderPage();
    const useful = screen.getByRole("button", { name: "Useful" });
    expect(useful.getAttribute("aria-pressed")).toBe("true");
    await userEvent.click(useful);
    expect(mutate).toHaveBeenCalledTimes(1);
  });

  it("blocks repeated clicks while feedback is saving", async () => {
    const mutate = vi.fn();
    vi.mocked(useAssetReaction).mockReturnValue({ mutate, isPending: true } as never);
    renderPage();
    const useful = screen.getByRole("button", { name: "Useful" });
    expect(useful.hasAttribute("disabled")).toBe(true);
    expect(useful.getAttribute("aria-busy")).toBe("true");
    await userEvent.click(useful);
    expect(mutate).not.toHaveBeenCalled();
  });

  it("keeps the confirmed count when saving fails and explains the failure", async () => {
    const showError = vi.spyOn(toast, "error").mockImplementation(() => "error");
    vi.mocked(useAssetReaction).mockReturnValue({
      mutate: vi.fn((_params, callbacks) => callbacks.onError(new Error("Unavailable"))), isPending: false
    } as never);
    renderPage();
    await userEvent.click(screen.getByRole("button", { name: "Useful" }));
    expect(screen.getByText("4", { exact: true })).not.toBeNull();
    expect(showError).toHaveBeenCalledWith("Failed to update Useful feedback. Please try again.");
  });
});
