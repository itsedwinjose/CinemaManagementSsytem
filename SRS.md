# Cinema Ticketing & Theatre Management System — SRS

**Version:** 1.0  
**Application:** Native Windows Desktop  
**Technology:** C# / .NET / Windows Forms  
**Database:** MySQL 8+  
**Connectivity:** Direct network connectivity from Windows clients to MySQL  
**API:** None  
**Primary Printer:** Epson thermal / ESC-POS compatible

---

# 1. Purpose

Build a Windows desktop cinema ticketing system from scratch that functionally and visually reproduces the client's existing cinema ticketing application.

The supplied screenshots are the primary visual reference.

The system shall preserve the existing application's:
- booking workflow
- Audi/show organization
- seating layout
- seat statuses
- movie assignment
- reservation workflow
- accounting/cashout workflow
- ticket printing
- cashout report printing
- keyboard shortcuts
- colors and UI appearance

The objective is replication, not redesign.

---

# 2. Technology and Architecture

## 2.1 Application

Use:
- C#
- modern supported .NET version
- Windows Forms
- MySqlConnector or equivalent MySQL .NET driver
- Epson/ESC-POS compatible printer implementation

## 2.2 Database

Use MySQL 8+.

Multiple Windows clients may connect simultaneously to the same MySQL database over the network.

## 2.3 No API

The application must NOT use:
- REST API
- REST backend
- Flask
- FastAPI
- Node backend
- PHP backend
- WebSocket server
- browser frontend
- cloud backend

Architecture:

```text
Windows Client 1 ─┐
Windows Client 2 ─┼── Network ── MySQL Server
Windows Client 3 ─┘
                       │
                       └── Epson Printer
```

---

# 3. Multi-Client Concurrency

Multiple operators may use the application at the same time.

The system must prevent two operators from successfully selling the same seat.

Booking must use a MySQL transaction and row locking.

Required conceptual sequence:

```text
START TRANSACTION
    ↓
SELECT selected screening seats FOR UPDATE
    ↓
Verify availability
    ↓
Verify physical seat is not damaged
    ↓
Create booking
    ↓
Create booking seats
    ↓
Create payment
    ↓
Update screening seats
    ↓
COMMIT
```

If validation fails:

```text
ROLLBACK
```

The operator must receive a clear message that one or more seats are no longer available.

---

# 4. Application Modules

Required modules:

1. Login
2. Main Ticket Booking
3. Theatre Settings
4. Audi Layout Management
5. Seat Class Management
6. Movie Management
7. Set Movie
8. Reservation
9. Accounts / Cashout
10. Accounts / Reports
11. Customer Ticket Printing
12. Cashout Report Printing
13. Reprint
14. Password Change
15. Application Settings

The following existing features are NOT required:

- Employee Info
- Role Based Login
- Family
- Seat Allocation Percentage

---

# 5. Global UI Requirements

The UI must visually match the supplied screenshots.

Match:
- colors
- fonts
- font sizes
- button appearance
- grid appearance
- tabs
- headers
- borders
- spacing
- labels
- input controls
- seat colors
- status colors
- compact Windows desktop appearance

Do not introduce:
- Material Design
- Bootstrap
- modern dashboard cards
- dark mode
- web-style responsive design
- pill-shaped buttons
- excessive rounded corners
- unnecessary animations

The visual design is maintained separately in `DESIGN.md`.

`DESIGN.md` is the single source of truth for UI design.

---

# 6. Login

The application shall provide a login form.

Fields:
- Username
- Password

Action:
- Login

Passwords must be stored using secure password hashes.

On successful login:
- open Main Booking Form

On failure:
- display an operator-friendly error

Role-based permissions are not required.

---

# 7. Main Ticket Booking Form

This is the default form after login.

---

## 7.1 Date

The current system date is selected by default.

Changing the date reloads shows/screenings for that date.

---

## 7.2 Show Selection

All shows configured for the selected date are displayed.

Shows must follow the configured Show Type order.

Selecting a show updates:
- selected screening
- movie
- show time
- seat availability
- pricing
- statistics
- current selection context

---

## 7.3 Audi Tabs

All active Audis are displayed as tabs.

Example:

```text
Audi-1 | Audi-2 | Audi-3 | Audi-4
```

