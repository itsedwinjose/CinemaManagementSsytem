using CinemaTicketing.Core.Entities;

namespace CinemaTicketing.Core.Repositories;

public interface IApplicationSettingRepository
{
    Task<IReadOnlyList<ApplicationSetting>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ApplicationSetting?> GetByKeyAsync(string settingKey, CancellationToken cancellationToken = default);
    Task UpsertAsync(ApplicationSetting setting, CancellationToken cancellationToken = default);
}
