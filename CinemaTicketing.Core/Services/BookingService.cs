using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Enums;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Core.Services;

public sealed class BookingService(
    IBookingRepository bookingRepository,
    IApplicationSettingRepository settingRepository) : IBookingService
{
    public async Task<BookingResult> ProcessBookingAsync(BookingRequest request, CancellationToken cancellationToken = default)
    {
        if (request.SelectedSeats.Count == 0)
        {
            return new BookingResult(false, "No seats selected.");
        }

        foreach (var seat in request.SelectedSeats)
        {
            if (!seat.IsSeat)
            {
                return new BookingResult(false, "Non-seat / stair space cannot be booked.");
            }
            if (seat.IsDamaged)
            {
                return new BookingResult(false, $"Seat {seat.SeatNumber} is damaged and cannot be booked.");
            }
            if (seat.Status != SeatStatus.Available)
            {
                return new BookingResult(false, $"Seat {seat.SeatNumber} is not available.");
            }
        }

        // Fetch charge settings
        var resChargeSetting = await settingRepository.GetByKeyAsync("reservation_charge_per_seat", cancellationToken);
        var resChargePerSeat = decimal.TryParse(resChargeSetting?.SettingValue, out var rc) ? rc : 10m;

        var threeDChargeSetting = await settingRepository.GetByKeyAsync("three_d_charge_per_seat", cancellationToken);
        var threeDChargePerSeat = decimal.TryParse(threeDChargeSetting?.SettingValue, out var td) ? td : 30m;

        decimal ticketAmount = 0m;
        var bookingSeats = new List<BookingSeat>();

        foreach (var s in request.SelectedSeats)
        {
            ticketAmount += s.Price;
            bookingSeats.Add(new BookingSeat
            {
                ScreeningSeatId = s.Id,
                SeatClassName = string.IsNullOrWhiteSpace(s.SeatClassName) ? "Normal" : s.SeatClassName,
                SeatNumber = s.SeatNumber,
                Price = s.Price
            });
        }

        decimal reservationAmount = request.ApplyReservationCharge ? (request.SelectedSeats.Count * resChargePerSeat) : 0m;
        decimal threeDAmount = request.Apply3DCharge ? (request.SelectedSeats.Count * threeDChargePerSeat) : 0m;

        // If free ticket, zero cost ticket amount
        if (request.BookingType == BookingType.FreeTicket)
        {
            ticketAmount = 0m;
            reservationAmount = 0m;
            threeDAmount = 0m;
        }

        decimal totalAmount = ticketAmount + reservationAmount + threeDAmount;
        string bookingNum = $"BK-{DateTime.UtcNow:yyyyMMddHHmmss}-{Random.Shared.Next(1000, 9999)}";

        var booking = new Booking
        {
            ScreeningId = request.ScreeningId,
            UserId = request.UserId,
            BookingNumber = bookingNum,
            BookingType = request.BookingType,
            PaymentMode = request.PaymentMode,
            CustomerName = request.CustomerName,
            CustomerPhone = request.CustomerPhone,
            CustomerAddress = request.CustomerAddress,
            TicketAmount = ticketAmount,
            ReservationAmount = reservationAmount,
            ThreeDAmount = threeDAmount,
            TaxAmount = 0m,
            TotalAmount = totalAmount,
            CreatedUtc = DateTime.UtcNow,
            Seats = bookingSeats
        };

        try
        {
            var bookingId = await bookingRepository.ProcessBookingTransactionAsync(
                booking,
                request.SelectedSeats.Select(s => s.Id),
                cancellationToken);

            var createdBooking = await bookingRepository.GetByIdAsync(bookingId, cancellationToken);
            return new BookingResult(true, "Booking completed successfully.", createdBooking);
        }
        catch (Exception ex)
        {
            return new BookingResult(false, $"Booking failed: {ex.Message}");
        }
    }

    public Task<Booking?> GetBookingByIdAsync(long bookingId, CancellationToken cancellationToken = default)
        => bookingRepository.GetByIdAsync(bookingId, cancellationToken);

    public Task<Booking?> GetLatestBookingAsync(CancellationToken cancellationToken = default)
        => bookingRepository.GetLatestBookingAsync(cancellationToken);
}
