namespace CinemaTicketing.WinForms;

partial class ReservationDialog
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label headerLabel;
    private System.Windows.Forms.Label nameLabel;
    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.Label phoneLabel;
    private System.Windows.Forms.TextBox phoneTextBox;
    private System.Windows.Forms.Label addressLabel;
    private System.Windows.Forms.TextBox addressTextBox;
    private System.Windows.Forms.GroupBox chargesGroup;
    private System.Windows.Forms.CheckBox resChargeCheckBox;
    private System.Windows.Forms.CheckBox threeDChargeCheckBox;
    private System.Windows.Forms.GroupBox paymentGroup;
    private System.Windows.Forms.RadioButton cashRadio;
    private System.Windows.Forms.RadioButton onlineRadio;
    private System.Windows.Forms.RadioButton cardRadio;
    private System.Windows.Forms.RadioButton upiRadio;
    private System.Windows.Forms.Label summaryLabel;
    private System.Windows.Forms.Button onlineBookingButton;
    private System.Windows.Forms.Button unblockButton;
    private System.Windows.Forms.Button freeTicketButton;
    private System.Windows.Forms.Button reservedButton;
    private System.Windows.Forms.Button teleReservedButton;
    private System.Windows.Forms.Button cancelButton;

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
        nameLabel = new System.Windows.Forms.Label();
        nameTextBox = new System.Windows.Forms.TextBox();
        phoneLabel = new System.Windows.Forms.Label();
        phoneTextBox = new System.Windows.Forms.TextBox();
        addressLabel = new System.Windows.Forms.Label();
        addressTextBox = new System.Windows.Forms.TextBox();
        chargesGroup = new System.Windows.Forms.GroupBox();
        threeDChargeCheckBox = new System.Windows.Forms.CheckBox();
        resChargeCheckBox = new System.Windows.Forms.CheckBox();
        paymentGroup = new System.Windows.Forms.GroupBox();
        upiRadio = new System.Windows.Forms.RadioButton();
        cardRadio = new System.Windows.Forms.RadioButton();
        onlineRadio = new System.Windows.Forms.RadioButton();
        cashRadio = new System.Windows.Forms.RadioButton();
        summaryLabel = new System.Windows.Forms.Label();
        onlineBookingButton = new System.Windows.Forms.Button();
        unblockButton = new System.Windows.Forms.Button();
        freeTicketButton = new System.Windows.Forms.Button();
        reservedButton = new System.Windows.Forms.Button();
        teleReservedButton = new System.Windows.Forms.Button();
        cancelButton = new System.Windows.Forms.Button();
        chargesGroup.SuspendLayout();
        paymentGroup.SuspendLayout();
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
        headerLabel.Size = new System.Drawing.Size(484, 35);
        headerLabel.TabIndex = 0;
        headerLabel.Text = " Reservation / Special Booking";
        headerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // nameLabel
        // 
        nameLabel.AutoSize = true;
        nameLabel.Location = new System.Drawing.Point(15, 50);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new System.Drawing.Size(42, 15);
        nameLabel.TabIndex = 1;
        nameLabel.Text = "Name:";
        // 
        // nameTextBox
        // 
        nameTextBox.Location = new System.Drawing.Point(85, 47);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.Size = new System.Drawing.Size(385, 23);
        nameTextBox.TabIndex = 2;
        // 
        // phoneLabel
        // 
        phoneLabel.AutoSize = true;
        phoneLabel.Location = new System.Drawing.Point(15, 80);
        phoneLabel.Name = "phoneLabel";
        phoneLabel.Size = new System.Drawing.Size(63, 15);
        phoneLabel.TabIndex = 3;
        phoneLabel.Text = "Phone No:";
        // 
        // phoneTextBox
        // 
        phoneTextBox.Location = new System.Drawing.Point(85, 77);
        phoneTextBox.Name = "phoneTextBox";
        phoneTextBox.Size = new System.Drawing.Size(180, 23);
        phoneTextBox.TabIndex = 4;
        // 
        // addressLabel
        // 
        addressLabel.AutoSize = true;
        addressLabel.Location = new System.Drawing.Point(15, 110);
        addressLabel.Name = "addressLabel";
        addressLabel.Size = new System.Drawing.Size(52, 15);
        addressLabel.TabIndex = 5;
        addressLabel.Text = "Address:";
        // 
        // addressTextBox
        // 
        addressTextBox.Location = new System.Drawing.Point(85, 107);
        addressTextBox.Name = "addressTextBox";
        addressTextBox.Size = new System.Drawing.Size(385, 23);
        addressTextBox.TabIndex = 6;
        // 
        // chargesGroup
        // 
        chargesGroup.Controls.Add(threeDChargeCheckBox);
        chargesGroup.Controls.Add(resChargeCheckBox);
        chargesGroup.Location = new System.Drawing.Point(15, 140);
        chargesGroup.Name = "chargesGroup";
        chargesGroup.Size = new System.Drawing.Size(455, 55);
        chargesGroup.TabIndex = 7;
        chargesGroup.TabStop = false;
        chargesGroup.Text = "Applicable Additional Charges";
        // 
        // threeDChargeCheckBox
        // 
        threeDChargeCheckBox.AutoSize = true;
        threeDChargeCheckBox.Location = new System.Drawing.Point(230, 23);
        threeDChargeCheckBox.Name = "threeDChargeCheckBox";
        threeDChargeCheckBox.Size = new System.Drawing.Size(161, 19);
        threeDChargeCheckBox.TabIndex = 1;
        threeDChargeCheckBox.Text = "3D Charge (₹30 Per Seat)";
        threeDChargeCheckBox.UseVisualStyleBackColor = true;
        threeDChargeCheckBox.CheckedChanged += Charge_CheckedChanged;
        // 
        // resChargeCheckBox
        // 
        resChargeCheckBox.AutoSize = true;
        resChargeCheckBox.Location = new System.Drawing.Point(20, 23);
        resChargeCheckBox.Name = "resChargeCheckBox";
        resChargeCheckBox.Size = new System.Drawing.Size(193, 19);
        resChargeCheckBox.TabIndex = 0;
        resChargeCheckBox.Text = "Reservation Charge (₹10/Seat)";
        resChargeCheckBox.UseVisualStyleBackColor = true;
        resChargeCheckBox.CheckedChanged += Charge_CheckedChanged;
        // 
        // paymentGroup
        // 
        paymentGroup.Controls.Add(upiRadio);
        paymentGroup.Controls.Add(cardRadio);
        paymentGroup.Controls.Add(onlineRadio);
        paymentGroup.Controls.Add(cashRadio);
        paymentGroup.Location = new System.Drawing.Point(15, 205);
        paymentGroup.Name = "paymentGroup";
        paymentGroup.Size = new System.Drawing.Size(455, 50);
        paymentGroup.TabIndex = 8;
        paymentGroup.TabStop = false;
        paymentGroup.Text = "Payment Mode";
        // 
        // upiRadio
        // 
        upiRadio.AutoSize = true;
        upiRadio.Location = new System.Drawing.Point(340, 20);
        upiRadio.Name = "upiRadio";
        upiRadio.Size = new System.Drawing.Size(65, 19);
        upiRadio.TabIndex = 3;
        upiRadio.Text = "UPI/QR";
        upiRadio.UseVisualStyleBackColor = true;
        // 
        // cardRadio
        // 
        cardRadio.AutoSize = true;
        cardRadio.Location = new System.Drawing.Point(240, 20);
        cardRadio.Name = "cardRadio";
        cardRadio.Size = new System.Drawing.Size(50, 19);
        cardRadio.TabIndex = 2;
        cardRadio.Text = "Card";
        cardRadio.UseVisualStyleBackColor = true;
        // 
        // onlineRadio
        // 
        onlineRadio.AutoSize = true;
        onlineRadio.Location = new System.Drawing.Point(130, 20);
        onlineRadio.Name = "onlineRadio";
        onlineRadio.Size = new System.Drawing.Size(60, 19);
        onlineRadio.TabIndex = 1;
        onlineRadio.Text = "Online";
        onlineRadio.UseVisualStyleBackColor = true;
        // 
        // cashRadio
        // 
        cashRadio.AutoSize = true;
        cashRadio.Checked = true;
        cashRadio.Location = new System.Drawing.Point(20, 20);
        cashRadio.Name = "cashRadio";
        cashRadio.Size = new System.Drawing.Size(51, 19);
        cashRadio.TabIndex = 0;
        cashRadio.TabStop = true;
        cashRadio.Text = "Cash";
        cashRadio.UseVisualStyleBackColor = true;
        // 
        // summaryLabel
        // 
        summaryLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        summaryLabel.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
        summaryLabel.Location = new System.Drawing.Point(15, 260);
        summaryLabel.Name = "summaryLabel";
        summaryLabel.Size = new System.Drawing.Size(455, 30);
        summaryLabel.TabIndex = 9;
        summaryLabel.Text = "Selected Seats: 0 | Total Amount: ₹0.00";
        summaryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // onlineBookingButton
        // 
        onlineBookingButton.Location = new System.Drawing.Point(15, 300);
        onlineBookingButton.Name = "onlineBookingButton";
        onlineBookingButton.Size = new System.Drawing.Size(85, 32);
        onlineBookingButton.TabIndex = 10;
        onlineBookingButton.Text = "Online";
        onlineBookingButton.UseVisualStyleBackColor = true;
        onlineBookingButton.Click += onlineBookingButton_Click;
        // 
        // unblockButton
        // 
        unblockButton.Location = new System.Drawing.Point(105, 300);
        unblockButton.Name = "unblockButton";
        unblockButton.Size = new System.Drawing.Size(85, 32);
        unblockButton.TabIndex = 11;
        unblockButton.Text = "Unblock";
        unblockButton.UseVisualStyleBackColor = true;
        unblockButton.Click += unblockButton_Click;
        // 
        // freeTicketButton
        // 
        freeTicketButton.Location = new System.Drawing.Point(195, 300);
        freeTicketButton.Name = "freeTicketButton";
        freeTicketButton.Size = new System.Drawing.Size(85, 32);
        freeTicketButton.TabIndex = 12;
        freeTicketButton.Text = "Free";
        freeTicketButton.UseVisualStyleBackColor = true;
        freeTicketButton.Click += freeTicketButton_Click;
        // 
        // reservedButton
        // 
        reservedButton.Location = new System.Drawing.Point(285, 300);
        reservedButton.Name = "reservedButton";
        reservedButton.Size = new System.Drawing.Size(85, 32);
        reservedButton.TabIndex = 13;
        reservedButton.Text = "Reserved";
        reservedButton.UseVisualStyleBackColor = true;
        reservedButton.Click += reservedButton_Click;
        // 
        // teleReservedButton
        // 
        teleReservedButton.Location = new System.Drawing.Point(375, 300);
        teleReservedButton.Name = "teleReservedButton";
        teleReservedButton.Size = new System.Drawing.Size(95, 32);
        teleReservedButton.TabIndex = 14;
        teleReservedButton.Text = "Tele Reserved";
        teleReservedButton.UseVisualStyleBackColor = true;
        teleReservedButton.Click += teleReservedButton_Click;
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        cancelButton.Location = new System.Drawing.Point(375, 340);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new System.Drawing.Size(95, 28);
        cancelButton.TabIndex = 15;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // ReservationDialog
        // 
        CancelButton = cancelButton;
        ClientSize = new System.Drawing.Size(484, 380);
        Controls.Add(cancelButton);
        Controls.Add(teleReservedButton);
        Controls.Add(reservedButton);
        Controls.Add(freeTicketButton);
        Controls.Add(unblockButton);
        Controls.Add(onlineBookingButton);
        Controls.Add(summaryLabel);
        Controls.Add(paymentGroup);
        Controls.Add(chargesGroup);
        Controls.Add(addressTextBox);
        Controls.Add(addressLabel);
        Controls.Add(phoneTextBox);
        Controls.Add(phoneLabel);
        Controls.Add(nameTextBox);
        Controls.Add(nameLabel);
        Controls.Add(headerLabel);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ReservationDialog";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "Reservation / Special Booking";
        chargesGroup.ResumeLayout(false);
        chargesGroup.PerformLayout();
        paymentGroup.ResumeLayout(false);
        paymentGroup.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
