using System.Text.Json;
using CinemaTicketing.Core.Configuration;

namespace CinemaTicketing.WinForms.Configuration;

internal static class ConfigurationLoader
{
    public static AppSettings Load(string basePath)
    {
        var settingsPath = Path.Combine(basePath, "appsettings.json");

        if (!File.Exists(settingsPath))
        {
            throw new FileNotFoundException("The application settings file was not found.", settingsPath);
        }

        var json = File.ReadAllText(settingsPath);
        var settings = JsonSerializer.Deserialize<AppSettings>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (settings is null)
        {
            throw new InvalidOperationException("The application settings file is empty or invalid.");
        }

        return settings;
    }
}
