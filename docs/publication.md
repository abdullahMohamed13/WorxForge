# Publication notes

The source includes the MIT license naming ITWORX as copyright holder. Retain the
copyright and permission notices when redistributing it; see [LICENSE](../LICENSE).

## Choose a clean publication source

The existing private clone retains earlier commits containing removed private
reference documents and development configuration. Do not make that history
public without sanitizing and auditing every published ref, including branches
and tags. Pushing sanitized files to the private remote does not clean old commits.
Any real credential or signing key previously used must be replaced at its service.

For a new public repository, publish only a reviewed source snapshot with a new
Git history. If using the prepared source ZIP, extract it into an empty directory
and initialize a new Git repository there. Do not copy the original `.git`
directory, private backups, local configuration, dependencies, or build output.

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

## Approved whitepaper

The approved public artifact is
[Juniors Can. Here’s the Code.](Whitepaper/WorxForge-Juniors-and-AI-Coding-Agents.pdf),
publication version **1.5**, **12 pages**. Keep its filename and location unchanged.

SHA-256: `e5f26cb7b45091b6bbf6860768aaad49a353a8f4d746f81e5863af7f5fedb840`.

Keep `docs/Whitepaper/` limited to this final PDF. Do not publish editable exports,
private authoring files, review notes, source requirements, the original training
workbook, internal academy links, or original screenshots. The approved PDF is
published as supplied; changes require a separately approved publication artifact.
