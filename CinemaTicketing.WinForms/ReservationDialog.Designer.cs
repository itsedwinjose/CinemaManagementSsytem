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
        headerLabel = new Label();
        nameLabel = new Label();
        nameTextBox = new TextBox();
        phoneLabel = new Label();
        phoneTextBox = new TextBox();
        addressLabel = new Label();
        addressTextBox = new TextBox();
        chargesGroup = new GroupBox();
        threeDChargeCheckBox = new CheckBox();
        resChargeCheckBox = new CheckBox();
        paymentGroup = new GroupBox();
        upiRadio = new RadioButton();
        cardRadio = new RadioButton();
        onlineRadio = new RadioButton();
        cashRadio = new RadioButton();
        summaryLabel = new Label();
        onlineBookingButton = new Button();
        unblockButton = new Button();
        freeTicketButton = new Button();
        reservedButton = new Button();
        teleReservedButton = new Button();
        cancelButton = new Button();
        paymentGroup.SuspendLayout();
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
        headerLabel.Size = new Size(662, 47);
        headerLabel.TabIndex = 0;
        headerLabel.Text = " Reservation / Special Booking";
        headerLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nameLabel
        // 
        nameLabel.AutoSize = true;
        nameLabel.Location = new Point(35, 66);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new Size(83, 32);
        nameLabel.TabIndex = 1;
        nameLabel.Text = "Name:";
        // 
        // nameTextBox
        // 
        nameTextBox.Location = new Point(253, 50);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.Size = new Size(385, 39);
        nameTextBox.TabIndex = 2;
        nameTextBox.TextChanged += nameTextBox_TextChanged;
        // 
        // phoneLabel
        // 
        phoneLabel.AutoSize = true;
        phoneLabel.Location = new Point(35, 119);
        phoneLabel.Name = "phoneLabel";
        phoneLabel.Size = new Size(126, 32);
        phoneLabel.TabIndex = 3;
        phoneLabel.Text = "Phone No:";
        // 
        // phoneTextBox
        // 
        phoneTextBox.Location = new Point(253, 112);
        phoneTextBox.Name = "phoneTextBox";
        phoneTextBox.Size = new Size(385, 39);
        phoneTextBox.TabIndex = 4;
        // 
        // addressLabel
        // 
        addressLabel.AutoSize = true;
        addressLabel.Location = new Point(35, 170);
        addressLabel.Name = "addressLabel";
        addressLabel.Size = new Size(103, 32);
        addressLabel.TabIndex = 5;
        addressLabel.Text = "Address:";
        // 
        // addressTextBox
        // 
        addressTextBox.Location = new Point(253, 170);
        addressTextBox.Name = "addressTextBox";
        addressTextBox.Size = new Size(385, 39);
        addressTextBox.TabIndex = 6;
        addressTextBox.TextChanged += addressTextBox_TextChanged;
        // 
        // chargesGroup
        // 
        chargesGroup.Location = new Point(21, 229);
        chargesGroup.Name = "chargesGroup";
        chargesGroup.Size = new Size(617, 154);
        chargesGroup.TabIndex = 7;
        chargesGroup.TabStop = false;
        chargesGroup.Text = "Applicable Additional Charges";
        // 
        // threeDChargeCheckBox
        // 
        threeDChargeCheckBox.AutoSize = true;
        threeDChargeCheckBox.Location = new Point(35, 337);
        threeDChargeCheckBox.Name = "threeDChargeCheckBox";
        threeDChargeCheckBox.Size = new Size(312, 36);
        threeDChargeCheckBox.TabIndex = 1;
        threeDChargeCheckBox.Text = "3D Charge (₹30 Per Seat)";
        threeDChargeCheckBox.UseVisualStyleBackColor = true;
        threeDChargeCheckBox.CheckedChanged += Charge_CheckedChanged;
        // 
        // resChargeCheckBox
        // 
        resChargeCheckBox.AutoSize = true;
        resChargeCheckBox.Location = new Point(35, 281);
        resChargeCheckBox.Name = "resChargeCheckBox";
        resChargeCheckBox.Size = new Size(367, 36);
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
        paymentGroup.Location = new Point(21, 389);
        paymentGroup.Name = "paymentGroup";
        paymentGroup.Size = new Size(617, 111);
        paymentGroup.TabIndex = 8;
        paymentGroup.TabStop = false;
        paymentGroup.Text = "Payment Mode";
        // 
        // upiRadio
        // 
        upiRadio.AutoSize = true;
        upiRadio.Location = new Point(362, 50);
        upiRadio.Name = "upiRadio";
        upiRadio.Size = new Size(121, 36);
        upiRadio.TabIndex = 3;
        upiRadio.Text = "UPI/QR";
        upiRadio.UseVisualStyleBackColor = true;
        // 
        // cardRadio
        // 
        cardRadio.AutoSize = true;
        cardRadio.Location = new Point(250, 50);
        cardRadio.Name = "cardRadio";
        cardRadio.Size = new Size(94, 36);
        cardRadio.TabIndex = 2;
        cardRadio.Text = "Card";
        cardRadio.UseVisualStyleBackColor = true;
        // 
        // onlineRadio
        // 
        onlineRadio.AutoSize = true;
        onlineRadio.Location = new Point(128, 50);
        onlineRadio.Name = "onlineRadio";
        onlineRadio.Size = new Size(116, 36);
        onlineRadio.TabIndex = 1;
        onlineRadio.Text = "Online";
        onlineRadio.UseVisualStyleBackColor = true;
        // 
        // cashRadio
        // 
        cashRadio.AutoSize = true;
        cashRadio.Checked = true;
        cashRadio.Location = new Point(24, 50);
        cashRadio.Name = "cashRadio";
        cashRadio.Size = new Size(96, 36);
        cashRadio.TabIndex = 0;
        cashRadio.TabStop = true;
        cashRadio.Text = "Cash";
        cashRadio.UseVisualStyleBackColor = true;
        // 
        // summaryLabel
        // 
        summaryLabel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        summaryLabel.ForeColor = Color.FromArgb(0, 102, 204);
        summaryLabel.Location = new Point(21, 525);
        summaryLabel.Name = "summaryLabel";
        summaryLabel.Size = new Size(455, 30);
        summaryLabel.TabIndex = 9;
        summaryLabel.Text = "Selected Seats: 0 | Total Amount: ₹0.00";
        summaryLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // onlineBookingButton
        // 
        onlineBookingButton.Location = new Point(12, 604);
        onlineBookingButton.Name = "onlineBookingButton";
        onlineBookingButton.Size = new Size(217, 111);
        onlineBookingButton.TabIndex = 10;
        onlineBookingButton.Text = "Online";
        onlineBookingButton.UseVisualStyleBackColor = true;
        onlineBookingButton.Click += onlineBookingButton_Click;
        // 
        // unblockButton
        // 
        unblockButton.Location = new Point(374, 604);
        unblockButton.Name = "unblockButton";
        unblockButton.Size = new Size(244, 104);
        unblockButton.TabIndex = 11;
        unblockButton.Text = "Unblock";
        unblockButton.UseVisualStyleBackColor = true;
        unblockButton.Click += unblockButton_Click;
        // 
        // freeTicketButton
        // 
        freeTicketButton.Location = new Point(0, 747);
        freeTicketButton.Name = "freeTicketButton";
        freeTicketButton.Size = new Size(188, 95);
        freeTicketButton.TabIndex = 12;
        freeTicketButton.Text = "Free";
        freeTicketButton.UseVisualStyleBackColor = true;
        freeTicketButton.Click += freeTicketButton_Click;
        // 
        // reservedButton
        // 
        reservedButton.Location = new Point(198, 747);
        reservedButton.Name = "reservedButton";
        reservedButton.Size = new Size(204, 95);
        reservedButton.TabIndex = 13;
        reservedButton.Text = "Reserved";
        reservedButton.UseVisualStyleBackColor = true;
        reservedButton.Click += reservedButton_Click;
        // 
        // teleReservedButton
        // 
        teleReservedButton.Location = new Point(417, 747);
        teleReservedButton.Name = "teleReservedButton";
        teleReservedButton.Size = new Size(233, 95);
        teleReservedButton.TabIndex = 14;
        teleReservedButton.Text = "Tele Reserved";
        teleReservedButton.UseVisualStyleBackColor = true;
        teleReservedButton.Click += teleReservedButton_Click;
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(409, 1011);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(241, 116);
        cancelButton.TabIndex = 15;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // ReservationDialog
        // 
        CancelButton = cancelButton;
        ClientSize = new Size(662, 1139);
        Controls.Add(resChargeCheckBox);
        Controls.Add(threeDChargeCheckBox);
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
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ReservationDialog";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Reservation / Special Booking";
        paymentGroup.ResumeLayout(false);
        paymentGroup.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
