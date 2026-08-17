using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Enums;
using CinemaTicketing.Core.Services;
using CinemaTicketing.WinForms.UI.Design;

namespace CinemaTicketing.WinForms;

internal partial class ReservationDialog : Form
{
    private readonly IReadOnlyList<ScreeningSeat> _seats;
    private readonly Screening _screening;
    private readonly decimal _resChargePerSeat;
    private readonly decimal _threeDChargePerSeat;

    public ReservationDialog(
        IReadOnlyList<ScreeningSeat> seats,
        Screening screening,
        decimal resChargePerSeat = 10m,
        decimal threeDChargePerSeat = 30m)
    {
        _seats = seats;
        _screening = screening;
        _resChargePerSeat = resChargePerSeat;
        _threeDChargePerSeat = threeDChargePerSeat;

        InitializeComponent();
        AppTheme.ApplyTheme(this);
        AppTheme.ApplyTealButton(onlineBookingButton);
        AppTheme.ApplyPrimaryButton(reservedButton);
        AppTheme.ApplySuccessButton(teleReservedButton);
        AppTheme.ApplyDestructiveButton(unblockButton);

        UpdateSummary();
    }

    public BookingRequest? ActionRequest { get; private set; }
    public bool IsUnblockAction { get; private set; }

    private void Charge_CheckedChanged(object sender, EventArgs e)
    {
        UpdateSummary();
    }

    private void UpdateSummary()
    {
        decimal ticketAmount = _seats.Sum(s => s.Price);
        decimal resCharge = resChargeCheckBox.Checked ? (_seats.Count * _resChargePerSeat) : 0m;
        decimal threeDCharge = threeDChargeCheckBox.Checked ? (_seats.Count * _threeDChargePerSeat) : 0m;
        decimal total = ticketAmount + resCharge + threeDCharge;

        summaryLabel.Text = $"Seats: {_seats.Count} | Ticket: ₹{ticketAmount:F2} | Res: ₹{resCharge:F2} | 3D: ₹{threeDCharge:F2} | Total: ₹{total:F2}";
    }

    private PaymentMode GetSelectedPaymentMode()
    {
        if (onlineRadio.Checked) return PaymentMode.Online;
        if (cardRadio.Checked) return PaymentMode.Card;
        if (upiRadio.Checked) return PaymentMode.UpiQr;
        return PaymentMode.Cash;
    }

    private void onlineBookingButton_Click(object sender, EventArgs e)
    {
        CreateRequest(BookingType.OnlineBooking);
    }

    private void freeTicketButton_Click(object sender, EventArgs e)
    {
        CreateRequest(BookingType.FreeTicket);
    }

    private void reservedButton_Click(object sender, EventArgs e)
    {
        CreateRequest(BookingType.CounterReservation);
    }

    private void teleReservedButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(phoneTextBox.Text))
        {
            MessageBox.Show("Please enter a customer phone number for Telephone Reservation.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        CreateRequest(BookingType.TelephoneReservation);
    }

    private void unblockButton_Click(object sender, EventArgs e)
    {
        IsUnblockAction = true;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void CreateRequest(BookingType bookingType)
    {
        ActionRequest = new BookingRequest(
            _screening.Id,
            1, // Default system operator user
            bookingType,
            GetSelectedPaymentMode(),
            nameTextBox.Text.Trim(),
            phoneTextBox.Text.Trim(),
            addressTextBox.Text.Trim(),
            resChargeCheckBox.Checked,
            threeDChargeCheckBox.Checked,
            _seats
        );

        DialogResult = DialogResult.OK;
        Close();
    }

    private void addressTextBox_TextChanged(object sender, EventArgs e)
    {

    }

    private void nameTextBox_TextChanged(object sender, EventArgs e)
    {

    }
}
