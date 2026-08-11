using CinemaTicketing.Core.Entities;

namespace CinemaTicketing.Core.Services;

public interface IAudiLayoutService
{
    Task<IReadOnlyList<Audi>> GetAudisAsync(long cinemaId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SeatClass>> GetSeatClassesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AudiLayoutCell>> GetLayoutAsync(long audiId, CancellationToken cancellationToken = default);
    Task SaveLayoutAsync(long audiId, int totalRows, int totalCols, IEnumerable<AudiLayoutCell> cells, CancellationToken cancellationToken = default);
    Task ToggleDamageAsync(long cellId, bool isDamaged, CancellationToken cancellationToken = default);
}
