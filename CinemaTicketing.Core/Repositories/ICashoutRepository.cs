using CinemaTicketing.Core.Entities;

namespace CinemaTicketing.Core.Repositories;

public interface ICashoutRepository
{
    Task<IReadOnlyList<Screening>> GetEligibleCashoutScreeningsAsync(long cinemaId, DateOnly date, int delayMinutes, CancellationToken cancellationToken = default);
    Task<CashoutRecord?> CalculateCashoutForScreeningAsync(long screeningId, CancellationToken cancellationToken = default);
    Task SaveCashoutRecordAsync(CashoutRecord record, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CashoutRecord>> GetUnprintedCashoutReportsAsync(long cinemaId, CancellationToken cancellationToken = default);
    Task MarkCashoutReportPrintedAsync(long cashoutId, string printedBy, CancellationToken cancellationToken = default);
}
