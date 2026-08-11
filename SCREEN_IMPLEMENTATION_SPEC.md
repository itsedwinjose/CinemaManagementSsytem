# Cinema Ticketing System — Screen-by-Screen Implementation Specification

Version: 1.0

## 1. Purpose

This document converts the approved SRS and supplied screenshots into implementation-level UI requirements for a C# WinForms cinema ticketing application.

The application is a native Windows desktop application with direct MySQL network connectivity. There is no API/backend web service.

The supplied screenshots are the visual reference. The existing workflow must be reproduced rather than redesigned.

---

# 2. Global UI Rules

All forms must use the centralized design system documented in `DESIGN.md`.

Do not:
- introduce Material/Bootstrap styling
- introduce card-based modern layouts
- replace the compact Windows desktop appearance
- use arbitrary colors per form
- create independent button styles
- hard-code seat colors in individual controls
- create different fonts/sizes for different forms without documenting the exception in `DESIGN.md`

All screens must inherit the common:
- application background
- title/header style
- button style
- input style
- grid style
- tab style
- label style
- status color system

---

# 3. Login Form

## Controls

- Application title/header
- Username textbox
- Password textbox
- Login button
- Optional status/error label

## Behavior

- Validate credentials against MySQL.
- Passwords are stored as hashes.
- On successful login, open Main Booking Form.
- On failure, display a concise error using the common UI style.

No role-based login is required.

---

# 4. Main Booking Form

This is the default form after login.

## 4.1 Top-right Show Selection Area

Controls:

- Select Date
- Show selector
- Payment mode radio buttons:
  - Cash
  - Online
  - Card
  - UPI/QR

Default date:
- current system date

Changing date:
- reload screenings for that date
- preserve configured Show Type order

Changing show:
- update current/selected show details
- reload seat status for the selected Audi

## 4.2 Audi Tabs

Display all active Audis as tabs at the bottom of the seat map.

Examples:
- Audi-1
- Audi-2
- Audi-3
- Audi-4
- Audi-5

The number and names are data-driven.

Selecting an Audi loads its physical layout and the selected screening's seat states.

## 4.3 Seat Map

Use a custom `CinemaSeatMapControl`.

Requirements:
- variable row/column counts
- non-seat cells
- physical seats
- seat classes
- status colors
- mouse click selection
- mouse drag selection
- multi-seat selection
- dynamic cell sizing

The layout must expand to use the available area.

For a small layout, cells become larger.
For a large layout, cells become smaller.

The relative physical layout must never change.

## 4.4 Seat Status Priority

Render status in this priority:

1. Physical Damaged
2. Screening-specific status
3. Available

A damaged physical seat is always unavailable regardless of the screening.

## 4.5 Current/Selected Show Information

Display:
- Show Type
- Show Time
- Movie Name
- Ticket Price / applicable class pricing
- Selected seat count
- Current calculated amount

## 4.6 Main Actions

Buttons/actions:
- Ticket / Space
- Reservation
- Theatre Settings
- Set Movie
- Set Current Show
- Accounts
- Password Change
- Refresh / F5
- Reprint / F6
- Damaged / Unblocked

Do not display:
- Employee Info
- Role Based Login
- Family
- Seat Allocation Percentage

## 4.7 Bottom Statistics

Display:
- Sold / Total
- Counter Amount

Use the application's existing visual treatment.

---

# 5. Seat Selection Behavior

## Individual Selection

Clicking an available seat selects it.

Clicking a selected seat deselects it.

## Drag Selection

Mouse drag can select multiple available seats.

Do not allow:
- non-seat selection
- damaged seat selection
- already sold seat selection
- incompatible status selection

## Selection Visual

Use a dedicated selection state from `DESIGN.md`.

Do not overwrite permanent seat-status meaning.

---

# 6. Ticket Workflow

When Ticket is clicked:

