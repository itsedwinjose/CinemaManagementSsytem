using System.Data.Common;
using CinemaTicketing.Core.Data;
using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Data.Repositories;

public sealed class AudiRepository(IMySqlConnectionFactory connectionFactory) : IAudiRepository
{
    public async Task<IReadOnlyList<Audi>> GetAllAsync(long cinemaId, CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT id, cinema_id, name, display_order, total_rows, total_cols
FROM audis
WHERE cinema_id = @cinemaId
ORDER BY display_order, name;
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@cinemaId", cinemaId);

        var list = new List<Audi>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            list.Add(MapAudi(reader));
        }

        return list;
    }

    public async Task<Audi?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT id, cinema_id, name, display_order, total_rows, total_cols FROM audis WHERE id = @id;";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@id", id);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (await reader.ReadAsync(cancellationToken))
        {
            return MapAudi(reader);
        }

        return null;
    }

    public async Task<long> CreateAsync(Audi audi, CancellationToken cancellationToken = default)
    {
        const string sql = """
INSERT INTO audis (cinema_id, name, display_order, total_rows, total_cols)
VALUES (@cinemaId, @name, @displayOrder, @totalRows, @totalCols);
SELECT LAST_INSERT_ID();
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@cinemaId", audi.CinemaId);
        AddParameter(command, "@name", audi.Name);
        AddParameter(command, "@displayOrder", audi.DisplayOrder);
        AddParameter(command, "@totalRows", audi.TotalRows);
        AddParameter(command, "@totalCols", audi.TotalCols);

        var result = await command.ExecuteScalarAsync(cancellationToken);
        return Convert.ToInt64(result);
    }

    public async Task UpdateAsync(Audi audi, CancellationToken cancellationToken = default)
    {
        const string sql = """
UPDATE audis
SET name = @name, display_order = @displayOrder, total_rows = @totalRows, total_cols = @totalCols
WHERE id = @id;
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@id", audi.Id);
        AddParameter(command, "@name", audi.Name);
        AddParameter(command, "@displayOrder", audi.DisplayOrder);
        AddParameter(command, "@totalRows", audi.TotalRows);
        AddParameter(command, "@totalCols", audi.TotalCols);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        const string sql = "DELETE FROM audis WHERE id = @id;";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@id", id);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static Audi MapAudi(DbDataReader reader) => new()
    {
        Id = reader.GetInt64(0),
        CinemaId = reader.GetInt64(1),
        Name = reader.GetString(2),
        DisplayOrder = reader.GetInt32(3),
        TotalRows = reader.GetInt32(4),
        TotalCols = reader.GetInt32(5)
    };

    private static void AddParameter(DbCommand command, string name, object value)
    {
        var param = command.CreateParameter();
        param.ParameterName = name;
        param.Value = value;
        command.Parameters.Add(param);
    }
}
