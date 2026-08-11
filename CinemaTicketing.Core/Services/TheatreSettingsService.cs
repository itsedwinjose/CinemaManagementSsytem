using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Core.Services;

public sealed record TheatreSettingSaveRequest(long? Id, long CinemaId, long ShowTypeId, TimeSpan ShowTime, decimal Price);
public sealed record TheatreSettingSaveResult(bool IsSuccess, string Message);

public interface ITheatreSettingsService
{
    Task<IReadOnlyList<CinemaInfo>> GetCinemasAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ShowType>> GetShowTypesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TheatreSetting>> GetSettingsAsync(CancellationToken cancellationToken = default);
    Task<TheatreSettingSaveResult> SaveAsync(TheatreSettingSaveRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<decimal> Get3DChargeAsync(CancellationToken cancellationToken = default);
    Task Save3DChargeAsync(decimal amount, CancellationToken cancellationToken = default);
    Task CreateShowTypeAsync(string name, CancellationToken cancellationToken = default);
    Task DeleteShowTypeAsync(long id, CancellationToken cancellationToken = default);
}

public sealed class TheatreSettingsService(
    ICinemaRepository cinemaRepository,
    IShowTypeRepository showTypeRepository,
    ITheatreSettingRepository theatreSettingRepository,
    IApplicationSettingRepository applicationSettingRepository) : ITheatreSettingsService
{
    public Task<IReadOnlyList<CinemaInfo>> GetCinemasAsync(CancellationToken cancellationToken = default)
    {
        return cinemaRepository.GetActiveAsync(cancellationToken);
    }

    public Task<IReadOnlyList<ShowType>> GetShowTypesAsync(CancellationToken cancellationToken = default)
    {
        return showTypeRepository.GetAllAsync(cancellationToken);
    }

    public Task<IReadOnlyList<TheatreSetting>> GetSettingsAsync(CancellationToken cancellationToken = default)
    {
        return theatreSettingRepository.GetAllAsync(cancellationToken);
    }

    public async Task<TheatreSettingSaveResult> SaveAsync(TheatreSettingSaveRequest request, CancellationToken cancellationToken = default)
    {
        if (request.CinemaId <= 0)
        {
            return new TheatreSettingSaveResult(false, "Select a theatre.");
        }

        if (request.ShowTypeId <= 0)
        {
            return new TheatreSettingSaveResult(false, "Select a show type.");
        }

        if (request.Price < 0)
        {
            return new TheatreSettingSaveResult(false, "Price cannot be negative.");
        }

        var setting = new TheatreSetting
        {
            Id = request.Id ?? 0,
            CinemaId = request.CinemaId,
            ShowTypeId = request.ShowTypeId,
            ShowTime = request.ShowTime,
            Price = request.Price
        };

        if (request.Id is null)
        {
            await theatreSettingRepository.CreateAsync(setting, cancellationToken);
            return new TheatreSettingSaveResult(true, "Theatre setting saved.");
        }

        await theatreSettingRepository.UpdateAsync(setting, cancellationToken);
        return new TheatreSettingSaveResult(true, "Theatre setting updated.");
    }

    public Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        return theatreSettingRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<decimal> Get3DChargeAsync(CancellationToken cancellationToken = default)
    {
        var setting = await applicationSettingRepository.GetByKeyAsync("three_d_charge", cancellationToken);
        if (setting is not null && decimal.TryParse(setting.SettingValue, out var val))
        {
            return val;
        }
        return 30m;
    }

    public Task Save3DChargeAsync(decimal amount, CancellationToken cancellationToken = default)
    {
        return applicationSettingRepository.UpsertAsync(new ApplicationSetting
        {
            SettingKey = "three_d_charge",
            SettingValue = amount.ToString("F0"),
            Description = "Default optional 3D charge per seat"
        }, cancellationToken);
    }

    public async Task CreateShowTypeAsync(string name, CancellationToken cancellationToken = default)
    {
        var all = await showTypeRepository.GetAllAsync(cancellationToken);
        int maxOrder = all.Count > 0 ? all.Max(x => x.DisplayOrder) : 0;
        await showTypeRepository.CreateAsync(new ShowType
        {
            Name = name.Trim(),
            DisplayOrder = maxOrder + 1
        }, cancellationToken);
    }

    public Task DeleteShowTypeAsync(long id, CancellationToken cancellationToken = default)
    {
        return showTypeRepository.DeleteAsync(id, cancellationToken);
    }
}
