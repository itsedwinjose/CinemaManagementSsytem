namespace CinemaTicketing.Core.Diagnostics;

public sealed record DatabaseHealthCheckResult(bool IsSuccess, string Message);
