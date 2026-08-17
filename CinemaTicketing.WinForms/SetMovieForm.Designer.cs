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
        headerPanel = new Panel();
        headerTitleLabel = new Label();
        topInputsPanel = new Panel();
        dateLabel = new Label();
        datePicker = new DateTimePicker();
        theatreLabel = new Label();
        theatreComboBox = new ComboBox();
        showTypeLabel = new Label();
        showTypeComboBox = new ComboBox();
        movieLabel = new Label();
        movieComboBox = new ComboBox();
        saveButton = new Button();
        showAllButton = new Button();
        setSameMovieButton = new Button();
        centerPanel = new Panel();
        mainGrid = new DataGridView();
        dateHeaderLabel = new Label();
        rightPanel = new Panel();
        masterGroup = new GroupBox();
        mNameLabel = new Label();
        mNameTextBox = new TextBox();
        m3dCheckBox = new CheckBox();
        mAddButton = new Button();
        masterGrid = new DataGridView();
        mgMovieColumn = new DataGridViewTextBoxColumn();
        mg3dColumn = new DataGridViewCheckBoxColumn();
        mgDeleteColumn = new DataGridViewButtonColumn();
        statusStrip = new StatusStrip();
        statusLabel = new ToolStripStatusLabel();
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
        headerPanel.BackColor = Color.FromArgb(0, 168, 223);
        headerPanel.Controls.Add(headerTitleLabel);
        headerPanel.Dock = DockStyle.Top;
        headerPanel.Location = new Point(0, 0);
        headerPanel.Name = "headerPanel";
        headerPanel.Size = new Size(1935, 36);
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
        headerTitleLabel.Size = new Size(1935, 36);
        headerTitleLabel.TabIndex = 0;
        headerTitleLabel.Text = "Set Movie";
        headerTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // topInputsPanel
        // 
        topInputsPanel.BackColor = Color.FromArgb(220, 238, 245);
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
        topInputsPanel.Dock = DockStyle.Top;
        topInputsPanel.Location = new Point(0, 36);
        topInputsPanel.Name = "topInputsPanel";
        topInputsPanel.Size = new Size(1935, 154);
        topInputsPanel.TabIndex = 1;
        // 
        // dateLabel
        // 
        dateLabel.AutoSize = true;
        dateLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        dateLabel.Location = new Point(10, 6);
        dateLabel.Name = "dateLabel";
        dateLabel.Size = new Size(134, 31);
        dateLabel.TabIndex = 0;
        dateLabel.Text = "Select Date";
        // 
        // datePicker
        // 
        datePicker.Format = DateTimePickerFormat.Short;
        datePicker.Location = new Point(12, 47);
        datePicker.Name = "datePicker";
        datePicker.Size = new Size(257, 39);
        datePicker.TabIndex = 1;
        datePicker.ValueChanged += Filter_Changed;
        // 
        // theatreLabel
        // 
        theatreLabel.AutoSize = true;
        theatreLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        theatreLabel.Location = new Point(305, 6);
        theatreLabel.Name = "theatreLabel";
        theatreLabel.Size = new Size(164, 31);
        theatreLabel.TabIndex = 2;
        theatreLabel.Text = "Theatre Name";
        // 
        // theatreComboBox
        // 
        theatreComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        theatreComboBox.FormattingEnabled = true;
        theatreComboBox.Location = new Point(305, 47);
        theatreComboBox.Name = "theatreComboBox";
        theatreComboBox.Size = new Size(304, 40);
        theatreComboBox.TabIndex = 3;
        theatreComboBox.SelectedIndexChanged += Filter_Changed;
        // 
        // showTypeLabel
        // 
        showTypeLabel.AutoSize = true;
        showTypeLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        showTypeLabel.Location = new Point(658, 6);
        showTypeLabel.Name = "showTypeLabel";
        showTypeLabel.Size = new Size(129, 31);
        showTypeLabel.TabIndex = 4;
        showTypeLabel.Text = "Show Type";
        // 
        // showTypeComboBox
        // 
        showTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        showTypeComboBox.FormattingEnabled = true;
        showTypeComboBox.Location = new Point(658, 47);
        showTypeComboBox.Name = "showTypeComboBox";
        showTypeComboBox.Size = new Size(289, 40);
        showTypeComboBox.TabIndex = 5;
        showTypeComboBox.SelectedIndexChanged += Filter_Changed;
        // 
        // movieLabel
        // 
        movieLabel.AutoSize = true;
        movieLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        movieLabel.Location = new Point(992, 6);
        movieLabel.Name = "movieLabel";
        movieLabel.Size = new Size(150, 31);
        movieLabel.TabIndex = 6;
        movieLabel.Text = "Movie Name";
        // 
        // movieComboBox
        // 
        movieComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        movieComboBox.FormattingEnabled = true;
        movieComboBox.Location = new Point(992, 47);
        movieComboBox.Name = "movieComboBox";
        movieComboBox.Size = new Size(333, 40);
        movieComboBox.TabIndex = 7;
        // 
        // saveButton
        // 
        saveButton.BackColor = Color.FromArgb(0, 168, 223);
        saveButton.FlatStyle = FlatStyle.Flat;
        saveButton.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        saveButton.ForeColor = Color.White;
        saveButton.Location = new Point(1358, 34);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(111, 49);
        saveButton.TabIndex = 8;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = false;
        // 
        // showAllButton
        // 
        showAllButton.BackColor = Color.FromArgb(40, 167, 69);
        showAllButton.FlatStyle = FlatStyle.Flat;
        showAllButton.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        showAllButton.ForeColor = Color.White;
        showAllButton.Location = new Point(442, 101);
        showAllButton.Name = "showAllButton";
        showAllButton.Size = new Size(209, 47);
        showAllButton.TabIndex = 9;
        showAllButton.Text = "Show All";
        showAllButton.UseVisualStyleBackColor = false;
        showAllButton.Click += showAllButton_Click;
        // 
        // setSameMovieButton
        // 
        setSameMovieButton.BackColor = Color.FromArgb(40, 167, 69);
        setSameMovieButton.FlatStyle = FlatStyle.Flat;
        setSameMovieButton.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        setSameMovieButton.ForeColor = Color.White;
        setSameMovieButton.Location = new Point(685, 101);
        setSameMovieButton.Name = "setSameMovieButton";
        setSameMovieButton.Size = new Size(262, 50);
        setSameMovieButton.TabIndex = 10;
        setSameMovieButton.Text = "Set Same Movie";
        setSameMovieButton.UseVisualStyleBackColor = false;
        setSameMovieButton.Click += setSameMovieButton_Click;
        // 
        // centerPanel
        // 
        centerPanel.Controls.Add(mainGrid);
        centerPanel.Controls.Add(dateHeaderLabel);
        centerPanel.Dock = DockStyle.Fill;
        centerPanel.Location = new Point(0, 190);
        centerPanel.Name = "centerPanel";
        centerPanel.Size = new Size(1469, 889);
        centerPanel.TabIndex = 2;
        // 
        // mainGrid
        // 
        mainGrid.AllowUserToAddRows = false;
        mainGrid.AllowUserToDeleteRows = false;
        mainGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        mainGrid.BackgroundColor = Color.White;
        mainGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        mainGrid.Dock = DockStyle.Fill;
        mainGrid.Location = new Point(0, 53);
        mainGrid.Name = "mainGrid";
        mainGrid.RowHeadersWidth = 26;
        mainGrid.Size = new Size(1469, 836);
        mainGrid.TabIndex = 1;
        // 
        // dateHeaderLabel
        // 
        dateHeaderLabel.BackColor = Color.FromArgb(235, 240, 245);
        dateHeaderLabel.Dock = DockStyle.Top;
        dateHeaderLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        dateHeaderLabel.ForeColor = Color.FromArgb(30, 30, 30);
        dateHeaderLabel.Location = new Point(0, 0);
        dateHeaderLabel.Name = "dateHeaderLabel";
        dateHeaderLabel.Size = new Size(1469, 53);
        dateHeaderLabel.TabIndex = 0;
        dateHeaderLabel.Text = "08-08-2026";
        dateHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // rightPanel
        // 
        rightPanel.BackColor = Color.FromArgb(215, 232, 240);
        rightPanel.Controls.Add(masterGroup);
        rightPanel.Dock = DockStyle.Right;
        rightPanel.Location = new Point(1469, 190);
        rightPanel.Name = "rightPanel";
        rightPanel.Padding = new Padding(6);
        rightPanel.Size = new Size(466, 889);
        rightPanel.TabIndex = 3;
        // 
        // masterGroup
        // 
        masterGroup.Controls.Add(mNameLabel);
        masterGroup.Controls.Add(mNameTextBox);
        masterGroup.Controls.Add(m3dCheckBox);
        masterGroup.Controls.Add(mAddButton);
        masterGroup.Controls.Add(masterGrid);
        masterGroup.Dock = DockStyle.Fill;
        masterGroup.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        masterGroup.ForeColor = Color.FromArgb(0, 102, 204);
        masterGroup.Location = new Point(6, 6);
        masterGroup.Name = "masterGroup";
        masterGroup.Padding = new Padding(6);
        masterGroup.Size = new Size(454, 877);
        masterGroup.TabIndex = 0;
        masterGroup.TabStop = false;
        masterGroup.Text = "Add New Movie";
        // 
        // mNameLabel
        // 
        mNameLabel.AutoSize = true;
        mNameLabel.Font = new Font("Segoe UI", 8.5F);
        mNameLabel.ForeColor = Color.Black;
        mNameLabel.Location = new Point(6, 49);
        mNameLabel.Name = "mNameLabel";
        mNameLabel.Size = new Size(144, 31);
        mNameLabel.TabIndex = 0;
        mNameLabel.Text = "Movie Name";
        // 
        // mNameTextBox
        // 
        mNameTextBox.Font = new Font("Segoe UI", 8.5F);
        mNameTextBox.Location = new Point(6, 83);
        mNameTextBox.Name = "mNameTextBox";
        mNameTextBox.Size = new Size(394, 38);
        mNameTextBox.TabIndex = 1;
        // 
        // m3dCheckBox
        // 
        m3dCheckBox.AutoSize = true;
        m3dCheckBox.Font = new Font("Segoe UI", 8.5F);
        m3dCheckBox.ForeColor = Color.Black;
        m3dCheckBox.Location = new Point(211, 189);
        m3dCheckBox.Name = "m3dCheckBox";
        m3dCheckBox.Size = new Size(80, 35);
        m3dCheckBox.TabIndex = 2;
        m3dCheckBox.Text = "3 D";
        m3dCheckBox.UseVisualStyleBackColor = true;
        // 
        // mAddButton
        // 
        mAddButton.BackColor = Color.FromArgb(0, 168, 223);
        mAddButton.FlatStyle = FlatStyle.Flat;
        mAddButton.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        mAddButton.ForeColor = Color.White;
        mAddButton.Location = new Point(317, 141);
        mAddButton.Name = "mAddButton";
        mAddButton.Size = new Size(83, 46);
        mAddButton.TabIndex = 3;
        mAddButton.Text = "Add";
        mAddButton.UseVisualStyleBackColor = false;
        mAddButton.Click += mAddButton_Click;
        // 
        // masterGrid
        // 
        masterGrid.AllowUserToAddRows = false;
        masterGrid.AllowUserToDeleteRows = false;
        masterGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        masterGrid.BackgroundColor = Color.White;
        masterGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        masterGrid.Columns.AddRange(new DataGridViewColumn[] { mgMovieColumn, mg3dColumn, mgDeleteColumn });
        masterGrid.Dock = DockStyle.Bottom;
        masterGrid.Font = new Font("Segoe UI", 8.5F);
        masterGrid.Location = new Point(6, 230);
        masterGrid.Name = "masterGrid";
        masterGrid.ReadOnly = true;
        masterGrid.RowHeadersWidth = 24;
        masterGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        masterGrid.Size = new Size(442, 641);
        masterGrid.TabIndex = 4;
        masterGrid.CellContentClick += masterGrid_CellContentClick;
        masterGrid.CellDoubleClick += masterGrid_CellDoubleClick;
        // 
        // mgMovieColumn
        // 
        mgMovieColumn.DataPropertyName = "Movie";
        mgMovieColumn.HeaderText = "Movie Name";
        mgMovieColumn.MinimumWidth = 10;
        mgMovieColumn.Name = "mgMovieColumn";
        mgMovieColumn.ReadOnly = true;
        // 
        // mg3dColumn
        // 
        mg3dColumn.DataPropertyName = "Is3D";
        mg3dColumn.HeaderText = "3 D";
        mg3dColumn.MinimumWidth = 10;
        mg3dColumn.Name = "mg3dColumn";
        mg3dColumn.ReadOnly = true;
        // 
        // mgDeleteColumn
        // 
        mgDeleteColumn.HeaderText = "DELETE";
        mgDeleteColumn.MinimumWidth = 10;
        mgDeleteColumn.Name = "mgDeleteColumn";
        mgDeleteColumn.ReadOnly = true;
        mgDeleteColumn.Text = "DELETE";
        mgDeleteColumn.UseColumnTextForButtonValue = true;
        // 
        // statusStrip
        // 
        statusStrip.ImageScalingSize = new Size(32, 32);
        statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
        statusStrip.Location = new Point(0, 1079);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(1935, 42);
        statusStrip.TabIndex = 4;
        // 
        // statusLabel
        // 
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(83, 32);
        statusLabel.Text = "Ready.";
        // 
        // SetMovieForm
        // 
        ClientSize = new Size(1935, 1121);
        Controls.Add(centerPanel);
        Controls.Add(rightPanel);
        Controls.Add(topInputsPanel);
        Controls.Add(headerPanel);
        Controls.Add(statusStrip);
        Name = "SetMovieForm";
        StartPosition = FormStartPosition.CenterParent;
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
