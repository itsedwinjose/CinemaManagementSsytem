using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Enums;
using CinemaTicketing.Core.Repositories;
using CinemaTicketing.Core.Services;

namespace CinemaTicketing.Tests;

public class BookingServiceTests
{
    [Fact]
    public async Task ProcessBookingAsync_CalculatesCorrectTicketTotalAndCharges()
    {
        var bookingRepo = new StubBookingRepository();
        var settingRepo = new StubSettingRepository();
        var service = new BookingService(bookingRepo, settingRepo);

        var seats = new List<ScreeningSeat>
        {
            new() { Id = 1, SeatNumber = "A1", Price = 150m, Status = SeatStatus.Available },
            new() { Id = 2, SeatNumber = "A2", Price = 200m, Status = SeatStatus.Available }
        };

        var request = new BookingRequest(
            ScreeningId: 1,
            UserId: 1,
            BookingType: BookingType.CounterTicket,
            PaymentMode: PaymentMode.Cash,
            CustomerName: "John",
            CustomerPhone: "12345",
            CustomerAddress: "City",
            ApplyReservationCharge: true,
            Apply3DCharge: true,
            SelectedSeats: seats
        );

        var result = await service.ProcessBookingAsync(request);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Booking);
        Assert.Equal(350m, result.Booking.TicketAmount);
        Assert.Equal(20m, result.Booking.ReservationAmount); // 2 seats * 10
        Assert.Equal(60m, result.Booking.ThreeDAmount); // 2 seats * 30
        Assert.Equal(430m, result.Booking.TotalAmount);
    }

    [Fact]
    public async Task ProcessBookingAsync_FreeTicket_SetsZeroCostAmounts()
    {
        var bookingRepo = new StubBookingRepository();
        var settingRepo = new StubSettingRepository();
        var service = new BookingService(bookingRepo, settingRepo);

        var seats = new List<ScreeningSeat>
        {
            new() { Id = 1, SeatNumber = "A1", Price = 150m, Status = SeatStatus.Available }
        };

        var request = new BookingRequest(
            ScreeningId: 1,
            UserId: 1,
            BookingType: BookingType.FreeTicket,
            PaymentMode: PaymentMode.Cash,
            CustomerName: "Guest",
            CustomerPhone: "",
            CustomerAddress: "",
            ApplyReservationCharge: true,
            Apply3DCharge: true,
            SelectedSeats: seats
        );

        var result = await service.ProcessBookingAsync(request);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Booking);
        Assert.Equal(BookingType.FreeTicket, result.Booking.BookingType);
        Assert.Equal(0m, result.Booking.TicketAmount);
        Assert.Equal(0m, result.Booking.ReservationAmount);
        Assert.Equal(0m, result.Booking.ThreeDAmount);
        Assert.Equal(0m, result.Booking.TotalAmount);
        Assert.All(result.Booking.Seats, seat => Assert.Equal(0m, seat.Price));
    }

    [Fact]
    public async Task ProcessBookingAsync_CounterReservation_PreservesBookingTypeAndPaymentMode()
    {
        var bookingRepo = new StubBookingRepository();
        var settingRepo = new StubSettingRepository();
        var service = new BookingService(bookingRepo, settingRepo);
        var request = new BookingRequest(
            ScreeningId: 1,
            UserId: 1,
            BookingType: BookingType.CounterReservation,
            PaymentMode: PaymentMode.Card,
            CustomerName: "Guest",
            CustomerPhone: "",
            CustomerAddress: "",
            ApplyReservationCharge: false,
            Apply3DCharge: false,
            SelectedSeats: new List<ScreeningSeat>
            {
                new() { Id = 1, SeatNumber = "A1", Price = 150m, Status = SeatStatus.Available }
            }
        );

        var result = await service.ProcessBookingAsync(request);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Booking);
        Assert.Equal(BookingType.CounterReservation, result.Booking.BookingType);
        Assert.Equal(PaymentMode.Card, result.Booking.PaymentMode);
    }

    [Fact]
    public async Task ProcessBookingAsync_TelephoneReservation_PreservesBookingTypeAndPaymentMode()
    {
        var bookingRepo = new StubBookingRepository();
        var settingRepo = new StubSettingRepository();
        var service = new BookingService(bookingRepo, settingRepo);
        var request = new BookingRequest(
            ScreeningId: 1,
            UserId: 1,
            BookingType: BookingType.TelephoneReservation,
            PaymentMode: PaymentMode.UpiQr,
            CustomerName: "Guest",
            CustomerPhone: "1234567890",
            CustomerAddress: "",
            ApplyReservationCharge: false,
            Apply3DCharge: false,
            SelectedSeats: new List<ScreeningSeat>
            {
                new() { Id = 1, SeatNumber = "A1", Price = 150m, Status = SeatStatus.Available }
            }
        );

        var result = await service.ProcessBookingAsync(request);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Booking);
        Assert.Equal(BookingType.TelephoneReservation, result.Booking.BookingType);
        Assert.Equal(PaymentMode.UpiQr, result.Booking.PaymentMode);
    }

    [Theory]
    [InlineData(PaymentMode.Card)]
    [InlineData(PaymentMode.UpiQr)]
    public async Task ProcessBookingAsync_CounterTicket_PreservesSelectedPaymentMode(PaymentMode paymentMode)
    {
        var bookingRepo = new StubBookingRepository();
        var service = new BookingService(bookingRepo, new StubSettingRepository());
        var request = new BookingRequest(
            ScreeningId: 1,
            UserId: 1,
            BookingType: BookingType.CounterTicket,
            PaymentMode: paymentMode,
            CustomerName: "",
            CustomerPhone: "",
            CustomerAddress: "",
            ApplyReservationCharge: false,
            Apply3DCharge: false,
            SelectedSeats: new List<ScreeningSeat>
            {
                new() { Id = 1, SeatNumber = "A1", Price = 150m, Status = SeatStatus.Available }
            });

        var result = await service.ProcessBookingAsync(request);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Booking);
        Assert.Equal(paymentMode, result.Booking.PaymentMode);
    }

    [Theory]
    [InlineData(BookingType.CounterReservation)]
    [InlineData(BookingType.TelephoneReservation)]
    public async Task ProcessBookingAsync_ReservationsAddSelectedChargesPerSeat(BookingType bookingType)
    {
        var bookingRepo = new StubBookingRepository();
        var settingRepo = new StubSettingRepository();
        var service = new BookingService(bookingRepo, settingRepo);
        var seats = new List<ScreeningSeat>
        {
            new() { Id = 1, SeatNumber = "A1", Price = 150m, Status = SeatStatus.Available },
            new() { Id = 2, SeatNumber = "A2", Price = 200m, Status = SeatStatus.Available }
        };
        var request = new BookingRequest(
            ScreeningId: 1,
            UserId: 1,
            BookingType: bookingType,
            PaymentMode: PaymentMode.Cash,
            CustomerName: "Guest",
            CustomerPhone: "1234567890",
            CustomerAddress: "",
            ApplyReservationCharge: true,
            Apply3DCharge: true,
            SelectedSeats: seats);

        var result = await service.ProcessBookingAsync(request);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Booking);
        Assert.Equal(bookingType, result.Booking.BookingType);
        Assert.Equal(350m, result.Booking.TicketAmount);
        Assert.Equal(20m, result.Booking.ReservationAmount);
        Assert.Equal(60m, result.Booking.ThreeDAmount);
        Assert.Equal(430m, result.Booking.TotalAmount);
    }

    [Fact]
    public async Task ProcessBookingAsync_RejectsDamagedSeat()
    {
        var bookingRepo = new StubBookingRepository();
        var settingRepo = new StubSettingRepository();
        var service = new BookingService(bookingRepo, settingRepo);

        var seats = new List<ScreeningSeat>
        {
            new() { Id = 1, SeatNumber = "A1", Price = 150m, Status = SeatStatus.Available, IsDamaged = true }
        };

        var request = new BookingRequest(
            ScreeningId: 1,
            UserId: 1,
            BookingType: BookingType.CounterTicket,
            PaymentMode: PaymentMode.Cash,
            CustomerName: "",
            CustomerPhone: "",
            CustomerAddress: "",
            ApplyReservationCharge: false,
            Apply3DCharge: false,
            SelectedSeats: seats
        );

        var result = await service.ProcessBookingAsync(request);

        Assert.False(result.IsSuccess);
        Assert.Contains("damaged", result.Message, StringComparison.OrdinalIgnoreCase);
    }

    private sealed class StubBookingRepository : IBookingRepository
    {
        private Booking? _lastSaved;

        public Task<Booking?> GetByIdAsync(long bookingId, CancellationToken cancellationToken = default)
            => Task.FromResult(_lastSaved);

        public Task<Booking?> GetByBookingNumberAsync(string bookingNumber, CancellationToken cancellationToken = default)
            => Task.FromResult(_lastSaved);

        public Task<Booking?> GetLatestBookingAsync(CancellationToken cancellationToken = default)
            => Task.FromResult(_lastSaved);

        public Task<IReadOnlyList<Booking>> GetForScreeningSeatsAsync(
            long screeningId,
            IEnumerable<long> screeningSeatIds,
            CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Booking>>(Array.Empty<Booking>());

        public Task MarkTicketPrintedAsync(
            IEnumerable<long> bookingIds,
            IEnumerable<long> confirmedReservationBookingIds,
            CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task<long> ProcessBookingTransactionAsync(Booking booking, IEnumerable<long> screeningSeatIds, CancellationToken cancellationToken = default)
        {
            _lastSaved = booking;
            return Task.FromResult(1L);
        }
    }

    private sealed class StubSettingRepository : IApplicationSettingRepository
    {
        public Task<IReadOnlyList<ApplicationSetting>> GetAllAsync(CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<ApplicationSetting>>(Array.Empty<ApplicationSetting>());

        public Task<ApplicationSetting?> GetByKeyAsync(string key, CancellationToken cancellationToken = default)
        {
            if (key == "reservation_charge") return Task.FromResult<ApplicationSetting?>(new ApplicationSetting { SettingKey = key, SettingValue = "10" });
            if (key == "three_d_charge") return Task.FromResult<ApplicationSetting?>(new ApplicationSetting { SettingKey = key, SettingValue = "30" });
            return Task.FromResult<ApplicationSetting?>(null);
        }

        public Task UpsertAsync(ApplicationSetting setting, CancellationToken cancellationToken = default)
            => Task.CompletedTask;
    }
}
