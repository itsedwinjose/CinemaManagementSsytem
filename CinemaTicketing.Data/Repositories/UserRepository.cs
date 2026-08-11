using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;
using CinemaTicketing.Core.Security;

namespace CinemaTicketing.Data.Repositories;

public sealed class UserRepository(
    CinemaTicketing.Core.Data.IMySqlConnectionFactory connectionFactory,
    IPasswordHasher passwordHasher) : IUserRepository
{
    public async Task<bool> AnyUsersAsync(CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT COUNT(*) FROM users;";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        var count = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
        return count > 0;
    }

    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT id, user_name, display_name, password_hash, is_active, created_utc
FROM users
WHERE user_name = @userName;
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@userName", userName);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new User
        {
            Id = reader.GetInt64(0),
            UserName = reader.GetString(1),
            DisplayName = reader.GetString(2),
            PasswordHash = reader.GetString(3),
            IsActive = reader.GetBoolean(4),
            CreatedUtc = reader.GetDateTime(5)
        };
    }

    public async Task<long> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        const string sql = """
INSERT INTO users (user_name, display_name, password_hash, is_active)
VALUES (@userName, @displayName, @passwordHash, @isActive);
SELECT LAST_INSERT_ID();
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@userName", user.UserName);
        AddParameter(command, "@displayName", user.DisplayName);
        AddParameter(command, "@passwordHash", user.PasswordHash);
        AddParameter(command, "@isActive", user.IsActive);

        var result = await command.ExecuteScalarAsync(cancellationToken);
        return Convert.ToInt64(result);
    }

    public async Task<bool> ValidateCredentialsAsync(string userName, string password, CancellationToken cancellationToken = default)
    {
        var user = await GetByUserNameAsync(userName, cancellationToken);
        if (user is null || !user.IsActive)
        {
            return false;
        }

        return passwordHasher.VerifyPassword(password, user.PasswordHash);
    }

    public async Task UpdatePasswordHashAsync(long userId, string passwordHash, CancellationToken cancellationToken = default)
    {
        const string sql = "UPDATE users SET password_hash = @passwordHash WHERE id = @id;";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@id", userId);
        AddParameter(command, "@passwordHash", passwordHash);

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
