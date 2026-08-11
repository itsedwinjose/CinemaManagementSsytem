# Cinema Ticketing System — DESIGN.md

Version: 1.0

## 1. Purpose

This file is the single source of truth for the visual design of the Cinema Ticketing System.

IMPORTANT:

All UI implementation must follow this file.

If a developer/Codex wants to change:
- colors
- fonts
- font sizes
- button styles
- borders
- spacing
- grid appearance
- seat colors
- headers
- tabs
- control dimensions
- visual behavior

the developer MUST update this file first and then update the centralized UI implementation.

Do not create undocumented one-off visual styles.

---

# 2. Visual Objective

The application is a replica/reimplementation of an existing Windows cinema ticketing system.

The design must look like the existing application shown in the client screenshots.

The objective is NOT:
- modernization
- Material Design
- Bootstrap
- Fluent UI
- dark mode
- card-based UI
- web-style responsive design

The objective is:
- compact
- practical
- Windows desktop
- information-dense
- colorful status-driven UI
- visually close to the existing application

---

# 3. Reference Screens

The following client screenshots are the visual references:

1. Main Booking / Seat Map
2. Theatre Settings
3. Set Movie
4. Customer Ticket
5. Accounts / Cashout
6. Reservation Dialog
7. Cashout Report Print
8. Main Booking with irregular/small Audi layout

When a screenshot and a generic UI convention conflict, prefer the screenshot.

---

# 4. Design System Architecture

Create a centralized WinForms design system.

Recommended structure:

CinemaTicketing.WinForms/
    UI/
        Design/
            AppTheme.cs
            AppColors.cs
            AppFonts.cs
            AppMetrics.cs
            ButtonStyles.cs
            GridStyles.cs
            ControlStyles.cs
            SeatStatusStyles.cs

All forms must consume these definitions.

Do not hard-code RGB values in individual forms.

---

# 5. Main Application Background

Use the same general light/off-white Windows desktop background appearance visible in the reference screenshots.

The application should not use a dark theme.

Large unused form areas should remain visually quiet.

---

# 6. Main Header

Reference appearance:
- bright blue/cyan horizontal header
- compact Windows desktop form title
- white/dark readable title text depending on screenshot
- no modern card treatment

All major forms should have a consistent header component.

Example:
- Main Booking
- Settings
- Set Movie
- Reports Accounts

The header should be implemented as a reusable control/component.

---

# 7. Primary Blue

The application uses a strong cyan/blue for:
- section headers
- form title bars
- some primary controls
- non-seat/background areas of the seat map

Do not use arbitrary blue shades.

Centralize the selected RGB/ARGB values in `AppColors.cs`.

During implementation, compare the running application against the screenshots and tune the centralized values.

---

# 8. Button Design

Buttons in the reference application are compact rectangular Windows-style buttons.

Requirements:
- compact height
- square/slightly rounded corners according to the screenshot
- strong saturated fill
- readable text
- minimal padding
- no modern elevation/shadow
- no pill-shaped buttons

Button categories should be centralized.

Suggested categories:

Primary:
- blue/cyan

Action/Success:
- green/teal

Destructive:
- pink/red

Neutral:
- light/gray

The exact colors must be kept in `AppColors.cs`.

---

# 9. Green Action Controls

The screenshots use bright green controls for certain actions and statuses.

Use green for:
- relevant positive/booking operations
- configured status controls
- 3D/status indicators where the original application uses green

Do not use green automatically for every success action.

Match the reference.

---

# 10. Pink/Red Controls

The screenshots use bright pink/red for destructive or delete operations.

Use for:
- Delete
- destructive state
- damaged seat
- relevant red status

Do not use generic Windows MessageBox red styling as a replacement for the application's visual system.

---

# 11. Teal Controls

Teal/cyan-green is used for some grid action buttons such as Update/Edit in the reference screens.

Keep this as a distinct action color.

---

# 12. Grid Design

The application uses dense Windows-style grids.

Grid requirements:
- compact row height
- visible borders
- light background
- small readable text
- centered numeric values where appropriate
- action cells with strong colors
- selected row highlight
- header styling matching reference

Do not use:
- rounded cards
- excessive whitespace
- modern floating headers
- large row heights

Create a reusable `AppGridStyle`.

---

# 13. Grid Action Cells

Where screenshots show colored action cells:

