namespace CinemaTicketing.Core.Diagnostics;

public interface IDatabaseHealthCheckService
{
    Task<DatabaseHealthCheckResult> CheckAsync(CancellationToken cancellationToken = default);
}
