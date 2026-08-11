using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Enums;

namespace CinemaTicketing.Core.Repositories;

public interface IScreeningRepository
{
    Task<IReadOnlyList<Screening>> GetScreeningsAsync(long cinemaId, DateOnly date, CancellationToken cancellationToken = default);
    Task<Screening?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task SetMovieAssignmentAsync(long cinemaId, DateOnly date, long audiId, long showTypeId, TimeSpan showTime, long movieId, CancellationToken cancellationToken = default);
    Task SetCurrentShowAsync(long screeningId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ScreeningSeat>> GetScreeningSeatsAsync(long screeningId, CancellationToken cancellationToken = default);
    Task UpdateScreeningSeatStatusAsync(long screeningSeatId, SeatStatus status, CancellationToken cancellationToken = default);
    Task EnsureScreeningsGeneratedForDateAsync(long cinemaId, DateOnly date, CancellationToken cancellationToken = default);
}
