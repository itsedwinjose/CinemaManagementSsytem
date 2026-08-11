# Codex Project Instructions — Cinema Ticketing System

## Mandatory Reading Order

Before changing or creating code, read:

1. `SRS.md`
2. `SCREEN_IMPLEMENTATION_SPEC.md`
3. `DESIGN.md`
4. Database schema/migrations

These documents are the source of truth.

## Design File Rule

`DESIGN.md` is mandatory and must remain in the repository root.

Never treat the visual design as implicit knowledge.

Any approved UI/design change must update:
- `DESIGN.md`
- centralized theme/style code
- affected screens

Do not implement a visual change in one form only unless it is explicitly a screen-specific exception documented in `DESIGN.md`.

## Architecture

Use:
- C#
- .NET
- Windows Forms
- MySQL 8+
- MySqlConnector
- direct network database access

No API.
No web backend.
No browser frontend.

## Code Organization

Separate:
- UI
- domain/business rules
- data access
- printing

Do not put SQL or complex business logic directly inside form event handlers.

## Database

Use parameterized SQL.

Use MySQL transactions for booking.

Lock selected screening seats before confirming a booking.

Physical seat damage belongs to the Audi layout cell.

Screening seat status belongs to the screening.

Never confuse these two levels.

## Booking

Never trust the UI's seat state alone.

Before booking:
- start transaction
- lock seats
- re-read status
- verify physical seat is not damaged
- calculate price
- save booking
- save booking seats
- save payment
- update screening seats
- commit

Rollback on failure.

## Pricing

Seat class is defined by physical Audi layout.

Show pricing is configured per class.

Historical booking price/class/charges must be stored in booking records.

Never recalculate historical tickets using current configuration.

## Printing

Keep printer code separate from booking code.

Customer ticket and Cashout Report must have separate print templates.

Use Epson/ESC-POS compatible printing.

Reprint must never create a new sale.

## UI

Use the centralized design system.

Do not introduce modern UI patterns.

Do not hard-code colors.

Use `DESIGN.md` and the theme classes.

## Testing

After each implementation phase:
- compile
- run tests
- fix errors
- continue only after the current phase is stable

Test:
- seat pricing
- reservation charges
- 3D charges
- free tickets
- damaged seats
- concurrent booking
- payment reconciliation
- cashout eligibility
- print/reprint logic

## Change Discipline

Do not rewrite working modules unnecessarily.

When a requirement changes:
1. update the relevant specification
2. update `DESIGN.md` if visual
3. update database migration if data-related
4. implement the smallest correct change
5. run regression tests
