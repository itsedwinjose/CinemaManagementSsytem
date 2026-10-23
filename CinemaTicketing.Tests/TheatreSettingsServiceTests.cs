using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;
using CinemaTicketing.Core.Services;

namespace CinemaTicketing.Tests;

public class TheatreSettingsServiceTests
{
    [Fact]
    public async Task SaveAsync_RejectsNegativePrice()
    {
        var service = new TheatreSettingsService(new StubCinemaRepository(), new StubShowTypeRepository(), new InMemoryTheatreSettingRepository(), new StubAppSettingRepository());

        var result = await service.SaveAsync(new TheatreSettingSaveRequest(null, 1, 1, new TimeSpan(9, 0, 0), -1));

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task SaveAsync_CreatesSetting_WhenValuesAreValid()
    {
        var repository = new InMemoryTheatreSettingRepository();
        var service = new TheatreSettingsService(new StubCinemaRepository(), new StubShowTypeRepository(), repository, new StubAppSettingRepository());

        var result = await service.SaveAsync(new TheatreSettingSaveRequest(null, 1, 1, new TimeSpan(9, 0, 0), 150));

        Assert.True(result.IsSuccess);
        Assert.Single(await repository.GetAllAsync());
    }

    [Fact]
    public async Task GetChargeAsync_ReadsLegacyConfiguredChargeValues()
    {
        var settingsRepository = new StubAppSettingRepository(new Dictionary<string, string>
        {
            ["reservation_charge"] = "12",
            ["three_d_charge"] = "35"
        });
        var service = new TheatreSettingsService(
            new StubCinemaRepository(),
            new StubShowTypeRepository(),
            new InMemoryTheatreSettingRepository(),
            settingsRepository);

        Assert.Equal(12m, await service.GetReservationChargeAsync());
        Assert.Equal(35m, await service.Get3DChargeAsync());
    }

    private sealed class StubCinemaRepository : ICinemaRepository
    {
        public Task<IReadOnlyList<CinemaInfo>> GetActiveAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyList<CinemaInfo>>(new[]
            {
                new CinemaInfo { Id = 1, Name = "Movie", IsActive = true }
            });
        }

        public Task<CinemaInfo?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<CinemaInfo?>(new CinemaInfo { Id = 1, Name = "Movie", IsActive = true });
        }
    }

    private sealed class StubShowTypeRepository : IShowTypeRepository
    {
        public Task<IReadOnlyList<ShowType>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyList<ShowType>>(new[]
            {
                new ShowType { Id = 1, Name = "Morning", DisplayOrder = 1 }
            });
        }

        public Task<long> CreateAsync(ShowType showType, CancellationToken cancellationToken = default) => Task.FromResult(1L);
        public Task DeleteAsync(long id, CancellationToken cancellationToken = default) => Task.CompletedTask;
    }

    private sealed class StubAppSettingRepository : IApplicationSettingRepository
    {
        private readonly Dictionary<string, string> _settings;

        public StubAppSettingRepository(Dictionary<string, string>? settings = null)
        {
            _settings = settings ?? new Dictionary<string, string>();
        }

        public Task<IReadOnlyList<ApplicationSetting>> GetAllAsync(CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyList<ApplicationSetting>>(Array.Empty<ApplicationSetting>());
        public Task<ApplicationSetting?> GetByKeyAsync(string settingKey, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_settings.TryGetValue(settingKey, out var value)
                ? new ApplicationSetting { SettingKey = settingKey, SettingValue = value }
                : null);
        }

        public Task UpsertAsync(ApplicationSetting setting, CancellationToken cancellationToken = default)
        {
            _settings[setting.SettingKey] = setting.SettingValue;
            return Task.CompletedTask;
        }
    }

    private sealed class InMemoryTheatreSettingRepository : ITheatreSettingRepository
    {
        private readonly List<TheatreSetting> _items = new();

        public Task<IReadOnlyList<TheatreSetting>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyList<TheatreSetting>>(_items.ToList());
        }

        public Task<long> CreateAsync(TheatreSetting setting, CancellationToken cancellationToken = default)
        {
            _items.Add(new TheatreSetting
            {
                Id = _items.Count + 1,
                CinemaId = setting.CinemaId,
                ShowTypeId = setting.ShowTypeId,
                ShowTime = setting.ShowTime,
                Price = setting.Price
            });
            return Task.FromResult((long)_items.Count);
        }

        public Task UpdateAsync(TheatreSetting setting, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            _items.RemoveAll(x => x.Id == id);
            return Task.CompletedTask;
        }
    }
}
