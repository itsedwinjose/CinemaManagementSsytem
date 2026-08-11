using CinemaTicketing.Core.Enums;

namespace CinemaTicketing.Core.Entities;

public sealed class Booking
{
    public long Id { get; init; }
    public long ScreeningId { get; init; }
    public long UserId { get; init; }
    public string BookingNumber { get; init; } = string.Empty;
    public BookingType BookingType { get; init; }
    public PaymentMode PaymentMode { get; init; }
    public string CustomerName { get; init; } = string.Empty;
    public string CustomerPhone { get; init; } = string.Empty;
    public string CustomerAddress { get; init; } = string.Empty;
    public decimal TicketAmount { get; init; }
    public decimal ReservationAmount { get; init; }
    public decimal ThreeDAmount { get; init; }
    public decimal TaxAmount { get; init; }
    public decimal TotalAmount { get; init; }
    public DateTime CreatedUtc { get; init; } = DateTime.UtcNow;

    public List<BookingSeat> Seats { get; init; } = new();
}
