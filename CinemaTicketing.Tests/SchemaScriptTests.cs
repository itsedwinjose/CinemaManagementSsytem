namespace CinemaTicketing.Tests;

public class SchemaScriptTests
{
    [Fact]
    public void CreateSchemaScript_ContainsUsersAndSettingsTables()
    {
        var root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        var scriptPath = Path.Combine(root, "CinemaTicketing.Data", "Database", "Scripts", "001_create_schema.sql");
        var sql = File.ReadAllText(scriptPath);

        Assert.Contains("CREATE TABLE IF NOT EXISTS users", sql);
        Assert.Contains("CREATE TABLE IF NOT EXISTS application_settings", sql);
    }

    [Fact]
    public void TicketPrintedMigration_AddsPersistentSeatFlag()
    {
        var root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        var scriptPath = Path.Combine(root, "CinemaTicketing.Data", "Database", "Scripts", "006_add_ticket_printed_flag.sql");
        var sql = File.ReadAllText(scriptPath);

        Assert.Contains("ALTER TABLE screening_seats ADD COLUMN is_ticket_printed", sql);
    }
}
