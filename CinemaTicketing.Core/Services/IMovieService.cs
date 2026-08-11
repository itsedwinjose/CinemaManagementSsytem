using CinemaTicketing.Core.Entities;

namespace CinemaTicketing.Core.Services;

public interface IMovieService
{
    Task<IReadOnlyList<Movie>> GetMoviesAsync(CancellationToken cancellationToken = default);
    Task<long> SaveMovieAsync(Movie movie, CancellationToken cancellationToken = default);
    Task DeleteMovieAsync(long id, CancellationToken cancellationToken = default);
}
