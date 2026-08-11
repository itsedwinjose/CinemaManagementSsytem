namespace CinemaTicketing.Core.Entities;

public sealed class Audi
{
    public long Id { get; init; }
    public long CinemaId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int DisplayOrder { get; init; } = 1;
    public int TotalRows { get; init; } = 10;
    public int TotalCols { get; init; } = 15;
}
