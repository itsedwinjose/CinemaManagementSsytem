namespace CinemaTicketing.WinForms
{
    partial class InitialAdminSetupForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            headerPanel = new Panel();
            titleLabel = new Label();
            infoLabel = new Label();
            userNameLabel = new Label();
            displayNameLabel = new Label();
            passwordLabel = new Label();
            confirmPasswordLabel = new Label();
            userNameTextBox = new TextBox();
            displayNameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            confirmPasswordTextBox = new TextBox();
            createButton = new Button();
            statusLabel = new Label();
            headerPanel.SuspendLayout();
            SuspendLayout();
            headerPanel.BackColor = Color.FromArgb(22, 97, 171);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Size = new Size(490, 54);
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(16, 14);
            titleLabel.Text = "Initial Administrator Setup";
            infoLabel.AutoSize = true;
            infoLabel.Location = new Point(31, 71);
            infoLabel.Size = new Size(357, 15);
            infoLabel.Text = "No users exist yet. Create the first administrator to unlock the app.";
            userNameLabel.AutoSize = true;
            userNameLabel.Location = new Point(31, 104);
            userNameLabel.Text = "Username";
            userNameTextBox.Location = new Point(34, 123);
            userNameTextBox.Size = new Size(410, 23);
            displayNameLabel.AutoSize = true;
            displayNameLabel.Location = new Point(31, 159);
            displayNameLabel.Text = "Display Name";
            displayNameTextBox.Location = new Point(34, 178);
            displayNameTextBox.Size = new Size(410, 23);
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(31, 214);
            passwordLabel.Text = "Password";
            passwordTextBox.Location = new Point(34, 233);
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(410, 23);
            confirmPasswordLabel.AutoSize = true;
            confirmPasswordLabel.Location = new Point(31, 269);
            confirmPasswordLabel.Text = "Confirm Password";
            confirmPasswordTextBox.Location = new Point(34, 288);
            confirmPasswordTextBox.PasswordChar = '*';
            confirmPasswordTextBox.Size = new Size(410, 23);
            createButton.BackColor = Color.FromArgb(40, 167, 69);
            createButton.FlatStyle = FlatStyle.Flat;
            createButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            createButton.ForeColor = Color.White;
            createButton.Location = new Point(34, 333);
            createButton.Size = new Size(227, 38);
            createButton.Text = "Create Administrator";
            createButton.UseVisualStyleBackColor = false;
            createButton.Click += createButton_Click;
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(31, 388);
            statusLabel.Text = "Create the first secure login.";
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(490, 426);
            Controls.Add(statusLabel);
            Controls.Add(createButton);
            Controls.Add(confirmPasswordTextBox);
            Controls.Add(confirmPasswordLabel);
            Controls.Add(passwordTextBox);
            Controls.Add(passwordLabel);
            Controls.Add(displayNameTextBox);
            Controls.Add(displayNameLabel);
            Controls.Add(userNameTextBox);
            Controls.Add(userNameLabel);
            Controls.Add(infoLabel);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InitialAdminSetupForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Initial Setup";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Panel headerPanel;
        private Label titleLabel;
        private Label infoLabel;
        private Label userNameLabel;
        private Label displayNameLabel;
        private Label passwordLabel;
        private Label confirmPasswordLabel;
        private TextBox userNameTextBox;
        private TextBox displayNameTextBox;
        private TextBox passwordTextBox;
        private TextBox confirmPasswordTextBox;
        private Button createButton;
        private Label statusLabel;
    }
}
