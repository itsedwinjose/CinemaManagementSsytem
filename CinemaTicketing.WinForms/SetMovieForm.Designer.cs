namespace CinemaTicketing.WinForms;

partial class SetMovieForm
{
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Panel headerPanel;
    private System.Windows.Forms.Label headerTitleLabel;

    // Top Controls Panel
    private System.Windows.Forms.Panel topInputsPanel;
    private System.Windows.Forms.Label dateLabel;
    private System.Windows.Forms.DateTimePicker datePicker;
    private System.Windows.Forms.Label theatreLabel;
    private System.Windows.Forms.ComboBox theatreComboBox;
    private System.Windows.Forms.Label showTypeLabel;
    private System.Windows.Forms.ComboBox showTypeComboBox;
    private System.Windows.Forms.Label movieLabel;
    private System.Windows.Forms.ComboBox movieComboBox;
    private System.Windows.Forms.Button saveButton;
    private System.Windows.Forms.Button showAllButton;
    private System.Windows.Forms.Button setSameMovieButton;

    // Main Center Grid
    private System.Windows.Forms.Panel centerPanel;
    private System.Windows.Forms.Label dateHeaderLabel;
    private System.Windows.Forms.DataGridView mainGrid;

    // Right Side Movie Master Panel
    private System.Windows.Forms.Panel rightPanel;
    private System.Windows.Forms.GroupBox masterGroup;
    private System.Windows.Forms.Label mNameLabel;
    private System.Windows.Forms.TextBox mNameTextBox;
    private System.Windows.Forms.CheckBox m3dCheckBox;
    private System.Windows.Forms.Button mAddButton;
    private System.Windows.Forms.DataGridView masterGrid;
    private System.Windows.Forms.DataGridViewTextBoxColumn mgMovieColumn;
    private System.Windows.Forms.DataGridViewCheckBoxColumn mg3dColumn;
    private System.Windows.Forms.DataGridViewButtonColumn mgDeleteColumn;

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
        dateLabel = new System.Windows.Forms.Label();
        datePicker = new System.Windows.Forms.DateTimePicker();
        theatreLabel = new System.Windows.Forms.Label();
        theatreComboBox = new System.Windows.Forms.ComboBox();
        showTypeLabel = new System.Windows.Forms.Label();
        showTypeComboBox = new System.Windows.Forms.ComboBox();
        movieLabel = new System.Windows.Forms.Label();
        movieComboBox = new System.Windows.Forms.ComboBox();
        saveButton = new System.Windows.Forms.Button();
        showAllButton = new System.Windows.Forms.Button();
        setSameMovieButton = new System.Windows.Forms.Button();

        centerPanel = new System.Windows.Forms.Panel();
        dateHeaderLabel = new System.Windows.Forms.Label();
        mainGrid = new System.Windows.Forms.DataGridView();

        rightPanel = new System.Windows.Forms.Panel();
        masterGroup = new System.Windows.Forms.GroupBox();
        mNameLabel = new System.Windows.Forms.Label();
        mNameTextBox = new System.Windows.Forms.TextBox();
        m3dCheckBox = new System.Windows.Forms.CheckBox();
        mAddButton = new System.Windows.Forms.Button();
        masterGrid = new System.Windows.Forms.DataGridView();
        mgMovieColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        mg3dColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
        mgDeleteColumn = new System.Windows.Forms.DataGridViewButtonColumn();

        statusStrip = new System.Windows.Forms.StatusStrip();
        statusLabel = new System.Windows.Forms.ToolStripStatusLabel();

