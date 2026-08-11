namespace CinemaTicketing.Core.Entities;

public sealed class User
{
    public long Id { get; init; }
    public string UserName { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
    public string PasswordHash { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public DateTime CreatedUtc { get; init; }
}
