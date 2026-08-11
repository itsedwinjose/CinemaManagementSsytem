using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Core.Services;

public sealed class CashoutService(
    ICashoutRepository cashoutRepository,
    IApplicationSettingRepository settingRepository) : ICashoutService
{
    public async Task<IReadOnlyList<Screening>> GetEligibleScreeningsAsync(long cinemaId, DateOnly date, CancellationToken cancellationToken = default)
    {
        var delaySetting = await settingRepository.GetByKeyAsync("cashout_delay_minutes", cancellationToken);
        int delayMinutes = int.TryParse(delaySetting?.SettingValue, out var m) ? m : 30;
        return await cashoutRepository.GetEligibleCashoutScreeningsAsync(cinemaId, date, delayMinutes, cancellationToken);
    }

    public Task<CashoutRecord?> CalculateCashoutAsync(long screeningId, CancellationToken cancellationToken = default)
        => cashoutRepository.CalculateCashoutForScreeningAsync(screeningId, cancellationToken);

    public Task ProcessCashoutAsync(CashoutRecord record, string operatorName, CancellationToken cancellationToken = default)
    {
        var recordToSave = new CashoutRecord
        {
            CinemaId = record.CinemaId,
            ShowDate = record.ShowDate,
            ShowTime = record.ShowTime,
            ShowTypeName = record.ShowTypeName,
            MovieName = record.MovieName,
            SoldSeats = record.SoldSeats,
            FreeSeats = record.FreeSeats,
            ReservedSeats = record.ReservedSeats,
            TicketAmount = record.TicketAmount,
            ReservationAmount = record.ReservationAmount,
            ThreeDAmount = record.ThreeDAmount,
            TotalAmount = record.TotalAmount,
            CashAmount = record.CashAmount,
            CardAmount = record.CardAmount,
            OnlineAmount = record.OnlineAmount,
            UpiAmount = record.UpiAmount,
            CashedOutAt = DateTime.UtcNow,
            CashedOutBy = operatorName,
            IsPrinted = false
        };

        return cashoutRepository.SaveCashoutRecordAsync(recordToSave, cancellationToken);
    }

    public Task<IReadOnlyList<CashoutRecord>> GetUnprintedReportsAsync(long cinemaId, CancellationToken cancellationToken = default)
        => cashoutRepository.GetUnprintedCashoutReportsAsync(cinemaId, cancellationToken);

    public Task MarkReportPrintedAsync(long cashoutId, string printedBy, CancellationToken cancellationToken = default)
        => cashoutRepository.MarkCashoutReportPrintedAsync(cashoutId, printedBy, cancellationToken);
}
