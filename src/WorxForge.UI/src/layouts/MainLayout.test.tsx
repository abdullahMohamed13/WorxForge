import { render, screen, waitFor, within } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import { describe, expect, it } from "vitest";
import { ROUTES } from "../constants/routes.constants";
import { MainLayout } from "./MainLayout";

function renderLayout(initialPath: string) {
  return render(
    <MemoryRouter initialEntries={[initialPath]}>
      <Routes>
        <Route path="/" element={<MainLayout />}>
          <Route path={ROUTES.HOME.slice(1)} element={<div>Home page</div>} />
          <Route path={ROUTES.EXPLORE.slice(1)} element={<div>Explore page</div>} />
          <Route path={ROUTES.MY_LIBRARY.slice(1)} element={<div>Library page</div>} />
          <Route path={ROUTES.MY_ASSETS.slice(1)} element={<div>My assets page</div>} />
          <Route path={ROUTES.MY_ASSETS_CREATE.slice(1)} element={<div>Create asset page</div>} />
          <Route path={ROUTES.MY_ASSETS_EDIT(":assetId").slice(1)} element={<div>Edit asset page</div>} />
          <Route path={ROUTES.NOTIFICATIONS.slice(1)} element={<div>Notifications page</div>} />
          <Route path={ROUTES.PROFILE_ME.slice(1)} element={<div>Profile page</div>} />
        </Route>
      </Routes>
    </MemoryRouter>
  );
}

async function waitForSidebarToggle(name: string): Promise<HTMLButtonElement> {
  let toggle: HTMLButtonElement | null = null;

  await waitFor(() => {
    toggle = screen.getByRole("button", { name }) as HTMLButtonElement;
    expect(toggle.disabled).toBe(false);
  }, { timeout: 3000 });

  return toggle!;
}

describe("MainLayout", () => {
  it("cycles desktop sidebar states with one unified toggle", async () => {
    const user = userEvent.setup();

    renderLayout(ROUTES.HOME);

    const collapseToggle = screen.getByRole("button", { name: "Collapse sidebar" });
    expect(screen.getByRole("link", { name: "Explore" })).not.toBeNull();
    expect(screen.queryByRole("dialog", { name: "Primary navigation" })).toBeNull();

    await user.click(collapseToggle);
    const closeToggle = await waitForSidebarToggle("Close sidebar");

    await user.click(closeToggle);
    const openToggle = await waitForSidebarToggle("Open sidebar");

    await user.click(openToggle);
    expect(await waitForSidebarToggle("Collapse sidebar")).not.toBeNull();
  }, 10000);

  it("activates Publish Asset without also activating My Assets on the create route", () => {
    renderLayout(ROUTES.MY_ASSETS_CREATE);

    const publishAssetLink = screen.getByRole("link", { name: "Publish Asset" });
    const myAssetsLink = screen.getByRole("link", { name: "My Assets" });

    expect(publishAssetLink.getAttribute("aria-current")).toBe("page");
    expect(myAssetsLink.getAttribute("aria-current")).toBeNull();
  });

  it("keeps navigation links accessible in collapsed desktop state", async () => {
    const user = userEvent.setup();

    renderLayout(ROUTES.HOME);

    const collapseToggle = await waitForSidebarToggle("Collapse sidebar");
    await user.click(collapseToggle);

    expect(await waitForSidebarToggle("Close sidebar")).not.toBeNull();
    expect(screen.getByRole("link", { name: "Home" }).getAttribute("aria-current")).toBe("page");
  });

  it("activates My Assets for the edit route without activating Publish Asset", () => {
    renderLayout(ROUTES.MY_ASSETS_EDIT("asset-42"));

    const publishAssetLink = screen.getByRole("link", { name: "Publish Asset" });
    const myAssetsLink = screen.getByRole("link", { name: "My Assets" });

    expect(publishAssetLink.getAttribute("aria-current")).toBeNull();
    expect(myAssetsLink.getAttribute("aria-current")).toBe("page");
  });

  it("opens the mobile drawer, focuses the close control, and closes on navigation", async () => {
    const user = userEvent.setup();

    renderLayout(ROUTES.HOME);

    await user.click(screen.getByRole("button", { name: "Open navigation menu" }));

    const dialog = screen.getByRole("dialog", { name: "Primary navigation" });
    const closeButton = within(dialog).getByRole("button", { name: "Close navigation menu" });

    expect(document.activeElement).toBe(closeButton);

    await user.click(within(dialog).getByRole("link", { name: "Explore" }));

    expect(screen.queryByRole("dialog", { name: "Primary navigation" })).toBeNull();
    expect(screen.getByText("Explore page")).not.toBeNull();
  });
});