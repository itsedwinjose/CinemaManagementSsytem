using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;
using CinemaTicketing.Core.Services;
using CinemaTicketing.WinForms.Printing;
using CinemaTicketing.WinForms.UI.Design;

namespace CinemaTicketing.WinForms;

internal partial class AccountsForm : Form
{
    private readonly ICashoutService _cashoutService;
    private readonly ITheatreSettingsService _theatreSettingsService;
    private readonly ICinemaRepository _cinemaRepository;
    private readonly IThermalPrintService _printService;
    private readonly User _currentUser;
    private readonly long _cinemaId;

    private IReadOnlyList<CinemaInfo> _cinemas = Array.Empty<CinemaInfo>();
    private IReadOnlyList<Screening> _eligibleScreenings = Array.Empty<Screening>();
    private IReadOnlyList<CashoutRecord> _unprintedReports = Array.Empty<CashoutRecord>();
    private CinemaInfo? _currentCinema;

    public AccountsForm(
        ICashoutService cashoutService,
        ITheatreSettingsService theatreSettingsService,
        ICinemaRepository cinemaRepository,
        IThermalPrintService printService,
        User currentUser,
        long cinemaId = 1)
    {
        _cashoutService = cashoutService;
        _theatreSettingsService = theatreSettingsService;
        _cinemaRepository = cinemaRepository;
        _printService = printService;
        _currentUser = currentUser;
        _cinemaId = cinemaId;

        InitializeComponent();
        AppTheme.ApplyTheme(this);
        AppTheme.ApplySuccessButton(cashoutPrintButton);
        AppTheme.ApplyPrimaryButton(printReportButton);
        AppTheme.ApplyGridDefaultStyle(cashoutGrid);
        AppTheme.ApplyGridDefaultStyle(reportsGrid);

        SetupCashoutGridColumns();
        SetupReportsGridColumns();
    }

    private async void AccountsForm_Shown(object sender, EventArgs e)
    {
        _cinemas = await _theatreSettingsService.GetCinemasAsync();
        _currentCinema = await _cinemaRepository.GetByIdAsync(_cinemaId);

        theatreComboBox.DataSource = _cinemas.ToList();
        theatreComboBox.DisplayMember = nameof(CinemaInfo.Name);
        theatreComboBox.ValueMember = nameof(CinemaInfo.Id);

        await LoadCashoutGridAsync();
    }