Audi names are data-driven.

Each Audi can have a completely different seating layout.

Selecting an Audi loads its layout.

---

# 8. Audi Layout

Each Audi has its own configurable physical layout.

A layout consists of a configurable grid of rows and columns.

A cell can be:

```text
SEAT
NON_SEAT
```

Non-seat cells represent the blue/empty regions visible in the existing application.

Non-seat cells:
- cannot be selected
- cannot be booked
- cannot be reserved
- do not count as seats

---

# 9. Dynamic Seat Map

The Main Booking screen shall use a custom seat-map control.

It must support:
- variable rows
- variable columns
- irregular layouts
- non-seat areas
- physical seats
- seat classes
- seat statuses
- mouse click
- mouse drag selection
- multi-seat selection

## 9.1 Dynamic Cell Size

Cell dimensions must be calculated from:
- available width
- available height
- row count
- column count
- labels
- margins
- padding

Small layout:

```text
few rows/columns
→ larger cells
```

Large layout:

```text
many rows/columns
→ smaller cells
```

The layout must use the available booking-screen area efficiently.

Changing cell size must never change the physical relative positions.

Do not hard-code a fixed 30×30 grid.

---

# 10. Physical Seats

Each physical seat belongs to an Audi layout.

A seat contains:
- row
- column
- row label
- seat number
- seat class
- physical condition

Example:

```text
A1
A2
B1
B2
```

---

# 11. Seat Classes

Seat classes are configurable.

Examples:
- Normal
- Executive
- Premium

Do not hard-code:
- Class1
- Class2
- Class3
- Class4

The operator can create and name classes.

Example:

```text
Class 1 → Normal
Class 2 → Executive
```

The actual configured names must be displayed everywhere applicable.

---

# 12. Seat Class Ownership

The physical Audi layout defines which class a seat belongs to.

Example:

```text
A1 → Normal
A2 → Normal
A3 → Normal

B1 → Executive
B2 → Executive
B3 → Executive
```

The seat class belongs to the physical seat.

The movie does not determine the seat class.

---

# 13. Seat Class Pricing

The current application can use one common ticket price for all seats.

Example:

```text
All seats → ₹150
```

The architecture must support future class-specific pricing.

Example:

```text
Normal     → ₹150
Executive  → ₹200
Premium    → ₹250
```

The price is associated with:

```text
Show Configuration + Seat Class
```

Booking price is determined by:

```text
Physical Seat
    ↓
Seat Class
    ↓
Selected Show's Class Price
```

Historical bookings must store the actual price charged.

Changing future prices must not alter old tickets.

---

# 14. Physical Seat Status

Physical seat status is independent of a particular show.

At minimum:

```text
Normal
Damaged
```

---

# 15. Damaged / Unblocked

The Main Booking screen shall provide a:

**Damaged / Unblocked**

button.

If a normal physical seat is selected:
- mark it damaged

If a damaged seat is selected:
- unblock it

A damaged seat:
- cannot be booked
- cannot be reserved
- remains damaged across all dates
- remains damaged across all movies
- remains damaged across all shows
- remains damaged for all operators

It remains damaged until explicitly unblocked.

The damaged state belongs to the physical Audi layout cell, not the screening seat.

---

# 16. Screening Seat Status

Screening-specific status belongs to a particular show on a particular date.

Supported states:

- Available
- Sold
- Online Booking
- Counter Reserved
- Telephone Reserved
- Free Ticket
- Online Blocked
- Seat Blocked

Physical damaged status has priority over screening status.

---

# 17. Seat Status Colors

Use the supplied screenshot legend as the reference.

Required visual states include:

| Status | Reference |
|---|---|
| Non-seat | Blue |
| Online Block | Purple/Violet |
| Seat Damaged | Red |
| Online Booking | Teal/Green |
| Ticket Reserved | Orange |
| Seat Blocked | Existing legend color |
| Available | Existing available-seat color |

The exact color values shall be centralized in the design system and tuned against the supplied screenshots.

---

# 18. Seat Selection

Click:
- select available seat
- click selected seat again to deselect

Drag:
- select multiple available seats

Do not allow selection of:
- non-seat
- damaged seat
- already sold seat
- incompatible occupied state

