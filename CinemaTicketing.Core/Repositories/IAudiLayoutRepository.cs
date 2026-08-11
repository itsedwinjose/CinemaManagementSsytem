using CinemaTicketing.Core.Entities;

namespace CinemaTicketing.Core.Repositories;

public interface IAudiLayoutRepository
{
    Task<IReadOnlyList<AudiLayoutCell>> GetCellsForAudiAsync(long audiId, CancellationToken cancellationToken = default);
    Task SaveLayoutCellsAsync(long audiId, int totalRows, int totalCols, IEnumerable<AudiLayoutCell> cells, CancellationToken cancellationToken = default);
    Task UpdateCellDamageStatusAsync(long cellId, bool isDamaged, CancellationToken cancellationToken = default);
}
