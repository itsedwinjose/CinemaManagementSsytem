using System.Data.Common;
using CinemaTicketing.Core.Data;
using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Data.Repositories;

public sealed class MovieRepository(IMySqlConnectionFactory connectionFactory) : IMovieRepository
{
    public async Task<IReadOnlyList<Movie>> GetAllActiveAsync(CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT id, name, is_3d, is_active FROM movies WHERE is_active = 1 ORDER BY name;";
        return await QueryListAsync(sql, cancellationToken);
    }

    public async Task<IReadOnlyList<Movie>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT id, name, is_3d, is_active FROM movies ORDER BY name;";
        return await QueryListAsync(sql, cancellationToken);
    }

    public async Task<long> CreateAsync(Movie movie, CancellationToken cancellationToken = default)
    {
        const string sql = """
INSERT INTO movies (name, is_3d, is_active)
VALUES (@name, @is3D, @isActive);
SELECT LAST_INSERT_ID();
""";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@name", movie.Name);
        AddParameter(command, "@is3D", movie.Is3D ? 1 : 0);
        AddParameter(command, "@isActive", movie.IsActive ? 1 : 0);

        var result = await command.ExecuteScalarAsync(cancellationToken);
        return Convert.ToInt64(result);
    }

    public async Task UpdateAsync(Movie movie, CancellationToken cancellationToken = default)
    {
        const string sql = """
UPDATE movies
SET name = @name, is_3d = @is3D, is_active = @isActive
WHERE id = @id;
""";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@id", movie.Id);
        AddParameter(command, "@name", movie.Name);
        AddParameter(command, "@is3D", movie.Is3D ? 1 : 0);
        AddParameter(command, "@isActive", movie.IsActive ? 1 : 0);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        const string sql = "DELETE FROM movies WHERE id = @id;";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@id", id);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task<IReadOnlyList<Movie>> QueryListAsync(string sql, CancellationToken cancellationToken)
    {
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        var list = new List<Movie>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            list.Add(new Movie
            {
                Id = reader.GetInt64(0),
                Name = reader.GetString(1),
                Is3D = reader.GetBoolean(2),
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