Selection highlighting must be visually different from permanent seat status.

---

# 19. Payment Modes

The application supports exactly these payment modes:

- Cash
- Online
- Card
- UPI/QR

Payment mode must be stored with each booking/payment transaction.

These payment modes are also used by Cashout reporting.

---

# 20. Ticket Booking

Main screen contains the Ticket action.

The Space key should invoke the Ticket action if that matches the existing application behavior.

Workflow:

1. Select seats.
2. Select payment mode.
3. Click Ticket / press Space.
4. Validate selection.
5. Start database transaction.
6. Lock selected screening seats.
7. Recheck availability.
8. Recheck physical damaged status.
9. Calculate pricing.
10. Create booking.
11. Create booking-seat records.
12. Create payment record.
13. Update screening-seat statuses.
14. Commit.
15. Print ticket.
16. Clear selection.

If any validation fails:
- rollback
- display message
- refresh affected seats

---

# 21. Reservation

Clicking Reservation opens the reservation dialog.

Fields:
- Address
- Phone No
- Name

Charge options:
- Reservation Charge
- 3D Charge

Payment options:
- Cash
- Online
- Card
- UPI/QR

Actions:
- Online
- Unblock
- Free
- Reserved
- Tele Reserved

---

# 22. Reservation Charge

Default:

```text
₹10 per seat
```

Stored in application settings.

If selected:

```text
Reservation Charge =
Selected Seats × Reservation Charge Per Seat
```

---

# 23. 3D Charge

Default:

```text
₹30 per seat
```

Stored in application settings.

If selected:

```text
3D Charge =
Selected Seats × 3D Charge Per Seat
```

Important:

A movie being marked as 3D does NOT automatically add the 3D charge.

The operator must explicitly select the 3D Charge option.

---

# 24. Reservation Types

## Online

Marks the selected seats as online booking/sold according to the existing application behavior.

## Unblock

Unblocks the applicable selected seat status.

## Free

Creates a free ticket:
- zero cost
- occupied seat state
- not an available seat

## Reserved

Creates an offline/counter reservation.

Reservation charge:
- ₹10 per seat by default

## Tele Reserved

Creates a telephone reservation.

Reservation charge:
- ₹10 per seat by default

Phone number must be stored.

---

# 25. Booking Types

The database shall distinguish:

```text
COUNTER_TICKET
ONLINE_BOOKING
FREE_TICKET
COUNTER_RESERVATION
TELEPHONE_RESERVATION
```

Do not implement booking type as a single boolean.

---

# 26. Pricing Calculation

For one class:

```text
2 × ₹150 = ₹300
```

For multiple classes:

```text
Normal:
2 × ₹150 = ₹300

Executive:
1 × ₹200 = ₹200

Ticket Amount = ₹500
```

Additional charges:

```text
Reservation Charge = seats × reservation charge
3D Charge = seats × 3D charge
```

Final:

```text
Total =
Ticket Amount
+ Reservation Amount
+ 3D Amount
+ applicable tax
```

The exact tax behavior must follow configured tax rules.

---

# 27. Theatre Settings

Theatre Settings configures shows.

Fields:
- Theatre/Audi
- Show Type
- Show Time
- Price

Theatre dropdown displays all configured Audis.

Show Type dropdown displays all active Show Types.

Show Time:
- select existing time
- type a new time where supported

Save adds/updates the show configuration.

---

# 28. Theatre Settings Grid

Grid contains:
- Theatre
- Show Type
- Show Time
- Price
- Edit
- Delete

---

# 29. Show Types

Show Types are configurable.

Operator can:
- add
- edit
- delete/deactivate
- reorder

Examples:

```text
Morning Show
Noon Show
Matinee Show
First Show
Second Show
```

Everywhere shows are displayed, they must follow `display_order`.

Do not depend on database insertion order.

---

# 30. 3D Charge Setting

Provide configurable 3D charge.

Default:

```text
₹30 per seat
```

It is not automatically added merely because a movie is 3D.

---

# 31. Reservation Charge Setting

Provide configurable reservation charge.

Default:

```text
₹10 per seat
```

---

# 32. Movie Management

The Set Movie form contains movie master functionality.

Movie fields:
- Movie Name
- 3D checkbox

Actions:
- Add
- Edit
- Delete

