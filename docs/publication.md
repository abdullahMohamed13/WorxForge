# Publication notes

The source includes the MIT license naming ITWORX as copyright holder. Retain the
copyright and permission notices when redistributing it; see [LICENSE](../LICENSE).

## Keep publication material public-safe

Review source and documentation for credentials and private data before publishing.
Keep local configuration, backups, dependencies, and build output out of public
commits. Ignoring or deleting a file does not remove it from earlier commits.
Replace any exposed credential at its service.

## Review the snapshot

1. Check the final file list and content for credentials, account details,
   private documents, and organization knowledge assets.
2. Keep real settings in User Secrets, environment variables, or ignored local
   files. Sanitized appsettings and `.env.example` are public templates.
3. Keep private backups, source archives, validation tools, logs, database exports,
   dependencies, and build outputs outside the published source.
4. Run the backend/frontend checks and dependency audits listed in the
   [README](../README.md#validation). Record any remaining skipped tests or
   unfinished features; see [feature status](feature-status.md).
5. Configure a fresh database with public/demo content. Every visitor shares the
   guest identity, including the ability to edit/delete guest-owned assets.
6. Check the repository description uses the WorxForge branding and that the
   README, license, and setup instructions are included.

The ignore rules prevent future additions of known private/local files. They do
not untrack already committed files or erase Git history. Inspect both the
published snapshot and any history you intend to retain.

## Source publication and app hosting

Publishing the source does not deploy the application. Hosting requires SQL Server,
schema migrations, an API connection string, a frontend origin in CORS, HTTPS,
and routing for the frontend/API/SignalR endpoints. Guest access is enabled by
default in every environment; authentication and account completion are separate
work. Email delivery remains a successful no-op until an SMTP sender is implemented
and registered and an account is configured.

## Approved case-study materials

[Juniors Can. Here’s the Code.](Whitepaper/WorxForge-Juniors-and-AI-Coding-Agents.pdf)
is the approved whitepaper, publication version **1.6**, **11 pages**.
Keep its filename and location unchanged.

SHA-256: `3b69691ec9b8743492999fa57beae632e294691c49028074fe838e6ef21afd79`.

The [eight-page case summary](Whitepaper/WorxForge-LinkedIn-Carousel.pdf) is the
approved carousel.

SHA-256: `3d04a726417ae1d277748f6cbf3c5aa4667592bcb4a181ad1853be93e008c48f`.

Public narrative concerns only the four-student, four-week ITWorx Summer 2026
internship: the students' AI-assisted project work, technical mentorship, local
demonstration, and evidence limits. Keep the case scoped to this cohort and
application. Use provider-neutral coding-agent references, including figures.

The mentor's estimate of about two months or more concerns comparable learning
with all code written manually. Attribute it as a retrospective estimate, not a
measured speed comparison, productivity multiplier, or guaranteed learning outcome.
Distinguish the historical demonstration from the current public release; it does
not establish current feature completeness or production readiness.

Keep `docs/Whitepaper/` limited to these two finished, approved PDFs. Do not publish
editable exports, authoring scripts, private source documents, review notes, logs,
rendered images, original screenshots, or backups. Publish the approved files
unchanged; revisions require separately approved publication artifacts.
