namespace CinemaTicketing.Core.Entities;

public sealed class ShowType
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int DisplayOrder { get; init; }
}
