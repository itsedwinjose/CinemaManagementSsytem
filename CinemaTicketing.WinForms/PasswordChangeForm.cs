using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;
using CinemaTicketing.Core.Security;
using CinemaTicketing.WinForms.UI.Design;

namespace CinemaTicketing.WinForms;

internal partial class PasswordChangeForm : Form
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly User _currentUser;

    public PasswordChangeForm(IUserRepository userRepository, IPasswordHasher passwordHasher, User currentUser)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _currentUser = currentUser;

        InitializeComponent();
        AppTheme.ApplyTheme(this);
        AppTheme.ApplyPrimaryButton(changeButton);
    }

    private async void changeButton_Click(object sender, EventArgs e)
    {
        string currentPass = currentPassTextBox.Text;
        string newPass = newPassTextBox.Text;
        string confirmPass = confirmPassTextBox.Text;

        if (string.IsNullOrEmpty(currentPass))
        {
            statusLabel.Text = "Enter current password.";
            statusLabel.ForeColor = Color.DarkRed;
            return;
        }

        if (string.IsNullOrEmpty(newPass) || newPass.Length < 4)
        {
            statusLabel.Text = "New password must be at least 4 characters.";
            statusLabel.ForeColor = Color.DarkRed;
            return;
        }

        if (newPass != confirmPass)
        {
            statusLabel.Text = "New passwords do not match.";
            statusLabel.ForeColor = Color.DarkRed;
            return;
        }

        var dbUser = await _userRepository.GetByUserNameAsync(_currentUser.UserName);
        if (dbUser is null || !_passwordHasher.VerifyPassword(currentPass, dbUser.PasswordHash))
        {
            statusLabel.Text = "Current password is incorrect.";
            statusLabel.ForeColor = Color.DarkRed;
            return;
        }

        string newHash = _passwordHasher.HashPassword(newPass);
        await _userRepository.UpdatePasswordHashAsync(_currentUser.Id, newHash);

        statusLabel.Text = "Password updated successfully.";
        statusLabel.ForeColor = Color.DarkGreen;

        await Task.Delay(1000);
        DialogResult = DialogResult.OK;
        Close();
    }

    private void currentPassTextBox_TextChanged(object sender, EventArgs e)
    {

    }
}
