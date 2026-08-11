using CinemaTicketing.Core.Entities;

namespace CinemaTicketing.Core.Repositories;

public interface ITheatreSettingRepository
{
    Task<IReadOnlyList<TheatreSetting>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<long> CreateAsync(TheatreSetting setting, CancellationToken cancellationToken = default);
    Task UpdateAsync(TheatreSetting setting, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
