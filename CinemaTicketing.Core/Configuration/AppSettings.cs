namespace CinemaTicketing.Core.Configuration;

public sealed class AppSettings
{
    public DatabaseSettings Database { get; init; } = new();
}
