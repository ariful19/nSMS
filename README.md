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
   The default SQLite database is stored at `./dev.db`. For a clean slate, run `npm run db:reset`.
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
sample person/student/teacher records tied to the new relational models.

## Key Scripts

- `npm run dev` — Start the server with `nodemon`.
- `npm run db:seed` — Seed roles and default users via Prisma.
- `npm run db:reset` — Reset the database (drops and re-applies migrations, then re-seeds).
- `npm run prisma:studio` — Inspect data using Prisma Studio.

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
