import { render, screen } from "@testing-library/react";
import { expect, it } from "vitest";
import { AssetCardStats } from "./AssetCardStats";

it("labels the shared asset card count as Useful and preserves comments", () => {
  render(<AssetCardStats reactionCount={3} commentCount={2} />);
  const counts = screen.getAllByRole("listitem");
  expect(counts[0].textContent).toBe("3 Useful");
  expect(counts[1].textContent).toBe("2 comments");
});
