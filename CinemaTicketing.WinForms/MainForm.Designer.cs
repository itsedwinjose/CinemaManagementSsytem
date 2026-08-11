namespace CinemaTicketing.WinForms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    // Center Seat Map Area
    private System.Windows.Forms.Panel centerPanel;
    private System.Windows.Forms.Panel audiHeaderPanel;
    private System.Windows.Forms.Label audiHeaderLabel;
    private CinemaTicketing.WinForms.Controls.CinemaSeatMapControl seatMap;
    private System.Windows.Forms.Panel bottomBarPanel;
    private System.Windows.Forms.TabControl audiTabControl;
    private System.Windows.Forms.FlowLayoutPanel legendFlowPanel;

    // Right Control Panel
    private System.Windows.Forms.Panel rightPanel;

    // Top Section (Date, Show, Payment, Ticket/Reservation)
    private System.Windows.Forms.Label dateLabel;
    private System.Windows.Forms.DateTimePicker datePicker;
    private System.Windows.Forms.Label showLabel;
    private System.Windows.Forms.ComboBox showComboBox;
    private System.Windows.Forms.GroupBox paymentGroup;
    private System.Windows.Forms.RadioButton cashRadio;
    private System.Windows.Forms.RadioButton cardRadio;
    private System.Windows.Forms.RadioButton onlineRadio;
    private System.Windows.Forms.RadioButton upiRadio;
    private System.Windows.Forms.Button ticketButton;
    private System.Windows.Forms.Button reservationButton;

    // Current / Selected Show Group
    private System.Windows.Forms.GroupBox showInfoGroup;
    private System.Windows.Forms.Label currentShowTitleLabel;
    private System.Windows.Forms.Label showTypeNameLabel;
    private System.Windows.Forms.Label showTimeTitleLabel;
    private System.Windows.Forms.Label showTimeLabel;
    private System.Windows.Forms.Label movieNameTitleLabel;
    private System.Windows.Forms.Label movieNameLabel;
    private System.Windows.Forms.Label ticketPriceTitleLabel;
    private System.Windows.Forms.Label ticketPriceLabel;

    // Options Checkboxes
    private System.Windows.Forms.RadioButton familyRadio;
    private System.Windows.Forms.CheckBox reprintCheckBox;
    private System.Windows.Forms.RadioButton ticketRadio;
    private System.Windows.Forms.CheckBox resvChargeCheckBox;
    private System.Windows.Forms.CheckBox threeDChargeCheckBox;

    // Action Buttons Matrix
    private System.Windows.Forms.Button theatreSettingsButton;
    private System.Windows.Forms.Button setMovieButton;
    private System.Windows.Forms.Button setCurrentShowButton;
    private System.Windows.Forms.Button accountsButton;
    private System.Windows.Forms.Button employeeInfoButton;
    private System.Windows.Forms.Button roleBasedLogButton;
    private System.Windows.Forms.Button passwordChangeButton;
    private System.Windows.Forms.Button seatAllocButton;
    private System.Windows.Forms.Button refreshButton;

    // Bottom Stats Strips
    private System.Windows.Forms.Panel statsPanel;
    private System.Windows.Forms.Label soldTotalHeaderLabel;
    private System.Windows.Forms.Label soldTotalValueLabel;
    private System.Windows.Forms.Label counterAmountHeaderLabel;
    private System.Windows.Forms.Label counterAmountValueLabel;

    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.ToolStripStatusLabel statusLabel;

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
        centerPanel = new Panel();
        seatMap = new CinemaTicketing.WinForms.Controls.CinemaSeatMapControl();
        audiHeaderPanel = new Panel();
        audiHeaderLabel = new Label();
        bottomBarPanel = new Panel();
        audiTabControl = new TabControl();
        legendFlowPanel = new FlowLayoutPanel();
        rightPanel = new Panel();
        dateLabel = new Label();
        datePicker = new DateTimePicker();
        showLabel = new Label();
        showComboBox = new ComboBox();
        paymentGroup = new GroupBox();
        cashRadio = new RadioButton();
        cardRadio = new RadioButton();
        onlineRadio = new RadioButton();
        upiRadio = new RadioButton();
        ticketButton = new Button();
        reservationButton = new Button();
        showInfoGroup = new GroupBox();
        currentShowTitleLabel = new Label();
        showTypeNameLabel = new Label();
        showTimeTitleLabel = new Label();
        showTimeLabel = new Label();
        movieNameTitleLabel = new Label();
        movieNameLabel = new Label();
        ticketPriceTitleLabel = new Label();
        ticketPriceLabel = new Label();
        familyRadio = new RadioButton();
        reprintCheckBox = new CheckBox();
        ticketRadio = new RadioButton();
        resvChargeCheckBox = new CheckBox();
        threeDChargeCheckBox = new CheckBox();
        theatreSettingsButton = new Button();
        setMovieButton = new Button();
        setCurrentShowButton = new Button();
        accountsButton = new Button();
        employeeInfoButton = new Button();
        roleBasedLogButton = new Button();
        passwordChangeButton = new Button();
        seatAllocButton = new Button();
        refreshButton = new Button();
        statsPanel = new Panel();
        soldTotalHeaderLabel = new Label();
        soldTotalValueLabel = new Label();
        counterAmountHeaderLabel = new Label();
        counterAmountValueLabel = new Label();
        statusStrip = new StatusStrip();
        statusLabel = new ToolStripStatusLabel();
        centerPanel.SuspendLayout();
        audiHeaderPanel.SuspendLayout();
        bottomBarPanel.SuspendLayout();
        rightPanel.SuspendLayout();
        paymentGroup.SuspendLayout();
        showInfoGroup.SuspendLayout();
        statsPanel.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();
        // 
        // centerPanel
        // 
        centerPanel.Controls.Add(seatMap);
        centerPanel.Controls.Add(audiHeaderPanel);
        centerPanel.Controls.Add(bottomBarPanel);
        centerPanel.Dock = DockStyle.Fill;
        centerPanel.Location = new Point(0, 0);
        centerPanel.Name = "centerPanel";
        centerPanel.Size = new Size(1550, 1039);
        centerPanel.TabIndex = 0;
        // 
        // seatMap
        // 
        seatMap.BackColor = Color.FromArgb(245, 247, 250);
        seatMap.Dock = DockStyle.Fill;
        seatMap.Location = new Point(0, 28);
        seatMap.Name = "seatMap";
        seatMap.Size = new Size(1550, 971);
        seatMap.TabIndex = 1;
        seatMap.SelectionChanged += seatMap_SelectionChanged;
        // 
        // audiHeaderPanel
        // 
        audiHeaderPanel.BackColor = Color.FromArgb(235, 240, 245);
        audiHeaderPanel.Controls.Add(audiHeaderLabel);
        audiHeaderPanel.Dock = DockStyle.Top;
        audiHeaderPanel.Location = new Point(0, 0);
        audiHeaderPanel.Name = "audiHeaderPanel";
        audiHeaderPanel.Size = new Size(1550, 28);
        audiHeaderPanel.TabIndex = 0;
        // 
        // audiHeaderLabel
        // 
        audiHeaderLabel.Dock = DockStyle.Fill;
        audiHeaderLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        audiHeaderLabel.ForeColor = Color.FromArgb(30, 30, 30);
        audiHeaderLabel.Location = new Point(0, 0);
        audiHeaderLabel.Name = "audiHeaderLabel";
        audiHeaderLabel.Size = new Size(1550, 28);
        audiHeaderLabel.TabIndex = 0;
        audiHeaderLabel.Text = "Audi-1";
        audiHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // bottomBarPanel
        // 
        bottomBarPanel.BackColor = Color.FromArgb(225, 235, 240);
        bottomBarPanel.Controls.Add(audiTabControl);
        bottomBarPanel.Controls.Add(legendFlowPanel);
        bottomBarPanel.Dock = DockStyle.Bottom;
        bottomBarPanel.Location = new Point(0, 999);
        bottomBarPanel.Name = "bottomBarPanel";
        bottomBarPanel.Size = new Size(1550, 40);
        bottomBarPanel.TabIndex = 2;
        // 
        // audiTabControl
        // 
        audiTabControl.Dock = DockStyle.Left;
        audiTabControl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        audiTabControl.Location = new Point(0, 0);
        audiTabControl.Name = "audiTabControl";
        audiTabControl.SelectedIndex = 0;
        audiTabControl.Size = new Size(380, 40);
        audiTabControl.TabIndex = 0;
        audiTabControl.SelectedIndexChanged += audiTabControl_SelectedIndexChanged;
        // 
        // legendFlowPanel
        // 
        legendFlowPanel.Dock = DockStyle.Right;
        legendFlowPanel.Font = new Font("Segoe UI", 8F);
        legendFlowPanel.Location = new Point(1015, 0);
        legendFlowPanel.Name = "legendFlowPanel";
        legendFlowPanel.Padding = new Padding(0, 8, 0, 0);
        legendFlowPanel.Size = new Size(535, 40);
        legendFlowPanel.TabIndex = 1;
        // 
        // rightPanel
        // 
        rightPanel.BackColor = Color.FromArgb(212, 235, 242);
        rightPanel.Controls.Add(dateLabel);
        rightPanel.Controls.Add(datePicker);
        rightPanel.Controls.Add(showLabel);
        rightPanel.Controls.Add(showComboBox);
        rightPanel.Controls.Add(paymentGroup);
        rightPanel.Controls.Add(ticketButton);
        rightPanel.Controls.Add(reservationButton);
        rightPanel.Controls.Add(showInfoGroup);
        rightPanel.Controls.Add(familyRadio);
        rightPanel.Controls.Add(reprintCheckBox);
        rightPanel.Controls.Add(ticketRadio);
        rightPanel.Controls.Add(resvChargeCheckBox);
        rightPanel.Controls.Add(threeDChargeCheckBox);
        rightPanel.Controls.Add(theatreSettingsButton);
        rightPanel.Controls.Add(setMovieButton);
        rightPanel.Controls.Add(setCurrentShowButton);
        rightPanel.Controls.Add(accountsButton);
        rightPanel.Controls.Add(employeeInfoButton);
        rightPanel.Controls.Add(roleBasedLogButton);
        rightPanel.Controls.Add(passwordChangeButton);
        rightPanel.Controls.Add(seatAllocButton);
        rightPanel.Controls.Add(refreshButton);
        rightPanel.Controls.Add(statsPanel);
        rightPanel.Dock = DockStyle.Right;
        rightPanel.Location = new Point(1550, 0);
        rightPanel.Name = "rightPanel";
        rightPanel.Padding = new Padding(8);
        rightPanel.Size = new Size(320, 1039);
        rightPanel.TabIndex = 1;
        // 
        // dateLabel
        // 
        dateLabel.AutoSize = true;
        dateLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dateLabel.Location = new Point(10, 10);
        dateLabel.Name = "dateLabel";
        dateLabel.Size = new Size(141, 32);
        dateLabel.TabIndex = 0;
        dateLabel.Text = "Select Date";
        // 
        // datePicker
        // 
        datePicker.Format = DateTimePickerFormat.Short;
        datePicker.Location = new Point(90, 7);
        datePicker.Name = "datePicker";
        datePicker.Size = new Size(218, 39);
        datePicker.TabIndex = 1;
        datePicker.ValueChanged += datePicker_ValueChanged;
        // 
        // showLabel
        // 
        showLabel.AutoSize = true;
        showLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        showLabel.Location = new Point(10, 38);
        showLabel.Name = "showLabel";
        showLabel.Size = new Size(75, 32);
        showLabel.TabIndex = 2;
        showLabel.Text = "Show";
        // 
        // showComboBox
        // 
        showComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        showComboBox.FormattingEnabled = true;
        showComboBox.Location = new Point(90, 35);
        showComboBox.Name = "showComboBox";
        showComboBox.Size = new Size(218, 40);
        showComboBox.TabIndex = 3;
        showComboBox.SelectedIndexChanged += showComboBox_SelectedIndexChanged;
        // 
        // paymentGroup
        // 
        paymentGroup.Controls.Add(cashRadio);
        paymentGroup.Controls.Add(cardRadio);
        paymentGroup.Controls.Add(onlineRadio);
        paymentGroup.Controls.Add(upiRadio);
        paymentGroup.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        paymentGroup.Location = new Point(10, 63);
        paymentGroup.Name = "paymentGroup";
        paymentGroup.Size = new Size(298, 62);
        paymentGroup.TabIndex = 4;
        paymentGroup.TabStop = false;
        paymentGroup.Text = "Payment Mode";
        // 
        // cashRadio
        // 
        cashRadio.AutoSize = true;
        cashRadio.Checked = true;
        cashRadio.Font = new Font("Segoe UI", 8.5F);
        cashRadio.Location = new Point(8, 18);
        cashRadio.Name = "cashRadio";
        cashRadio.Size = new Size(162, 35);
        cashRadio.TabIndex = 0;
        cashRadio.TabStop = true;
        cashRadio.Text = "Cash   ( 45 )";
        cashRadio.UseVisualStyleBackColor = true;
        // 
        // cardRadio
        // 
        cardRadio.AutoSize = true;
        cardRadio.Font = new Font("Segoe UI", 8.5F);
        cardRadio.Location = new Point(150, 18);
        cardRadio.Name = "cardRadio";
        cardRadio.Size = new Size(149, 35);
        cardRadio.TabIndex = 1;
        cardRadio.Text = "Card   ( 0 )";
        cardRadio.UseVisualStyleBackColor = true;
        // 
        // onlineRadio
        // 
        onlineRadio.AutoSize = true;
        onlineRadio.Font = new Font("Segoe UI", 8.5F);
        onlineRadio.Location = new Point(8, 38);
        onlineRadio.Name = "onlineRadio";
        onlineRadio.Size = new Size(156, 35);
        onlineRadio.TabIndex = 2;
        onlineRadio.Text = "Online ( 0 )";
        onlineRadio.UseVisualStyleBackColor = true;
        // 
        // upiRadio
        // 
        upiRadio.AutoSize = true;
        upiRadio.Font = new Font("Segoe UI", 8.5F);
        upiRadio.Location = new Point(150, 38);
        upiRadio.Name = "upiRadio";
        upiRadio.Size = new Size(173, 35);
        upiRadio.TabIndex = 3;
        upiRadio.Text = "QR UPI ( 72 )";
        upiRadio.UseVisualStyleBackColor = true;
        // 
        // ticketButton
        // 
        ticketButton.BackColor = Color.FromArgb(0, 168, 223);
        ticketButton.FlatStyle = FlatStyle.Flat;
        ticketButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        ticketButton.ForeColor = Color.White;
        ticketButton.Location = new Point(10, 130);
        ticketButton.Name = "ticketButton";
        ticketButton.Size = new Size(145, 30);
        ticketButton.TabIndex = 5;
        ticketButton.Text = "Ticket / Space";
        ticketButton.UseVisualStyleBackColor = false;
        ticketButton.Click += ticketButton_Click;
        // 
        // reservationButton
        // 
        reservationButton.BackColor = Color.FromArgb(0, 168, 223);
        reservationButton.FlatStyle = FlatStyle.Flat;
        reservationButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        reservationButton.ForeColor = Color.White;
        reservationButton.Location = new Point(162, 130);
        reservationButton.Name = "reservationButton";
        reservationButton.Size = new Size(145, 30);
        reservationButton.TabIndex = 6;
        reservationButton.Text = "Reservation";
        reservationButton.UseVisualStyleBackColor = false;
        reservationButton.Click += reservationButton_Click;
        // 
        // showInfoGroup
        // 
        showInfoGroup.BackColor = Color.FromArgb(228, 242, 247);
        showInfoGroup.Controls.Add(currentShowTitleLabel);
        showInfoGroup.Controls.Add(showTypeNameLabel);
        showInfoGroup.Controls.Add(showTimeTitleLabel);
        showInfoGroup.Controls.Add(showTimeLabel);
        showInfoGroup.Controls.Add(movieNameTitleLabel);
        showInfoGroup.Controls.Add(movieNameLabel);
        showInfoGroup.Controls.Add(ticketPriceTitleLabel);
        showInfoGroup.Controls.Add(ticketPriceLabel);
        showInfoGroup.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        showInfoGroup.Location = new Point(10, 166);
        showInfoGroup.Name = "showInfoGroup";
        showInfoGroup.Size = new Size(298, 125);
        showInfoGroup.TabIndex = 7;
        showInfoGroup.TabStop = false;
        // 
        // currentShowTitleLabel
        // 
        currentShowTitleLabel.AutoSize = true;
        currentShowTitleLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        currentShowTitleLabel.ForeColor = Color.FromArgb(30, 30, 30);
        currentShowTitleLabel.Location = new Point(6, 12);
        currentShowTitleLabel.Name = "currentShowTitleLabel";
        currentShowTitleLabel.Size = new Size(272, 31);
        currentShowTitleLabel.TabIndex = 0;
        currentShowTitleLabel.Text = "Current / Selected Show";
        // 
        // showTypeNameLabel
        // 
        showTypeNameLabel.AutoSize = true;
        showTypeNameLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        showTypeNameLabel.ForeColor = Color.FromArgb(217, 83, 79);
        showTypeNameLabel.Location = new Point(6, 28);
        showTypeNameLabel.Name = "showTypeNameLabel";
        showTypeNameLabel.Size = new Size(123, 31);
        showTypeNameLabel.TabIndex = 1;
        showTypeNameLabel.Text = "First show";
        // 
        // showTimeTitleLabel
        // 
        showTimeTitleLabel.AutoSize = true;
        showTimeTitleLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        showTimeTitleLabel.ForeColor = Color.FromArgb(80, 80, 80);
        showTimeTitleLabel.Location = new Point(6, 45);
        showTimeTitleLabel.Name = "showTimeTitleLabel";
        showTimeTitleLabel.Size = new Size(126, 30);
        showTimeTitleLabel.TabIndex = 2;
        showTimeTitleLabel.Text = "Show Time";
        // 
        // showTimeLabel
        // 
        showTimeLabel.AutoSize = true;
        showTimeLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        showTimeLabel.ForeColor = Color.FromArgb(217, 83, 79);
        showTimeLabel.Location = new Point(6, 59);
        showTimeLabel.Name = "showTimeLabel";
        showTimeLabel.Size = new Size(108, 31);
        showTimeLabel.TabIndex = 3;
        showTimeLabel.Text = "06:00PM";
        // 
        // movieNameTitleLabel
        // 
        movieNameTitleLabel.AutoSize = true;
        movieNameTitleLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        movieNameTitleLabel.ForeColor = Color.FromArgb(80, 80, 80);
        movieNameTitleLabel.Location = new Point(6, 76);
        movieNameTitleLabel.Name = "movieNameTitleLabel";
        movieNameTitleLabel.Size = new Size(144, 30);
        movieNameTitleLabel.TabIndex = 4;
        movieNameTitleLabel.Text = "Movie Name";
        // 
        // movieNameLabel
        // 
        movieNameLabel.AutoSize = true;
        movieNameLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        movieNameLabel.ForeColor = Color.FromArgb(217, 83, 79);
        movieNameLabel.Location = new Point(6, 90);
        movieNameLabel.Name = "movieNameLabel";
        movieNameLabel.Size = new Size(163, 31);
        movieNameLabel.TabIndex = 5;
        movieNameLabel.Text = "THUDAKKAM";
        // 
        // ticketPriceTitleLabel
        // 
        ticketPriceTitleLabel.AutoSize = true;
        ticketPriceTitleLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        ticketPriceTitleLabel.ForeColor = Color.FromArgb(80, 80, 80);
        ticketPriceTitleLabel.Location = new Point(180, 76);
        ticketPriceTitleLabel.Name = "ticketPriceTitleLabel";
        ticketPriceTitleLabel.Size = new Size(134, 30);
        ticketPriceTitleLabel.TabIndex = 6;
        ticketPriceTitleLabel.Text = "Ticket Price";
        // 
        // ticketPriceLabel
        // 
        ticketPriceLabel.AutoSize = true;
        ticketPriceLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        ticketPriceLabel.ForeColor = Color.FromArgb(30, 30, 30);
        ticketPriceLabel.Location = new Point(180, 90);
        ticketPriceLabel.Name = "ticketPriceLabel";
        ticketPriceLabel.Size = new Size(53, 31);
        ticketPriceLabel.TabIndex = 7;
        ticketPriceLabel.Text = "150";
        // 
        // familyRadio
        // 
        familyRadio.AutoSize = true;
        familyRadio.Font = new Font("Segoe UI", 8.5F);
        familyRadio.Location = new Point(10, 296);
        familyRadio.Name = "familyRadio";
        familyRadio.Size = new Size(154, 35);
        familyRadio.TabIndex = 8;
        familyRadio.Text = "Family / F9";
        familyRadio.UseVisualStyleBackColor = true;
        // 
        // reprintCheckBox
        // 
        reprintCheckBox.AutoSize = true;
        reprintCheckBox.Checked = true;
        reprintCheckBox.CheckState = CheckState.Checked;
        reprintCheckBox.Font = new Font("Segoe UI", 8.5F);
        reprintCheckBox.Location = new Point(105, 296);
        reprintCheckBox.Name = "reprintCheckBox";
        reprintCheckBox.Size = new Size(164, 35);
        reprintCheckBox.TabIndex = 9;
        reprintCheckBox.Text = "Reprint / F6";
        reprintCheckBox.UseVisualStyleBackColor = true;
        // 
        // ticketRadio
        // 
        ticketRadio.AutoSize = true;
        ticketRadio.Checked = true;
        ticketRadio.Font = new Font("Segoe UI", 8.5F);
        ticketRadio.Location = new Point(205, 296);
        ticketRadio.Name = "ticketRadio";
        ticketRadio.Size = new Size(105, 35);
        ticketRadio.TabIndex = 10;
        ticketRadio.TabStop = true;
        ticketRadio.Text = "Ticket";
        ticketRadio.UseVisualStyleBackColor = true;
        // 
        // resvChargeCheckBox
        // 
        resvChargeCheckBox.AutoSize = true;
        resvChargeCheckBox.Font = new Font("Segoe UI", 8F);
        resvChargeCheckBox.Location = new Point(10, 318);
        resvChargeCheckBox.Name = "resvChargeCheckBox";
        resvChargeCheckBox.Size = new Size(391, 34);
        resvChargeCheckBox.TabIndex = 11;
        resvChargeCheckBox.Text = "Reservation Charge - 10 Rs Per Seat";
        resvChargeCheckBox.UseVisualStyleBackColor = true;
        resvChargeCheckBox.CheckedChanged += resvChargeCheckBox_CheckedChanged;
        // 
        // threeDChargeCheckBox
        // 
        threeDChargeCheckBox.AutoSize = true;
        threeDChargeCheckBox.Font = new Font("Segoe UI", 8F);
        threeDChargeCheckBox.Location = new Point(10, 337);
        threeDChargeCheckBox.Name = "threeDChargeCheckBox";
        threeDChargeCheckBox.Size = new Size(312, 34);
        threeDChargeCheckBox.TabIndex = 12;
        threeDChargeCheckBox.Text = "3 D Charge - 30 Rs Per Seat";
        threeDChargeCheckBox.UseVisualStyleBackColor = true;
        threeDChargeCheckBox.CheckedChanged += threeDChargeCheckBox_CheckedChanged;
        // 
        // theatreSettingsButton
        // 
        theatreSettingsButton.BackColor = Color.FromArgb(0, 130, 200);
        theatreSettingsButton.FlatStyle = FlatStyle.Flat;
        theatreSettingsButton.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        theatreSettingsButton.ForeColor = Color.White;
        theatreSettingsButton.Location = new Point(10, 360);
        theatreSettingsButton.Name = "theatreSettingsButton";
        theatreSettingsButton.Size = new Size(144, 28);
        theatreSettingsButton.TabIndex = 13;
        theatreSettingsButton.Text = "Theatre Settings";
        theatreSettingsButton.UseVisualStyleBackColor = false;
        theatreSettingsButton.Click += theatreSettingsButton_Click;
        // 
        // setMovieButton
        // 
        setMovieButton.BackColor = Color.FromArgb(0, 130, 200);
        setMovieButton.FlatStyle = FlatStyle.Flat;
        setMovieButton.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        setMovieButton.ForeColor = Color.White;
        setMovieButton.Location = new Point(164, 360);
        setMovieButton.Name = "setMovieButton";
        setMovieButton.Size = new Size(144, 28);
        setMovieButton.TabIndex = 14;
        setMovieButton.Text = "Set Movie";
        setMovieButton.UseVisualStyleBackColor = false;
        setMovieButton.Click += setMovieButton_Click;
        // 
        // setCurrentShowButton
        // 
        setCurrentShowButton.BackColor = Color.FromArgb(0, 130, 200);
        setCurrentShowButton.FlatStyle = FlatStyle.Flat;
        setCurrentShowButton.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        setCurrentShowButton.ForeColor = Color.White;
        setCurrentShowButton.Location = new Point(10, 394);
        setCurrentShowButton.Name = "setCurrentShowButton";
        setCurrentShowButton.Size = new Size(144, 28);
        setCurrentShowButton.TabIndex = 15;
        setCurrentShowButton.Text = "Set Current Show";
        setCurrentShowButton.UseVisualStyleBackColor = false;
        // 
        // accountsButton
        // 
        accountsButton.BackColor = Color.FromArgb(0, 130, 200);
        accountsButton.FlatStyle = FlatStyle.Flat;
        accountsButton.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        accountsButton.ForeColor = Color.White;
        accountsButton.Location = new Point(164, 394);
        accountsButton.Name = "accountsButton";
        accountsButton.Size = new Size(144, 28);
        accountsButton.TabIndex = 16;
        accountsButton.Text = "Accounts";
        accountsButton.UseVisualStyleBackColor = false;
        accountsButton.Click += accountsButton_Click;
        // 
        // employeeInfoButton
        // 
        employeeInfoButton.BackColor = Color.FromArgb(0, 130, 200);
        employeeInfoButton.FlatStyle = FlatStyle.Flat;
        employeeInfoButton.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        employeeInfoButton.ForeColor = Color.White;
        employeeInfoButton.Location = new Point(10, 428);
        employeeInfoButton.Name = "employeeInfoButton";
        employeeInfoButton.Size = new Size(144, 28);
        employeeInfoButton.TabIndex = 17;
        employeeInfoButton.Text = "Employee Info";
        employeeInfoButton.UseVisualStyleBackColor = false;
        // 
        // roleBasedLogButton
        // 
        roleBasedLogButton.BackColor = Color.FromArgb(0, 130, 200);
        roleBasedLogButton.FlatStyle = FlatStyle.Flat;
        roleBasedLogButton.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        roleBasedLogButton.ForeColor = Color.White;
        roleBasedLogButton.Location = new Point(164, 428);
        roleBasedLogButton.Name = "roleBasedLogButton";
        roleBasedLogButton.Size = new Size(144, 28);
        roleBasedLogButton.TabIndex = 18;
        roleBasedLogButton.Text = "Role Based Log...";
        roleBasedLogButton.UseVisualStyleBackColor = false;
        // 
        // passwordChangeButton
        // 
        passwordChangeButton.BackColor = Color.FromArgb(0, 130, 200);
        passwordChangeButton.FlatStyle = FlatStyle.Flat;
        passwordChangeButton.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        passwordChangeButton.ForeColor = Color.White;
        passwordChangeButton.Location = new Point(10, 462);
        passwordChangeButton.Name = "passwordChangeButton";
        passwordChangeButton.Size = new Size(144, 28);
        passwordChangeButton.TabIndex = 19;
        passwordChangeButton.Text = "Password Change";
        passwordChangeButton.UseVisualStyleBackColor = false;
        passwordChangeButton.Click += passwordChangeButton_Click;
        // 
        // seatAllocButton
        // 
        seatAllocButton.BackColor = Color.FromArgb(0, 130, 200);
        seatAllocButton.FlatStyle = FlatStyle.Flat;
        seatAllocButton.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        seatAllocButton.ForeColor = Color.White;
        seatAllocButton.Location = new Point(164, 462);
        seatAllocButton.Name = "seatAllocButton";
        seatAllocButton.Size = new Size(144, 28);
        seatAllocButton.TabIndex = 20;
        seatAllocButton.Text = "Seat Allocation %";
        seatAllocButton.UseVisualStyleBackColor = false;
        // 
        // refreshButton
        // 
        refreshButton.BackColor = Color.FromArgb(0, 168, 223);
        refreshButton.FlatStyle = FlatStyle.Flat;
        refreshButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        refreshButton.ForeColor = Color.White;
        refreshButton.Location = new Point(10, 498);
        refreshButton.Name = "refreshButton";
        refreshButton.Size = new Size(298, 30);
        refreshButton.TabIndex = 21;
        refreshButton.Text = "Refresh / F5";
        refreshButton.UseVisualStyleBackColor = false;
        refreshButton.Click += refreshButton_Click;
        // 
        // statsPanel
        // 
        statsPanel.Controls.Add(soldTotalHeaderLabel);
        statsPanel.Controls.Add(soldTotalValueLabel);
        statsPanel.Controls.Add(counterAmountHeaderLabel);
        statsPanel.Controls.Add(counterAmountValueLabel);
        statsPanel.Location = new Point(10, 534);
        statsPanel.Name = "statsPanel";
        statsPanel.Size = new Size(298, 45);
        statsPanel.TabIndex = 22;
        // 
        // soldTotalHeaderLabel
        // 
        soldTotalHeaderLabel.BackColor = Color.FromArgb(181, 23, 158);
        soldTotalHeaderLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        soldTotalHeaderLabel.ForeColor = Color.White;
        soldTotalHeaderLabel.Location = new Point(0, 0);
        soldTotalHeaderLabel.Name = "soldTotalHeaderLabel";
        soldTotalHeaderLabel.Size = new Size(144, 18);
        soldTotalHeaderLabel.TabIndex = 0;
        soldTotalHeaderLabel.Text = "Sold / Total";
        soldTotalHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // soldTotalValueLabel
        // 
        soldTotalValueLabel.BackColor = Color.White;
        soldTotalValueLabel.BorderStyle = BorderStyle.FixedSingle;
        soldTotalValueLabel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        soldTotalValueLabel.ForeColor = Color.FromArgb(30, 30, 30);
        soldTotalValueLabel.Location = new Point(0, 18);
        soldTotalValueLabel.Name = "soldTotalValueLabel";
        soldTotalValueLabel.Size = new Size(144, 25);
        soldTotalValueLabel.TabIndex = 1;
        soldTotalValueLabel.Text = "67 / 484";
        soldTotalValueLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // counterAmountHeaderLabel
        // 
        counterAmountHeaderLabel.BackColor = Color.FromArgb(181, 23, 158);
        counterAmountHeaderLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        counterAmountHeaderLabel.ForeColor = Color.White;
        counterAmountHeaderLabel.Location = new Point(154, 0);
        counterAmountHeaderLabel.Name = "counterAmountHeaderLabel";
        counterAmountHeaderLabel.Size = new Size(144, 18);
        counterAmountHeaderLabel.TabIndex = 2;
        counterAmountHeaderLabel.Text = "Counter Amount";
        counterAmountHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // counterAmountValueLabel
        // 
        counterAmountValueLabel.BackColor = Color.White;
        counterAmountValueLabel.BorderStyle = BorderStyle.FixedSingle;
        counterAmountValueLabel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        counterAmountValueLabel.ForeColor = Color.FromArgb(30, 30, 30);
        counterAmountValueLabel.Location = new Point(154, 18);
        counterAmountValueLabel.Name = "counterAmountValueLabel";
        counterAmountValueLabel.Size = new Size(144, 25);
        counterAmountValueLabel.TabIndex = 3;
        counterAmountValueLabel.Text = "10090";
        counterAmountValueLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // statusStrip
        // 
        statusStrip.ImageScalingSize = new Size(32, 32);
        statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
        statusStrip.Location = new Point(0, 1039);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(1870, 42);
        statusStrip.TabIndex = 2;
        // 
        // statusLabel
        // 
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(83, 32);
        statusLabel.Text = "Ready.";
        // 
        // MainForm
        // 
        BackColor = Color.FromArgb(212, 235, 242);
        ClientSize = new Size(1870, 1081);
        Controls.Add(centerPanel);
        Controls.Add(rightPanel);
        Controls.Add(statusStrip);
        KeyPreview = true;
        MinimumSize = new Size(1024, 650);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "WINTER GREEN";
        WindowState = FormWindowState.Maximized;
        Shown += MainForm_Shown;
        KeyDown += MainForm_KeyDown;
        centerPanel.ResumeLayout(false);
        audiHeaderPanel.ResumeLayout(false);
        bottomBarPanel.ResumeLayout(false);
        rightPanel.ResumeLayout(false);
        rightPanel.PerformLayout();
        paymentGroup.ResumeLayout(false);
        paymentGroup.PerformLayout();
        showInfoGroup.ResumeLayout(false);
        showInfoGroup.PerformLayout();
        statsPanel.ResumeLayout(false);
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
