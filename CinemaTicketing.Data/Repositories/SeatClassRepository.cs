using System.Data.Common;
using CinemaTicketing.Core.Data;
using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Data.Repositories;

public sealed class SeatClassRepository(IMySqlConnectionFactory connectionFactory) : ISeatClassRepository
{
    public async Task<IReadOnlyList<SeatClass>> GetAllActiveAsync(CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT id, name, display_order, is_active FROM seat_classes WHERE is_active = 1 ORDER BY display_order, name;";
        return await QueryListAsync(sql, cancellationToken);
    }

    public async Task<IReadOnlyList<SeatClass>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT id, name, display_order, is_active FROM seat_classes ORDER BY display_order, name;";
        return await QueryListAsync(sql, cancellationToken);
    }

    public async Task<long> CreateAsync(SeatClass seatClass, CancellationToken cancellationToken = default)
    {
        const string sql = """
INSERT INTO seat_classes (name, display_order, is_active)
VALUES (@name, @displayOrder, @isActive);
SELECT LAST_INSERT_ID();
""";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@name", seatClass.Name);
        AddParameter(command, "@displayOrder", seatClass.DisplayOrder);
        AddParameter(command, "@isActive", seatClass.IsActive ? 1 : 0);

        var result = await command.ExecuteScalarAsync(cancellationToken);
        return Convert.ToInt64(result);
    }

    public async Task UpdateAsync(SeatClass seatClass, CancellationToken cancellationToken = default)
    {
        const string sql = """
UPDATE seat_classes
SET name = @name, display_order = @displayOrder, is_active = @isActive
WHERE id = @id;
""";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@id", seatClass.Id);
        AddParameter(command, "@name", seatClass.Name);
        AddParameter(command, "@displayOrder", seatClass.DisplayOrder);
        AddParameter(command, "@isActive", seatClass.IsActive ? 1 : 0);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        const string sql = "DELETE FROM seat_classes WHERE id = @id;";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@id", id);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task<IReadOnlyList<SeatClass>> QueryListAsync(string sql, CancellationToken cancellationToken)
    {
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        var list = new List<SeatClass>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            list.Add(new SeatClass
            {
                Id = reader.GetInt64(0),
                Name = reader.GetString(1),
                DisplayOrder = reader.GetInt32(2),
                IsActive = reader.GetBoolean(3)
            });
        }
        return list;
    }

    private static void AddParameter(DbCommand command, string name, object value)
    {
        var param = command.CreateParameter();
        param.ParameterName = name;
        param.Value = value;
        command.Parameters.Add(param);
    }
}