    private void SetupCashoutGridColumns()
    {
        cashoutGrid.Columns.Clear();
        cashoutGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Show", HeaderText = "Show", Width = 110 });
        cashoutGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Movie", HeaderText = "Movie Name", Width = 150 });
        cashoutGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoldSeat", HeaderText = "Sold", Width = 60 });
        cashoutGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "FreeSeat", HeaderText = "Free", Width = 60 });
        cashoutGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReservedSeat", HeaderText = "Reserved", Width = 70 });
        cashoutGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Amount", HeaderText = "Ticket Amt", Width = 90 });
        cashoutGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReservationAmt", HeaderText = "Resv Amt", Width = 80 });
        cashoutGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ThreeDAmt", HeaderText = "3D Amt", Width = 80 });
        cashoutGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Total", Width = 100 });
    }

    private void SetupReportsGridColumns()
    {
        reportsGrid.Columns.Clear();
        reportsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ShowDate", HeaderText = "Date", Width = 90 });
        reportsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ShowTime", HeaderText = "Time", Width = 80 });
        reportsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ShowType", HeaderText = "Show Type", Width = 110 });
        reportsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Movie", HeaderText = "Movie Name", Width = 160 });
        reportsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Total Amount", Width = 100 });
        reportsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "CashedOutAt", HeaderText = "Cashed Out At", Width = 140 });
        reportsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Operator", HeaderText = "Operator", Width = 100 });
    }

    private async Task LoadCashoutGridAsync()
    {
        var date = DateOnly.FromDateTime(datePicker.Value);
        _eligibleScreenings = await _cashoutService.GetEligibleScreeningsAsync(_cinemaId, date);

        cashoutGrid.Rows.Clear();
        decimal totalCounterBalance = 0m;

        foreach (var s in _eligibleScreenings)
        {
            var record = await _cashoutService.CalculateCashoutAsync(s.Id);
            if (record is null) continue;

            totalCounterBalance += record.TotalAmount;

            int rowIndex = cashoutGrid.Rows.Add(
                $"{s.ShowTypeName} ({DateTime.Today.Add(s.ShowTime):hh:mm tt})",
                record.MovieName,
                record.SoldSeats,
                record.FreeSeats,
                record.ReservedSeats,
                $"₹{record.TicketAmount:F2}",
                $"₹{record.ReservationAmount:F2}",
                $"₹{record.ThreeDAmount:F2}",
                $"₹{record.TotalAmount:F2}"
            );
            cashoutGrid.Rows[rowIndex].Tag = (s, record);
        }

        counterBalanceLabel.Text = $"Counter Balance: ₹{totalCounterBalance:F2}";
        statusLabel.Text = $"Found {_eligibleScreenings.Count} eligible shows for cashout on {date:dd-MMM-yyyy}.";
    }

    private async Task LoadUnprintedReportsGridAsync()
    {
        _unprintedReports = await _cashoutService.GetUnprintedReportsAsync(_cinemaId);

        reportsGrid.Rows.Clear();
        foreach (var r in _unprintedReports)
        {
            int rowIndex = reportsGrid.Rows.Add(
                r.ShowDate.ToString("dd-MMM-yyyy"),
                DateTime.Today.Add(r.ShowTime).ToString("hh:mm tt"),
                r.ShowTypeName,
                r.MovieName,
                $"₹{r.TotalAmount:F2}",
                r.CashedOutAt.ToString("g"),
                r.CashedOutBy
            );
            reportsGrid.Rows[rowIndex].Tag = r;
        }

        statusLabel.Text = $"Found {_unprintedReports.Count} unprinted cashout reports.";
    }

    private async void Filter_Changed(object sender, EventArgs e)
    {
        await LoadCashoutGridAsync();
    }

    private async void refreshButton_Click(object sender, EventArgs e)
    {
        await LoadCashoutGridAsync();
    }

    private void cashoutGrid_SelectionChanged(object sender, EventArgs e)
    {
        if (cashoutGrid.SelectedRows.Count > 0 && cashoutGrid.SelectedRows[0].Tag is (Screening s, CashoutRecord r))
        {
            selectedShowLabel.Text = $"Selected Show: {s.ShowTypeName} at {DateTime.Today.Add(s.ShowTime):hh:mm tt} - Total: ₹{r.TotalAmount:F2}";
        }
        else
        {
            selectedShowLabel.Text = "Selected Show: None";
        }
    }

    private async void cashoutPrintButton_Click(object sender, EventArgs e)
    {
        if (cashoutGrid.SelectedRows.Count == 0 || cashoutGrid.SelectedRows[0].Tag is not (Screening screening, CashoutRecord record))
        {
            statusLabel.Text = "Please select a show from the grid to cash out.";
            statusLabel.ForeColor = Color.DarkRed;
            return;
        }

        try
        {
            cashoutPrintButton.Enabled = false;
            await _cashoutService.ProcessCashoutAsync(record, _currentUser.DisplayName);

            if (_currentCinema is not null)
            {
                await _printService.PrintCashoutReportAsync(record, _currentCinema, "");
            }

            statusLabel.Text = $"Successfully processed cashout for {screening.ShowTypeName} ({DateTime.Today.Add(screening.ShowTime):hh:mm tt}).";
            statusLabel.ForeColor = Color.DarkGreen;

            await LoadCashoutGridAsync();
        }
        catch (Exception ex)
        {
            statusLabel.Text = $"Cashout failed: {ex.Message}";
            statusLabel.ForeColor = Color.DarkRed;
        }
        finally
        {
            cashoutPrintButton.Enabled = true;
        }
    }

    private async void tabControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tabControl.SelectedTab == reportsTabPage)
        {
            await LoadUnprintedReportsGridAsync();
        }
        else
        {
            await LoadCashoutGridAsync();
        }
    }

    private async void refreshReportsButton_Click(object sender, EventArgs e)
    {
        await LoadUnprintedReportsGridAsync();
    }

    private async void printReportButton_Click(object sender, EventArgs e)
    {
        if (reportsGrid.SelectedRows.Count == 0 || reportsGrid.SelectedRows[0].Tag is not CashoutRecord record)
        {
            statusLabel.Text = "Please select a report to print.";
            statusLabel.ForeColor = Color.DarkRed;
            return;
        }

        try
        {
            printReportButton.Enabled = false;
            if (_currentCinema is not null)
            {
                await _printService.PrintCashoutReportAsync(record, _currentCinema, "");
            }

            await _cashoutService.MarkReportPrintedAsync(record.Id, _currentUser.DisplayName);
            statusLabel.Text = "Report printed and marked as printed.";
            statusLabel.ForeColor = Color.DarkGreen;

            await LoadUnprintedReportsGridAsync();
        }
        finally
        {
            printReportButton.Enabled = true;
        }
    }
}
