namespace CinemaTicketing.WinForms;

partial class PasswordChangeForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label headerLabel;
    private System.Windows.Forms.Label currentPassLabel;
    private System.Windows.Forms.TextBox currentPassTextBox;
    private System.Windows.Forms.Label newPassLabel;
    private System.Windows.Forms.TextBox newPassTextBox;
    private System.Windows.Forms.Label confirmPassLabel;
    private System.Windows.Forms.TextBox confirmPassTextBox;
    private System.Windows.Forms.Button changeButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Label statusLabel;

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
        headerLabel = new Label();
        currentPassLabel = new Label();
        currentPassTextBox = new TextBox();
        newPassLabel = new Label();
        newPassTextBox = new TextBox();
        confirmPassLabel = new Label();
        confirmPassTextBox = new TextBox();
        changeButton = new Button();
        cancelButton = new Button();
        statusLabel = new Label();
        SuspendLayout();
        // 
        // headerLabel
        // 
        headerLabel.BackColor = Color.FromArgb(0, 120, 215);
        headerLabel.Dock = DockStyle.Top;
        headerLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        headerLabel.ForeColor = Color.White;
        headerLabel.Location = new Point(0, 0);
        headerLabel.Name = "headerLabel";
        headerLabel.Size = new Size(557, 35);
        headerLabel.TabIndex = 0;
        headerLabel.Text = " Password Change";
        headerLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // currentPassLabel
        // 
        currentPassLabel.AutoSize = true;
        currentPassLabel.Location = new Point(20, 55);
        currentPassLabel.Name = "currentPassLabel";
        currentPassLabel.Size = new Size(203, 32);
        currentPassLabel.TabIndex = 1;
        currentPassLabel.Text = "Current Password:";
        // 
        // currentPassTextBox
        // 
        currentPassTextBox.Location = new Point(252, 55);
        currentPassTextBox.Name = "currentPassTextBox";
        currentPassTextBox.Size = new Size(284, 39);
        currentPassTextBox.TabIndex = 2;
        currentPassTextBox.UseSystemPasswordChar = true;
        currentPassTextBox.TextChanged += currentPassTextBox_TextChanged;
        // 
        // newPassLabel
        // 
        newPassLabel.AutoSize = true;
        newPassLabel.Location = new Point(20, 111);
        newPassLabel.Name = "newPassLabel";
        newPassLabel.Size = new Size(171, 32);
        newPassLabel.TabIndex = 3;
        newPassLabel.Text = "New Password:";
        // 
        // newPassTextBox
        // 
        newPassTextBox.Location = new Point(252, 111);
        newPassTextBox.Name = "newPassTextBox";
        newPassTextBox.Size = new Size(284, 39);
        newPassTextBox.TabIndex = 4;
        newPassTextBox.UseSystemPasswordChar = true;
        // 
        // confirmPassLabel
        // 
        confirmPassLabel.AutoSize = true;
        confirmPassLabel.Location = new Point(20, 156);
        confirmPassLabel.Name = "confirmPassLabel";
        confirmPassLabel.Size = new Size(209, 32);
        confirmPassLabel.TabIndex = 5;
        confirmPassLabel.Text = "Confirm Password:";
        // 
        // confirmPassTextBox
        // 
        confirmPassTextBox.Location = new Point(252, 156);
        confirmPassTextBox.Name = "confirmPassTextBox";
        confirmPassTextBox.Size = new Size(284, 39);
        confirmPassTextBox.TabIndex = 6;
        confirmPassTextBox.UseSystemPasswordChar = true;
        // 
        // changeButton
        // 
        changeButton.Location = new Point(172, 270);
        changeButton.Name = "changeButton";
        changeButton.Size = new Size(183, 56);
        changeButton.TabIndex = 7;
        changeButton.Text = "Change Password";
        changeButton.UseVisualStyleBackColor = true;
        changeButton.Click += changeButton_Click;
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(376, 270);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(160, 56);
        cancelButton.TabIndex = 8;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // statusLabel
        // 
        statusLabel.AutoSize = true;
        statusLabel.Location = new Point(20, 225);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(83, 32);
        statusLabel.TabIndex = 9;
        statusLabel.Text = "Ready.";
        // 
        // PasswordChangeForm
        // 
        CancelButton = cancelButton;
        ClientSize = new Size(557, 383);
        Controls.Add(statusLabel);
        Controls.Add(cancelButton);
        Controls.Add(changeButton);
        Controls.Add(confirmPassTextBox);
        Controls.Add(confirmPassLabel);
        Controls.Add(newPassTextBox);
        Controls.Add(newPassLabel);
        Controls.Add(currentPassTextBox);
        Controls.Add(currentPassLabel);
        Controls.Add(headerLabel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "PasswordChangeForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Password Change";
        ResumeLayout(false);
        PerformLayout();
    }
}
