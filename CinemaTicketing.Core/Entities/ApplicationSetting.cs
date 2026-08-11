namespace CinemaTicketing.Core.Entities;

public sealed class ApplicationSetting
{
    public string SettingKey { get; init; } = string.Empty;
    public string SettingValue { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
