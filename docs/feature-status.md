# Feature status

## Available without login

Home dashboard; explore/search/filter assets; asset details and Markdown preview;
publish, edit, and delete guest-owned assets; shared library/bookmarks; comments
and Useful feedback; profile; in-app notifications and SignalR.

The guest workspace is shared across visitors. Use public/demo content; it has no
per-user isolation. Visitors can edit/delete guest-owned assets. Existing application
validation and ownership rules still apply; ordinary guests are not administrators.

## Intentionally skipped

- Login: pending configuring an authentication provider and completing account flows.
- Email notifications: pending adding an SMTP account. The registered no-op sender
  returns successfully and performs no outbound network call. Enabling delivery
  requires implementing and registering an SMTP sender and configuring an account.
- Email update/verification: reports `skipped`, does not change an address or mark
  it verified. Password-reset email requests are also skipped.

## Existing unfinished work

Several account and admin methods remain scaffold placeholders in this codebase:
password registration/login, password reset completion, password change, account
deletion, admin user/asset/comment management, and authorization service helpers.
The normal navigation does not expose those account flows. Admin routes continue
to require administrator access; they are not part of the public guest workspace.
Completing these features is separate from SMTP integration and configuring an account.

SQL Server and schema migrations remain required. The source ships no database
backup or populated organization knowledgebase.

## Useful feedback

Assets have one public Useful action and count. Click Useful again to remove your
feedback. The API retains `Like`, `likeCount`, and `reactionCount` as compatibility
names; these represent positive Useful feedback only. New Dislike requests return
a validation error. Historical dislikes remain stored but are excluded from counts,
ranking, current-user selection, and notification lists/unread badges. Existing
positive feedback is preserved; no database migration or data deletion is required.
