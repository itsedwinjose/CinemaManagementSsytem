using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Enums;
using CinemaTicketing.Core.Repositories;
using CinemaTicketing.Core.Security;
using CinemaTicketing.Core.Services;
using CinemaTicketing.WinForms.Printing;
using CinemaTicketing.WinForms.UI.Design;

namespace CinemaTicketing.WinForms;

internal partial class MainForm : Form
{
    private readonly IScreeningRepository _screeningRepository;
    private readonly IAudiRepository _audiRepository;
    private readonly IAudiLayoutRepository _audiLayoutRepository;
    private readonly IBookingService _bookingService;
    private readonly ICashoutService _cashoutService;
    private readonly IMovieService _movieService;
    private readonly IAudiLayoutService _layoutService;
    private readonly ITheatreSettingsService _theatreSettingsService;
    private readonly ICinemaRepository _cinemaRepository;
    private readonly ISeatClassRepository _seatClassRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IThermalPrintService _printService;
    private readonly User _currentUser;
    private readonly long _cinemaId = 1;

    private IReadOnlyList<Audi> _audis = Array.Empty<Audi>();
    private IReadOnlyList<Screening> _currentScreenings = Array.Empty<Screening>();
    private IReadOnlyList<ScreeningSeat> _currentSeats = Array.Empty<ScreeningSeat>();
    private Screening? _selectedScreening;
    private CinemaInfo? _currentCinema;

    public MainForm(
        IScreeningRepository screeningRepository,
        IAudiRepository audiRepository,
        IAudiLayoutRepository audiLayoutRepository,
        IBookingService bookingService,
        ICashoutService cashoutService,
        IMovieService movieService,
        IAudiLayoutService layoutService,
        ITheatreSettingsService theatreSettingsService,
        ICinemaRepository cinemaRepository,
        ISeatClassRepository seatClassRepository,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IThermalPrintService printService,
        User currentUser)
    {
        _screeningRepository = screeningRepository;
        _audiRepository = audiRepository;
        _audiLayoutRepository = audiLayoutRepository;
        _bookingService = bookingService;
        _cashoutService = cashoutService;
        _movieService = movieService;
        _layoutService = layoutService;
        _theatreSettingsService = theatreSettingsService;
        _cinemaRepository = cinemaRepository;
        _seatClassRepository = seatClassRepository;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _printService = printService;
        _currentUser = currentUser;

        InitializeComponent();
        InitializeLegendSwatches();
    }

    private void InitializeLegendSwatches()
    {
        legendFlowPanel.Controls.Clear();

        AddLegendItem("Empty / Stair", SeatStatusStyles.NonSeat);
        AddLegendItem("Online Block", SeatStatusStyles.OnlineBlocked);
        AddLegendItem("Family", SeatStatusStyles.Family);
        AddLegendItem("Seat Damaged", SeatStatusStyles.Damaged);
        AddLegendItem("Online Booking", SeatStatusStyles.OnlineBooking);
        AddLegendItem("Ticket Reserved", SeatStatusStyles.CounterReserved);
        AddLegendItem("Seat Blocked", SeatStatusStyles.SeatBlocked);
    }

    private void AddLegendItem(string text, Color color)
    {
        var panel = new Panel
        {
            Width = 82,
            Height = 22,
            Margin = new Padding(1)
        };

        var swatch = new Panel
        {
            Width = 12,
            Height = 12,
            BackColor = color,
            Location = new Point(2, 4),
            BorderStyle = BorderStyle.FixedSingle
        };

        var label = new Label
        {
            Text = text,
            Font = new Font("Segoe UI", 7.5F),
            AutoSize = false,
            Width = 65,
            Height = 20,
            Location = new Point(16, 2),
            TextAlign = ContentAlignment.MiddleLeft
        };

        panel.Controls.Add(swatch);
        panel.Controls.Add(label);
        legendFlowPanel.Controls.Add(panel);
    }

    private async void MainForm_Shown(object sender, EventArgs e)
    {
        WindowState = FormWindowState.Maximized;
        _currentCinema = await _cinemaRepository.GetByIdAsync(_cinemaId);
        if (_currentCinema is not null && !string.IsNullOrWhiteSpace(_currentCinema.Name))
        {
            Text = _currentCinema.Name.ToUpperInvariant();
        }
        await LoadAudiTabsAsync();
    }

