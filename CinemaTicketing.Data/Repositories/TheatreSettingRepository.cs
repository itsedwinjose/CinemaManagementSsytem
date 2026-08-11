using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Data.Repositories;

public sealed class TheatreSettingRepository(CinemaTicketing.Core.Data.IMySqlConnectionFactory connectionFactory) : ITheatreSettingRepository
{
    public async Task<IReadOnlyList<TheatreSetting>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT ts.id,
       ts.cinema_id,
       c.name,
       ts.show_type_id,
       st.name,
       ts.show_time,
       ts.price
FROM theatre_settings ts
INNER JOIN cinemas c ON c.id = ts.cinema_id
INNER JOIN show_types st ON st.id = ts.show_type_id
ORDER BY c.name, st.display_order, ts.show_time;
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        var items = new List<TheatreSetting>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new TheatreSetting
            {
                Id = reader.GetInt64(0),
                CinemaId = reader.GetInt64(1),
                CinemaName = reader.GetString(2),
                ShowTypeId = reader.GetInt64(3),
                ShowTypeName = reader.GetString(4),
                ShowTime = reader.GetFieldValue<TimeSpan>(5),
                Price = reader.GetDecimal(6)
            });
        }

        return items;
    }

    public async Task<long> CreateAsync(TheatreSetting setting, CancellationToken cancellationToken = default)
    {
        const string sql = """
INSERT INTO theatre_settings (cinema_id, show_type_id, show_time, price)
VALUES (@cinemaId, @showTypeId, @showTime, @price);
SELECT LAST_INSERT_ID();
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@cinemaId", setting.CinemaId);
        AddParameter(command, "@showTypeId", setting.ShowTypeId);
        AddParameter(command, "@showTime", setting.ShowTime);
        AddParameter(command, "@price", setting.Price);

        var result = await command.ExecuteScalarAsync(cancellationToken);
        return Convert.ToInt64(result);
    }

    public async Task UpdateAsync(TheatreSetting setting, CancellationToken cancellationToken = default)
    {
        const string sql = """
UPDATE theatre_settings
SET cinema_id = @cinemaId,
    show_type_id = @showTypeId,
    show_time = @showTime,
    price = @price
WHERE id = @id;
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@id", setting.Id);
        AddParameter(command, "@cinemaId", setting.CinemaId);
        AddParameter(command, "@showTypeId", setting.ShowTypeId);
        AddParameter(command, "@showTime", setting.ShowTime);
        AddParameter(command, "@price", setting.Price);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        const string sql = "DELETE FROM theatre_settings WHERE id = @id;";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@id", id);

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
