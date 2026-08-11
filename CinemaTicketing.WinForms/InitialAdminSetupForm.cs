using CinemaTicketing.Core.Services;

namespace CinemaTicketing.WinForms;

internal partial class InitialAdminSetupForm : Form
{
    private readonly IInitialAdminSetupService _setupService;

    public InitialAdminSetupForm(IInitialAdminSetupService setupService)
    {
        _setupService = setupService;
        InitializeComponent();
    }

    private async void createButton_Click(object sender, EventArgs e)
    {
        await CreateAdminAsync();
    }

    private async Task CreateAdminAsync()
    {
        try
        {
            createButton.Enabled = false;
            statusLabel.Text = "Creating administrator account...";
            statusLabel.ForeColor = Color.FromArgb(33, 37, 41);

            var request = new InitialAdminSetupRequest(
                userNameTextBox.Text,
                displayNameTextBox.Text,
                passwordTextBox.Text,
                confirmPasswordTextBox.Text);

            var result = await _setupService.CreateInitialAdminAsync(request);
            statusLabel.Text = result.Message;
            statusLabel.ForeColor = result.IsSuccess ? Color.DarkGreen : Color.DarkRed;

            if (result.IsSuccess)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
        finally
        {
            createButton.Enabled = true;
        }
    }
}
