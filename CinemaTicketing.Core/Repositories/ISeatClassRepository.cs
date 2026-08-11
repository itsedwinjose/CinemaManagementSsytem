using CinemaTicketing.Core.Entities;

namespace CinemaTicketing.Core.Repositories;

public interface ISeatClassRepository
{
    Task<IReadOnlyList<SeatClass>> GetAllActiveAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SeatClass>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<long> CreateAsync(SeatClass seatClass, CancellationToken cancellationToken = default);
    Task UpdateAsync(SeatClass seatClass, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
