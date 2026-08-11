using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Enums;
using CinemaTicketing.WinForms.Printing;

namespace CinemaTicketing.Tests;

public class ThermalPrintServiceTests
{
    [Fact]
    public void BuildTicketText_FormatsCinemaAndSeatInfoCorrectly()
    {
        var cinema = new CinemaInfo { Name = "Star Multiplex", Mobile = "9876543210", GstIn = "GST12345" };
        var screening = new Screening { AudiName = "Audi-1", ScreeningDate = new DateOnly(2026, 8, 11), ShowTime = new TimeSpan(14, 30, 0), ShowTypeName = "Matinee", MovieName = "Avatar", IsMovie3D = true };
        var booking = new Booking
        {
            BookingNumber = "BK-123",
            TicketAmount = 300m,
            ThreeDAmount = 60m,
            TotalAmount = 360m,
            PaymentMode = PaymentMode.Cash,
            Seats = new List<BookingSeat>
            {
                new() { SeatNumber = "A1", SeatClassName = "Executive", Price = 150m },
                new() { SeatNumber = "A2", SeatClassName = "Executive", Price = 150m }
            }
        };

        var text = ThermalPrintService.BuildTicketText(booking, cinema, screening);

        Assert.Contains("STAR MULTIPLEX", text);
        Assert.Contains("GSTIN: GST12345", text);
        Assert.Contains("Audi-1", text);
        Assert.Contains("Avatar (3D)", text);
        Assert.Contains("A1", text);
        Assert.Contains("A2", text);
        Assert.Contains("360.00", text);
    }

    [Fact]
    public void BuildCashoutReportText_FormatsFinancialBreakdown()
    {
        var cinema = new CinemaInfo { Name = "Star Multiplex" };
        var record = new CashoutRecord
        {
            ShowDate = new DateOnly(2026, 8, 11),
            ShowTime = new TimeSpan(11, 0, 0),
            ShowTypeName = "Morning Show",
            MovieName = "Oppenheimer",
            SoldSeats = 50,
            FreeSeats = 2,
            ReservedSeats = 5,
            TicketAmount = 7500m,
            ReservationAmount = 50m,
            ThreeDAmount = 0m,
            TotalAmount = 7550m,
            CashAmount = 5000m,
            CardAmount = 2550m,
            CashedOutBy = "Operator1"
        };

        var text = ThermalPrintService.BuildCashoutReportText(record, cinema);

        Assert.Contains("CASHOUT REPORT", text);
        Assert.Contains("Morning Show", text);
        Assert.Contains("Oppenheimer", text);
        Assert.Contains("7550.00", text);
        Assert.Contains("5000.00", text);
    }
}