Movie master stores `is_3d`.

The 3D flag does not automatically add a charge.

---

# 33. Set Movie

The Set Movie form allows a movie to be assigned to actual screenings.

Controls:
- Date
- Theatre
- Show
- Movie

When theatre is selected:
- load shows configured for that theatre/date

Movie assignment is saved to the applicable screening.

---

# 34. Set Same Movie

The operator can select a movie and apply it to all selected/displayed applicable screenings.

Support:
- row selection
- Select All
- Set Same Movie

---

# 35. Show All

Provide Show All/filter behavior matching the existing application.

---

# 36. Movie Assignment Display

If one seat class exists:
- display the actual class name

If multiple seat classes exist:
- display the actual configured class names

Example:

```text
Normal
Executive
Premium
```

Never display unused hard-coded class placeholders.

---

# 37. Current Show

The Main Booking form contains Set Current Show.

When selected:
- mark the selected screening as the current show
- clear previous current-show designation

Other applicable screens may use this current show context.

---

# 38. Accounts

Accounts contains two tabs:

```text
Cashout
Reports
```

Cashout is the default tab.

---

# 39. Cashout Eligibility

Only shows that have passed 30 minutes after their scheduled show time are displayed.

Example:

```text
Show: 11:00 AM

11:29 AM → not eligible
11:30 AM → eligible
```

Eligibility:

```text
Current DateTime >= Show DateTime + 30 minutes
```

The threshold is stored in settings and defaults to 30 minutes.

Already processed/cashed-out shows must not appear.

---

# 40. Cashout Filters

Operator selects:
- Date
- Theatre

The system displays eligible shows matching the selected date and theatre.

---

# 41. Cashout Grid

Display:
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

When a grid item is selected, its details appear in the bottom section.

---

# 42. Cashout Bottom Details

Display the relevant information from the selected account, including:
- Counter Balance
- Selected/Current Show
- CashOut
- Next Show

Use the existing screenshot as the visual reference.

---

# 43. CashOut / Print

When CashOut/Print is clicked:

1. Generate the cashout/account report.
2. Print it.
3. Record the cashout.
4. Mark the account/show as processed.
5. Remove it from the active Cashout list.

Historical ticket/payment transactions should preferably remain in the database for audit/history even though the active Cashout item is removed.

---

# 44. Accounts — Reports Tab

The Reports tab displays all account records whose reports have not yet been printed.

Workflow:

```text
Open Reports
    ↓
Load unprinted accounts
    ↓
Select account
    ↓
Print
    ↓
Mark printed
    ↓
Remove from unprinted list
```

The system must explicitly track printed/unprinted state.

---

# 45. Cashout Report

The printed report must reproduce the supplied reference.

Required information:

```text
Cashout Report

Cashout Date/Time

Theatre
Date
Time
Show
Movie

Sold Seat
Free Seat
Reservation Seat

Amount
Reservation Amt
3D Amt
Total

Cash       count - amount
Card       count - amount
Online     count - amount
QR UPI     count - amount

CashOut
```

Payment-mode totals must reconcile with the applicable collection total.

---

# 46. Customer Ticket Printing

The customer ticket must reproduce the supplied Epson print format as closely as possible.

It includes:

- Cinema name
- Mobile
- GSTIN
- Audi
- Date
- Day
- Show time
- Show type
- Seat number
- Total seats
- Movie
- Ticket amount
- Reservation amount
- Total
- Tax information
- Footer/disclaimer text

The exact formatting should follow the supplied screenshot.

---

# 47. Printing Technology

Use Epson-compatible thermal printing.

Preferred approach:

```text
C#
TicketPrintService
    ↓
ESC/POS commands
    ↓
Epson thermal printer
```

Do not use PDF as the primary ticket printing mechanism.

Keep ticket printing isolated from booking logic.

---

# 48. Reprint

Reprint allows an already-printed ticket to be printed again.

Requirements:
- use original transaction
- do not create a new sale
- do not change seat status
- do not change price
- do not change payment mode

F6 invokes Reprint where the existing workflow uses F6.

---

# 49. Refresh

F5 / Refresh:
- clear selected seats
- reset temporary UI selection

It must not:
- change date
- change show
- change Audi
- delete booking
- alter database records

