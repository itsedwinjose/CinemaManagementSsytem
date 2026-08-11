using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Data.Repositories;

public sealed class ApplicationSettingRepository(CinemaTicketing.Core.Data.IMySqlConnectionFactory connectionFactory) : IApplicationSettingRepository
{
    public async Task<IReadOnlyList<ApplicationSetting>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT setting_key, setting_value, description
FROM application_settings
ORDER BY setting_key;
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        var settings = new List<ApplicationSetting>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            settings.Add(new ApplicationSetting
            {
                SettingKey = reader.GetString(0),
                SettingValue = reader.GetString(1),
                Description = reader.GetString(2)
            });
        }

        return settings;
    }

    public async Task<ApplicationSetting?> GetByKeyAsync(string settingKey, CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT setting_key, setting_value, description
FROM application_settings
WHERE setting_key = @settingKey;
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@settingKey", settingKey);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new ApplicationSetting
        {
            SettingKey = reader.GetString(0),
            SettingValue = reader.GetString(1),
            Description = reader.GetString(2)
        };
    }

    public async Task UpsertAsync(ApplicationSetting setting, CancellationToken cancellationToken = default)
    {
        const string sql = """
INSERT INTO application_settings (setting_key, setting_value, description)
VALUES (@settingKey, @settingValue, @description)
ON DUPLICATE KEY UPDATE
    setting_value = VALUES(setting_value),
    description = VALUES(description);
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@settingKey", setting.SettingKey);
        AddParameter(command, "@settingValue", setting.SettingValue);
        AddParameter(command, "@description", setting.Description);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static void AddParameter(System.Data.Common.DbCommand command, string name, object value)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value;
        command.Parameters.Add(parameter);
    }
}
