# Repository Guidelines

## Project Structure & Module Organization
The application lives under `src/`, split into `config/`, `controllers/`, `routes/`, `middleware/`, `views/`, and `public/`. Prisma schema and migrations reside in `prisma/`, while shared documentation (including Phase plans) is inside `doc/`. Runtime data (SQLite sessions, logs) defaults to `var/data/` and is created automatically. Static assets, such as base styling, are stored in `src/public/styles/`.

## Build, Test, and Development Commands
- `npm install` – install all runtime and dev dependencies.
- `npm run dev` – start the Express server with nodemon reloading on file changes.
- `npm start` – run the production server entry (`src/server.js`).
- `npm run prisma:generate` – regenerate the Prisma client after schema changes.
- `npm run prisma:migrate` – apply pending migrations to the local SQLite database.
- `npm run prisma:studio` – open Prisma Studio for inspecting seed and development data.
Add new scripts for tests or linting once those stacks are introduced.

## Coding Style & Naming Conventions
Code currently targets Node.js CommonJS. Use two-space indentation for JS/EJS and keep files ASCII unless localization is required. Directory and file names are lowercase kebab-case (e.g., `homeController.js`, `phase-1-detailed-plan.md`). Favor descriptive function names (`getHome`, `ensureAuthenticated`) and short, purposeful comments when logic is non-obvious. When introducing tooling, align with `eslint --fix` and Prettier defaults.

## Testing Guidelines
No automated test harness is committed yet; Phase 1 focuses on scaffolding and authentication. When adding tests, place unit/integration suites under `tests/` mirroring the source tree, use Jest or Supertest for HTTP flows, and name files `*.spec.js`. Ensure critical auth workflows (login, CSRF handling, role guards) are covered and document the commands in this file.

## Commit & Pull Request Guidelines
Follow the existing history: concise, imperative commit subjects (`Add auth seed script`) with optional explanatory bodies. Reference issue IDs when applicable. For pull requests, include:
- Summary of the change and affected modules.
- Setup notes (migrations, seeds, env vars).
- Screenshots or terminal output when touching UI or CLI behavior.
Run `npm run dev` locally to verify startup before submitting, and mention completed manual checks in the PR description.

## Security & Configuration Tips
Keep secrets out of version control—only `.env.example` is tracked. Update it whenever a new configuration key is introduced. Use separate `.env` files per environment and regenerate Prisma migrations after schema edits to maintain SQLite/MySQL parity.				   	
