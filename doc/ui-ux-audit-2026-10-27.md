# UI/UX audit - 2026-10-27

## Method and limits

Audit only; no UI implementation. Inspected routes, guards and templates and browsed the user's Edge extension against isolated synthetic SQLite data. Desktop and selected 390 x 844 layouts were observed. Findings below distinguish observation from recommended investigation. The audit is not a full functional, responsive, keyboard, screen-reader or WCAG certification. Screenshots were viewed but not saved. No live data or external services were used.

## Prioritized findings

### P1: Academic work is difficult to discover

Admin and staff navigation lacks Classes and Grade reports, although `/classes` and `/reports/grades` load for both. Teacher navigation has only Home, Dashboard, Notices and Events. A teacher can open `/classes/1` and its grade-entry page directly but receives 403 at `/classes`; there is no visible class-list route for that role. Users need a known URL to reach important workflows. Add role-aware academic/report navigation and a suitable teacher class landing, retaining server authorization. Review `src/views/partials/nav.ejs`, `src/routes/classRoutes.js`, `src/client/academic/`.

### P1: Teacher sees actions that the server rejects

On `/classes/1`, a teacher sees editable class fields, Save changes, Archive, subject assignment and Remove controls. Clicking Save unchanged returned `Unable to update class: Forbidden`. Hide unauthorized mutations or show an explicitly read-only view and permission-specific explanation; preserve server guards. Review `src/client/academic/components/ClassDetailApp.vue`, `src/controllers/classController.js`, `src/routes/classRoutes.js`.

### P1: Narrow navigation and form layout

At 390 x 844, the expanded vertical navigation takes approximately 490 px before content on Students, Classes and Create event. Student filter labels and native controls crowd together; the event heading and back action wrap heavily. Add compact, accessible navigation and stacked, spaced controls; test tables and action targets at narrow widths. Review `src/views/partials/nav.ejs`, `src/public/styles/main.css`, module component styles.

### P1: Accessible names missing in academic workflows

Edge's accessibility tree shows two unnamed search fields on `/classes`; grade-entry score/max steppers and comment fields lack student-specific names. Add explicit accessible labels including the student and field; verify table header associations and keyboard/screen-reader use. Review `src/client/academic/components/ClassManagementApp.vue` and `GradeEntryApp.vue`.

### P2: Inconsistent control styling and CSS delivery

Desktop Students filters appeared as touching labels and native controls; narrow layout crowds them further. Vite emits hashed CSS assets, while `src/views/layouts/base.ejs` includes main.css plus optional styles and the student controller supplies a script tag. Verify generated module CSS is actually loaded and referenced correctly before attributing all appearance to design. Standardize spacing, focus and control styles. Review `vite.config.js`, `src/views/layouts/base.ejs`, `src/controllers/studentController.js`, `src/client/students/`, shared styles.

### P2: Filters and status choices are confusing

Student and teacher lists show Clear search, Reset filters and Apply on server, while search changes results immediately. In a student no-match state, Reset filters appeared disabled despite active search, while Clear search was available. Notice/event status selects include both Draft and DRAFT; event visibility includes Internal and INTERNAL. Make application/reset behavior predictable and choices unique with readable labels. Review `src/client/students/`, `src/client/teachers/`, `src/client/communications/`.

### P2: Error, task context and accessibility copy

403 uses the heading `Something went wrong` even though the explanation is permission-specific; CSRF expiry has a friendly message and Return home but no direct sign-in recovery. Person detail names appear as plain text rather than h1 in the accessibility tree; repeated View/Edit/Delete actions lack row-specific names. Receipt links back to Fees rather than the originating ledger. Notice/event editing exposes raw seeded HTML in a textarea. Use condition-specific headings/actions, contextual control labels and a clear content-editing model. Review `src/views/pages/error.ejs`, auth views, `src/client/students/`, `src/client/teachers/`, `src/views/pages/fees/receipt.ejs`, `src/client/communications/`.

### P3: Development copy and demo credentials

Login displays default admin credentials and points to an onboarding guide; home describes the product as being built and links to a migration plan. These may be intentional in development. Review environment-specific guidance and replace roadmap copy with task-oriented content where appropriate. Review `src/views/pages/auth/login.ejs`, `src/views/pages/home.ejs`.

