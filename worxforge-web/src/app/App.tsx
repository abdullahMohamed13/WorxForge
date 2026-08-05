import { MemoryRouter, RouterProvider } from "react-router-dom";
import { AppProviders } from "../providers/AppProviders";
import { appRouter } from "../routes/appRouter";
import { AssetsDashboard } from "../features/assets/components/AssetsDashboard";
import type { AssetSummary } from "../features/assets/models/assets.models";

// TEMP preview data for the AssetsDashboard quick test; remove once reviewed.
const previewAssets: AssetSummary[] = [
  {
    assetId: "preview-1",
    title: "Prompt Library",
    description:
      "A curated set of prompts to classify, summarize, and draft responses for incoming support tickets.",
    assetTypeName: "Prompt",
    domainName: "Customer Support",
    tools: ["ChatGPT"],
    authorDisplayName: "Jordan Lee blah blah",
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString(),
    reactionCount: 24,
    commentCount: 5,
    isBookmarked: false,
  },
  {
    assetId: "preview-2",
    title: "Sales Workflow",
    description: "Automates transcription, summarization, and CRM logging for sales calls.",
    assetTypeName: "Workflow",
    domainName: "Sales",
    tools: ["Gemini", "Salesforce"],
    authorDisplayName: "Morgan Ade",
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString(),
    reactionCount: 12,
    commentCount: 2,
    isBookmarked: true,
  },
  {
    assetId: "preview-3",
    title: "Onboarding Generator",
    description: "Generates tailored onboarding checklists for new hires by role and department.",
    assetTypeName: "Template",
    domainName: "HR",
    tools: ["ChatGPT", "Notion"],
    authorDisplayName: "Sam Rivera",
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString(),
    reactionCount: 8,
    commentCount: 1,
    isBookmarked: false,
  },
  {
    assetId: "preview-4",
    title: "Bug Report Triage Assistant",
    description: "Classifies incoming bug reports by severity and routes them to the right team.",
    assetTypeName: "Prompt",
    domainName: "Engineering",
    tools: ["Claude", "Jira"],
    authorDisplayName: "Priya Nair",
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString(),
    reactionCount: 31,
    commentCount: 9,
    isBookmarked: false,
  },
];

export function App() {
  // TODO: Implementation
  return (
    <AppProviders>
      <div className="p-6">
        <MemoryRouter>
          <AssetsDashboard assets={previewAssets} />
        </MemoryRouter>
      </div>
      <RouterProvider router={appRouter} />
    </AppProviders>
  );
}

