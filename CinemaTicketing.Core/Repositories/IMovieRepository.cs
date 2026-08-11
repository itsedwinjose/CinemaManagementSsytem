using CinemaTicketing.Core.Entities;

namespace CinemaTicketing.Core.Repositories;

public interface IMovieRepository
{
    Task<IReadOnlyList<Movie>> GetAllActiveAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Movie>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<long> CreateAsync(Movie movie, CancellationToken cancellationToken = default);
    Task UpdateAsync(Movie movie, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