## Page and state coverage matrix

Viewed means rendered and inspected in Edge, not exhaustive testing. IDs are synthetic seeded examples. All page families were inventoried from route files. Roles are code guards; observations are called out separately.

| Page / route | Roles | Observed / remaining |
| --- | --- | --- |
| `/`, `/auth/login` | Guest; home also signed in | Guest and authenticated home and login viewed desktop. One Tab from login focused Home; full keyboard flow not tested. |
| `/dashboard` | Signed in | Admin, staff, teacher and student views observed desktop. |
| `/students` | Admin/Staff | Populated and no-match states observed; desktop and narrow. |
| `/students/create`, `/students/:id`, `/students/:id/edit` | Admin/Staff | Admin desktop create/detail/edit; required-field validation, cancel/back and synthetic CRUD/reload observed in earlier QA. |
| `/teachers` | Admin/Staff | Populated and no-match states observed desktop; staff list also viewed. Narrow not inspected. |
| `/teachers/create`, `/teachers/:id`, `/teachers/:id/edit` | Admin/Staff | Admin seeded create/detail/edit viewed desktop. Submission and validation not audited. |
| `/classes` | Admin/Staff | Admin desktop and narrow; staff desktop. Teacher forbidden. Inline create/edit not submitted. |
| `/classes/:id` | Admin/Staff/Teacher | Admin and teacher desktop. Teacher Save rejected 403. Archive, assignment and enrollment states not exercised. |
| `/classes/:id/subjects/:classSubjectId/grades` | Admin/Staff/Teacher | Populated admin and teacher desktop. Empty assessment, save and validation not exercised. |
| `/reports/grades` | Admin/Staff/Teacher | Admin, staff and teacher populated desktop. Filter/empty state not exercised. |
| `/fees`, `/fees/ledger/:studentId`, `/fees/receipt/:receiptNumber` | Admin/Staff | Admin populated desktop; ledger form and seeded receipt viewed. Search empty, payment/adjustment variants and print not exercised. |
| `/library`, `/library/books/:bookId` | Admin/Staff | Admin populated desktop; expanded Add book and seeded book edit/loan/copy forms viewed. Search empty, save and loading not exercised. |
| `/notices`, `/notices/:id` | Admin/Staff/Teacher/Student read | Admin populated list/detail desktop; role-specific lists and empty states not fully inspected. |
| `/notices/create`, `/notices/:id/edit` | Admin/Staff | Admin create/edit and staff create viewed. Empty submit focused required title with native validation; teacher create forbidden. Submission not audited. |
| `/events`, `/events/:id` | Admin/Staff/Teacher/Student read | Admin list/detail and RSVP form desktop; role-specific RSVP and empty state not audited. |
| `/events/create`, `/events/:id/edit` | Admin/Staff | Admin create/edit desktop, create narrow. Submission/validation not audited. |
| Unknown path / forbidden / CSRF error | Varies | 404, student `/students` 403, teacher `/classes` and notice-create 403 viewed. Earlier stale CSRF form produced friendly 403 and fresh login recovered; missing token covered by automated request test, not Edge. |
| No-JS fallback templates | Varies | Inventoried in `src/views/pages/**/_*-fallback.ejs`; not viewed. No browser security settings changed. |
| Loading, server/network failure, other breakpoints, zoom, screen readers | Varies | Not induced or assessed. |

Non-page GET endpoints: `/students/api/search`, `/teachers/api/search`, `/classes/:id/enrollment/search`, `/reports/grades/data`, `/fees/api/students`, `/library/api/books`, `/library/api/students`, `/library/api/loans/recent`. POST routes are actions rather than distinct pages; inspect route files for exact guards.

## Follow-up environment

Synthetic database: `C:/Users/ADMIN/Documents/Codex/2026-10-27/task/nsms-qa.db`; session data: `C:/Users/ADMIN/Documents/Codex/2026-10-27/task/nsms-qa-data`; local server: `http://localhost:31987`. No credentials reproduced here. Existing uncommitted CSRF and test fixes in `src/app.js` and `tests/app.test.js` were preserved. No commit, push or deploy.
