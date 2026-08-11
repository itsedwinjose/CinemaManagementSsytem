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
        headerLabel = new System.Windows.Forms.Label();
        tabControl = new System.Windows.Forms.TabControl();
        cashoutTabPage = new System.Windows.Forms.TabPage();
        cashoutGrid = new System.Windows.Forms.DataGridView();
        bottomPanel = new System.Windows.Forms.Panel();
        cashoutPrintButton = new System.Windows.Forms.Button();
        selectedShowLabel = new System.Windows.Forms.Label();
        counterBalanceLabel = new System.Windows.Forms.Label();
        topCashoutPanel = new System.Windows.Forms.Panel();
        refreshButton = new System.Windows.Forms.Button();
        theatreComboBox = new System.Windows.Forms.ComboBox();
        theatreLabel = new System.Windows.Forms.Label();
        datePicker = new System.Windows.Forms.DateTimePicker();
        dateLabel = new System.Windows.Forms.Label();
        reportsTabPage = new System.Windows.Forms.TabPage();
        reportsGrid = new System.Windows.Forms.DataGridView();
        reportsTopPanel = new System.Windows.Forms.Panel();
        refreshReportsButton = new System.Windows.Forms.Button();
        printReportButton = new System.Windows.Forms.Button();
        statusStrip = new System.Windows.Forms.StatusStrip();
        statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
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
        headerLabel.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
        headerLabel.Dock = System.Windows.Forms.DockStyle.Top;
        headerLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        headerLabel.ForeColor = System.Drawing.Color.White;
        headerLabel.Location = new System.Drawing.Point(0, 0);
        headerLabel.Name = "headerLabel";
        headerLabel.Size = new System.Drawing.Size(984, 35);
        headerLabel.TabIndex = 0;
        headerLabel.Text = " Accounts & Cashout Management";
        headerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // tabControl
        // 
        tabControl.Controls.Add(cashoutTabPage);
        tabControl.Controls.Add(reportsTabPage);
        tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
        tabControl.Location = new System.Drawing.Point(0, 35);
        tabControl.Name = "tabControl";
        tabControl.SelectedIndex = 0;
        tabControl.Size = new System.Drawing.Size(984, 495);
        tabControl.TabIndex = 1;
        tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
        // 
        // cashoutTabPage
        // 
        cashoutTabPage.Controls.Add(cashoutGrid);
        cashoutTabPage.Controls.Add(bottomPanel);
        cashoutTabPage.Controls.Add(topCashoutPanel);
        cashoutTabPage.Location = new System.Drawing.Point(4, 24);
        cashoutTabPage.Name = "cashoutTabPage";
        cashoutTabPage.Padding = new System.Windows.Forms.Padding(3);
        cashoutTabPage.Size = new System.Drawing.Size(976, 467);
        cashoutTabPage.TabIndex = 0;
        cashoutTabPage.Text = "Cashout";
        cashoutTabPage.UseVisualStyleBackColor = true;
        // 
        // cashoutGrid
        // 
        cashoutGrid.AllowUserToAddRows = false;
        cashoutGrid.AllowUserToDeleteRows = false;
        cashoutGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        cashoutGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        cashoutGrid.Location = new System.Drawing.Point(3, 48);
        cashoutGrid.MultiSelect = false;
        cashoutGrid.Name = "cashoutGrid";
        cashoutGrid.ReadOnly = true;
        cashoutGrid.RowHeadersVisible = false;
        cashoutGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        cashoutGrid.Size = new System.Drawing.Size(970, 351);
        cashoutGrid.TabIndex = 1;
        cashoutGrid.SelectionChanged += cashoutGrid_SelectionChanged;
        // 
        // bottomPanel
        // 
        bottomPanel.Controls.Add(cashoutPrintButton);
        bottomPanel.Controls.Add(selectedShowLabel);
        bottomPanel.Controls.Add(counterBalanceLabel);
        bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
        bottomPanel.Location = new System.Drawing.Point(3, 399);
        bottomPanel.Name = "bottomPanel";
        bottomPanel.Size = new System.Drawing.Size(970, 65);
        bottomPanel.TabIndex = 2;
        // 
        // cashoutPrintButton
        // 
        cashoutPrintButton.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
        cashoutPrintButton.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        cashoutPrintButton.ForeColor = System.Drawing.Color.White;
        cashoutPrintButton.Location = new System.Drawing.Point(800, 15);
        cashoutPrintButton.Name = "cashoutPrintButton";
        cashoutPrintButton.Size = new System.Drawing.Size(155, 35);
        cashoutPrintButton.TabIndex = 2;
        cashoutPrintButton.Text = "CashOut / Print";
        cashoutPrintButton.UseVisualStyleBackColor = false;
        cashoutPrintButton.Click += cashoutPrintButton_Click;
        // 
        // selectedShowLabel
        // 
        selectedShowLabel.AutoSize = true;
        selectedShowLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        selectedShowLabel.Location = new System.Drawing.Point(15, 38);
        selectedShowLabel.Name = "selectedShowLabel";
        selectedShowLabel.Size = new System.Drawing.Size(127, 15);
        selectedShowLabel.TabIndex = 1;
        selectedShowLabel.Text = "Selected Show: None";
        // 
        // counterBalanceLabel
        // 
        counterBalanceLabel.AutoSize = true;
        counterBalanceLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        counterBalanceLabel.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
        counterBalanceLabel.Location = new System.Drawing.Point(15, 12);
        counterBalanceLabel.Name = "counterBalanceLabel";
        counterBalanceLabel.Size = new System.Drawing.Size(161, 19);
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
        topCashoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
        topCashoutPanel.Location = new System.Drawing.Point(3, 3);
        topCashoutPanel.Name = "topCashoutPanel";
        topCashoutPanel.Size = new System.Drawing.Size(970, 45);
        topCashoutPanel.TabIndex = 0;
        // 
        // refreshButton
        // 
        refreshButton.Location = new System.Drawing.Point(380, 10);
        refreshButton.Name = "refreshButton";
        refreshButton.Size = new System.Drawing.Size(90, 26);
        refreshButton.TabIndex = 4;
        refreshButton.Text = "Refresh";
        refreshButton.UseVisualStyleBackColor = true;
        refreshButton.Click += refreshButton_Click;
        // 
        // theatreComboBox
        // 
        theatreComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        theatreComboBox.FormattingEnabled = true;
        theatreComboBox.Location = new System.Drawing.Point(215, 12);
        theatreComboBox.Name = "theatreComboBox";
        theatreComboBox.Size = new System.Drawing.Size(150, 23);
        theatreComboBox.TabIndex = 3;
        theatreComboBox.SelectedIndexChanged += Filter_Changed;
        // 
        // theatreLabel
        // 
        theatreLabel.AutoSize = true;
        theatreLabel.Location = new System.Drawing.Point(160, 15);
        theatreLabel.Name = "theatreLabel";
        theatreLabel.Size = new System.Drawing.Size(49, 15);
        theatreLabel.TabIndex = 2;
        theatreLabel.Text = "Theatre:";
        // 
        // datePicker
        // 
        datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        datePicker.Location = new System.Drawing.Point(50, 12);
        datePicker.Name = "datePicker";
        datePicker.Size = new System.Drawing.Size(100, 23);
        datePicker.TabIndex = 1;
        datePicker.ValueChanged += Filter_Changed;
        // 
        // dateLabel
        // 
        dateLabel.AutoSize = true;
        dateLabel.Location = new System.Drawing.Point(10, 15);
        dateLabel.Name = "dateLabel";
        dateLabel.Size = new System.Drawing.Size(34, 15);
        dateLabel.TabIndex = 0;
        dateLabel.Text = "Date:";
        // 
        // reportsTabPage
        // 
        reportsTabPage.Controls.Add(reportsGrid);
        reportsTabPage.Controls.Add(reportsTopPanel);
        reportsTabPage.Location = new System.Drawing.Point(4, 24);
        reportsTabPage.Name = "reportsTabPage";
        reportsTabPage.Padding = new System.Windows.Forms.Padding(3);
        reportsTabPage.Size = new System.Drawing.Size(976, 467);
        reportsTabPage.TabIndex = 1;
        reportsTabPage.Text = "Unprinted Reports";
        reportsTabPage.UseVisualStyleBackColor = true;
        // 
        // reportsGrid
        // 
        reportsGrid.AllowUserToAddRows = false;
        reportsGrid.AllowUserToDeleteRows = false;
        reportsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        reportsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        reportsGrid.Location = new System.Drawing.Point(3, 48);
        reportsGrid.MultiSelect = false;
        reportsGrid.Name = "reportsGrid";
        reportsGrid.ReadOnly = true;
        reportsGrid.RowHeadersVisible = false;
        reportsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        reportsGrid.Size = new System.Drawing.Size(970, 416);
        reportsGrid.TabIndex = 1;
        // 
        // reportsTopPanel
        // 
        reportsTopPanel.Controls.Add(refreshReportsButton);
        reportsTopPanel.Controls.Add(printReportButton);
        reportsTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
        reportsTopPanel.Location = new System.Drawing.Point(3, 3);
        reportsTopPanel.Name = "reportsTopPanel";
        reportsTopPanel.Size = new System.Drawing.Size(970, 45);
        reportsTopPanel.TabIndex = 0;
        // 
        // refreshReportsButton
        // 
        refreshReportsButton.Location = new System.Drawing.Point(135, 10);
        refreshReportsButton.Name = "refreshReportsButton";
        refreshReportsButton.Size = new System.Drawing.Size(100, 26);
        refreshReportsButton.TabIndex = 1;
        refreshReportsButton.Text = "Refresh";
        refreshReportsButton.UseVisualStyleBackColor = true;
        refreshReportsButton.Click += refreshReportsButton_Click;
        // 
        // printReportButton
        // 
        printReportButton.Location = new System.Drawing.Point(10, 10);
        printReportButton.Name = "printReportButton";
        printReportButton.Size = new System.Drawing.Size(115, 26);
        printReportButton.TabIndex = 0;
        printReportButton.Text = "Print Selected";
        printReportButton.UseVisualStyleBackColor = true;
        printReportButton.Click += printReportButton_Click;
        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { statusLabel });
        statusStrip.Location = new System.Drawing.Point(0, 530);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new System.Drawing.Size(984, 22);
        statusStrip.TabIndex = 2;
        // 
        // statusLabel
        // 
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new System.Drawing.Size(39, 17);
        statusLabel.Text = "Ready.";
        // 
        // AccountsForm
        // 
        ClientSize = new System.Drawing.Size(984, 552);
        Controls.Add(tabControl);
        Controls.Add(statusStrip);
        Controls.Add(headerLabel);
        Name = "AccountsForm";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
