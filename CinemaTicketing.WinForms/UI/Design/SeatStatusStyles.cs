using CinemaTicketing.Core.Enums;

namespace CinemaTicketing.WinForms.UI.Design;

public static class SeatStatusStyles
{
    public static readonly Color NonSeat = Color.FromArgb(0, 162, 232); // Vibrant Ocean Blue matching reference photo
    public static readonly Color Available = Color.White;
    public static readonly Color Sold = Color.FromArgb(245, 146, 30); // Orange / Gold
    public static readonly Color OnlineBooking = Color.FromArgb(16, 124, 65); // Bright Green
    public static readonly Color CounterReserved = Color.FromArgb(139, 58, 139); // Purple / Violet
    public static readonly Color TelephoneReserved = Color.FromArgb(180, 80, 180);
    public static readonly Color FreeTicket = Color.FromArgb(23, 162, 184);
    public static readonly Color OnlineBlocked = Color.FromArgb(0, 162, 232); // Ocean Blue
    public static readonly Color Family = Color.FromArgb(225, 211, 199); // Soft Tan / Pink
    public static readonly Color SeatBlocked = Color.FromArgb(118, 118, 118); // Dark Gray
    public static readonly Color Damaged = Color.FromArgb(232, 17, 35); // Solid Red

    public static readonly Color SelectionBorder = Color.FromArgb(255, 215, 0); // Vivid gold border
    public static readonly Color SelectionFill = Color.FromArgb(255, 243, 176); // Soft highlight fill

    public static Color GetStatusColor(SeatStatus status, bool isDamaged)
    {
        if (isDamaged)
        {
            return Damaged;
        }

        return status switch
        {
            SeatStatus.Available => Available,
            SeatStatus.Sold => Sold,
            SeatStatus.OnlineBooking => OnlineBooking,
            SeatStatus.CounterReserved => CounterReserved,
            SeatStatus.TelephoneReserved => TelephoneReserved,
            SeatStatus.FreeTicket => FreeTicket,
            SeatStatus.OnlineBlocked => OnlineBlocked,
            SeatStatus.SeatBlocked => SeatBlocked,
            SeatStatus.Damaged => Damaged,
            _ => Available
        };
    }
}
