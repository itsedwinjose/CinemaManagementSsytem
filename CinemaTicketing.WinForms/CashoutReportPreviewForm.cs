using CinemaTicketing.Core.Entities;
using CinemaTicketing.WinForms.Printing;
using CinemaTicketing.WinForms.UI.Design;

namespace CinemaTicketing.WinForms;

internal sealed class CashoutReportPreviewForm : Form
{
    private readonly IThermalPrintService _printService;
    private readonly CashoutRecord _record;
    private readonly CinemaInfo _cinema;
    private readonly Label _statusLabel;
    private readonly Button _printButton;

    public CashoutReportPreviewForm(
        CashoutRecord record,
        CinemaInfo cinema,
        IThermalPrintService printService)
    {
        _record = record;
        _cinema = cinema;
        _printService = printService;

        Text = "Cashout Report Preview";
        StartPosition = FormStartPosition.CenterParent;
        MinimumSize = new Size(680, 560);
        Size = new Size(900, 760);
        AppTheme.ApplyTheme(this);

        var layout = new TableLayoutPanel
        {
            ColumnCount = 1,
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            RowCount = 3
        };
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        var title = new Label
        {
            AutoSize = true,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 12F, FontStyle.Bold),
            Padding = new Padding(0, 0, 0, 10),
            Text = $"Cashout Report | {record.AudiName} | {record.ShowDate:dd-MMM-yyyy}"
        };

        var reportText = new TextBox
        {
            BackColor = Color.White,
            Dock = DockStyle.Fill,
            Font = new Font("Courier New", 11F),
            Multiline = true,
            ReadOnly = true,
            ScrollBars = ScrollBars.Both,
            Text = ThermalPrintService.BuildCashoutReportText(record, cinema),
            WordWrap = false
        };

        var footer = new TableLayoutPanel
        {
            ColumnCount = 2,
            Dock = DockStyle.Fill,
            Padding = new Padding(0, 10, 0, 0),
            RowCount = 1
        };
        footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        footer.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

        _statusLabel = new Label
        {
            AutoSize = true,
            Anchor = AnchorStyles.Left,
            Text = "Review the report, then choose Print to select a printer."
        };
        var buttons = new FlowLayoutPanel
        {
            AutoSize = true,
            FlowDirection = FlowDirection.RightToLeft,
            WrapContents = false
        };
        _printButton = new Button
        {
            AutoSize = true,
            Text = "Print...",
            UseVisualStyleBackColor = true
        };
        var cancelButton = new Button
        {
            AutoSize = true,
            DialogResult = DialogResult.Cancel,
            Text = "Cancel",
            UseVisualStyleBackColor = true
        };
        AppTheme.ApplyPrimaryButton(_printButton);
        _printButton.Click += printButton_Click;
        buttons.Controls.Add(_printButton);
        buttons.Controls.Add(cancelButton);
        footer.Controls.Add(_statusLabel, 0, 0);
        footer.Controls.Add(buttons, 1, 0);

        layout.Controls.Add(title, 0, 0);
        layout.Controls.Add(reportText, 0, 1);
        layout.Controls.Add(footer, 0, 2);
        Controls.Add(layout);

        AcceptButton = _printButton;
        CancelButton = cancelButton;
    }

    private async void printButton_Click(object? sender, EventArgs e)
    {
        _printButton.Enabled = false;
        _statusLabel.Text = "Select a printer in the next window.";

        try
        {
            var printed = await _printService.PrintCashoutReportAsync(_record, _cinema, string.Empty);
            if (printed)
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            _statusLabel.Text = "Printing was cancelled or failed. The report remains available.";
        }
        catch (Exception ex)
        {
            _statusLabel.Text = $"Print failed: {ex.Message}";
        }
        finally
        {
            if (!IsDisposed)
            {
                _printButton.Enabled = true;
            }
        }
    }
}
