using CinemaTicketing.Core.Entities;

namespace CinemaTicketing.Core.Repositories;

public interface IAudiRepository
{
    Task<IReadOnlyList<Audi>> GetAllAsync(long cinemaId, CancellationToken cancellationToken = default);
    Task<Audi?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<long> CreateAsync(Audi audi, CancellationToken cancellationToken = default);
    Task UpdateAsync(Audi audi, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
