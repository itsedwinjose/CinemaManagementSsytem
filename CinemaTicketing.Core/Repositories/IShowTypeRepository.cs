using CinemaTicketing.Core.Entities;

namespace CinemaTicketing.Core.Repositories;

public interface IShowTypeRepository
{
    Task<IReadOnlyList<ShowType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<long> CreateAsync(ShowType showType, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