    private async Task LoadAudiTabsAsync()
    {
        _audis = await _audiRepository.GetAllAsync(_cinemaId);
        audiTabControl.TabPages.Clear();

        foreach (var audi in _audis)
        {
            var page = new TabPage(audi.Name) { Tag = audi };
            audiTabControl.TabPages.Add(page);
        }

        if (_audis.Count > 0)
        {
            audiTabControl.SelectedIndex = 0;
            await LoadScreeningsForSelectedAudiAsync();
        }
    }

    private async void datePicker_ValueChanged(object sender, EventArgs e)
    {
        await LoadScreeningsForSelectedAudiAsync();
    }

    private async void audiTabControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        await LoadScreeningsForSelectedAudiAsync();
    }

    private async Task LoadScreeningsForSelectedAudiAsync()
    {
        if (audiTabControl.SelectedTab?.Tag is not Audi selectedAudi)
        {
            return;
        }

        audiHeaderLabel.Text = selectedAudi.Name;

        var date = DateOnly.FromDateTime(datePicker.Value);
        var allScreenings = await _screeningRepository.GetScreeningsAsync(_cinemaId, date);

        _currentScreenings = allScreenings.Where(s => s.AudiId == selectedAudi.Id).ToList();

        showComboBox.DisplayMember = "Display";
        showComboBox.ValueMember = "Screening";
        showComboBox.DataSource = _currentScreenings.Select(s => new
        {
            Screening = s,
            Display = $"{s.ShowTypeName} - {DateTime.Today.Add(s.ShowTime):hh:mm tt} [{s.MovieName}]"
        }).ToList();

        if (_currentScreenings.Count > 0)
        {
            showComboBox.SelectedIndex = 0;
            await SelectScreeningAsync(_currentScreenings[0]);
        }
        else
        {
            _selectedScreening = null;
            await LoadFallbackAudiSeatsAsync(selectedAudi);
            UpdateShowInfoLabels();
            UpdateStatistics();
        }
    }

    private async Task LoadFallbackAudiSeatsAsync(Audi audi)
    {
        var cells = await _audiLayoutRepository.GetCellsForAudiAsync(audi.Id);
        if (cells.Count == 0)
        {
            var defaultSeats = new List<ScreeningSeat>();
            for (int r = 0; r < audi.TotalRows; r++)
            {
                char rChar = (char)('A' + (r % 26));
                for (int c = 0; c < audi.TotalCols; c++)
                {
                    defaultSeats.Add(new ScreeningSeat
                    {
                        Id = (r * 1000) + c + 1,
                        RowIndex = r,
                        ColIndex = c,
                        IsSeat = true,
                        RowLabel = rChar.ToString(),
                        SeatNumber = $"{rChar}{c + 1}",
                        Status = SeatStatus.Available
                    });
                }
            }
            _currentSeats = defaultSeats;
        }
        else
        {
            _currentSeats = cells.Select(c => new ScreeningSeat
            {
                Id = c.Id > 0 ? c.Id : (c.RowIndex * 1000 + c.ColIndex + 1),
                RowIndex = c.RowIndex,
                ColIndex = c.ColIndex,
                IsSeat = c.IsSeat,
                RowLabel = c.RowLabel,
                SeatNumber = c.SeatNumber,
                SeatClassId = c.SeatClassId,
                SeatClassName = c.SeatClassName,
                IsDamaged = c.IsDamaged,
                Status = SeatStatus.Available
            }).ToList();
        }

        seatMap.LoadSeats(audi.TotalRows, audi.TotalCols, _currentSeats);
    }

    private async void showComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (showComboBox.SelectedValue is Screening screening)
        {
            await SelectScreeningAsync(screening);
        }
    }

    private async Task SelectScreeningAsync(Screening screening)
    {
        _selectedScreening = screening;
        _currentSeats = await _screeningRepository.GetScreeningSeatsAsync(screening.Id);

        var audi = _audis.FirstOrDefault(a => a.Id == screening.AudiId);
        int rows = audi?.TotalRows ?? 10;
        int cols = audi?.TotalCols ?? 15;

        if (audi is not null)
        {
            audiHeaderLabel.Text = audi.Name;
        }

        if (_currentSeats.Count == 0 && audi is not null)
        {
            await LoadFallbackAudiSeatsAsync(audi);
        }
        else
        {
            seatMap.LoadSeats(rows, cols, _currentSeats);
        }

        UpdateShowInfoLabels();
        UpdateStatistics();
    }

    private void seatMap_SelectionChanged(object sender, EventArgs e)
    {
        UpdateCalculatedAmount();
    }

    private void UpdateShowInfoLabels()
    {
        if (_selectedScreening is null)
        {
            showTypeNameLabel.Text = "No Show";
            showTimeLabel.Text = "--:--";
            movieNameLabel.Text = "[NO SHOW]";
            ticketPriceLabel.Text = "0";
            return;
        }

        showTypeNameLabel.Text = _selectedScreening.ShowTypeName;
        showTimeLabel.Text = DateTime.Today.Add(_selectedScreening.ShowTime).ToString("hh:mmtt").ToUpper();
        movieNameLabel.Text = _selectedScreening.MovieName.ToUpper();
        ticketPriceLabel.Text = $"{_selectedScreening.DefaultPrice:F0}";

        UpdateCalculatedAmount();
    }

    private void UpdateCalculatedAmount()
    {
        var selected = seatMap.GetSelectedSeats();
        decimal ticketTotal = selected.Sum(s => s.Price);
        decimal resvTotal = resvChargeCheckBox.Checked ? selected.Count * 10m : 0m;
        decimal threeDTotal = threeDChargeCheckBox.Checked ? selected.Count * 30m : 0m;
        decimal totalAmount = ticketTotal + resvTotal + threeDTotal;

        statusLabel.Text = $"Selected Seats: {selected.Count} | Total Amount: ₹{totalAmount:F2}";
        statusLabel.ForeColor = Color.FromArgb(30, 30, 30);
    }

    private void resvChargeCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateCalculatedAmount();
    }

    private void threeDChargeCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        UpdateCalculatedAmount();
    }

    private void UpdateStatistics()
    {
        int totalSeats = _currentSeats.Count(s => s.IsSeat);
        int soldSeats = _currentSeats.Count(s => s.IsSeat && (s.Status == SeatStatus.Sold || s.Status == SeatStatus.OnlineBooking));
        decimal counterTotal = _currentSeats.Where(s => s.IsSeat && s.Status == SeatStatus.Sold).Sum(s => s.Price);

        soldTotalValueLabel.Text = $"{soldSeats} / {totalSeats}";
        counterAmountValueLabel.Text = $"{counterTotal:F0}";

        int cashCount = _currentSeats.Count(s => s.IsSeat && s.Status == SeatStatus.Sold);
        int onlineCount = _currentSeats.Count(s => s.IsSeat && s.Status == SeatStatus.OnlineBooking);
        int cardCount = 0;
        int upiCount = 0;

        cashRadio.Text = $"Cash   ( {cashCount} )";
        cardRadio.Text = $"Card   ( {cardCount} )";
        onlineRadio.Text = $"Online ( {onlineCount} )";
        upiRadio.Text = $"QR UPI ( {upiCount} )";
    }

    private PaymentMode GetSelectedPaymentMode()
    {
        if (onlineRadio.Checked) return PaymentMode.Online;
        if (cardRadio.Checked) return PaymentMode.Card;
        if (upiRadio.Checked) return PaymentMode.UpiQr;
        return PaymentMode.Cash;
    }

    private async void ticketButton_Click(object sender, EventArgs e)
    {
        await ProcessTicketBookingAsync();
    }

    private async Task ProcessTicketBookingAsync()
    {
        if (_selectedScreening is null)
        {
            statusLabel.Text = "No show selected.";
            statusLabel.ForeColor = Color.DarkRed;
            return;
        }

        var selectedSeats = seatMap.GetSelectedSeats();
        if (selectedSeats.Count == 0)
        {
            statusLabel.Text = "Please select at least one seat.";
            statusLabel.ForeColor = Color.DarkRed;
            return;
        }

        var request = new BookingRequest(
            _selectedScreening.Id,
            _currentUser.Id,
            BookingType.CounterTicket,
            GetSelectedPaymentMode(),
            CustomerName: "",
            CustomerPhone: "",
            CustomerAddress: "",
            ApplyReservationCharge: resvChargeCheckBox.Checked,
            Apply3DCharge: threeDChargeCheckBox.Checked,
            SelectedSeats: selectedSeats
        );

        var result = await _bookingService.ProcessBookingAsync(request);
        statusLabel.Text = result.Message;
        statusLabel.ForeColor = result.IsSuccess ? Color.DarkGreen : Color.DarkRed;

        if (result.IsSuccess && result.Booking is not null && _currentCinema is not null)
        {
            await _printService.PrintTicketAsync(result.Booking, _currentCinema, _selectedScreening, "");
            await SelectScreeningAsync(_selectedScreening);
        }
    }

    private async void reservationButton_Click(object sender, EventArgs e)
    {
        if (_selectedScreening is null) return;

        var selectedSeats = seatMap.GetSelectedSeats();
        if (selectedSeats.Count == 0)
        {
            statusLabel.Text = "Select seats before clicking Reservation.";
            statusLabel.ForeColor = Color.DarkRed;
            return;
        }

        using var dialog = new ReservationDialog(selectedSeats, _selectedScreening);
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            if (dialog.IsUnblockAction)
            {
                foreach (var seat in selectedSeats)
                {
                    await _screeningRepository.UpdateScreeningSeatStatusAsync(seat.Id, SeatStatus.Available);
                }
                statusLabel.Text = $"Unblocked {selectedSeats.Count} seats.";
                statusLabel.ForeColor = Color.DarkGreen;
                await SelectScreeningAsync(_selectedScreening);
            }
            else if (dialog.ActionRequest is not null)
            {
                var reqWithUser = dialog.ActionRequest with { UserId = _currentUser.Id };
                var result = await _bookingService.ProcessBookingAsync(reqWithUser);

                statusLabel.Text = result.Message;
                statusLabel.ForeColor = result.IsSuccess ? Color.DarkGreen : Color.DarkRed;

                if (result.IsSuccess && result.Booking is not null && _currentCinema is not null)
                {
                    await _printService.PrintTicketAsync(result.Booking, _currentCinema, _selectedScreening, "");
                    await SelectScreeningAsync(_selectedScreening);
                }
            }
        }
    }

    private async void refreshButton_Click(object sender, EventArgs e)
    {
        seatMap.ClearSelection();
        if (_selectedScreening is not null)
        {
            await SelectScreeningAsync(_selectedScreening);
        }
        statusLabel.Text = "Refreshed seat map.";
        statusLabel.ForeColor = Color.FromArgb(33, 37, 41);
    }

    private async Task ReprintLatestTicketAsync()
    {
        var latestBooking = await _bookingService.GetLatestBookingAsync();
        if (latestBooking is null)
        {
            statusLabel.Text = "No previous booking found to reprint.";
            statusLabel.ForeColor = Color.DarkRed;
            return;
        }

        var screening = await _screeningRepository.GetByIdAsync(latestBooking.ScreeningId);
        if (screening is not null && _currentCinema is not null)
        {
            await _printService.PrintTicketAsync(latestBooking, _currentCinema, screening, "");
            statusLabel.Text = $"Reprinted ticket #{latestBooking.BookingNumber}.";
            statusLabel.ForeColor = Color.DarkGreen;
        }
    }

    private void theatreSettingsButton_Click(object sender, EventArgs e)
    {
        using var form = new TheatreSettingsForm(_theatreSettingsService);
        form.ShowDialog(this);
    }

    private async void layoutBtn_Click(object sender, EventArgs e)
    {
        using var form = new AudiLayoutForm(_layoutService, _seatClassRepository, _cinemaId);
        form.ShowDialog(this);
        await LoadAudiTabsAsync();
    }

    private void setMovieButton_Click(object sender, EventArgs e)
    {
        using var form = new SetMovieForm(_screeningRepository, _movieService, _theatreSettingsService, _cinemaId);
        form.ShowDialog(this);
    }

    private void accountsButton_Click(object sender, EventArgs e)
    {
        using var form = new AccountsForm(_cashoutService, _theatreSettingsService, _cinemaRepository, _printService, _currentUser, _cinemaId);
        form.ShowDialog(this);
    }

    private void passwordChangeButton_Click(object sender, EventArgs e)
    {
        using var form = new PasswordChangeForm(_userRepository, _passwordHasher, _currentUser);
        form.ShowDialog(this);
    }

    private async void MainForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F5)
        {
            e.Handled = true;
            refreshButton.PerformClick();
        }
        else if (e.KeyCode == Keys.F6)
        {
            e.Handled = true;
            await ReprintLatestTicketAsync();
        }
        else if (e.KeyCode == Keys.Space && !IsInputControlFocused())
        {
            e.Handled = true;
            await ProcessTicketBookingAsync();
        }
    }

    private bool IsInputControlFocused()
    {
        var active = ActiveControl;
        return active is TextBox || active is ComboBox || active is DateTimePicker;
    }

    private void soldTotalValueLabel_Click(object sender, EventArgs e)
    {

    }

    private void reprintCheckBox_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void ticketRadio_CheckedChanged(object sender, EventArgs e)
    {

    }
}
