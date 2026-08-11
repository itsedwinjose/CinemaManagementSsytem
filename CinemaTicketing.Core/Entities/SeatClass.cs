namespace CinemaTicketing.Core.Entities;

public sealed class SeatClass
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int DisplayOrder { get; init; } = 1;
    public bool IsActive { get; init; } = true;
}
