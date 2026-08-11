namespace CinemaTicketing.Core.Entities;

public sealed class CashoutRecord
{
    public long Id { get; init; }
    public long CinemaId { get; init; }
    public DateOnly ShowDate { get; init; }
    public TimeSpan ShowTime { get; init; }
    public string ShowTypeName { get; init; } = string.Empty;
    public string MovieName { get; init; } = string.Empty;
    public int SoldSeats { get; init; }
    public int FreeSeats { get; init; }
    public int ReservedSeats { get; init; }
    public decimal TicketAmount { get; init; }
    public decimal ReservationAmount { get; init; }
    public decimal ThreeDAmount { get; init; }
    public decimal TotalAmount { get; init; }
    public decimal CashAmount { get; init; }
    public decimal CardAmount { get; init; }
    public decimal OnlineAmount { get; init; }
    public decimal UpiAmount { get; init; }
    public DateTime CashedOutAt { get; init; } = DateTime.UtcNow;
    public string CashedOutBy { get; init; } = string.Empty;
    public bool IsPrinted { get; init; }
    public DateTime? PrintedAt { get; init; }
    public string? PrintedBy { get; init; }
}
