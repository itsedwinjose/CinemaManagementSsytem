using System.Data.Common;
using CinemaTicketing.Core.Configuration;
using CinemaTicketing.Core.Data;
using MySqlConnector;

namespace CinemaTicketing.Data.Database;

public sealed class MySqlConnectionFactory(DatabaseSettings settings) : IMySqlConnectionFactory
{
    private readonly string _connectionString = BuildConnectionString(settings);
    private readonly string _displayConnectionString = BuildDisplayConnectionString(settings);

    public DbConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString);
    }

    public string GetDisplayConnectionString()
    {
        return _displayConnectionString;
    }

    private static string BuildConnectionString(DatabaseSettings settings)
    {
        var builder = new MySqlConnectionStringBuilder
        {
            Server = settings.Server,
            Port = settings.Port,
            Database = settings.Database,
            UserID = settings.UserId,
            Password = settings.Password,
            ConnectionTimeout = settings.ConnectionTimeoutSeconds,
            SslMode = Enum.TryParse<MySqlSslMode>(settings.SslMode, true, out var sslMode)
                ? sslMode
                : MySqlSslMode.None
        };

        return builder.ConnectionString;
    }

    private static string BuildDisplayConnectionString(DatabaseSettings settings)
    {
        return $"{settings.Server}:{settings.Port}/{settings.Database} ({settings.UserId})";
    }
}