1. Validate selected seats.
2. Validate payment mode.
3. Start MySQL transaction.
4. Lock selected screening seats with `FOR UPDATE`.
5. Recheck availability.
6. Recheck physical damaged state.
7. Calculate class-based prices.
8. Apply explicitly selected charges.
9. Create booking.
10. Create booking-seat records.
11. Create payment record.
12. Update screening-seat states.
13. Commit.
14. Print ticket.
15. Clear selection.

If any database validation fails:
- rollback
- display error
- refresh affected seats

Never allow duplicate sale from two network clients.

---

# 7. Reservation Dialog

Opened by Reservation button.

## Fields

- Address
- Phone No
- Name

## Charge Options

- Reservation Charge
- 3D Charge

Current default settings:
- Reservation: ₹10 per seat
- 3D: ₹30 per seat

These values come from `application_settings`.

## Payment

- Cash
- Online
- Card
- UPI/QR

## Actions

- Online
- Unblock
- Free
- Reserved
- Tele Reserved

## Rules

### Free
- creates zero-cost free ticket
- seat is occupied
- not the same as available

### Reserved
- counter reservation
- reservation charge applies per seat when selected

### Tele Reserved
- telephone reservation
- reservation charge applies per seat
- phone number should be stored

### Online
- set selected seats to online booking/sold state according to the existing application behavior

### Unblock
- context-sensitive unblock operation

---

# 8. Theatre Settings Form

## Top Input Area

- Theatre/Audi dropdown
- Show Type dropdown
- Show Time selector/input
- Price
- Save button

## Main Grid

Columns:
- Theatre
- Show Type
- Show Time
- Price
- Edit
- Delete

## Show Type Management Area

Controls:
- Show Type input
- Add button

Grid:
- order
- Show Type
- Edit
- Delete

The order is functional and must be respected everywhere shows are displayed.

Do not rely on insertion order.

## 3D Charge Area

Provide configurable 3D charge input.

The value is stored in application settings.

Do not auto-calculate 3D charge merely because a movie has `Is3D = true`.

---

# 9. Audi Layout Management Form

This is a new form required by the project.

## Top Controls

- Audi dropdown
- Rows
- Columns
- Generate/Resize Layout
- Save
- Optional Reset/Reload

## Visual Editor

Display a configurable grid.

Each cell can be:
- Non-seat
- Seat

For a seat:
- row label
- seat number
- seat class

## Layout Operations

Support:
- click cell
- drag/multi-select
- mark selected cells as seats
- mark selected cells as non-seat
- assign seat class
- save

## Layout Safety

If a seat has historical transactions:
- do not physically destroy historical identity
- prefer deactivation rather than destructive deletion
- preserve historical class and price in booking records

---

# 10. Seat Class Management

Provide a configuration form or section.

Fields:
- Class Name
- Display Order
- Active

Actions:
- Add
- Edit
- Deactivate/Delete where safe
- Reorder

Examples:
- Normal
- Executive
- Premium

Only configured classes should be displayed.

Do not display unused Class1/Class2/Class3 placeholders.

---

# 11. Set Movie Form

## Top Controls

- Select Date
- Theatre Name
- Show Type
- Movie Name
- Save

## Buttons

- Show All
- Set Same Movie
- Save

## Main Grid

Columns:
- selection
- Theatre
- Show Type
- Show Time
- 3D
- Movie Name
- dynamic seat-class pricing columns where applicable

If one class:
- display the actual class name

If multiple classes:
- display actual configured class names

## Movie Master Area

Fields:
- Movie Name
- 3D checkbox
- Add

Grid:
- Movie Name
- 3D
- Edit
- Delete

## Multi-select

Header checkbox:
- Select All

Rows:
- individually selectable

Set Same Movie applies the selected movie to the selected/displayed applicable rows.

---

# 12. Accounts Form

Two tabs:
- Cashout
- Reports

Cashout is the default tab.

---

# 13. Accounts — Cashout Tab

## Filters

- Select Date
- Theatre

## Eligibility

Only shows where:

`current time >= show time + 30 minutes`

are eligible.

A show must also not already be cashed out.

## Grid

