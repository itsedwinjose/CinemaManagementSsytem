using CinemaTicketing.Core.Entities;

namespace CinemaTicketing.Core.Repositories;

public interface ICinemaRepository
{
    Task<IReadOnlyList<CinemaInfo>> GetActiveAsync(CancellationToken cancellationToken = default);
    Task<CinemaInfo?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
}
