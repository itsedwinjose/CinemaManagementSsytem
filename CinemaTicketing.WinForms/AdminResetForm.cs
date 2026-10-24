using CinemaTicketing.Core.Services;
using CinemaTicketing.WinForms.UI.Design;

namespace CinemaTicketing.WinForms;

internal sealed class AdminResetForm : Form
{
    private readonly IAdminResetService _adminResetService;
    private readonly TextBox _confirmationTextBox;
    private readonly Button _resetButton;
    private readonly Label _statusLabel;

    public AdminResetForm(IAdminResetService adminResetService)
    {
        _adminResetService = adminResetService;

        Text = "Administrator Reset";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(560, 285);

        var warningLabel = new Label
        {
            AutoSize = false,
            Location = new Point(20, 20),
            Size = new Size(520, 115),
            Text = "This permanently deletes all data from every database table except users. " +
                "Cinema and Auditorium setup, layouts, movies, shows, bookings, reports, and settings will be removed. " +
                "This action cannot be undone.",
            ForeColor = Color.DarkRed
        };

        var confirmationLabel = new Label
        {
            AutoSize = true,
            Location = new Point(20, 150),
            Text = "Type RESET to confirm:"
        };

        _confirmationTextBox = new TextBox
        {
            Location = new Point(20, 180),
            Size = new Size(330, 35)
        };
        _confirmationTextBox.TextChanged += ConfirmationTextBox_TextChanged;

        _resetButton = new Button
        {
            Location = new Point(370, 178),
            Size = new Size(170, 40),
            Text = "Reset",
            Enabled = false
        };
        _resetButton.Click += ResetButton_Click;

        _statusLabel = new Label
        {
            AutoSize = false,
            Location = new Point(20, 230),
            Size = new Size(520, 40)
        };

        Controls.Add(warningLabel);
        Controls.Add(confirmationLabel);
        Controls.Add(_confirmationTextBox);
        Controls.Add(_resetButton);
        Controls.Add(_statusLabel);

        AppTheme.ApplyTheme(this);
        AppTheme.ApplyDestructiveButton(_resetButton);
    }

    public bool WasReset { get; private set; }

    private void ConfirmationTextBox_TextChanged(object? sender, EventArgs e)
    {
        _resetButton.Enabled = string.Equals(
            _confirmationTextBox.Text,
            "RESET",
            StringComparison.Ordinal);
    }

    private async void ResetButton_Click(object? sender, EventArgs e)
    {
        var confirm = MessageBox.Show(
            this,
            "Are you sure you want to permanently delete all data except users?",
            "Confirm Database Reset",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2);
        if (confirm != DialogResult.Yes) return;

        try
        {
            _resetButton.Enabled = false;
            _confirmationTextBox.Enabled = false;
            _statusLabel.Text = "Resetting database...";
            _statusLabel.ForeColor = Color.FromArgb(30, 30, 30);

            await _adminResetService.ResetAllDataExceptUsersAsync();

            WasReset = true;
            _statusLabel.Text = "All data except users was deleted.";
            _statusLabel.ForeColor = Color.DarkGreen;
            MessageBox.Show(
                this,
                "All data except users was deleted successfully.",
                "Database Reset Complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            Close();
        }
        catch (Exception ex)
        {
            _statusLabel.Text = $"Reset failed: {ex.Message}";
            _statusLabel.ForeColor = Color.DarkRed;
            MessageBox.Show(this, ex.Message, "Database Reset Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _resetButton.Enabled = true;
            _confirmationTextBox.Enabled = true;
        }
    }
}
