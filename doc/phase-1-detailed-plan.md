# Phase 1 Detailed Plan — Project Bootstrap, Sessions, and Authentication

## Objectives
- Deliver a runnable Express app with persistent sessions backed by SQLite (per migration strategy).
- Implement secure, session-based user authentication (login, logout, role capture).
- Establish the primary server-rendered layout (header, footer, nav) and a post-login landing page.
- Provide developer ergonomics: seed data, environment samples, and documentation for kick-off.

## Scope & Deliverables
- **Database & Prisma**
  - Define Prisma models for `User`, `Role`, and `UserRole` (if many-to-many) plus timestamps.
  - Generate initial migration targeting SQLite (`prisma/migrations/...`) and ensure compatibility with MySQL (no SQLite-only features).
  - Seed script creating:
    - Admin role(s) and default admin user (`admin@example.com` / placeholder password).
    - Optional teacher/staff role definitions for future phases.
- **Authentication Flow**
  - Password hashing with bcryptjs (configurable cost factor).
  - Login page & controller (`GET /auth/login`, `POST /auth/login`).
  - Logout route (`POST /auth/logout`) clearing session & CSRF-protected.
  - Session middleware enhancements:
    - Store `userId`, `roleIds`, and a minimal profile payload.
    - Custom middleware: `ensureAuthenticated`, `ensureGuest`, `ensureRole([...roles])`.
  - CSRF integration for auth forms and error handling for bad tokens.
- **Layout & Navigation**
  - Shared EJS layout containing header with dynamic nav links based on role.
  - Flash messaging / feedback area (for login errors, success messages).
  - Authenticated landing page (`GET /dashboard`) showing placeholder cards for future modules.
  - Public-facing pages stay minimal; redirect authenticated users away from login page.
- **Tooling & Docs**
  - `.env.example` capturing required env vars (`PORT`, `DATABASE_URL`, `SESSION_SECRET`, etc.).
  - Update README or new doc entry with setup instructions (install, migrate, seed, run).
  - npm scripts:
    - `npm run db:reset` (drop & re-seed dev DB).
    - `npm run lint` placeholder (if lint config added) or note for later phase.
  - Basic Jest or supertest harness stub (optional early wiring for API tests).

## Workstreams & Sequence

1. **Data Model & Migrations**
   1. Draft Prisma schema (`User`, `Role`, `UserRole`):
      - Fields: `id`, `username`, `email`, `passwordHash`, `isActive`, `lastLoginAt`, metadata columns.
      - Enforce unique constraints (email/username).
   2. Run `prisma migrate dev --name init-auth` (creates SQLite `dev.db`).
   3. Implement seed script (`prisma/seed.ts` or `src/scripts/seed.js`) invoked via `prisma db seed`.
   4. Document MySQL compatibility notes (e.g., column types) for later phases.

2. **Session & Auth Infrastructure**
   1. Extend `src/config/index.js` with session cookie durations, bcrypt cost configs, redirect URLs.
   2. Create middleware modules:
      - `src/middleware/ensureAuth.js`
      - `src/middleware/ensureGuest.js`
      - `src/middleware/ensureRole.js`
      - `src/middleware/attachUser.js` (loads user entity into `res.locals` from Prisma).
   3. Build auth controller (`src/controllers/authController.js`):
      - `showLogin`, `handleLogin`, `handleLogout`.
   4. Add auth routes module (`src/routes/authRoutes.js`) and mount under `/auth`.
   5. Handle login failures gracefully (flash or inline messaging) without revealing password validity.
   6. Update CSRF handling to skip token requirement on safe methods only.

3. **Views & Layout**
   1. Upgrade `layouts/base.ejs` with nav placeholders for Admin/Teacher/Staff once roles exist.
   2. Create partials:
      - `partials/alerts.ejs` (flash messages).
      - `partials/nav.ejs` with conditional links.
   3. Build `views/pages/auth/login.ejs` form (username/email + password, hidden csrf).
   4. Build `views/pages/dashboard/index.ejs` placeholder with summary sections.
   5. Ensure layout pulls in `res.locals.csrfToken`, `currentUser`, `isAuthenticated`.

4. **Quality & Documentation**
   1. Write integration test skeleton (if testing added) covering login happy path + unauthorized redirect.
   2. Update `README.md` (or new doc) with:
      - Installation, migration, seeding instructions.
      - Default admin credentials.
      - Phase 1 feature summary & next steps.
   3. Verify `npm run start` & `npm run dev` operate with seeded data.
   4. Record manual test checklist in docs (login success/failure, CSRF error, session persistence).

## Dependencies & Inputs
- Migration roadmap document (this repo). Align field names with legacy schema when possible.
- Confirmation of required initial roles (Admin, Teacher, Accountant, etc.).
- Password policy (length, complexity) — default to 8+ chars until specified.
- Branding / theme guidelines (placeholder styling acceptable now).

## Definition of Done
- App launches locally, login/logout works using seeded admin credentials.
- Auth-protected routes redirect unauthenticated users to login.
- Layout displays authenticated state and includes nav placeholders.
- CSRF protection enforced on form submissions with friendly error message.
- Documentation `.env.example` + instructions allow new developer to get running in <10 minutes.
- Prisma migration + seed checked into repo; `prisma migrate dev` succeeds on clean environment.

## Risks & Mitigations
- **Password handling**: use bcryptjs (no native build) to avoid tooling friction.
- **Session store file locking**: ensure store path resides on writable volume (`var/data/sessions`). Add troubleshooting note.
- **CSRF errors confusing users**: provide dedicated error message and re-render login form with a fresh token.
- **Future MySQL migration**: avoid SQLite-specific features (e.g., `AUTOINCREMENT` w/out `INTEGER PRIMARY KEY`). Leverage Prisma defaults to maintain compatibility.

## Open Questions
- Will the system need "remember me" functionality in Phase 1? (Default: no.)
- Any additional public pages required pre-authentication?
- Should we enable rate limiting on login attempts in Phase 1, or defer to a later hardening phase?
