# Publication notes

The source tree is prepared for a new public knowledgebase repository. The proposed
MIT license names ITWORX as copyright holder and requires retaining its copyright
and permission notices. Review that proposed license before publishing.

Publish a clean source snapshot with a new Git history. Do not publish the original
private clone's `.git` directory or historical branches: previous commits can retain
removed development configuration and private reference documents. If retaining
history is required, sanitize every published ref and audit it separately first.
Any previously used real signing key or credential must be replaced at its service.

The clean source archive excludes Git metadata, local environment files, User Secrets,
dependencies, build outputs, and removed private reference documents. Keep private
backups outside the public repository. Configure a fresh database with demo/public
content rather than exporting an existing knowledgebase.

Before publication, run backend build/tests with the pinned SDK, frontend lint/tests/
build, and dependency audits. Inspect the final source snapshot for credentials and
private data. Known validation limitations are recorded in the handoff report.

The ignore rules exclude private/local files and the removed reference paths from
future additions. They do not untrack existing files or erase earlier Git history.
Sanitized appsettings and `.env.example` remain public configuration templates;
keep real values in User Secrets, environment variables, or ignored local files.

Pushing updates to the existing private remote retains its historical commits.
Publishing publicly requires the separate clean-history step described above.
