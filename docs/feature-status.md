# Feature status

## Available without login

Home dashboard; explore/search/filter assets; asset details and Markdown preview;
publish, edit, and delete guest-owned assets; shared library/bookmarks; comments
and reactions; profile; in-app notifications and SignalR.

The guest workspace is shared across visitors. It is suitable for public/demo
content, not private data or per-user isolation. Existing application validation
and ownership rules still apply; ordinary guests are not administrators.

## Intentionally skipped

- Login: pending adding an SMTP account and completing account flows.
- Email notifications: pending adding an SMTP account. The registered no-op sender
  returns successfully and performs no outbound network call.
- Email update/verification: reports `skipped`, does not change an address or mark
  it verified. Password-reset email requests are also skipped.

## Existing unfinished work

Several account and admin methods remain scaffold placeholders in this codebase:
password registration/login, password reset completion, password change, account
deletion, admin user/asset/comment management, and authorization service helpers.
The normal navigation does not expose those account flows. Admin routes continue
to require administrator access; they are not part of the public guest workspace.
Completing these features is separate from configuring an SMTP account.

SQL Server and schema migrations remain required. The source ships no database
backup or populated organization knowledgebase.
