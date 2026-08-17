namespace CinemaTicketing.WinForms;

partial class AccountsForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label headerLabel;
    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TabPage cashoutTabPage;
    private System.Windows.Forms.TabPage reportsTabPage;
    private System.Windows.Forms.Panel topCashoutPanel;
    private System.Windows.Forms.Label dateLabel;
    private System.Windows.Forms.DateTimePicker datePicker;
    private System.Windows.Forms.Label theatreLabel;
    private System.Windows.Forms.ComboBox theatreComboBox;
    private System.Windows.Forms.Button refreshButton;
    private System.Windows.Forms.DataGridView cashoutGrid;
    private System.Windows.Forms.Panel bottomPanel;
    private System.Windows.Forms.Label counterBalanceLabel;
    private System.Windows.Forms.Label selectedShowLabel;
    private System.Windows.Forms.Button cashoutPrintButton;
    private System.Windows.Forms.DataGridView reportsGrid;
    private System.Windows.Forms.Panel reportsTopPanel;
    private System.Windows.Forms.Button printReportButton;
    private System.Windows.Forms.Button refreshReportsButton;
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
        headerLabel = new Label();
        tabControl = new TabControl();
        cashoutTabPage = new TabPage();
        cashoutGrid = new DataGridView();
        bottomPanel = new Panel();
        cashoutPrintButton = new Button();
        selectedShowLabel = new Label();
        counterBalanceLabel = new Label();
        topCashoutPanel = new Panel();
        refreshButton = new Button();
        theatreComboBox = new ComboBox();
        theatreLabel = new Label();
        datePicker = new DateTimePicker();
        dateLabel = new Label();
        reportsTabPage = new TabPage();
        reportsGrid = new DataGridView();
        reportsTopPanel = new Panel();
        refreshReportsButton = new Button();
        printReportButton = new Button();
        statusStrip = new StatusStrip();
        statusLabel = new ToolStripStatusLabel();
        tabControl.SuspendLayout();
        cashoutTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)cashoutGrid).BeginInit();
        bottomPanel.SuspendLayout();
        topCashoutPanel.SuspendLayout();
        reportsTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)reportsGrid).BeginInit();
        reportsTopPanel.SuspendLayout();
        statusStrip.SuspendLayout();
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
        headerLabel.Size = new Size(2036, 35);
        headerLabel.TabIndex = 0;
        headerLabel.Text = " Accounts & Cashout Management";
        headerLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // tabControl
        // 
        tabControl.Controls.Add(cashoutTabPage);
        tabControl.Controls.Add(reportsTabPage);
        tabControl.Dock = DockStyle.Fill;
        tabControl.Location = new Point(0, 35);
        tabControl.Name = "tabControl";
        tabControl.SelectedIndex = 0;
        tabControl.Size = new Size(2036, 1078);
        tabControl.TabIndex = 1;
        tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
        // 
        // cashoutTabPage
        // 
        cashoutTabPage.Controls.Add(cashoutGrid);
        cashoutTabPage.Controls.Add(bottomPanel);
        cashoutTabPage.Controls.Add(topCashoutPanel);
        cashoutTabPage.Location = new Point(8, 46);
        cashoutTabPage.Name = "cashoutTabPage";
        cashoutTabPage.Padding = new Padding(3);
        cashoutTabPage.Size = new Size(2020, 1024);
        cashoutTabPage.TabIndex = 0;
        cashoutTabPage.Text = "Cashout";
        cashoutTabPage.UseVisualStyleBackColor = true;
        // 
        // cashoutGrid
        // 
        cashoutGrid.AllowUserToAddRows = false;
        cashoutGrid.AllowUserToDeleteRows = false;
        cashoutGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        cashoutGrid.Dock = DockStyle.Fill;
        cashoutGrid.Location = new Point(3, 82);
        cashoutGrid.MultiSelect = false;
        cashoutGrid.Name = "cashoutGrid";
        cashoutGrid.ReadOnly = true;
        cashoutGrid.RowHeadersVisible = false;
        cashoutGrid.RowHeadersWidth = 82;
        cashoutGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        cashoutGrid.Size = new Size(2014, 737);
        cashoutGrid.TabIndex = 1;
        cashoutGrid.SelectionChanged += cashoutGrid_SelectionChanged;
        // 
        // bottomPanel
        // 
        bottomPanel.Controls.Add(cashoutPrintButton);
        bottomPanel.Controls.Add(selectedShowLabel);
        bottomPanel.Controls.Add(counterBalanceLabel);
        bottomPanel.Dock = DockStyle.Bottom;
        bottomPanel.Location = new Point(3, 819);
        bottomPanel.Name = "bottomPanel";
        bottomPanel.Size = new Size(2014, 202);
        bottomPanel.TabIndex = 2;
        // 
        // cashoutPrintButton
        // 
        cashoutPrintButton.BackColor = Color.FromArgb(40, 167, 69);
        cashoutPrintButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        cashoutPrintButton.ForeColor = Color.White;
        cashoutPrintButton.Location = new Point(800, 15);
        cashoutPrintButton.Name = "cashoutPrintButton";
        cashoutPrintButton.Size = new Size(319, 71);
        cashoutPrintButton.TabIndex = 2;
        cashoutPrintButton.Text = "CashOut / Print";
        cashoutPrintButton.UseVisualStyleBackColor = false;
        cashoutPrintButton.Click += cashoutPrintButton_Click;
        // 
        // selectedShowLabel
        // 
        selectedShowLabel.AutoSize = true;
        selectedShowLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        selectedShowLabel.Location = new Point(15, 75);
        selectedShowLabel.Name = "selectedShowLabel";
        selectedShowLabel.Size = new Size(253, 32);
        selectedShowLabel.TabIndex = 1;
        selectedShowLabel.Text = "Selected Show: None";
        // 
        // counterBalanceLabel
        // 
        counterBalanceLabel.AutoSize = true;
        counterBalanceLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        counterBalanceLabel.ForeColor = Color.FromArgb(0, 102, 204);
        counterBalanceLabel.Location = new Point(15, 12);
        counterBalanceLabel.Name = "counterBalanceLabel";
        counterBalanceLabel.Size = new Size(311, 37);
        counterBalanceLabel.TabIndex = 0;
        counterBalanceLabel.Text = "Counter Balance: ₹0.00";
        // 
        // topCashoutPanel
        // 
        topCashoutPanel.Controls.Add(refreshButton);
        topCashoutPanel.Controls.Add(theatreComboBox);
        topCashoutPanel.Controls.Add(theatreLabel);
        topCashoutPanel.Controls.Add(datePicker);
        topCashoutPanel.Controls.Add(dateLabel);
        topCashoutPanel.Dock = DockStyle.Top;
        topCashoutPanel.Location = new Point(3, 3);
        topCashoutPanel.Name = "topCashoutPanel";
        topCashoutPanel.Size = new Size(2014, 79);
        topCashoutPanel.TabIndex = 0;
        // 
        // refreshButton
        // 
        refreshButton.Location = new Point(1190, 12);
        refreshButton.Name = "refreshButton";
        refreshButton.Size = new Size(240, 43);
        refreshButton.TabIndex = 4;
        refreshButton.Text = "Refresh";
        refreshButton.UseVisualStyleBackColor = true;
        refreshButton.Click += refreshButton_Click;
        // 
        // theatreComboBox
        // 
        theatreComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        theatreComboBox.FormattingEnabled = true;
        theatreComboBox.Location = new Point(783, 15);
        theatreComboBox.Name = "theatreComboBox";
        theatreComboBox.Size = new Size(351, 40);
        theatreComboBox.TabIndex = 3;
        theatreComboBox.SelectedIndexChanged += Filter_Changed;
        // 
        // theatreLabel
        // 
        theatreLabel.AutoSize = true;
        theatreLabel.Location = new Point(605, 19);
        theatreLabel.Name = "theatreLabel";
        theatreLabel.Size = new Size(100, 32);
        theatreLabel.TabIndex = 2;
        theatreLabel.Text = "Theatre:";
        // 
        // datePicker
        // 
        datePicker.Format = DateTimePickerFormat.Short;
        datePicker.Location = new Point(117, 15);
        datePicker.Name = "datePicker";
        datePicker.Size = new Size(230, 39);
        datePicker.TabIndex = 1;
        datePicker.ValueChanged += Filter_Changed;
        // 
        // dateLabel
        // 
        dateLabel.AutoSize = true;
        dateLabel.Location = new Point(10, 15);
        dateLabel.Name = "dateLabel";
        dateLabel.Size = new Size(69, 32);
        dateLabel.TabIndex = 0;
        dateLabel.Text = "Date:";
        // 
        // reportsTabPage
        // 
        reportsTabPage.Controls.Add(reportsGrid);
        reportsTabPage.Controls.Add(reportsTopPanel);
        reportsTabPage.Location = new Point(8, 46);
        reportsTabPage.Name = "reportsTabPage";
        reportsTabPage.Padding = new Padding(3);
        reportsTabPage.Size = new Size(2020, 1024);
        reportsTabPage.TabIndex = 1;
        reportsTabPage.Text = "Unprinted Reports";
        reportsTabPage.UseVisualStyleBackColor = true;
        // 
        // reportsGrid
        // 
        reportsGrid.AllowUserToAddRows = false;
        reportsGrid.AllowUserToDeleteRows = false;
        reportsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        reportsGrid.Dock = DockStyle.Fill;
        reportsGrid.Location = new Point(3, 89);
        reportsGrid.MultiSelect = false;
        reportsGrid.Name = "reportsGrid";
        reportsGrid.ReadOnly = true;
        reportsGrid.RowHeadersVisible = false;
        reportsGrid.RowHeadersWidth = 82;
        reportsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        reportsGrid.Size = new Size(2014, 932);
        reportsGrid.TabIndex = 1;
        // 
        // reportsTopPanel
        // 
        reportsTopPanel.Controls.Add(refreshReportsButton);
        reportsTopPanel.Controls.Add(printReportButton);
        reportsTopPanel.Dock = DockStyle.Top;
        reportsTopPanel.Location = new Point(3, 3);
        reportsTopPanel.Name = "reportsTopPanel";
        reportsTopPanel.Size = new Size(2014, 86);
        reportsTopPanel.TabIndex = 0;
        // 
        // refreshReportsButton
        // 
        refreshReportsButton.Location = new Point(453, 10);
        refreshReportsButton.Name = "refreshReportsButton";
        refreshReportsButton.Size = new Size(190, 55);
        refreshReportsButton.TabIndex = 1;
        refreshReportsButton.Text = "Refresh";
        refreshReportsButton.UseVisualStyleBackColor = true;
        refreshReportsButton.Click += refreshReportsButton_Click;
        // 
        // printReportButton
        // 
        printReportButton.Location = new Point(192, 10);
        printReportButton.Name = "printReportButton";
        printReportButton.Size = new Size(215, 55);
        printReportButton.TabIndex = 0;
        printReportButton.Text = "Print Selected";
        printReportButton.UseVisualStyleBackColor = true;
        printReportButton.Click += printReportButton_Click;
        // 
        // statusStrip
        // 
        statusStrip.ImageScalingSize = new Size(32, 32);
        statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
        statusStrip.Location = new Point(0, 1113);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(2036, 42);
        statusStrip.TabIndex = 2;
        // 
        // statusLabel
        // 
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(83, 32);
        statusLabel.Text = "Ready.";
        // 
        // AccountsForm
        // 
        ClientSize = new Size(2036, 1155);
        Controls.Add(tabControl);
        Controls.Add(statusStrip);
        Controls.Add(headerLabel);
        Name = "AccountsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Accounts & Cashout Management";
        Shown += AccountsForm_Shown;
        tabControl.ResumeLayout(false);
        cashoutTabPage.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)cashoutGrid).EndInit();
        bottomPanel.ResumeLayout(false);
        bottomPanel.PerformLayout();
        topCashoutPanel.ResumeLayout(false);
        topCashoutPanel.PerformLayout();
        reportsTabPage.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)reportsGrid).EndInit();
        reportsTopPanel.ResumeLayout(false);
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
