using System.Drawing.Printing;
using System.Text;
using CinemaTicketing.Core.Entities;

namespace CinemaTicketing.WinForms.Printing;

public interface IThermalPrintService
{
    Task<bool> PrintTicketAsync(Booking booking, CinemaInfo cinema, Screening screening, string printerName);
    Task<bool> PrintCashoutReportAsync(CashoutRecord record, CinemaInfo cinema, string printerName);
}

public sealed class ThermalPrintService : IThermalPrintService
{
    public Task<bool> PrintTicketAsync(Booking booking, CinemaInfo cinema, Screening screening, string printerName)
    {
        var text = BuildTicketText(booking, cinema, screening);
        return Task.FromResult(SendToPrinter(text, printerName, "Customer Ticket"));
    }

    public Task<bool> PrintCashoutReportAsync(CashoutRecord record, CinemaInfo cinema, string printerName)
    {
        var text = BuildCashoutReportText(record, cinema);
        return Task.FromResult(SendToPrinter(text, printerName, "Cashout Report"));
    }

    public static string BuildTicketText(Booking booking, CinemaInfo cinema, Screening screening)
    {
        var sb = new StringBuilder();
        sb.AppendLine("========================================");
        sb.AppendLine($"           {cinema.Name.ToUpperInvariant()}           ");
        sb.AppendLine($"Mobile: {cinema.Mobile} | GSTIN: {cinema.GstIn}");
        sb.AppendLine("========================================");
        sb.AppendLine($"Audi      : {screening.AudiName}");
        sb.AppendLine($"Date      : {screening.ScreeningDate:dd-MMM-yyyy} ({screening.ScreeningDate.DayOfWeek})");
        sb.AppendLine($"Time      : {DateTime.Today.Add(screening.ShowTime):hh:mm tt}");
        sb.AppendLine($"Show Type : {screening.ShowTypeName}");
        sb.AppendLine($"Movie     : {screening.MovieName} {(screening.IsMovie3D ? "(3D)" : "")}");
        sb.AppendLine("----------------------------------------");
        sb.AppendLine("SEATS :");
        foreach (var seat in booking.Seats)
        {
            sb.AppendLine($"  {seat.SeatNumber,-8} [{seat.SeatClassName,-10}] : ₹{seat.Price,6:F2}");
        }
        sb.AppendLine("----------------------------------------");
        sb.AppendLine($"Ticket Amt : ₹{booking.TicketAmount,8:F2}");
        if (booking.ReservationAmount > 0)
        {
            sb.AppendLine($"Resv Charge: ₹{booking.ReservationAmount,8:F2}");
        }
        if (booking.ThreeDAmount > 0)
        {
            sb.AppendLine($"3D Charge  : ₹{booking.ThreeDAmount,8:F2}");
        }
        sb.AppendLine($"TOTAL      : ₹{booking.TotalAmount,8:F2}");
        sb.AppendLine($"Payment    : {booking.PaymentMode}");
        sb.AppendLine($"Booking #  : {booking.BookingNumber}");
        sb.AppendLine("========================================");
        sb.AppendLine("    Thank you for visiting! Enjoy!     ");
        sb.AppendLine("========================================");
        return sb.ToString();
    }

    public static string BuildCashoutReportText(CashoutRecord record, CinemaInfo cinema)
    {
        var sb = new StringBuilder();
        sb.AppendLine("========================================");
        sb.AppendLine("             CASHOUT REPORT             ");
        sb.AppendLine($"Cinema: {cinema.Name}");
        sb.AppendLine($"Cashed Out At: {record.CashedOutAt:dd-MMM-yyyy hh:mm tt}");
        sb.AppendLine($"Cashed Out By: {record.CashedOutBy}");
        sb.AppendLine("========================================");
        sb.AppendLine($"Show Date : {record.ShowDate:dd-MMM-yyyy}");
        sb.AppendLine($"Show Time : {DateTime.Today.Add(record.ShowTime):hh:mm tt}");
        sb.AppendLine($"Show Type : {record.ShowTypeName}");
        sb.AppendLine($"Movie     : {record.MovieName}");
        sb.AppendLine("----------------------------------------");
        sb.AppendLine($"Sold Seats       : {record.SoldSeats}");
        sb.AppendLine($"Free Seats       : {record.FreeSeats}");
        sb.AppendLine($"Reserved Seats   : {record.ReservedSeats}");
        sb.AppendLine("----------------------------------------");
        sb.AppendLine($"Ticket Amount    : ₹{record.TicketAmount,9:F2}");
        sb.AppendLine($"Reservation Amt  : ₹{record.ReservationAmount,9:F2}");
        sb.AppendLine($"3D Amount        : ₹{record.ThreeDAmount,9:F2}");
        sb.AppendLine($"TOTAL AMOUNT     : ₹{record.TotalAmount,9:F2}");
        sb.AppendLine("----------------------------------------");
        sb.AppendLine("PAYMENT BREAKDOWN:");
        sb.AppendLine($"  Cash    : ₹{record.CashAmount,9:F2}");
        sb.AppendLine($"  Card    : ₹{record.CardAmount,9:F2}");
        sb.AppendLine($"  Online  : ₹{record.OnlineAmount,9:F2}");
        sb.AppendLine($"  UPI/QR  : ₹{record.UpiAmount,9:F2}");
        sb.AppendLine("========================================");
        return sb.ToString();
    }

    private static bool SendToPrinter(string printText, string printerName, string documentName)
    {
        try
        {
            using var pd = new PrintDocument();
            if (!string.IsNullOrWhiteSpace(printerName))
            {
                pd.PrinterSettings.PrinterName = printerName;
            }

            pd.DocumentName = documentName;
            pd.PrintPage += (sender, ev) =>
            {
                using var font = new Font("Courier New", 9F, FontStyle.Regular);
                using var brush = new SolidBrush(Color.Black);
                ev.Graphics?.DrawString(printText, font, brush, 10, 10);
            };

            pd.Print();
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Printing failed: {ex.Message}");
            return false;
        }
    }
}
