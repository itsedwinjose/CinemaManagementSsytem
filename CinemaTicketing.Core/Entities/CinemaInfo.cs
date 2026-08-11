namespace CinemaTicketing.Core.Entities;

public sealed class CinemaInfo
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string Mobile { get; init; } = string.Empty;
    public string GstIn { get; init; } = string.Empty;
    public bool IsActive { get; init; }
}
