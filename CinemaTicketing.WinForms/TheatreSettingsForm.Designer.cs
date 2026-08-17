namespace CinemaTicketing.WinForms;

partial class TheatreSettingsForm
{
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Panel headerPanel;
    private System.Windows.Forms.Label headerTitleLabel;

    // Top Input Bar
    private System.Windows.Forms.Panel topInputsPanel;
    private System.Windows.Forms.Label cinemaLabel;
    private System.Windows.Forms.ComboBox cinemaComboBox;
    private System.Windows.Forms.Label showTypeLabel;
    private System.Windows.Forms.ComboBox showTypeComboBox;
    private System.Windows.Forms.Label showTimeLabel;
    private System.Windows.Forms.TextBox showTimeTextBox;
    private System.Windows.Forms.Label priceLabel;
    private System.Windows.Forms.NumericUpDown priceNumericUpDown;
    private System.Windows.Forms.Button saveButton;

    // Left Main Grid
    private System.Windows.Forms.DataGridView settingsGrid;
    private System.Windows.Forms.DataGridViewTextBoxColumn theatreColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn showTypeColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn showTimeColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn priceColumn;
    private System.Windows.Forms.DataGridViewButtonColumn editButtonColumn;
    private System.Windows.Forms.DataGridViewButtonColumn deleteButtonColumn;

    // Right Side Panels Container
    private System.Windows.Forms.Panel rightPanel;

    // Add 3D Charge Group
    private System.Windows.Forms.GroupBox threeDGroup;
    private System.Windows.Forms.Label threeDPriceLabel;
    private System.Windows.Forms.NumericUpDown threeDPriceNumeric;
    private System.Windows.Forms.Button threeDAddButton;

