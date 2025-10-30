# School Queens — Phase 1 Bootstrap

This repository contains the in-progress migration of School Queens to a modern Express + Prisma stack.
Phase 1 focuses on foundational plumbing: database schema, authentication, sessions, and the initial
layout shell.

## Getting Started

1. **Install dependencies**
   ```bash
   npm install
   ```
2. **Configure environment variables**
   ```bash
   cp .env.example .env
   # Update SESSION_SECRET, CSRF_SECRET, etc. as needed
   ```
3. **Apply database migrations & seed data**
   ```bash
   npx prisma migrate dev
   npm run db:seed
   ```
   The default SQLite database is stored at `./dev.db`. The seed now loads full lookup tables (grade levels, statuses, genders)
   along with demo student/teacher records linked to the default accounts, representative fee ledger entries, and a starter
   library catalog with sample copies and loans. Set the `SEED_*` environment variables before running the command if you want
   to override default passwords.
4. **Start the server**
   ```bash
   npm run dev
   # or npm start
   ```

Visit [http://localhost:3000](http://localhost:3000) to load the app.

## Default Credentials

All users are created via the Prisma seed script. The teacher and student accounts are linked to the new profile records so you
can exercise the related tables immediately.

| Role    | Email                 | Password         |
|---------|-----------------------|------------------|
| Admin   | `admin@example.com`   | `ChangeMe123!`   |
| Teacher | `teacher@example.com` | `TeacherPass123!`|
| Staff   | `staff@example.com`   | `StaffPass123!`  |
| Student | `student@example.com` | `StudentPass123!`|

Use the admin account to explore the authenticated dashboard. Passwords are hashed with bcrypt and
can be overridden via the `SEED_ADMIN_PASSWORD`, `SEED_TEACHER_PASSWORD`, `SEED_STAFF_PASSWORD`, and `SEED_STUDENT_PASSWORD`
environment variables before seeding.

The seed script also inserts core lookup data (genders, student statuses, grade levels, staff types, and employment statuses) and
multiple sample person/student/teacher records tied to the new relational models so that list, detail, and edit views have
representative fixtures.

## Resetting Development Data

The quickest way to return to a clean development database is:

```bash
npm run db:reset
```

The command drops the SQLite database, reapplies migrations, and re-runs the Prisma seed script. Use it whenever you change
lookups or want to reload the demo students/teachers. If you override any `SEED_*` passwords or set a custom
`DATABASE_URL`, export those values before calling the reset script.

## Key Scripts

- `npm run dev` — Start the server with `nodemon`.
- `npm run db:seed` — Seed roles and default users via Prisma.
- `npm run db:reset` — Reset the database (drops and re-applies migrations, then re-seeds).
- `npm run prisma:studio` — Inspect data using Prisma Studio.

## Security Middleware

The Express app enables `helmet()` globally to provide sensible HTTP security headers for every route, including the new
student and teacher modules. CSRF protection is handled via `csurf`, which issues a per-request token that controllers
expose to the rendered templates. Set `SESSION_SECRET` (and `CSRF_SECRET` if you want deterministic tokens) for each
environment so sessions and form submissions remain protected. Because the guards run before the routers, unauthenticated
or unprivileged users are redirected or served a `403` response prior to any controller logic executing.

## Operations & Monitoring

HTTP access logs are handled by `morgan`. In production mode the app writes to `var/data/logs/access.log` (or the directory
pointed to by `LOGS_DIR`). Ensure that `APP_DATA_DIR`, `SESSION_STORE_DIR`, and `LOGS_DIR` resolve to persistent storage in
deployed environments so session and log data survive restarts. From there you can forward the access log to your preferred
observability stack. The defaults work out-of-the-box for development and are configurable through the environment without
code changes.

## Manual QA

For end-to-end verification steps covering create, edit, and list flows, see [`doc/manual-qa-checklist.md`](doc/manual-qa-checklist.md).

## Library Module

The Library workspace (available to Admin and Staff roles) introduces searchable catalog management and loan tracking:

- Browse or filter the catalog by keyword or category, with availability counts for each title.
- Add new books with metadata, register additional copy barcodes, and archive outdated entries.
- Issue loans after selecting an available copy and locating a borrower via the shared student search, and mark loans as
  returned once books come back.
- Review recent library activity from the dashboard or from each book’s detail page.

After running `npm run db:seed`, sign in as `staff@example.com` to explore the module with the seeded titles and loans.

## Testing Authentication Flow

1. Navigate to `/auth/login` and sign in with the admin credentials.
2. Upon success you should land on `/dashboard` and see placeholder cards for future modules.
3. Use the "Sign out" button in the header to end the session (CSRF protected).
4. Attempting to access `/dashboard` while logged out redirects you back to the login screen with a
   friendly flash message.

## Next Steps

Future phases will build on this foundation by layering in real domain modules (attendance, finance,
communications) and expanding automated test coverage. See [`doc/phase-1-detailed-plan.md`](doc/phase-1-detailed-plan.md)
for the broader migration roadmap.
