using CinemaTicketing.Core.Enums;

namespace CinemaTicketing.Core.Entities;

public sealed class ScreeningSeat
{
    public long Id { get; init; }
    public long ScreeningId { get; init; }
    public long AudiLayoutCellId { get; init; }
    public int RowIndex { get; init; }
    public int ColIndex { get; init; }
    public bool IsSeat { get; init; }
    public string RowLabel { get; init; } = string.Empty;
    public string SeatNumber { get; init; } = string.Empty;
    public long? SeatClassId { get; init; }
    public string SeatClassName { get; init; } = string.Empty;
    public bool IsDamaged { get; init; }
    public SeatStatus Status { get; init; } = SeatStatus.Available;
    public decimal Price { get; init; }
}
