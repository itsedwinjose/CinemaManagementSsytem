namespace CinemaTicketing.Core.Entities;

public sealed class Screening
{
    public long Id { get; init; }
    public long CinemaId { get; init; }
    public long AudiId { get; init; }
    public string AudiName { get; init; } = string.Empty;
    public long ShowTypeId { get; init; }
    public string ShowTypeName { get; init; } = string.Empty;
    public TimeSpan ShowTime { get; init; }
    public DateOnly ScreeningDate { get; init; }
    public long? MovieId { get; init; }
    public string MovieName { get; init; } = string.Empty;
    public bool IsMovie3D { get; init; }
    public bool IsCurrentShow { get; init; }
    public decimal DefaultPrice { get; init; }
}
