using System.Data.Common;
using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Data.Repositories;

public sealed class CinemaRepository(CinemaTicketing.Core.Data.IMySqlConnectionFactory connectionFactory) : ICinemaRepository
{
    public async Task<IReadOnlyList<CinemaInfo>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT id, name, address, mobile, gstin, is_active
FROM cinemas
WHERE is_active = 1
ORDER BY name;
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        var items = new List<CinemaInfo>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(MapCinema(reader));
        }

        return items;
    }

    public async Task<CinemaInfo?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT id, name, address, mobile, gstin, is_active FROM cinemas WHERE id = @id;";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        var param = command.CreateParameter();
        param.ParameterName = "@id";
        param.Value = id;
        command.Parameters.Add(param);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (await reader.ReadAsync(cancellationToken))
        {
            return MapCinema(reader);
        }

        return null;
    }

    private static CinemaInfo MapCinema(DbDataReader reader) => new()
    {
        Id = reader.GetInt64(0),
        Name = reader.GetString(1),
        Address = reader.GetString(2),
        Mobile = reader.GetString(3),
        GstIn = reader.GetString(4),
        IsActive = reader.GetBoolean(5)
    };
}