---

# 50. Main Screen Totals

At the bottom of the Main Booking screen display:

```text
Sold Tickets / Total Tickets
```

Example:

```text
67 / 484
```

Also display the applicable total amount/counter amount.

The total ticket count is based on actual configured physical seats for that Audi, excluding non-seat cells.

---

# 51. Database Model

The database must separate:

## Configuration

- cinemas
- audis
- seat_classes
- audi_layout_cells
- show_types
- show_configurations
- show_class_prices
- movies
- application_settings

## Screening

- screenings
- movie_assignments
- screening_seats

## Transactions

- bookings
- booking_seats
- payments
- booking_taxes

## Accounting

- account_records
- cashout_records

---

# 52. Physical Seat vs Screening Seat

This distinction is mandatory.

## Physical seat

Defines:
- Audi
- row
- seat number
- class
- damaged state

## Screening seat

Defines:
- screening
- physical seat
- status
- price snapshot/class snapshot

Example:

```text
Physical:
Audi-2 / I-17
Class = Executive
Damaged = false

08-Aug First Show:
Sold

09-Aug First Show:
Available
```

---

# 53. Historical Transaction Data

Every booking must preserve:
- seat
- seat class
- ticket price
- reservation charge
- 3D charge
- total
- payment mode
- booking type
- tax values where applicable

Do not calculate old ticket values from current settings.

---

# 54. Database Configuration

Connection settings must be configurable:

- Server
- Port
- Database
- Username
- Password

Do not hard-code credentials.

The application should validate database connectivity at startup/login.

---

# 55. Application Settings

At minimum:

```text
reservation_charge_per_seat
three_d_charge_per_seat
cashout_delay_minutes
default_ticket_printer
default_cashout_printer
cinema_name
mobile
GSTIN
ticket_footer
```

Defaults:
- reservation charge = ₹10
- 3D charge = ₹30
- cashout delay = 30 minutes

---

# 56. Security

- Passwords must be hashed.
- Use parameterized SQL.
- Do not log passwords.
- Do not expose database credentials in source code.
- Do not display raw database exceptions to operators.

---

# 57. Error Handling

Handle:
- database unavailable
- printer unavailable
- duplicate booking
- invalid selection
- missing movie
- missing show
- missing price
- missing seat class
- transaction failure

Use operator-friendly messages.

Technical details should go to application logs.

---

# 58. Printer Failure

Printing failure must not silently create duplicate sales.

The application must distinguish:
- transaction saved
- printing successful
- printing failed

A controlled reprint/retry mechanism must be available.

---

# 59. Performance

The UI must remain responsive.

Use appropriate async database operations.

Do not perform long-running database operations directly on the UI thread.

Seat-map rendering must remain efficient even for large Audi layouts.

---

# 60. Project Structure

Recommended:

```text
CinemaTicketing.sln

src/
├── CinemaTicketing.WinForms/
│   ├── Forms/
│   ├── Controls/
│   ├── UI/
│   ├── Printing/
│   └── Program.cs
│
├── CinemaTicketing.Core/
│   ├── Entities/
│   ├── Enums/
│   ├── DTOs/
│   ├── Services/
│   └── BusinessRules/
│
├── CinemaTicketing.Data/
│   ├── MySql/
│   ├── Repositories/
│   └── Transactions/
│
└── CinemaTicketing.Tests/

database/
├── 001_initial_schema.sql
└── seed/

SRS.md
SCREEN_IMPLEMENTATION_SPEC.md
DESIGN.md
CODEX_INSTRUCTIONS.md
```

---

# 61. Core Services

Implement services such as:

- AuthenticationService
- TheatreService
- AudiService
- LayoutService
- SeatClassService
- ShowTypeService
- ShowConfigurationService
- MovieService
- MovieAssignmentService
- ScreeningService
- SeatAvailabilityService
- BookingService
- ReservationService
- PaymentService
- CashoutService
- ReportingService
- TicketPrintService
- CashoutPrintService
- SettingsService

---

# 62. Custom Seat Map Control

Implement a reusable:

```text
CinemaSeatMapControl
```

Responsibilities:
- dynamic layout rendering
- cell sizing
- non-seat rendering
- seat rendering
- seat label rendering
- class rendering where required
- status colors
- click selection
- drag selection
- selection highlighting
- damaged status
- refresh