    // Add Show Type Group
    private System.Windows.Forms.GroupBox showTypeGroup;
    private System.Windows.Forms.Label newShowTypeLabel;
    private System.Windows.Forms.TextBox newShowTypeTextBox;
    private System.Windows.Forms.Button addShowTypeButton;
    private System.Windows.Forms.DataGridView showTypeGrid;
    private System.Windows.Forms.DataGridViewTextBoxColumn stNameColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn stIdColumn;
    private System.Windows.Forms.DataGridViewButtonColumn stEditButtonColumn;
    private System.Windows.Forms.DataGridViewButtonColumn stDeleteButtonColumn;

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
        headerPanel = new Panel();
        headerTitleLabel = new Label();
        topInputsPanel = new Panel();
        cinemaLabel = new Label();
        cinemaComboBox = new ComboBox();
        showTypeLabel = new Label();
        showTypeComboBox = new ComboBox();
        showTimeLabel = new Label();
        showTimeTextBox = new TextBox();
        priceLabel = new Label();
        priceNumericUpDown = new NumericUpDown();
        saveButton = new Button();
        settingsGrid = new DataGridView();
        theatreColumn = new DataGridViewTextBoxColumn();
        showTypeColumn = new DataGridViewTextBoxColumn();
        showTimeColumn = new DataGridViewTextBoxColumn();
        priceColumn = new DataGridViewTextBoxColumn();
        editButtonColumn = new DataGridViewButtonColumn();
        deleteButtonColumn = new DataGridViewButtonColumn();
        rightPanel = new Panel();
        threeDGroup = new GroupBox();
        threeDPriceLabel = new Label();
        threeDPriceNumeric = new NumericUpDown();
        threeDAddButton = new Button();
        showTypeGroup = new GroupBox();
        newShowTypeLabel = new Label();
        newShowTypeTextBox = new TextBox();
        addShowTypeButton = new Button();
        showTypeGrid = new DataGridView();
        stNameColumn = new DataGridViewTextBoxColumn();
        stIdColumn = new DataGridViewTextBoxColumn();
        stEditButtonColumn = new DataGridViewButtonColumn();
        stDeleteButtonColumn = new DataGridViewButtonColumn();
        statusStrip = new StatusStrip();
        statusLabel = new ToolStripStatusLabel();
        headerPanel.SuspendLayout();
        topInputsPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)priceNumericUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)settingsGrid).BeginInit();
        rightPanel.SuspendLayout();
        threeDGroup.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)threeDPriceNumeric).BeginInit();
        showTypeGroup.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)showTypeGrid).BeginInit();
        statusStrip.SuspendLayout();
        SuspendLayout();
        // 
        // headerPanel
        // 
        headerPanel.BackColor = Color.FromArgb(0, 168, 223);
        headerPanel.Controls.Add(headerTitleLabel);
        headerPanel.Dock = DockStyle.Top;
        headerPanel.Location = new Point(0, 0);
        headerPanel.Name = "headerPanel";
        headerPanel.Size = new Size(1870, 36);
        headerPanel.TabIndex = 0;
        // 
        // headerTitleLabel
        // 
        headerTitleLabel.Dock = DockStyle.Fill;
        headerTitleLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        headerTitleLabel.ForeColor = Color.White;
        headerTitleLabel.Location = new Point(0, 0);
        headerTitleLabel.Name = "headerTitleLabel";
        headerTitleLabel.Padding = new Padding(10, 0, 0, 0);
        headerTitleLabel.Size = new Size(1870, 36);
        headerTitleLabel.TabIndex = 0;
        headerTitleLabel.Text = "Settings";
        headerTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // topInputsPanel
        // 
        topInputsPanel.BackColor = Color.FromArgb(220, 238, 245);
        topInputsPanel.Controls.Add(cinemaLabel);
        topInputsPanel.Controls.Add(cinemaComboBox);
        topInputsPanel.Controls.Add(showTypeLabel);
        topInputsPanel.Controls.Add(showTypeComboBox);
        topInputsPanel.Controls.Add(showTimeLabel);
        topInputsPanel.Controls.Add(showTimeTextBox);
        topInputsPanel.Controls.Add(priceLabel);
        topInputsPanel.Controls.Add(priceNumericUpDown);
        topInputsPanel.Controls.Add(saveButton);
        topInputsPanel.Dock = DockStyle.Top;
        topInputsPanel.Location = new Point(0, 36);
        topInputsPanel.Name = "topInputsPanel";
        topInputsPanel.Size = new Size(1870, 132);
        topInputsPanel.TabIndex = 1;
        // 
        // cinemaLabel
        // 
        cinemaLabel.AutoSize = true;
        cinemaLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        cinemaLabel.Location = new Point(82, 6);
        cinemaLabel.Name = "cinemaLabel";
        cinemaLabel.Size = new Size(164, 31);
        cinemaLabel.TabIndex = 0;
        cinemaLabel.Text = "Theatre Name";
        // 
        // cinemaComboBox
        // 
        cinemaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        cinemaComboBox.FormattingEnabled = true;
        cinemaComboBox.Location = new Point(82, 58);
        cinemaComboBox.Name = "cinemaComboBox";
        cinemaComboBox.Size = new Size(277, 40);
        cinemaComboBox.TabIndex = 1;
        // 
        // showTypeLabel
        // 
        showTypeLabel.AutoSize = true;
        showTypeLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        showTypeLabel.Location = new Point(401, 6);
        showTypeLabel.Name = "showTypeLabel";
        showTypeLabel.Size = new Size(126, 31);
        showTypeLabel.TabIndex = 2;
        showTypeLabel.Text = "Show type";
        showTypeLabel.Click += showTypeLabel_Click;
        // 
        // showTypeComboBox
        // 
        showTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        showTypeComboBox.FormattingEnabled = true;
        showTypeComboBox.Location = new Point(401, 58);
        showTypeComboBox.Name = "showTypeComboBox";
        showTypeComboBox.Size = new Size(286, 40);
        showTypeComboBox.TabIndex = 3;
        // 
        // showTimeLabel
        // 
        showTimeLabel.AutoSize = true;
        showTimeLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        showTimeLabel.Location = new Point(735, 6);
        showTimeLabel.Name = "showTimeLabel";
        showTimeLabel.Size = new Size(132, 31);
        showTimeLabel.TabIndex = 4;
        showTimeLabel.Text = "Show Time";
        // 
        // showTimeTextBox
        // 
        showTimeTextBox.Location = new Point(735, 58);
        showTimeTextBox.Name = "showTimeTextBox";
        showTimeTextBox.Size = new Size(205, 39);
        showTimeTextBox.TabIndex = 5;
        showTimeTextBox.Text = "11:30 AM";
        // 
        // priceLabel
        // 
        priceLabel.AutoSize = true;
        priceLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        priceLabel.Location = new Point(972, 6);
        priceLabel.Name = "priceLabel";
        priceLabel.Size = new Size(67, 31);
        priceLabel.TabIndex = 6;
        priceLabel.Text = "Price";
        // 
        // priceNumericUpDown
        // 
        priceNumericUpDown.DecimalPlaces = 2;
        priceNumericUpDown.Location = new Point(972, 58);
        priceNumericUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        priceNumericUpDown.Name = "priceNumericUpDown";
        priceNumericUpDown.Size = new Size(140, 39);
        priceNumericUpDown.TabIndex = 7;
        priceNumericUpDown.Value = new decimal(new int[] { 150, 0, 0, 0 });
        // 
        // saveButton
        // 
        saveButton.BackColor = Color.FromArgb(0, 168, 223);
        saveButton.FlatStyle = FlatStyle.Flat;
        saveButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        saveButton.ForeColor = Color.White;
        saveButton.Location = new Point(1196, 42);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(164, 55);
        saveButton.TabIndex = 8;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = false;
        saveButton.Click += saveButton_Click;
        // 
        // settingsGrid
        // 
        settingsGrid.AllowUserToAddRows = false;
        settingsGrid.AllowUserToDeleteRows = false;
        settingsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        settingsGrid.BackgroundColor = Color.White;
        settingsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        settingsGrid.Columns.AddRange(new DataGridViewColumn[] { theatreColumn, showTypeColumn, showTimeColumn, priceColumn, editButtonColumn, deleteButtonColumn });
        settingsGrid.Dock = DockStyle.Fill;
        settingsGrid.Location = new Point(0, 168);
        settingsGrid.MultiSelect = false;
        settingsGrid.Name = "settingsGrid";
        settingsGrid.ReadOnly = true;
        settingsGrid.RowHeadersWidth = 28;
        settingsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        settingsGrid.Size = new Size(1360, 915);
        settingsGrid.TabIndex = 2;
        settingsGrid.CellContentClick += settingsGrid_CellContentClick;
        // 
        // theatreColumn
        // 
        theatreColumn.DataPropertyName = "Theatre";
        theatreColumn.HeaderText = "Theatre";
        theatreColumn.MinimumWidth = 10;
        theatreColumn.Name = "theatreColumn";
        theatreColumn.ReadOnly = true;
        // 
        // showTypeColumn
        // 
        showTypeColumn.DataPropertyName = "ShowType";
        showTypeColumn.HeaderText = "Show Type";
        showTypeColumn.MinimumWidth = 10;
        showTypeColumn.Name = "showTypeColumn";
        showTypeColumn.ReadOnly = true;
        // 
        // showTimeColumn
        // 
        showTimeColumn.DataPropertyName = "ShowTime";
        showTimeColumn.HeaderText = "Show Time";
        showTimeColumn.MinimumWidth = 10;
        showTimeColumn.Name = "showTimeColumn";
        showTimeColumn.ReadOnly = true;
        // 
        // priceColumn
        // 
        priceColumn.DataPropertyName = "Price";
        priceColumn.HeaderText = "Price";
        priceColumn.MinimumWidth = 10;
        priceColumn.Name = "priceColumn";
        priceColumn.ReadOnly = true;
        // 
        // editButtonColumn
        // 
        editButtonColumn.HeaderText = "EDIT";
        editButtonColumn.MinimumWidth = 10;
        editButtonColumn.Name = "editButtonColumn";
        editButtonColumn.ReadOnly = true;
        editButtonColumn.Text = "UPDATE";
        editButtonColumn.UseColumnTextForButtonValue = true;
        // 
        // deleteButtonColumn
        // 
        deleteButtonColumn.HeaderText = "DELETE";
        deleteButtonColumn.MinimumWidth = 10;
        deleteButtonColumn.Name = "deleteButtonColumn";
        deleteButtonColumn.ReadOnly = true;
        deleteButtonColumn.Text = "DELETE";
        deleteButtonColumn.UseColumnTextForButtonValue = true;
        // 
        // rightPanel
        // 
        rightPanel.BackColor = Color.FromArgb(215, 232, 240);
        rightPanel.Controls.Add(showTypeGroup);
        rightPanel.Controls.Add(threeDGroup);
        rightPanel.Dock = DockStyle.Right;
        rightPanel.Location = new Point(1360, 168);
        rightPanel.Name = "rightPanel";
        rightPanel.Padding = new Padding(6);
        rightPanel.Size = new Size(510, 915);
        rightPanel.TabIndex = 3;
        // 
        // threeDGroup
        // 
        threeDGroup.Controls.Add(threeDPriceLabel);
        threeDGroup.Controls.Add(threeDAddButton);
        threeDGroup.Controls.Add(threeDPriceNumeric);
        threeDGroup.Dock = DockStyle.Top;
        threeDGroup.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        threeDGroup.ForeColor = Color.FromArgb(0, 102, 204);
        threeDGroup.Location = new Point(6, 6);
        threeDGroup.Name = "threeDGroup";
        threeDGroup.Size = new Size(498, 107);
        threeDGroup.TabIndex = 0;
        threeDGroup.TabStop = false;
        threeDGroup.Text = "Add 3D Charge";
        // 
        // threeDPriceLabel
        // 
        threeDPriceLabel.AutoSize = true;
        threeDPriceLabel.Font = new Font("Segoe UI", 8.5F);
        threeDPriceLabel.ForeColor = Color.Black;
        threeDPriceLabel.Location = new Point(21, 59);
        threeDPriceLabel.Name = "threeDPriceLabel";
        threeDPriceLabel.Size = new Size(64, 31);
        threeDPriceLabel.TabIndex = 0;
        threeDPriceLabel.Text = "Price";
        // 
        // threeDPriceNumeric
        // 
        threeDPriceNumeric.DecimalPlaces = 2;
        threeDPriceNumeric.Font = new Font("Segoe UI", 8.5F);
        threeDPriceNumeric.Location = new Point(174, 59);
        threeDPriceNumeric.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        threeDPriceNumeric.Name = "threeDPriceNumeric";
        threeDPriceNumeric.Size = new Size(150, 38);
        threeDPriceNumeric.TabIndex = 1;
        threeDPriceNumeric.Value = new decimal(new int[] { 30, 0, 0, 0 });
        // 
        // threeDAddButton
        // 
        threeDAddButton.BackColor = Color.FromArgb(0, 168, 223);
        threeDAddButton.FlatStyle = FlatStyle.Flat;
        threeDAddButton.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        threeDAddButton.ForeColor = Color.White;
        threeDAddButton.Location = new Point(349, 52);
        threeDAddButton.Name = "threeDAddButton";
        threeDAddButton.Size = new Size(113, 49);
        threeDAddButton.TabIndex = 2;
        threeDAddButton.Text = "Add";
        threeDAddButton.UseVisualStyleBackColor = false;
        threeDAddButton.Click += threeDAddButton_Click;
        // 
        // showTypeGroup
        // 
        showTypeGroup.Controls.Add(newShowTypeLabel);
        showTypeGroup.Controls.Add(newShowTypeTextBox);
        showTypeGroup.Controls.Add(addShowTypeButton);
        showTypeGroup.Controls.Add(showTypeGrid);
        showTypeGroup.Dock = DockStyle.Fill;
        showTypeGroup.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        showTypeGroup.ForeColor = Color.FromArgb(0, 102, 204);
        showTypeGroup.Location = new Point(6, 113);
        showTypeGroup.Name = "showTypeGroup";
        showTypeGroup.Padding = new Padding(6);
        showTypeGroup.Size = new Size(498, 796);
        showTypeGroup.TabIndex = 1;
        showTypeGroup.TabStop = false;
        showTypeGroup.Text = "Add Show Type";
        // 
        // newShowTypeLabel
        // 
        newShowTypeLabel.AutoSize = true;
        newShowTypeLabel.Font = new Font("Segoe UI", 8.5F);
        newShowTypeLabel.ForeColor = Color.Black;
        newShowTypeLabel.Location = new Point(9, 37);
        newShowTypeLabel.Name = "newShowTypeLabel";
        newShowTypeLabel.Size = new Size(123, 31);
        newShowTypeLabel.TabIndex = 0;
        newShowTypeLabel.Text = "Show Type";
        // 
        // newShowTypeTextBox
        // 
        newShowTypeTextBox.Font = new Font("Segoe UI", 8.5F);
        newShowTypeTextBox.Location = new Point(9, 82);
        newShowTypeTextBox.Name = "newShowTypeTextBox";
        newShowTypeTextBox.Size = new Size(315, 38);
        newShowTypeTextBox.TabIndex = 1;
        // 
        // addShowTypeButton
        // 
        addShowTypeButton.BackColor = Color.FromArgb(0, 168, 223);
        addShowTypeButton.FlatStyle = FlatStyle.Flat;
        addShowTypeButton.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        addShowTypeButton.ForeColor = Color.White;
        addShowTypeButton.Location = new Point(349, 71);
        addShowTypeButton.Name = "addShowTypeButton";
        addShowTypeButton.Size = new Size(113, 49);
        addShowTypeButton.TabIndex = 2;
        addShowTypeButton.Text = "Add";
        addShowTypeButton.UseVisualStyleBackColor = false;
        addShowTypeButton.Click += addShowTypeButton_Click;
        // 
        // showTypeGrid
        // 
        showTypeGrid.AllowUserToAddRows = false;
        showTypeGrid.AllowUserToDeleteRows = false;
        showTypeGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        showTypeGrid.BackgroundColor = Color.White;
        showTypeGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        showTypeGrid.Columns.AddRange(new DataGridViewColumn[] { stNameColumn, stIdColumn, stEditButtonColumn, stDeleteButtonColumn });
        showTypeGrid.Dock = DockStyle.Bottom;
        showTypeGrid.Font = new Font("Segoe UI", 8.5F);
        showTypeGrid.Location = new Point(6, 149);
        showTypeGrid.MultiSelect = false;
        showTypeGrid.Name = "showTypeGrid";
        showTypeGrid.ReadOnly = true;
        showTypeGrid.RowHeadersWidth = 24;
        showTypeGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        showTypeGrid.Size = new Size(486, 641);
        showTypeGrid.TabIndex = 3;
        showTypeGrid.CellContentClick += showTypeGrid_CellContentClick;
        // 
        // stNameColumn
        // 
        stNameColumn.DataPropertyName = "Name";
        stNameColumn.HeaderText = "Show Type";
        stNameColumn.MinimumWidth = 10;
        stNameColumn.Name = "stNameColumn";
        stNameColumn.ReadOnly = true;
        // 
        // stIdColumn
        // 
        stIdColumn.DataPropertyName = "Id";
        stIdColumn.HeaderText = "ID";
        stIdColumn.MinimumWidth = 10;
        stIdColumn.Name = "stIdColumn";
        stIdColumn.ReadOnly = true;
        // 
        // stEditButtonColumn
        // 
        stEditButtonColumn.HeaderText = "EDIT";
        stEditButtonColumn.MinimumWidth = 10;
        stEditButtonColumn.Name = "stEditButtonColumn";
        stEditButtonColumn.ReadOnly = true;
        stEditButtonColumn.Text = "UPDATE";
        stEditButtonColumn.UseColumnTextForButtonValue = true;
        // 
        // stDeleteButtonColumn
        // 
        stDeleteButtonColumn.HeaderText = "DELETE";
        stDeleteButtonColumn.MinimumWidth = 10;
        stDeleteButtonColumn.Name = "stDeleteButtonColumn";
        stDeleteButtonColumn.ReadOnly = true;
        stDeleteButtonColumn.Text = "DELETE";
        stDeleteButtonColumn.UseColumnTextForButtonValue = true;
        // 
        // statusStrip
        // 
        statusStrip.ImageScalingSize = new Size(32, 32);
        statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
        statusStrip.Location = new Point(0, 1083);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(1870, 42);
        statusStrip.TabIndex = 4;
        // 
        // statusLabel
        // 
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(83, 32);
        statusLabel.Text = "Ready.";
        // 
        // TheatreSettingsForm
        // 
        ClientSize = new Size(1870, 1125);
        Controls.Add(settingsGrid);
        Controls.Add(rightPanel);
        Controls.Add(topInputsPanel);
        Controls.Add(headerPanel);
        Controls.Add(statusStrip);
        Name = "TheatreSettingsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Settings";
        Shown += TheatreSettingsForm_Shown;
        headerPanel.ResumeLayout(false);
        topInputsPanel.ResumeLayout(false);
        topInputsPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)priceNumericUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)settingsGrid).EndInit();
        rightPanel.ResumeLayout(false);
        threeDGroup.ResumeLayout(false);
        threeDGroup.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)threeDPriceNumeric).EndInit();
        showTypeGroup.ResumeLayout(false);
        showTypeGroup.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)showTypeGrid).EndInit();
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
