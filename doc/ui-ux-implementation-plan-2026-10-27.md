# UI/UX implementation plan - 2026-10-27

## Completed implementation

- Audit P1 discoverability: Admin/Staff Classes and Grade reports links; Teacher Grade reports link. No teacher link to forbidden class index. Server guards unchanged.
- Audit P1 truthful controls: class detail receives server-derived canManage; management forms/actions hidden for Teacher; read-only explanation shown.
- Audit P1/P2 layout: native button menu toggle with aria-controls/expanded, Escape closes and returns focus; narrow navigation collapsed until activated; wrapped desktop links, stacked filters/header actions, contained table scroll, visible focus.
- Audit P2 CSS delivery: Vite manifest enabled; view helper emits entry CSS links from manifest, preventing stale hash selection. Build output retained.
- Audit P1 accessibility: academic search labels and student-specific score/max/comment names; person detail h1 and contextual people-list action labels.
- Audit P2 feedback: one Clear all filters action, descriptive Save search in URL label preserving existing GET contract; status/visibility blank options clarified as disabled defaults rather than duplicate choices; permission heading and safe dashboard/sign-in recovery; receipt ledger return when student ID exists; HTML editing guidance uses text only and existing Preview.
- Existing CSRF and Windows session-store teardown fixes preserved. No new dependencies or schema changes.

## Deferred with reasons

- Teacher class landing: existing class index is Admin/Staff only; no assignment-scoped listing contract established. Requires parent/product decision before changing business permissions.
- Filter persistence beyond existing search URL: status/grade/active filters are client-only under current contract. No new query semantics invented.
- Full visual/keyboard/responsive certification: independent Terra medium QA stage must inspect audit matrix at390x844,768px,1280px, menu open/close/Escape/focus, teacher controls, CSS/network load, forms/filters and recovery. No comprehensive WCAG claim.
- Demo credential/homepage copy: environment/product policy decision, outside bounded functional improvements.

## Validation

Targeted EJS role-navigation and toggle semantics tests added. Final full suite/build outcomes reported in task response. No commit/push/deploy. Local synthetic server remains available at localhost31987; restart required to load changed server code. Audit inventory remains in ui-ux-audit-2026-10-27.md.

QA follow-up fixes: list accessible names now derive from person.firstName/lastName with fallback; mobile module filter grid uses a constrained single column with shrinkable controls; shared content/table containers constrained; Escape listener applies to toggle and navigation. Behavioral name/menu regressions added. Await independent responsive recheck; no QA pass claimed.

Latest QA fixes: EventBoard mobile filters constrained to single-column grid and shrinkable controls; teacher ClassDetail now renders authorized name/code/grade/homeroom/description readonly and hides student search/results. Synthetic studentID5 confirmed present by isolatedDB readonly query; no deletion attempted. Await Terra browser recheck.
