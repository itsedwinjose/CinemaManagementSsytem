using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Core.Services;

public sealed class AudiLayoutService(
    IAudiRepository audiRepository,
    ISeatClassRepository seatClassRepository,
    IAudiLayoutRepository audiLayoutRepository) : IAudiLayoutService
{
    public Task<IReadOnlyList<Audi>> GetAudisAsync(long cinemaId, CancellationToken cancellationToken = default)
        => audiRepository.GetAllAsync(cinemaId, cancellationToken);

    public Task<IReadOnlyList<SeatClass>> GetSeatClassesAsync(CancellationToken cancellationToken = default)
        => seatClassRepository.GetAllActiveAsync(cancellationToken);

    public Task<IReadOnlyList<AudiLayoutCell>> GetLayoutAsync(long audiId, CancellationToken cancellationToken = default)
        => audiLayoutRepository.GetCellsForAudiAsync(audiId, cancellationToken);

    public Task SaveLayoutAsync(long audiId, int totalRows, int totalCols, IEnumerable<AudiLayoutCell> cells, CancellationToken cancellationToken = default)
        => audiLayoutRepository.SaveLayoutCellsAsync(audiId, totalRows, totalCols, cells, cancellationToken);

    public Task ToggleDamageAsync(long cellId, bool isDamaged, CancellationToken cancellationToken = default)
        => audiLayoutRepository.UpdateCellDamageStatusAsync(cellId, isDamaged, cancellationToken);
}
