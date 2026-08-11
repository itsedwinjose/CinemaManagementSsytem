namespace CinemaTicketing.Core.Configuration;

public sealed class DatabaseSettings
{
    public string Server { get; init; } = "127.0.0.1";
    public uint Port { get; init; } = 3306;
    public string Database { get; init; } = "cinema_ticketing";
    public string UserId { get; init; } = "root";
    public string Password { get; init; } = string.Empty;
    public string SslMode { get; init; } = "None";
    public uint ConnectionTimeoutSeconds { get; init; } = 15;
}
