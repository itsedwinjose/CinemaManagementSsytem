namespace CinemaTicketing.Core.Entities;

public sealed class BookingSeat
{
    public long Id { get; init; }
    public long BookingId { get; init; }
    public long ScreeningSeatId { get; init; }
    public string SeatClassName { get; init; } = string.Empty;
    public string SeatNumber { get; init; } = string.Empty;
    public decimal Price { get; init; }
}
