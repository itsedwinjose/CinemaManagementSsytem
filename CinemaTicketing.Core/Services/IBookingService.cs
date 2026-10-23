using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Enums;

namespace CinemaTicketing.Core.Services;

public record BookingRequest(
    long ScreeningId,
    long UserId,
    BookingType BookingType,
    PaymentMode PaymentMode,
    string CustomerName,
    string CustomerPhone,
    string CustomerAddress,
    bool ApplyReservationCharge,
    bool Apply3DCharge,
    IReadOnlyList<ScreeningSeat> SelectedSeats);

public record BookingResult(bool IsSuccess, string Message, Booking? Booking = null);

public interface IBookingService
{
    Task<BookingResult> ProcessBookingAsync(BookingRequest request, CancellationToken cancellationToken = default);
    Task<Booking?> GetBookingByIdAsync(long bookingId, CancellationToken cancellationToken = default);
    Task<Booking?> GetLatestBookingAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Booking>> GetForScreeningSeatsAsync(long screeningId, IEnumerable<long> screeningSeatIds, CancellationToken cancellationToken = default);
    Task MarkTicketPrintedAsync(IEnumerable<long> bookingIds, IEnumerable<long> confirmedReservationBookingIds, CancellationToken cancellationToken = default);
}