        headerPanel.SuspendLayout();
        topInputsPanel.SuspendLayout();
        centerPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)mainGrid).BeginInit();
        rightPanel.SuspendLayout();
        masterGroup.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)masterGrid).BeginInit();
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
        headerPanel.Size = new System.Drawing.Size(984, 36);
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
        headerTitleLabel.Size = new System.Drawing.Size(984, 36);
        headerTitleLabel.TabIndex = 0;
        headerTitleLabel.Text = "Set Movie";
        headerTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        // 
        // topInputsPanel
        // 
        topInputsPanel.BackColor = System.Drawing.Color.FromArgb(220, 238, 245);
        topInputsPanel.Controls.Add(dateLabel);
        topInputsPanel.Controls.Add(datePicker);
        topInputsPanel.Controls.Add(theatreLabel);
        topInputsPanel.Controls.Add(theatreComboBox);
        topInputsPanel.Controls.Add(showTypeLabel);
        topInputsPanel.Controls.Add(showTypeComboBox);
        topInputsPanel.Controls.Add(movieLabel);
        topInputsPanel.Controls.Add(movieComboBox);
        topInputsPanel.Controls.Add(saveButton);
        topInputsPanel.Controls.Add(showAllButton);
        topInputsPanel.Controls.Add(setSameMovieButton);
        topInputsPanel.Dock = System.Windows.Forms.DockStyle.Top;
        topInputsPanel.Location = new System.Drawing.Point(0, 36);
        topInputsPanel.Name = "topInputsPanel";
        topInputsPanel.Size = new System.Drawing.Size(984, 72);
        topInputsPanel.TabIndex = 1;

        // 
        // dateLabel
        // 
        dateLabel.AutoSize = true;
        dateLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        dateLabel.Location = new System.Drawing.Point(10, 6);
        dateLabel.Name = "dateLabel";
        dateLabel.Size = new System.Drawing.Size(69, 15);
        dateLabel.TabIndex = 0;
        dateLabel.Text = "Select Date";

        // 
        // datePicker
        // 
        datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        datePicker.Location = new System.Drawing.Point(10, 22);
        datePicker.Name = "datePicker";
        datePicker.Size = new System.Drawing.Size(95, 23);
        datePicker.TabIndex = 1;
        datePicker.ValueChanged += Filter_Changed;

        // 
        // theatreLabel
        // 
        theatreLabel.AutoSize = true;
        theatreLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        theatreLabel.Location = new System.Drawing.Point(115, 6);
        theatreLabel.Name = "theatreLabel";
        theatreLabel.Size = new System.Drawing.Size(84, 15);
        theatreLabel.TabIndex = 2;
        theatreLabel.Text = "Theatre Name";

        // 
        // theatreComboBox
        // 
        theatreComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        theatreComboBox.FormattingEnabled = true;
        theatreComboBox.Location = new System.Drawing.Point(115, 22);
        theatreComboBox.Name = "theatreComboBox";
        theatreComboBox.Size = new System.Drawing.Size(120, 23);
        theatreComboBox.TabIndex = 3;
        theatreComboBox.SelectedIndexChanged += Filter_Changed;

        // 
        // showTypeLabel
        // 
        showTypeLabel.AutoSize = true;
        showTypeLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        showTypeLabel.Location = new System.Drawing.Point(245, 6);
        showTypeLabel.Name = "showTypeLabel";
        showTypeLabel.Size = new System.Drawing.Size(65, 15);
        showTypeLabel.TabIndex = 4;
        showTypeLabel.Text = "Show Type";

        // 
        // showTypeComboBox
        // 
        showTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        showTypeComboBox.FormattingEnabled = true;
        showTypeComboBox.Location = new System.Drawing.Point(245, 22);
        showTypeComboBox.Name = "showTypeComboBox";
        showTypeComboBox.Size = new System.Drawing.Size(120, 23);
        showTypeComboBox.TabIndex = 5;
        showTypeComboBox.SelectedIndexChanged += Filter_Changed;

        // 
        // movieLabel
        // 
        movieLabel.AutoSize = true;
        movieLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        movieLabel.Location = new System.Drawing.Point(375, 6);
        movieLabel.Name = "movieLabel";
        movieLabel.Size = new System.Drawing.Size(74, 15);
        movieLabel.TabIndex = 6;
        movieLabel.Text = "Movie Name";

        // 
        // movieComboBox
        // 
        movieComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        movieComboBox.FormattingEnabled = true;
        movieComboBox.Location = new System.Drawing.Point(375, 22);
        movieComboBox.Name = "movieComboBox";
        movieComboBox.Size = new System.Drawing.Size(160, 23);
        movieComboBox.TabIndex = 7;

        // 
        // saveButton
        // 
        saveButton.BackColor = System.Drawing.Color.FromArgb(0, 168, 223);
        saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        saveButton.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        saveButton.ForeColor = System.Drawing.Color.White;
        saveButton.Location = new System.Drawing.Point(545, 21);
        saveButton.Name = "saveButton";
        saveButton.Size = new System.Drawing.Size(65, 25);
        saveButton.TabIndex = 8;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = false;

        // 
        // showAllButton
        // 
        showAllButton.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
        showAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        showAllButton.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        showAllButton.ForeColor = System.Drawing.Color.White;
        showAllButton.Location = new System.Drawing.Point(245, 47);
        showAllButton.Name = "showAllButton";
        showAllButton.Size = new System.Drawing.Size(80, 23);
        showAllButton.TabIndex = 9;
        showAllButton.Text = "Show All";
        showAllButton.UseVisualStyleBackColor = false;
        showAllButton.Click += showAllButton_Click;

        // 
        // setSameMovieButton
        // 
        setSameMovieButton.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
        setSameMovieButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        setSameMovieButton.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        setSameMovieButton.ForeColor = System.Drawing.Color.White;
        setSameMovieButton.Location = new System.Drawing.Point(330, 47);
        setSameMovieButton.Name = "setSameMovieButton";
        setSameMovieButton.Size = new System.Drawing.Size(125, 23);
        setSameMovieButton.TabIndex = 10;
        setSameMovieButton.Text = "Set Same Movie";
        setSameMovieButton.UseVisualStyleBackColor = false;
        setSameMovieButton.Click += setSameMovieButton_Click;

        // 
        // centerPanel
        // 
        centerPanel.Controls.Add(mainGrid);
        centerPanel.Controls.Add(dateHeaderLabel);
        centerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        centerPanel.Location = new System.Drawing.Point(0, 108);
        centerPanel.Name = "centerPanel";
        centerPanel.Size = new System.Drawing.Size(674, 444);
        centerPanel.TabIndex = 2;

        // 
        // dateHeaderLabel
        // 
        dateHeaderLabel.BackColor = System.Drawing.Color.FromArgb(235, 240, 245);
        dateHeaderLabel.Dock = System.Windows.Forms.DockStyle.Top;
        dateHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        dateHeaderLabel.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
        dateHeaderLabel.Location = new System.Drawing.Point(0, 0);
        dateHeaderLabel.Name = "dateHeaderLabel";
        dateHeaderLabel.Size = new System.Drawing.Size(674, 25);
        dateHeaderLabel.TabIndex = 0;
        dateHeaderLabel.Text = "08-08-2026";
        dateHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        // 
        // mainGrid
        // 
        mainGrid.AllowUserToAddRows = false;
        mainGrid.AllowUserToDeleteRows = false;
        mainGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        mainGrid.BackgroundColor = System.Drawing.Color.White;
        mainGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        mainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        mainGrid.Location = new System.Drawing.Point(0, 25);
        mainGrid.Name = "mainGrid";
        mainGrid.RowHeadersWidth = 26;
        mainGrid.Size = new System.Drawing.Size(674, 419);
        mainGrid.TabIndex = 1;

        // 
        // rightPanel
        // 
        rightPanel.BackColor = System.Drawing.Color.FromArgb(215, 232, 240);
        rightPanel.Controls.Add(masterGroup);
        rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
        rightPanel.Location = new System.Drawing.Point(674, 108);
        rightPanel.Name = "rightPanel";
        rightPanel.Padding = new System.Windows.Forms.Padding(6);
        rightPanel.Size = new System.Drawing.Size(310, 444);
        rightPanel.TabIndex = 3;

        // 
        // masterGroup
        // 
        masterGroup.Controls.Add(mNameLabel);
        masterGroup.Controls.Add(mNameTextBox);
        masterGroup.Controls.Add(m3dCheckBox);
        masterGroup.Controls.Add(mAddButton);
        masterGroup.Controls.Add(masterGrid);
        masterGroup.Dock = System.Windows.Forms.DockStyle.Fill;
        masterGroup.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        masterGroup.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
        masterGroup.Location = new System.Drawing.Point(6, 6);
        masterGroup.Name = "masterGroup";
        masterGroup.Padding = new System.Windows.Forms.Padding(6);
        masterGroup.Size = new System.Drawing.Size(298, 432);
        masterGroup.TabIndex = 0;
        masterGroup.TabStop = false;
        masterGroup.Text = "Add New Movie";

        // 
        // mNameLabel
        // 
        mNameLabel.AutoSize = true;
        mNameLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        mNameLabel.ForeColor = System.Drawing.Color.Black;
        mNameLabel.Location = new System.Drawing.Point(8, 20);
        mNameLabel.Name = "mNameLabel";
        mNameLabel.Size = new System.Drawing.Size(75, 15);
        mNameLabel.TabIndex = 0;
        mNameLabel.Text = "Movie Name";

        // 
        // mNameTextBox
        // 
        mNameTextBox.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        mNameTextBox.Location = new System.Drawing.Point(8, 38);
        mNameTextBox.Name = "mNameTextBox";
        mNameTextBox.Size = new System.Drawing.Size(280, 23);
        mNameTextBox.TabIndex = 1;

        // 
        // m3dCheckBox
        // 
        m3dCheckBox.AutoSize = true;
        m3dCheckBox.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        m3dCheckBox.ForeColor = System.Drawing.Color.Black;
        m3dCheckBox.Location = new System.Drawing.Point(8, 68);
        m3dCheckBox.Name = "m3dCheckBox";
        m3dCheckBox.Size = new System.Drawing.Size(41, 19);
        m3dCheckBox.TabIndex = 2;
        m3dCheckBox.Text = "3 D";
        m3dCheckBox.UseVisualStyleBackColor = true;

        // 
        // mAddButton
        // 
        mAddButton.BackColor = System.Drawing.Color.FromArgb(0, 168, 223);
        mAddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        mAddButton.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        mAddButton.ForeColor = System.Drawing.Color.White;
        mAddButton.Location = new System.Drawing.Point(205, 65);
        mAddButton.Name = "mAddButton";
        mAddButton.Size = new System.Drawing.Size(83, 25);
        mAddButton.TabIndex = 3;
        mAddButton.Text = "Add";
        mAddButton.UseVisualStyleBackColor = false;
        mAddButton.Click += mAddButton_Click;

        // 
        // masterGrid
        // 
        masterGrid.AllowUserToAddRows = false;
        masterGrid.AllowUserToDeleteRows = false;
        masterGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        masterGrid.BackgroundColor = System.Drawing.Color.White;
        masterGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        masterGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { mgMovieColumn, mg3dColumn, mgDeleteColumn });
        masterGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
        masterGrid.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        masterGrid.Location = new System.Drawing.Point(6, 95);
        masterGrid.Name = "masterGrid";
        masterGrid.ReadOnly = true;
        masterGrid.RowHeadersWidth = 24;
        masterGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        masterGrid.Size = new System.Drawing.Size(286, 331);
        masterGrid.TabIndex = 4;
        masterGrid.CellContentClick += masterGrid_CellContentClick;
        masterGrid.CellDoubleClick += masterGrid_CellDoubleClick;

        // 
        // mgMovieColumn
        // 
        mgMovieColumn.DataPropertyName = "Movie";
        mgMovieColumn.HeaderText = "Movie Name";
        mgMovieColumn.Name = "mgMovieColumn";
        mgMovieColumn.ReadOnly = true;

        // 
        // mg3dColumn
        // 
        mg3dColumn.DataPropertyName = "Is3D";
        mg3dColumn.HeaderText = "3 D";
        mg3dColumn.Name = "mg3dColumn";
        mg3dColumn.ReadOnly = true;

        // 
        // mgDeleteColumn
        // 
        mgDeleteColumn.HeaderText = "DELETE";
        mgDeleteColumn.Name = "mgDeleteColumn";
        mgDeleteColumn.ReadOnly = true;
        mgDeleteColumn.Text = "DELETE";
        mgDeleteColumn.UseColumnTextForButtonValue = true;

        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { statusLabel });
        statusStrip.Location = new System.Drawing.Point(0, 552);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new System.Drawing.Size(984, 22);
        statusStrip.TabIndex = 4;

        // 
        // statusLabel
        // 
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new System.Drawing.Size(39, 17);
        statusLabel.Text = "Ready.";

        // 
        // SetMovieForm
        // 
        ClientSize = new System.Drawing.Size(984, 574);
        Controls.Add(centerPanel);
        Controls.Add(rightPanel);
        Controls.Add(topInputsPanel);
        Controls.Add(headerPanel);
        Controls.Add(statusStrip);
        Name = "SetMovieForm";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "Set Movie";
        Shown += SetMovieForm_Shown;
        headerPanel.ResumeLayout(false);
        topInputsPanel.ResumeLayout(false);
        topInputsPanel.PerformLayout();
        centerPanel.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)mainGrid).EndInit();
        rightPanel.ResumeLayout(false);
        masterGroup.ResumeLayout(false);
        masterGroup.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)masterGrid).EndInit();
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
