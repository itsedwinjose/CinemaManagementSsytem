using System.Data.Common;
using CinemaTicketing.Core.Data;

namespace CinemaTicketing.Data.Database;

public sealed class DatabaseBootstrapper(IMySqlConnectionFactory connectionFactory) : IDatabaseBootstrapper
{
    private static readonly string[] ScriptNames =
    {
        "001_create_schema.sql",
        "002_seed_reference_data.sql",
        "003_expand_schema.sql",
        "004_seed_test_layout_data.sql"
    };

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        foreach (var scriptName in ScriptNames)
        {
            var script = await LoadScriptAsync(scriptName, cancellationToken);
            await ExecuteScriptAsync(connection, script, cancellationToken);
        }
    }

    private static async Task<string> LoadScriptAsync(string scriptName, CancellationToken cancellationToken)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Database", "Scripts", scriptName);
        if (!File.Exists(path))
        {
            path = Path.Combine(AppContext.BaseDirectory, scriptName);
        }

        return await File.ReadAllTextAsync(path, cancellationToken);
    }

    private static async Task ExecuteScriptAsync(DbConnection connection, string script, CancellationToken cancellationToken)
    {
        await using (var fkOffCmd = connection.CreateCommand())
        {
            fkOffCmd.CommandText = "SET FOREIGN_KEY_CHECKS = 0;";
            await fkOffCmd.ExecuteNonQueryAsync(cancellationToken);
        }

        var statements = script
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(static statement => !string.IsNullOrWhiteSpace(statement));

        foreach (var statement in statements)
        {
            try
            {
                await using var command = connection.CreateCommand();
                command.CommandText = statement;
                await command.ExecuteNonQueryAsync(cancellationToken);
            }
            catch (DbException ex) when (statement.StartsWith("ALTER TABLE", StringComparison.OrdinalIgnoreCase))
            {
                System.Diagnostics.Debug.WriteLine($"Migration notice: {ex.Message}");
            }
        }

        await using (var fkOnCmd = connection.CreateCommand())
        {
            fkOnCmd.CommandText = "SET FOREIGN_KEY_CHECKS = 1;";
            await fkOnCmd.ExecuteNonQueryAsync(cancellationToken);
        }
    }
}
