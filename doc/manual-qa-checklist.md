# Manual QA Checklist

Use this guide when validating student, teacher, and library management flows after applying migrations or updating seeds.

## Pre-flight

- [ ] Run `npm run db:reset` to load the lookup data plus demo records.
- [ ] Start the app with `npm run dev` and sign in as `admin@example.com`.

## Students module

- [ ] Navigate to `/students` and confirm the list renders multiple demo students (look for "Sara Nguyen", "Luis Martinez", and "Mira Patel").
- [ ] Click "Add student" and submit an empty form—verify inline validation errors for first name, last name, and enrollment status.
- [ ] Fill out the form with valid data (use a unique student number) and submit; you should be redirected to the student detail page with a success flash message.
- [ ] From the detail page, click "Edit", update a field (e.g., preferred name), and ensure the change appears after saving.
- [ ] Optionally trigger the delete flow and confirm the record is removed from the list and a success message appears.

## Teachers & staff module

- [ ] Navigate to `/teachers` and confirm the list shows the seeded examples ("Daniel Okafor", "Amina Chowdhury", "Noah Garcia").
- [ ] Test the "Add teacher or staff member" form with empty fields to verify validation errors for first name, last name, staff type, and employment status.
- [ ] Submit a valid record (unique employee number) and confirm the redirect to the detail page along with a success flash.
- [ ] Edit the record to update the primary subject or employment status and verify the change persists.

## Library module

- [ ] Sign in as `staff@example.com` and navigate to `/library`; confirm the catalog list shows seeded titles with availability counts.
- [ ] Use the catalog search to filter by a keyword (e.g., "Gatsby") and verify only matching books remain.
- [ ] Click "Add book", submit an empty form to confirm validation, then create a new book and ensure it appears in the search results.
- [ ] From the catalog, open a book detail page, add a new copy, and verify it appears in the inventory table.
- [ ] Use the "Issue a loan" form to select the new copy, search for a borrower, and record a loan; confirm it appears in the active loans table.
- [ ] Mark the loan as returned and check that it moves out of the active list while appearing in the recent activity log.

## Monitoring & logging expectations

- [ ] Confirm `APP_DATA_DIR`, `SESSION_STORE_DIR`, and `LOGS_DIR` point to writable locations (production should use persistent storage).
- [ ] In production mode, verify that `var/data/logs/access.log` (or your configured `LOGS_DIR`) receives `morgan` entries for the interactions above.
- [ ] Ensure your log shipping or monitoring solution captures the access log and any unexpected 4xx/5xx responses during the QA pass.