Do not use a standard DataGridView as the primary seat-map renderer.

---

# 63. Required Keyboard Shortcuts

At minimum:

```text
F5 → Refresh / clear current selection
F6 → Reprint
Space → Ticket, where applicable
```

The shortcuts must not interfere with standard text input behavior.

---

# 64. Excluded Features

Do not implement unless explicitly requested later:

- Employee Info
- Role-based permissions
- Family booking
- Seat Allocation Percentage
- REST API
- Web backend
- Mobile app
- Cloud service
- Browser client

---

# 65. Implementation Rules

Codex must:

1. Read `SRS.md`.
2. Read `SCREEN_IMPLEMENTATION_SPEC.md`.
3. Read `DESIGN.md`.
4. Read the database schema.
5. Implement the centralized design system before individual forms.
6. Keep UI styling centralized.
7. Keep business logic out of form event handlers.
8. Use repositories/services for database access.
9. Use transactions for booking.
10. Test each phase before moving to the next.

---

# 66. Implementation Order

Phase 1:
- solution
- database connection
- schema/migrations
- configuration
- logging

Phase 2:
- theme/design system
- login
- master data

Phase 3:
- Audi layout
- seat classes
- damaged/unblocked

Phase 4:
- show types
- show configuration
- pricing

Phase 5:
- movie master
- movie assignment
- screening generation

Phase 6:
- Main Booking
- seat map
- seat selection
- booking
- reservation

Phase 7:
- Epson ticket printing
- reprint

Phase 8:
- Accounts
- Cashout
- Reports
- Cashout printing

Phase 9:
- integration tests
- concurrency tests
- print tests
- UI verification

---

# 67. Testing Requirements

Automated tests must cover:

- class pricing
- single-class pricing
- multiple-class pricing
- reservation charge
- 3D charge
- free tickets
- damaged seat persistence
- seat availability
- show ordering
- cashout eligibility
- payment reconciliation
- booking concurrency
- reprint behavior

Integration tests should cover MySQL operations where practical.

---

# 68. Acceptance Criteria

The application is functionally acceptable when:

## Booking
- current date loads automatically
- shows load correctly
- Audis appear as tabs
- correct Audi layout appears
- cell size adapts to layout
- non-seat regions are not selectable
- seats can be selected by click
- seats can be selected by drag
- class-based prices work
- payment modes work
- ticket booking works
- reservation works

## Layout
- different Audis can have different layouts
- rows/columns are configurable
- non-seat cells work
- seat classes can be assigned
- class names can be changed
- class names appear wherever required
- dynamic sizing works

## Seat Status
- damaged status persists across dates
- damaged status persists across movies
- damaged seat cannot be booked
- damaged seat can be unblocked
- show-specific statuses work

## Movies
- movies can be added
- movies can be edited
- movies can be deleted/deactivated
- 3D flag works
- movies can be assigned to screenings
- Set Same Movie works
- Select All works

## Accounts
- shows become eligible after 30 minutes
- Cashout displays correct records
- Cashout report contains all required data
- payment breakdown is correct
- printed reports are removed from unprinted list
- Reports tab displays unprinted accounts

## Printing
- customer ticket matches reference
- cashout report matches reference
- Epson thermal printing works
- reprint does not create a new transaction

## Network
- multiple clients can connect
- simultaneous booking of the same seat is prevented

---

# 69. Final Domain Model

The authoritative conceptual structure is:

```text
Cinema
  ↓
Audi
  ↓
Audi Layout
  ↓
Physical Seats
  ↓
Seat Classes
  ↓
Show Configuration
  ↓
Screening
  ↓
Screening Seats
  ↓
Booking
  ↓
Booking Seats + Payment
  ↓
Account
  ↓
Cashout / Report
```

Important rules:

- Audi owns the physical layout.
- Physical seat owns its seat class.
- Physical seat owns persistent damaged status.
- Show configuration owns show time/type and class pricing.
- Screening represents an actual show on a particular date.
- Screening seat represents the physical seat's state for that screening.
- Booking records historical prices and charges.
- Payment records payment mode.
- Account records summarize a screening for accounting.
- Cashout closes the account operationally.
