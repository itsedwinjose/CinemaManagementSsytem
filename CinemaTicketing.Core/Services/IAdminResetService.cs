namespace CinemaTicketing.Core.Services;

public interface IAdminResetService
{
    Task ResetAllDataExceptUsersAsync(CancellationToken cancellationToken = default);
}
