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
        headerLabel = new System.Windows.Forms.Label();
        currentPassLabel = new System.Windows.Forms.Label();
        currentPassTextBox = new System.Windows.Forms.TextBox();
        newPassLabel = new System.Windows.Forms.Label();
        newPassTextBox = new System.Windows.Forms.TextBox();
        confirmPassLabel = new System.Windows.Forms.Label();
        confirmPassTextBox = new System.Windows.Forms.TextBox();
        changeButton = new System.Windows.Forms.Button();
        cancelButton = new System.Windows.Forms.Button();
        statusLabel = new System.Windows.Forms.Label();
        SuspendLayout();
        // 
        // headerLabel
        // 
        headerLabel.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
        headerLabel.Dock = System.Windows.Forms.DockStyle.Top;
        headerLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        headerLabel.ForeColor = System.Drawing.Color.White;
        headerLabel.Location = new System.Drawing.Point(0, 0);
        headerLabel.Name = "headerLabel";
        headerLabel.Size = new System.Drawing.Size(384, 35);
        headerLabel.TabIndex = 0;
        headerLabel.Text = " Password Change";
        headerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // currentPassLabel
        // 
        currentPassLabel.AutoSize = true;
        currentPassLabel.Location = new System.Drawing.Point(20, 55);
        currentPassLabel.Name = "currentPassLabel";
        currentPassLabel.Size = new System.Drawing.Size(104, 15);
        currentPassLabel.TabIndex = 1;
        currentPassLabel.Text = "Current Password:";
        // 
        // currentPassTextBox
        // 
        currentPassTextBox.Location = new System.Drawing.Point(135, 52);
        currentPassTextBox.Name = "currentPassTextBox";
        currentPassTextBox.UseSystemPasswordChar = true;
        currentPassTextBox.Size = new System.Drawing.Size(225, 23);
        currentPassTextBox.TabIndex = 2;
        // 
        // newPassLabel
        // 
        newPassLabel.AutoSize = true;
        newPassLabel.Location = new System.Drawing.Point(20, 95);
        newPassLabel.Name = "newPassLabel";
        newPassLabel.Size = new System.Drawing.Size(87, 15);
        newPassLabel.TabIndex = 3;
        newPassLabel.Text = "New Password:";
        // 
        // newPassTextBox
        // 
        newPassTextBox.Location = new System.Drawing.Point(135, 92);
        newPassTextBox.Name = "newPassTextBox";
        newPassTextBox.UseSystemPasswordChar = true;
        newPassTextBox.Size = new System.Drawing.Size(225, 23);
        newPassTextBox.TabIndex = 4;
        // 
        // confirmPassLabel
        // 
        confirmPassLabel.AutoSize = true;
        confirmPassLabel.Location = new System.Drawing.Point(20, 135);
        confirmPassLabel.Name = "confirmPassLabel";
        confirmPassLabel.Size = new System.Drawing.Size(107, 15);
        confirmPassLabel.TabIndex = 5;
        confirmPassLabel.Text = "Confirm Password:";
        // 
        // confirmPassTextBox
        // 
        confirmPassTextBox.Location = new System.Drawing.Point(135, 132);
        confirmPassTextBox.Name = "confirmPassTextBox";
        confirmPassTextBox.UseSystemPasswordChar = true;
        confirmPassTextBox.Size = new System.Drawing.Size(225, 23);
        confirmPassTextBox.TabIndex = 6;
        // 
        // changeButton
        // 
        changeButton.Location = new System.Drawing.Point(135, 180);
        changeButton.Name = "changeButton";
        changeButton.Size = new System.Drawing.Size(120, 30);
        changeButton.TabIndex = 7;
        changeButton.Text = "Change Password";
        changeButton.UseVisualStyleBackColor = true;
        changeButton.Click += changeButton_Click;
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        cancelButton.Location = new System.Drawing.Point(265, 180);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new System.Drawing.Size(95, 30);
        cancelButton.TabIndex = 8;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // statusLabel
        // 
        statusLabel.AutoSize = true;
        statusLabel.Location = new System.Drawing.Point(20, 225);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new System.Drawing.Size(42, 15);
        statusLabel.TabIndex = 9;
        statusLabel.Text = "Ready.";
        // 
        // PasswordChangeForm
        // 
        CancelButton = cancelButton;
        ClientSize = new System.Drawing.Size(384, 255);
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
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "PasswordChangeForm";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "Password Change";
        ResumeLayout(false);
        PerformLayout();
    }
}
