using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Data.Repositories;

public sealed class ShowTypeRepository(CinemaTicketing.Core.Data.IMySqlConnectionFactory connectionFactory) : IShowTypeRepository
{
    public async Task<IReadOnlyList<ShowType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT id, name, display_order
FROM show_types
ORDER BY display_order, name;
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        var items = new List<ShowType>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new ShowType
            {
                Id = reader.GetInt64(0),
                Name = reader.GetString(1),
                DisplayOrder = reader.GetInt32(2)
            });
        }

        return items;
    }

    public async Task<long> CreateAsync(ShowType showType, CancellationToken cancellationToken = default)
    {
        const string sql = """
INSERT INTO show_types (name, display_order)
VALUES (@name, @displayOrder);
SELECT LAST_INSERT_ID();
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        var pName = command.CreateParameter(); pName.ParameterName = "@name"; pName.Value = showType.Name; command.Parameters.Add(pName);
        var pOrd = command.CreateParameter(); pOrd.ParameterName = "@displayOrder"; pOrd.Value = showType.DisplayOrder; command.Parameters.Add(pOrd);

        var result = await command.ExecuteScalarAsync(cancellationToken);
        return Convert.ToInt64(result);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        const string sql = "DELETE FROM show_types WHERE id = @id;";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        var param = command.CreateParameter(); param.ParameterName = "@id"; param.Value = id; command.Parameters.Add(param);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
