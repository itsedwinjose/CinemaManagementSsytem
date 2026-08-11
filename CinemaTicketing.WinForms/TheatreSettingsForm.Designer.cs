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
        headerPanel = new System.Windows.Forms.Panel();
        headerTitleLabel = new System.Windows.Forms.Label();

        topInputsPanel = new System.Windows.Forms.Panel();
        cinemaLabel = new System.Windows.Forms.Label();
        cinemaComboBox = new System.Windows.Forms.ComboBox();
        showTypeLabel = new System.Windows.Forms.Label();
        showTypeComboBox = new System.Windows.Forms.ComboBox();
        showTimeLabel = new System.Windows.Forms.Label();
        showTimeTextBox = new System.Windows.Forms.TextBox();
        priceLabel = new System.Windows.Forms.Label();
        priceNumericUpDown = new System.Windows.Forms.NumericUpDown();
        saveButton = new System.Windows.Forms.Button();

        settingsGrid = new System.Windows.Forms.DataGridView();
        theatreColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        showTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        showTimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        priceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        editButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
        deleteButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();

        rightPanel = new System.Windows.Forms.Panel();
        threeDGroup = new System.Windows.Forms.GroupBox();
        threeDPriceLabel = new System.Windows.Forms.Label();
        threeDPriceNumeric = new System.Windows.Forms.NumericUpDown();
        threeDAddButton = new System.Windows.Forms.Button();

        showTypeGroup = new System.Windows.Forms.GroupBox();
        newShowTypeLabel = new System.Windows.Forms.Label();
        newShowTypeTextBox = new System.Windows.Forms.TextBox();
        addShowTypeButton = new System.Windows.Forms.Button();
        showTypeGrid = new System.Windows.Forms.DataGridView();
        stNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        stIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        stEditButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
        stDeleteButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();

        statusStrip = new System.Windows.Forms.StatusStrip();
        statusLabel = new System.Windows.Forms.ToolStripStatusLabel();

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
        headerPanel.BackColor = System.Drawing.Color.FromArgb(0, 168, 223);
        headerPanel.Controls.Add(headerTitleLabel);
        headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
        headerPanel.Location = new System.Drawing.Point(0, 0);
        headerPanel.Name = "headerPanel";
        headerPanel.Size = new System.Drawing.Size(950, 36);
        headerPanel.TabIndex = 0;

        // 
        // headerTitleLabel
        // 
        headerTitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        headerTitleLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        headerTitleLabel.ForeColor = System.Drawing.Color.White;
        headerTitleLabel.Location = new System.Drawing.Point(0, 0);
        headerTitleLabel.Name = "headerTitleLabel";
        headerTitleLabel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
        headerTitleLabel.Size = new System.Drawing.Size(950, 36);
        headerTitleLabel.TabIndex = 0;
        headerTitleLabel.Text = "Settings";
        headerTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        // 
        // topInputsPanel
        // 
        topInputsPanel.BackColor = System.Drawing.Color.FromArgb(220, 238, 245);
        topInputsPanel.Controls.Add(cinemaLabel);
        topInputsPanel.Controls.Add(cinemaComboBox);
        topInputsPanel.Controls.Add(showTypeLabel);
        topInputsPanel.Controls.Add(showTypeComboBox);
        topInputsPanel.Controls.Add(showTimeLabel);
        topInputsPanel.Controls.Add(showTimeTextBox);
        topInputsPanel.Controls.Add(priceLabel);
        topInputsPanel.Controls.Add(priceNumericUpDown);
        topInputsPanel.Controls.Add(saveButton);
        topInputsPanel.Dock = System.Windows.Forms.DockStyle.Top;
        topInputsPanel.Location = new System.Drawing.Point(0, 36);
        topInputsPanel.Name = "topInputsPanel";
        topInputsPanel.Size = new System.Drawing.Size(950, 48);
        topInputsPanel.TabIndex = 1;

        // 
        // cinemaLabel
        // 
        cinemaLabel.AutoSize = true;
        cinemaLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        cinemaLabel.Location = new System.Drawing.Point(10, 6);
        cinemaLabel.Name = "cinemaLabel";
        cinemaLabel.Size = new System.Drawing.Size(84, 15);
        cinemaLabel.TabIndex = 0;
        cinemaLabel.Text = "Theatre Name";

        // 
        // cinemaComboBox
        // 
        cinemaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cinemaComboBox.FormattingEnabled = true;
        cinemaComboBox.Location = new System.Drawing.Point(10, 22);
        cinemaComboBox.Name = "cinemaComboBox";
        cinemaComboBox.Size = new System.Drawing.Size(130, 23);
        cinemaComboBox.TabIndex = 1;

        // 
        // showTypeLabel
        // 
        showTypeLabel.AutoSize = true;
        showTypeLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        showTypeLabel.Location = new System.Drawing.Point(150, 6);
        showTypeLabel.Name = "showTypeLabel";
        showTypeLabel.Size = new System.Drawing.Size(65, 15);
        showTypeLabel.TabIndex = 2;
        showTypeLabel.Text = "Show type";

        // 
        // showTypeComboBox
        // 
        showTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        showTypeComboBox.FormattingEnabled = true;
        showTypeComboBox.Location = new System.Drawing.Point(150, 22);
        showTypeComboBox.Name = "showTypeComboBox";
        showTypeComboBox.Size = new System.Drawing.Size(120, 23);
        showTypeComboBox.TabIndex = 3;

        // 
        // showTimeLabel
        // 
        showTimeLabel.AutoSize = true;
        showTimeLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        showTimeLabel.Location = new System.Drawing.Point(280, 6);
        showTimeLabel.Name = "showTimeLabel";
        showTimeLabel.Size = new System.Drawing.Size(67, 15);
        showTimeLabel.TabIndex = 4;
        showTimeLabel.Text = "Show Time";

        // 
        // showTimeTextBox
        // 
        showTimeTextBox.Location = new System.Drawing.Point(280, 22);
        showTimeTextBox.Name = "showTimeTextBox";
        showTimeTextBox.Size = new System.Drawing.Size(85, 23);
        showTimeTextBox.TabIndex = 5;
        showTimeTextBox.Text = "11:30 AM";

        // 
        // priceLabel
        // 
        priceLabel.AutoSize = true;
        priceLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        priceLabel.Location = new System.Drawing.Point(375, 6);
        priceLabel.Name = "priceLabel";
        priceLabel.Size = new System.Drawing.Size(35, 15);
        priceLabel.TabIndex = 6;
        priceLabel.Text = "Price";

        // 
        // priceNumericUpDown
        // 
        priceNumericUpDown.DecimalPlaces = 2;
        priceNumericUpDown.Location = new System.Drawing.Point(375, 22);
        priceNumericUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        priceNumericUpDown.Name = "priceNumericUpDown";
        priceNumericUpDown.Size = new System.Drawing.Size(80, 23);
        priceNumericUpDown.TabIndex = 7;
        priceNumericUpDown.Value = new decimal(new int[] { 150, 0, 0, 0 });

        // 
        // saveButton
        // 
        saveButton.BackColor = System.Drawing.Color.FromArgb(0, 168, 223);
        saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        saveButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        saveButton.ForeColor = System.Drawing.Color.White;
        saveButton.Location = new System.Drawing.Point(465, 21);
        saveButton.Name = "saveButton";
        saveButton.Size = new System.Drawing.Size(75, 25);
        saveButton.TabIndex = 8;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = false;
        saveButton.Click += saveButton_Click;

        // 
        // settingsGrid
        // 
        settingsGrid.AllowUserToAddRows = false;
        settingsGrid.AllowUserToDeleteRows = false;
        settingsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        settingsGrid.BackgroundColor = System.Drawing.Color.White;
        settingsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        settingsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { theatreColumn, showTypeColumn, showTimeColumn, priceColumn, editButtonColumn, deleteButtonColumn });
        settingsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        settingsGrid.Location = new System.Drawing.Point(0, 84);
        settingsGrid.MultiSelect = false;
        settingsGrid.Name = "settingsGrid";
        settingsGrid.ReadOnly = true;
        settingsGrid.RowHeadersWidth = 28;
        settingsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        settingsGrid.Size = new System.Drawing.Size(640, 480);
        settingsGrid.TabIndex = 2;
        settingsGrid.CellContentClick += settingsGrid_CellContentClick;

        // 
        // theatreColumn
        // 
        theatreColumn.DataPropertyName = "Theatre";
        theatreColumn.HeaderText = "Theatre";
        theatreColumn.Name = "theatreColumn";
        theatreColumn.ReadOnly = true;

        // 
        // showTypeColumn
        // 
        showTypeColumn.DataPropertyName = "ShowType";
        showTypeColumn.HeaderText = "Show Type";
        showTypeColumn.Name = "showTypeColumn";
        showTypeColumn.ReadOnly = true;

        // 
        // showTimeColumn
        // 
        showTimeColumn.DataPropertyName = "ShowTime";
        showTimeColumn.HeaderText = "Show Time";
        showTimeColumn.Name = "showTimeColumn";
        showTimeColumn.ReadOnly = true;

        // 
        // priceColumn
        // 
        priceColumn.DataPropertyName = "Price";
        priceColumn.HeaderText = "Price";
        priceColumn.Name = "priceColumn";
        priceColumn.ReadOnly = true;

        // 
        // editButtonColumn
        // 
        editButtonColumn.HeaderText = "EDIT";
        editButtonColumn.Name = "editButtonColumn";
        editButtonColumn.ReadOnly = true;
        editButtonColumn.Text = "UPDATE";
        editButtonColumn.UseColumnTextForButtonValue = true;

        // 
        // deleteButtonColumn
        // 
        deleteButtonColumn.HeaderText = "DELETE";
        deleteButtonColumn.Name = "deleteButtonColumn";
        deleteButtonColumn.ReadOnly = true;
        deleteButtonColumn.Text = "DELETE";
        deleteButtonColumn.UseColumnTextForButtonValue = true;

        // 
        // rightPanel
        // 
        rightPanel.BackColor = System.Drawing.Color.FromArgb(215, 232, 240);
        rightPanel.Controls.Add(threeDGroup);
        rightPanel.Controls.Add(showTypeGroup);
        rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
        rightPanel.Location = new System.Drawing.Point(640, 84);
        rightPanel.Name = "rightPanel";
        rightPanel.Padding = new System.Windows.Forms.Padding(6);
        rightPanel.Size = new System.Drawing.Size(310, 480);
        rightPanel.TabIndex = 3;

        // 
        // threeDGroup
        // 
        threeDGroup.Controls.Add(threeDPriceLabel);
        threeDGroup.Controls.Add(threeDPriceNumeric);
        threeDGroup.Controls.Add(threeDAddButton);
        threeDGroup.Dock = System.Windows.Forms.DockStyle.Top;
        threeDGroup.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        threeDGroup.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
        threeDGroup.Location = new System.Drawing.Point(6, 6);
        threeDGroup.Name = "threeDGroup";
        threeDGroup.Size = new System.Drawing.Size(298, 75);
        threeDGroup.TabIndex = 0;
        threeDGroup.TabStop = false;
        threeDGroup.Text = "Add 3D Charge";

        // 
        // threeDPriceLabel
        // 
        threeDPriceLabel.AutoSize = true;
        threeDPriceLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        threeDPriceLabel.ForeColor = System.Drawing.Color.Black;
        threeDPriceLabel.Location = new System.Drawing.Point(10, 32);
        threeDPriceLabel.Name = "threeDPriceLabel";
        threeDPriceLabel.Size = new System.Drawing.Size(33, 15);
        threeDPriceLabel.TabIndex = 0;
        threeDPriceLabel.Text = "Price";

        // 
        // threeDPriceNumeric
        // 
        threeDPriceNumeric.DecimalPlaces = 2;
        threeDPriceNumeric.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        threeDPriceNumeric.Location = new System.Drawing.Point(50, 30);
        threeDPriceNumeric.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        threeDPriceNumeric.Name = "threeDPriceNumeric";
        threeDPriceNumeric.Size = new System.Drawing.Size(150, 23);
        threeDPriceNumeric.TabIndex = 1;
        threeDPriceNumeric.Value = new decimal(new int[] { 30, 0, 0, 0 });

        // 
        // threeDAddButton
        // 
        threeDAddButton.BackColor = System.Drawing.Color.FromArgb(0, 168, 223);
        threeDAddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        threeDAddButton.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        threeDAddButton.ForeColor = System.Drawing.Color.White;
        threeDAddButton.Location = new System.Drawing.Point(215, 29);
        threeDAddButton.Name = "threeDAddButton";
        threeDAddButton.Size = new System.Drawing.Size(75, 25);
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
        showTypeGroup.Dock = System.Windows.Forms.DockStyle.Fill;
        showTypeGroup.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        showTypeGroup.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
        showTypeGroup.Location = new System.Drawing.Point(6, 81);
        showTypeGroup.Name = "showTypeGroup";
        showTypeGroup.Padding = new System.Windows.Forms.Padding(6);
        showTypeGroup.Size = new System.Drawing.Size(298, 393);
        showTypeGroup.TabIndex = 1;
        showTypeGroup.TabStop = false;
        showTypeGroup.Text = "Add Show Type";

        // 
        // newShowTypeLabel
        // 
        newShowTypeLabel.AutoSize = true;
        newShowTypeLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        newShowTypeLabel.ForeColor = System.Drawing.Color.Black;
        newShowTypeLabel.Location = new System.Drawing.Point(8, 22);
        newShowTypeLabel.Name = "newShowTypeLabel";
        newShowTypeLabel.Size = new System.Drawing.Size(65, 15);
        newShowTypeLabel.TabIndex = 0;
        newShowTypeLabel.Text = "Show Type";

        // 
        // newShowTypeTextBox
        // 
        newShowTypeTextBox.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        newShowTypeTextBox.Location = new System.Drawing.Point(8, 40);
        newShowTypeTextBox.Name = "newShowTypeTextBox";
        newShowTypeTextBox.Size = new System.Drawing.Size(190, 23);
        newShowTypeTextBox.TabIndex = 1;

        // 
        // addShowTypeButton
        // 
        addShowTypeButton.BackColor = System.Drawing.Color.FromArgb(0, 168, 223);
        addShowTypeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        addShowTypeButton.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        addShowTypeButton.ForeColor = System.Drawing.Color.White;
        addShowTypeButton.Location = new System.Drawing.Point(205, 39);
        addShowTypeButton.Name = "addShowTypeButton";
        addShowTypeButton.Size = new System.Drawing.Size(85, 25);
        addShowTypeButton.TabIndex = 2;
        addShowTypeButton.Text = "Add";
        addShowTypeButton.UseVisualStyleBackColor = false;
        addShowTypeButton.Click += addShowTypeButton_Click;

        // 
        // showTypeGrid
        // 
        showTypeGrid.AllowUserToAddRows = false;
        showTypeGrid.AllowUserToDeleteRows = false;
        showTypeGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        showTypeGrid.BackgroundColor = System.Drawing.Color.White;
        showTypeGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        showTypeGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { stNameColumn, stIdColumn, stEditButtonColumn, stDeleteButtonColumn });
        showTypeGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
        showTypeGrid.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        showTypeGrid.Location = new System.Drawing.Point(6, 75);
        showTypeGrid.MultiSelect = false;
        showTypeGrid.Name = "showTypeGrid";
        showTypeGrid.ReadOnly = true;
        showTypeGrid.RowHeadersWidth = 24;
        showTypeGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        showTypeGrid.Size = new System.Drawing.Size(286, 312);
        showTypeGrid.TabIndex = 3;
        showTypeGrid.CellContentClick += showTypeGrid_CellContentClick;

        // 
        // stNameColumn
        // 
        stNameColumn.DataPropertyName = "Name";
        stNameColumn.HeaderText = "Show Type";
        stNameColumn.Name = "stNameColumn";
        stNameColumn.ReadOnly = true;

        // 
        // stIdColumn
        // 
        stIdColumn.DataPropertyName = "Id";
        stIdColumn.HeaderText = "ID";
        stIdColumn.Name = "stIdColumn";
        stIdColumn.ReadOnly = true;

        // 
        // stEditButtonColumn
        // 
        stEditButtonColumn.HeaderText = "EDIT";
        stEditButtonColumn.Name = "stEditButtonColumn";
        stEditButtonColumn.ReadOnly = true;
        stEditButtonColumn.Text = "UPDATE";
        stEditButtonColumn.UseColumnTextForButtonValue = true;

        // 
        // stDeleteButtonColumn
        // 
        stDeleteButtonColumn.HeaderText = "DELETE";
        stDeleteButtonColumn.Name = "stDeleteButtonColumn";
        stDeleteButtonColumn.ReadOnly = true;
        stDeleteButtonColumn.Text = "DELETE";
        stDeleteButtonColumn.UseColumnTextForButtonValue = true;

        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { statusLabel });
        statusStrip.Location = new System.Drawing.Point(0, 564);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new System.Drawing.Size(950, 22);
        statusStrip.TabIndex = 4;

        // 
        // statusLabel
        // 
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new System.Drawing.Size(39, 17);
        statusLabel.Text = "Ready.";

        // 
        // TheatreSettingsForm
        // 
        ClientSize = new System.Drawing.Size(950, 586);
        Controls.Add(settingsGrid);
        Controls.Add(rightPanel);
        Controls.Add(topInputsPanel);
        Controls.Add(headerPanel);
        Controls.Add(statusStrip);
        Name = "TheatreSettingsForm";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