Columns:
- Show
- Sold Seat
- Free Seat
- Reservation Seat
- Amount
- Reservation Amt
- 3D Amt
- Total
- CashOut
- Remarks

## Bottom Area

- Counter Balance
- Selected / Current Show
- CashOut
- Next Show
- CashOut / Print

## Cashout Action

1. Select account.
2. Calculate/recalculate accounting data from transactions.
3. Create/update account record.
4. Generate cashout report.
5. Print.
6. Mark account/cashout as processed.
7. Remove it from active Cashout list.

Do not physically delete historical ticket transactions merely to remove the item from active Cashout.

---

# 14. Accounts — Reports Tab

Purpose:
Display all account records whose reports have not yet been printed.

Controls:
- list/grid of unprinted reports
- select record
- print

After successful printing:
- set `is_printed = true`
- set `printed_at`
- set `printed_by`
- remove from unprinted list

---

# 15. Cashout Report Print

Reproduce the supplied report.

Required fields:

Header:
- Cashout Report
- actual cashout date/time

Show information:
- Theatre
- Date
- Time
- Show
- Movie

Seat statistics:
- Sold Seat
- Free Seat
- Reservation Seat

Financial:
- Amount
- Reservation Amt
- 3D Amt
- Total

Payment breakdown:
- Cash count - amount
- Card count - amount
- Online count - amount
- QR UPI count - amount

Final:
- CashOut

Payment-mode totals must reconcile with the applicable total.

---

# 16. Ticket Print

Use Epson-compatible thermal printing.

Do not use PDF as the primary ticket output.

Ticket must reproduce the supplied ticket layout:
- cinema name
- mobile
- GSTIN
- Audi
- date
- day
- time
- show
- ticket amount
- reservation
- taxes
- total
- seat number
- total seats
- movie
- footer

Use the actual configured cinema information.

The ticket print layout should be isolated in a printer/report class so it can be adjusted without changing booking logic.

---

# 17. Reprint

F6 / Reprint:
- locate an existing ticket
- print its original stored values
- do not create another booking
- do not update seat status
- do not modify amount
- do not change payment mode

---

# 18. Refresh

F5:
- clear current selection
- reload current seat display if needed
- do not change date
- do not change show
- do not change Audi
- do not modify database bookings

---

# 19. Password Change

Provide:
- Current Password
- New Password
- Confirm Password
- Change Password

Use password hashing.

---

# 20. Current Show

Set Current Show:
- mark selected screening as current
- clear previous current show
- persist in database
- other applicable screens can use current show

---

# 21. Application Settings

At minimum:
- Reservation Charge Per Seat
- 3D Charge Per Seat
- Cashout Delay Minutes
- Ticket Printer
- Cashout Printer
- Cinema Name
- Mobile
- GSTIN
- Footer text

---

# 22. Navigation

Recommended navigation:

Login
→ Main Booking

Main Booking
→ Theatre Settings
→ Audi Layout
→ Set Movie
→ Accounts
→ Password Change
→ Reservation Dialog

Accounts
→ Cashout
→ Reports

All secondary forms should return to the Main Booking form without unnecessarily creating multiple duplicate main windows.

---

# 23. Error Handling

Use a centralized error service.

Handle:
- database unavailable
- duplicate booking
- printer unavailable
- invalid configuration
- invalid selection
- missing movie
- missing pricing
- missing seat class
- transaction failure

Do not expose raw SQL exceptions to operators.

Log technical details.

Display operator-friendly messages.

---

# 24. UI Responsiveness

The seat map and normal UI must remain responsive.

Database work must not unnecessarily block the UI thread.

Use async operations where appropriate.

Do not create uncontrolled background threads.

---

# 25. Design Change Protocol

If any visual requirement is changed later:

1. Update `DESIGN.md`.
2. Update the centralized theme/style implementation.
3. Update affected forms.
4. Do not introduce one-off styling unless documented.
5. Keep screenshots/reference notes synchronized.

`DESIGN.md` is the single source of truth for visual design.
