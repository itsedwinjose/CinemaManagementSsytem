using System.Data.Common;
using CinemaTicketing.Core.Data;
using CinemaTicketing.Core.Services;

namespace CinemaTicketing.Data.Database;

public sealed class AdminResetService(IMySqlConnectionFactory connectionFactory) : IAdminResetService
{
    public async Task ResetAllDataExceptUsersAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        var tableNames = new List<string>();
        await using (var tablesCommand = connection.CreateCommand())
        {
            tablesCommand.CommandText = """
SELECT TABLE_NAME
FROM information_schema.TABLES
WHERE TABLE_SCHEMA = DATABASE()
  AND TABLE_TYPE = 'BASE TABLE'
  AND LOWER(TABLE_NAME) <> 'users';
""";
            await using var reader = await tablesCommand.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                tableNames.Add(reader.GetString(0));
            }
        }

        await using (var foreignKeyCommand = connection.CreateCommand())
        {
            foreignKeyCommand.CommandText = "SET FOREIGN_KEY_CHECKS = 0;";
            await foreignKeyCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        try
        {
            await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
            try
            {
                foreach (var tableName in tableNames)
                {
                    await using var deleteCommand = connection.CreateCommand();
                    deleteCommand.Transaction = transaction;
                    deleteCommand.CommandText = $"DELETE FROM `{tableName.Replace("`", "``", StringComparison.Ordinal)}`;";
                    await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
                }

                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
        finally
        {
            await using var foreignKeyCommand = connection.CreateCommand();
            foreignKeyCommand.CommandText = "SET FOREIGN_KEY_CHECKS = 1;";
            await foreignKeyCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }
}
