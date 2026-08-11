using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Core.Services;

public sealed class MovieService(IMovieRepository movieRepository) : IMovieService
{
    public Task<IReadOnlyList<Movie>> GetMoviesAsync(CancellationToken cancellationToken = default)
        => movieRepository.GetAllActiveAsync(cancellationToken);

    public Task<long> SaveMovieAsync(Movie movie, CancellationToken cancellationToken = default)
    {
        if (movie.Id > 0)
        {
            return movieRepository.UpdateAsync(movie, cancellationToken).ContinueWith(_ => movie.Id);
        }
        return movieRepository.CreateAsync(movie, cancellationToken);
    }

    public Task DeleteMovieAsync(long id, CancellationToken cancellationToken = default)
        => movieRepository.DeleteAsync(id, cancellationToken);
}
