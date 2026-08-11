using CinemaTicketing.Core.Data;
using CinemaTicketing.Core.Diagnostics;
using MySqlConnector;

namespace CinemaTicketing.Data.Database;

public sealed class DatabaseHealthCheckService(IMySqlConnectionFactory connectionFactory) : IDatabaseHealthCheckService
{
    public async Task<DatabaseHealthCheckResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await using var connection = connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT 1;";

            var result = await command.ExecuteScalarAsync(cancellationToken);
            var isHealthy = Convert.ToInt32(result) == 1;

            return isHealthy
                ? new DatabaseHealthCheckResult(true, $"Connected to MySQL successfully. Target: {connection.Database}")
                : new DatabaseHealthCheckResult(false, "MySQL responded, but the connectivity test returned an unexpected result.");
        }
        catch (MySqlException ex)
        {
            return new DatabaseHealthCheckResult(false, $"MySQL connection failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new DatabaseHealthCheckResult(false, $"Unexpected error while connecting: {ex.Message}");
        }
    }
}