UPDATE:
- use the reference teal/cyan action color

DELETE:
- use the reference pink/red color

These must be reusable DataGridView cell styles.

---

# 14. Input Controls

Use compact standard WinForms controls.

ComboBox:
- compact
- standard border
- light background

TextBox:
- compact
- standard Windows border

DateTimePicker:
- standard Windows style

CheckBox:
- standard compact checkbox

RadioButton:
- standard compact radio button

Avoid custom oversized modern controls.

---

# 15. Tabs

Audi tabs appear along the bottom of the main seat map.

They should be:
- compact
- text-based
- close to the reference appearance
- clearly identify the active Audi

Do not use browser-style tabs.

---

# 16. Seat Map Design

The seat map is the most important visual component.

Use a custom control.

Each physical cell can represent:
- seat
- non-seat

Seat cells display:
- seat identifier
- status color
- optionally class information where required

---

# 17. Seat Map Cell Sizing

Cell size must be calculated dynamically.

Inputs:
- available width
- available height
- row count
- column count
- row label width
- margins
- padding

The formula must ensure the layout uses the available area efficiently.

For small layouts:
- cells grow

For large layouts:
- cells shrink

Do not hard-code cell width/height.

---

# 18. Seat Map Non-seat Color

The reference shows large blue regions where there are no seats.

This blue is the visual representation of:
- start/empty area
- non-seat space
- aisle/unused layout position as applicable

Non-seat cells:
- cannot be selected
- do not count toward total seats
- do not participate in pricing

Use centralized `SeatStatusColors.NonSeat`.

---

# 19. Seat Status Colors

The legend in the client screenshot is the primary reference.

Required statuses:

## Online Block
Reference:
- purple/violet

## Family
Not implemented.

## Seat Damaged
Reference:
- red

## Online Booking
Reference:
- teal/green

## Ticket Reserved
Reference:
- orange

## Seat Blocked
Reference:
- use the existing legend reference

## Available
Reference:
- light/white seat background

The exact RGB values should be calibrated from the client screenshots and stored centrally.

---

# 20. Status Color Priority

When rendering a seat:

1. Damaged
2. Screening booking/status
3. Available

A damaged seat must always appear damaged.

Do not allow a sold/reserved color to override a damaged physical seat.

---

# 21. Seat Selection Color

Selection is a temporary UI state.

It must not permanently change the semantic status color.

Use a visible border/highlight/overlay that does not confuse:
- selected
- sold
- reserved
- damaged

The implementation should document the selection treatment in `SeatStatusStyles`.

---

# 22. Seat Class Display

Seat class is defined by the Audi layout.

Examples:
- Normal
- Executive
- Premium

Never display:
- Class1
- Class2
- Class3
- Class4

unless those are actually configured names.

Where a class is shown, use the configured name.

Only classes actually configured/used should be displayed.

---

# 23. Typography

The application uses compact desktop typography.

Default:
- Segoe UI or the closest Windows system font
- compact sizes
- bold only where reference screenshots show bold

Do not use:
- oversized headings
- web typography
- decorative fonts

Centralize fonts in `AppFonts.cs`.

Recommended starting point:
- normal: 8–10 pt
- labels: 8–10 pt
- grid: 8–9 pt
- form title: 10–12 pt bold
- major ticket values: larger/bold according to ticket reference

These are starting points and must be visually tuned against screenshots.

---

# 24. Spacing

Use compact spacing.

Prefer:
- 3–8 px internal spacing
- small control margins
- dense grid placement

Do not create large empty margins around ordinary controls.

The application should resemble the existing operational cinema software.

---

# 25. Main Booking Layout

The main form should roughly follow:

Top:
- show/date/payment controls

Center/left:
- large seat map

Right:
- show information
- ticket/reservation controls
- settings/action buttons
- totals

Bottom:
- Audi tabs
- legend
- totals/status information

Do not redesign this into a sidebar dashboard.

---

# 26. Main Booking Right Panel

Keep the right-side panel compact.

Sections:
1. Date/Show
2. Payment
3. Ticket/Reservation
4. Current Show
5. Pricing
6. Action buttons
7. Totals

Use the reference screenshots as the exact placement guide.

---

# 27. Reservation Dialog Design

The Reservation dialog should remain compact.

