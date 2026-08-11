namespace CinemaTicketing.Core.Entities;

public sealed class Movie
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public bool Is3D { get; init; }
    public bool IsActive { get; init; } = true;
}
