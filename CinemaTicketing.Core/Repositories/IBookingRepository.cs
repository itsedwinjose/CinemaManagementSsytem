using CinemaTicketing.Core.Entities;

namespace CinemaTicketing.Core.Repositories;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(long bookingId, CancellationToken cancellationToken = default);
    Task<Booking?> GetByBookingNumberAsync(string bookingNumber, CancellationToken cancellationToken = default);
    Task<Booking?> GetLatestBookingAsync(CancellationToken cancellationToken = default);
    Task<long> ProcessBookingTransactionAsync(Booking booking, IEnumerable<long> screeningSeatIds, CancellationToken cancellationToken = default);
}
