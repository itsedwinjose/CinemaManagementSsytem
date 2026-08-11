namespace CinemaTicketing.Core.Entities;

public sealed class TheatreSetting
{
    public long Id { get; init; }
    public long CinemaId { get; init; }
    public string CinemaName { get; init; } = string.Empty;
    public long ShowTypeId { get; init; }
    public string ShowTypeName { get; init; } = string.Empty;
    public TimeSpan ShowTime { get; init; }
    public decimal Price { get; init; }
}