Use:
- blue title/header
- white/light content area
- compact fields
- visible charge checkboxes
- payment radio buttons
- strong colored action buttons

The amount calculation should be visually prominent.

---

# 28. Theatre Settings Design

Follow screenshot:
- blue top title bar
- compact top input row
- large central grid
- right-side smaller management panel
- strong colored Add/Update/Delete buttons

Do not convert the grid into a modern data table.

---

# 29. Set Movie Design

Follow screenshot:
- blue title/header
- compact top filters
- green Show All / Set Same Movie controls
- central grid
- right-side movie master panel
- green 3D indicators/checkbox styling where appropriate
- compact dense layout

Dynamic class names must replace hard-coded Class1/Class2 labels.

---

# 30. Accounts Design

Follow screenshot:
- blue title/header
- Cashout/Reports tabs
- compact date/theatre filters
- dense report grid
- bottom accounting panel
- green CashOut / Print action

Do not introduce charts unless later requested.

---

# 31. Ticket Print Design

The ticket is a separate print design.

It must reproduce the supplied Epson ticket.

Use:
- monochrome thermal layout
- strong bold cinema name
- bordered date/time boxes
- seat information box
- movie box
- right-side financial/tax area
- footer disclaimer text

The print template must be isolated from screen UI styling.

---

# 32. Cashout Print Design

Reproduce the supplied cashout report.

It is a narrow monochrome printed report.

Fields must appear in the same logical order as the reference.

Do not convert it into a full-page modern report.

---

# 33. Print Layout Constants

Printer-specific measurements must be centralized.

Example:

PrintSettings:
- paper width
- printable width
- character width
- left margin
- right margin
- line spacing
- bold mode
- double-width mode
- double-height mode
- cut behavior

Do not scatter printer commands throughout forms.

---

# 34. Design Tokens

Create centralized tokens:

AppColors
AppFonts
AppMetrics
AppBorders
AppGridStyles
AppButtonStyles
SeatStatusColors
PrintSettings

All screens consume these tokens.

---

# 35. Screenshot Comparison Workflow

Whenever a form is implemented:

1. Build the form.
2. Run the application.
3. Compare against the client screenshot.
4. Adjust the centralized design tokens.
5. Re-check other forms.
6. Only add form-specific overrides when absolutely necessary.
7. Document legitimate exceptions in this file.

Do not simply declare the UI "close enough."

---

# 36. Design Change Rule

If a later requirement says:

"Change button color"

do not modify only one button.

Instead:

1. Update `DESIGN.md`.
2. Update the centralized button token/style.
3. Rebuild affected forms.
4. Verify all forms still match.

If only one screen intentionally differs, document:

- screen
- control
- reason
- exact override

in this file.

---

# 37. Forbidden Design Practices

Do not:
- introduce random colors
- use CSS/web styling concepts
- use Material icons unnecessarily
- replace text buttons with icon-only buttons
- make buttons pill-shaped
- add excessive rounded corners
- add drop shadows
- use dark mode
- use oversized fonts
- add animations
- add unnecessary transitions
- use modern dashboard cards
- create responsive web-like layouts

The application is a traditional Windows desktop cinema operational system.

---

# 38. Design Acceptance Criteria

A form is visually acceptable only when:

- layout resembles reference screenshot
- controls appear in the same logical positions
- colors match reference
- buttons match reference
- grid density matches reference
- seat colors match reference
- typography is compact
- no modern redesign has been introduced

---

# 39. Design File Ownership

`DESIGN.md` is authoritative for visual design.

The following source files implement it:

- `AppTheme.cs`
- `AppColors.cs`
- `AppFonts.cs`
- `AppMetrics.cs`
- `ButtonStyles.cs`
- `GridStyles.cs`
- `SeatStatusStyles.cs`

If implementation and `DESIGN.md` disagree, update the implementation to match `DESIGN.md`, unless a new approved requirement changes the design.

---

# 40. Final Instruction to Codex

Before implementing any UI:

1. Read `SRS.md`.
2. Read `SCREEN_IMPLEMENTATION_SPEC.md`.
3. Read `DESIGN.md`.
4. Inspect all supplied reference screenshots.
5. Implement the centralized design system first.
6. Build forms using the centralized styles.
7. Do not create one-off visual styling without updating `DESIGN.md`.

Every future UI change must keep `DESIGN.md` synchronized with the actual application.
