namespace CinemaTicketing.Core.Entities;

public sealed class AudiLayoutCell
{
    public long Id { get; init; }
    public long AudiId { get; init; }
    public int RowIndex { get; init; }
    public int ColIndex { get; init; }
    public bool IsSeat { get; init; } = true;
    public string RowLabel { get; init; } = string.Empty;
    public string SeatNumber { get; init; } = string.Empty;
    public long? SeatClassId { get; init; }
    public string SeatClassName { get; init; } = string.Empty;
    public bool IsDamaged { get; init; }
}
