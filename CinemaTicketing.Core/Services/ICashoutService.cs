using CinemaTicketing.Core.Entities;

namespace CinemaTicketing.Core.Services;

public interface ICashoutService
{
    Task<IReadOnlyList<Screening>> GetEligibleScreeningsAsync(long cinemaId, DateOnly date, CancellationToken cancellationToken = default);
    Task<CashoutRecord?> CalculateCashoutAsync(long screeningId, CancellationToken cancellationToken = default);
    Task ProcessCashoutAsync(CashoutRecord record, string operatorName, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CashoutRecord>> GetUnprintedReportsAsync(long cinemaId, CancellationToken cancellationToken = default);
    Task MarkReportPrintedAsync(long cashoutId, string printedBy, CancellationToken cancellationToken = default);
}
