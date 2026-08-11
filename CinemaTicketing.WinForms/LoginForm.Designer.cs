namespace CinemaTicketing.WinForms
{
    partial class LoginForm
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
            userNameLabel = new Label();
            passwordLabel = new Label();
            userNameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            loginButton = new Button();
            statusLabel = new Label();
            headerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(22, 97, 171);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Margin = new Padding(6, 6, 6, 6);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(799, 115);
            headerPanel.TabIndex = 6;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(30, 30);
            titleLabel.Margin = new Padding(6, 0, 6, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(272, 47);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Operator Login";
            // 
            // userNameLabel
            // 
            userNameLabel.AutoSize = true;
            userNameLabel.Location = new Point(52, 175);
            userNameLabel.Margin = new Padding(6, 0, 6, 0);
            userNameLabel.Name = "userNameLabel";
            userNameLabel.Size = new Size(121, 32);
            userNameLabel.TabIndex = 5;
            userNameLabel.Text = "Username";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(52, 292);
            passwordLabel.Margin = new Padding(6, 0, 6, 0);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(111, 32);
            passwordLabel.TabIndex = 3;
            passwordLabel.Text = "Password";
            // 
            // userNameTextBox
            // 
            userNameTextBox.Location = new Point(58, 215);
            userNameTextBox.Margin = new Padding(6, 6, 6, 6);
            userNameTextBox.Name = "userNameTextBox";
            userNameTextBox.Size = new Size(676, 39);
            userNameTextBox.TabIndex = 4;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(58, 333);
            passwordTextBox.Margin = new Padding(6, 6, 6, 6);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(676, 39);
            passwordTextBox.TabIndex = 2;
            passwordTextBox.KeyDown += passwordTextBox_KeyDown;
            // 
            // loginButton
            // 
            loginButton.BackColor = Color.FromArgb(40, 167, 69);
            loginButton.FlatStyle = FlatStyle.Flat;
            loginButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            loginButton.ForeColor = Color.White;
            loginButton.Location = new Point(58, 437);
            loginButton.Margin = new Padding(6, 6, 6, 6);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(299, 79);
            loginButton.TabIndex = 1;
            loginButton.Text = "Login";
            loginButton.UseVisualStyleBackColor = false;
            loginButton.Click += loginButton_Click;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(52, 553);
            statusLabel.Margin = new Padding(6, 0, 6, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(266, 32);
            statusLabel.TabIndex = 0;
            statusLabel.Text = "Enter your login details.";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(799, 644);
            Controls.Add(statusLabel);
            Controls.Add(loginButton);
            Controls.Add(passwordTextBox);
            Controls.Add(passwordLabel);
            Controls.Add(userNameTextBox);
            Controls.Add(userNameLabel);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(6, 6, 6, 6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cinema Login";
            Load += LoginForm_Load;
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Panel headerPanel;
        private Label titleLabel;
        private Label userNameLabel;
        private Label passwordLabel;
        private TextBox userNameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Label statusLabel;
    }
}
