using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Services;

namespace CinemaTicketing.WinForms;

internal partial class LoginForm : Form
{
    private readonly IAuthenticationService _authenticationService;

    public LoginForm(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
        InitializeComponent();
    }

    public User? AuthenticatedUser { get; private set; }

    private async void loginButton_Click(object sender, EventArgs e)
    {
        await AttemptLoginAsync();
    }

    private async void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
            await AttemptLoginAsync();
        }
    }

    private async Task AttemptLoginAsync()
    {
        try
        {
            loginButton.Enabled = false;
            statusLabel.Text = "Checking credentials...";
            statusLabel.ForeColor = Color.FromArgb(33, 37, 41);

            var result = await _authenticationService.LoginAsync(userNameTextBox.Text, passwordTextBox.Text);
            if (!result.IsSuccess || result.User is null)
            {
                statusLabel.Text = result.Message;
                statusLabel.ForeColor = Color.DarkRed;
                return;
            }

            AuthenticatedUser = result.User;
            DialogResult = DialogResult.OK;
            Close();
        }
        finally
        {
            loginButton.Enabled = true;
        }
    }

    private void LoginForm_Load(object sender, EventArgs e)
    {

    }
}
