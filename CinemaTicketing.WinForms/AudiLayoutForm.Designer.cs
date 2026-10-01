namespace CinemaTicketing.WinForms;

partial class AudiLayoutForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label headerLabel;
    private System.Windows.Forms.Panel topPanel;
    private System.Windows.Forms.Label audiLabel;
    private System.Windows.Forms.ComboBox audiComboBox;
    private System.Windows.Forms.Label rowsLabel;
    private System.Windows.Forms.NumericUpDown rowsNumeric;
    private System.Windows.Forms.Label colsLabel;
    private System.Windows.Forms.NumericUpDown colsNumeric;
    private System.Windows.Forms.Button resizeButton;
    private System.Windows.Forms.Button saveButton;
    private System.Windows.Forms.Button manageClassesButton;
    private System.Windows.Forms.GroupBox toolBox;
    private System.Windows.Forms.RadioButton markSeatRadio;
    private System.Windows.Forms.RadioButton markNonSeatRadio;
    private System.Windows.Forms.RadioButton markRowNonSeatRadio;
    private System.Windows.Forms.RadioButton markColNonSeatRadio;
    private System.Windows.Forms.RadioButton assignClassRadio;
    private System.Windows.Forms.RadioButton toggleDamagedRadio;
    private System.Windows.Forms.ComboBox classComboBox;
    private System.Windows.Forms.Button markSeatsStairButton;
    private System.Windows.Forms.Button markRowStairButton;
    private System.Windows.Forms.Button markColStairButton;
    private System.Windows.Forms.Button setAisleLabelButton;
    private System.Windows.Forms.Button autoRenumberButton;
    private System.Windows.Forms.Label seatNameLabel;
    private System.Windows.Forms.TextBox seatNameTextBox;
    private System.Windows.Forms.Button applySeatNameButton;
    private CinemaTicketing.WinForms.Controls.CinemaSeatMapControl seatMapEditor;
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
        topPanel = new Panel();
        manageClassesButton = new Button();
        saveButton = new Button();
        resizeButton = new Button();
        colsNumeric = new NumericUpDown();
        colsLabel = new Label();
        rowsNumeric = new NumericUpDown();
        rowsLabel = new Label();
        audiComboBox = new ComboBox();
        audiLabel = new Label();
        toolBox = new GroupBox();
        applySeatNameButton = new Button();
        seatNameTextBox = new TextBox();
        seatNameLabel = new Label();
        autoRenumberButton = new Button();
        setAisleLabelButton = new Button();
        markColStairButton = new Button();
        markRowStairButton = new Button();
        markSeatsStairButton = new Button();
        classComboBox = new ComboBox();
        toggleDamagedRadio = new RadioButton();
        assignClassRadio = new RadioButton();
        markColNonSeatRadio = new RadioButton();
        markRowNonSeatRadio = new RadioButton();
        markNonSeatRadio = new RadioButton();
        markSeatRadio = new RadioButton();
        seatMapEditor = new CinemaTicketing.WinForms.Controls.CinemaSeatMapControl();
        statusStrip = new StatusStrip();
        statusLabel = new ToolStripStatusLabel();
        topPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)colsNumeric).BeginInit();
        ((System.ComponentModel.ISupportInitialize)rowsNumeric).BeginInit();
        toolBox.SuspendLayout();
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
        headerLabel.Size = new Size(1914, 35);
        headerLabel.TabIndex = 0;
        headerLabel.Text = " Audi Seating Layout Management";
        headerLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // topPanel
        // 
        topPanel.Controls.Add(manageClassesButton);
        topPanel.Controls.Add(saveButton);
        topPanel.Controls.Add(resizeButton);
        topPanel.Controls.Add(colsNumeric);
        topPanel.Controls.Add(colsLabel);
        topPanel.Controls.Add(rowsNumeric);
        topPanel.Controls.Add(rowsLabel);
        topPanel.Controls.Add(audiComboBox);
        topPanel.Controls.Add(audiLabel);
        topPanel.Dock = DockStyle.Top;
        topPanel.Location = new Point(0, 35);
        topPanel.Name = "topPanel";
        topPanel.Size = new Size(1914, 65);
        topPanel.TabIndex = 1;
        // 
        // manageClassesButton
        // 
        manageClassesButton.Location = new Point(1648, 8);
        manageClassesButton.Name = "manageClassesButton";
        manageClassesButton.Size = new Size(254, 48);
        manageClassesButton.TabIndex = 8;
        manageClassesButton.Text = "Seat Classes...";
        manageClassesButton.UseVisualStyleBackColor = true;
        manageClassesButton.Click += manageClassesButton_Click;
        // 
        // saveButton
        // 
        saveButton.Location = new Point(1395, 8);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(206, 48);
        saveButton.TabIndex = 7;
        saveButton.Text = "Save Layout";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += saveButton_Click;
        // 
        // resizeButton
        // 
        resizeButton.Location = new Point(1135, 8);
        resizeButton.Name = "resizeButton";
        resizeButton.Size = new Size(216, 48);
        resizeButton.TabIndex = 6;
        resizeButton.Text = "Resize Grid";
        resizeButton.UseVisualStyleBackColor = true;
        resizeButton.Click += resizeButton_Click;
        // 
        // colsNumeric
        // 
        colsNumeric.Location = new Point(1005, 14);
        colsNumeric.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
        colsNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        colsNumeric.Name = "colsNumeric";
        colsNumeric.Size = new Size(80, 39);
        colsNumeric.TabIndex = 5;
        colsNumeric.Value = new decimal(new int[] { 15, 0, 0, 0 });
        // 
        // colsLabel
        // 
        colsLabel.AutoSize = true;
        colsLabel.Location = new Point(863, 16);
        colsLabel.Name = "colsLabel";
        colsLabel.Size = new Size(113, 32);
        colsLabel.TabIndex = 4;
        colsLabel.Text = "Columns:";
        // 
        // rowsNumeric
        // 
        rowsNumeric.Location = new Point(742, 14);
        rowsNumeric.Maximum = new decimal(new int[] { 40, 0, 0, 0 });
        rowsNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        rowsNumeric.Name = "rowsNumeric";
        rowsNumeric.Size = new Size(80, 39);
        rowsNumeric.TabIndex = 3;
        rowsNumeric.Value = new decimal(new int[] { 10, 0, 0, 0 });
        // 
        // rowsLabel
        // 
        rowsLabel.AutoSize = true;
        rowsLabel.Location = new Point(628, 16);
        rowsLabel.Name = "rowsLabel";
        rowsLabel.Size = new Size(73, 32);
        rowsLabel.TabIndex = 2;
        rowsLabel.Text = "Rows:";
        // 
        // audiComboBox
        // 
        audiComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        audiComboBox.FormattingEnabled = true;
        audiComboBox.Location = new Point(167, 12);
        audiComboBox.Name = "audiComboBox";
        audiComboBox.Size = new Size(436, 40);
        audiComboBox.TabIndex = 1;
        audiComboBox.SelectedIndexChanged += audiComboBox_SelectedIndexChanged;
        // 
        // audiLabel
        // 
        audiLabel.AutoSize = true;
        audiLabel.Location = new Point(12, 16);
        audiLabel.Name = "audiLabel";
        audiLabel.Size = new Size(139, 32);
        audiLabel.TabIndex = 0;
        audiLabel.Text = "Select Audi:";
        // 
        // toolBox
        // 
        toolBox.Controls.Add(applySeatNameButton);
        toolBox.Controls.Add(seatNameTextBox);
        toolBox.Controls.Add(seatNameLabel);
        toolBox.Controls.Add(autoRenumberButton);
        toolBox.Controls.Add(setAisleLabelButton);
        toolBox.Controls.Add(markColStairButton);
        toolBox.Controls.Add(markRowStairButton);
        toolBox.Controls.Add(markSeatsStairButton);
        toolBox.Controls.Add(classComboBox);
        toolBox.Controls.Add(toggleDamagedRadio);
        toolBox.Controls.Add(assignClassRadio);
        toolBox.Controls.Add(markColNonSeatRadio);
        toolBox.Controls.Add(markRowNonSeatRadio);
        toolBox.Controls.Add(markNonSeatRadio);
        toolBox.Controls.Add(markSeatRadio);
        toolBox.Dock = DockStyle.Top;
        toolBox.Location = new Point(0, 100);
        toolBox.Name = "toolBox";
        toolBox.Size = new Size(1914, 140);
        toolBox.TabIndex = 2;
        toolBox.TabStop = false;
        toolBox.Text = "Layout Editing Actions (Seat / Row / Column Empty-Stair & Naming)";
        // 
        // applySeatNameButton
        // 
        applySeatNameButton.Enabled = false;
        applySeatNameButton.Location = new Point(1740, 75);
        applySeatNameButton.Name = "applySeatNameButton";
        applySeatNameButton.Size = new Size(150, 48);
        applySeatNameButton.TabIndex = 14;
        applySeatNameButton.Text = "Apply Name";
        applySeatNameButton.UseVisualStyleBackColor = true;
        applySeatNameButton.Click += applySeatNameButton_Click;
        // 
        // seatNameTextBox
        // 
        seatNameTextBox.Enabled = false;
        seatNameTextBox.Location = new Point(1610, 78);
        seatNameTextBox.Name = "seatNameTextBox";
        seatNameTextBox.Size = new Size(120, 39);
        seatNameTextBox.TabIndex = 13;
        // 
        // seatNameLabel
        // 
        seatNameLabel.AutoSize = true;
        seatNameLabel.Location = new Point(1520, 81);
        seatNameLabel.Name = "seatNameLabel";
        seatNameLabel.Size = new Size(83, 32);
        seatNameLabel.TabIndex = 12;
        seatNameLabel.Text = "Name:";
        // 
        // autoRenumberButton
        // 
        autoRenumberButton.Location = new Point(1220, 75);
        autoRenumberButton.Name = "autoRenumberButton";
        autoRenumberButton.Size = new Size(280, 48);
        autoRenumberButton.TabIndex = 11;
        autoRenumberButton.Text = "Auto-Renumber Active Seats";
        autoRenumberButton.UseVisualStyleBackColor = true;
        autoRenumberButton.Click += autoRenumberButton_Click;
        // 
        // setAisleLabelButton
        // 
        setAisleLabelButton.Location = new Point(940, 75);
        setAisleLabelButton.Name = "setAisleLabelButton";
        setAisleLabelButton.Size = new Size(260, 48);
        setAisleLabelButton.TabIndex = 10;
        setAisleLabelButton.Text = "Set Aisle Row Label";
        setAisleLabelButton.UseVisualStyleBackColor = true;
        setAisleLabelButton.Click += setAisleLabelButton_Click;
        // 
        // markColStairButton
        // 
        markColStairButton.Location = new Point(620, 75);
        markColStairButton.Name = "markColStairButton";
        markColStairButton.Size = new Size(300, 48);
        markColStairButton.TabIndex = 9;
        markColStairButton.Text = "Mark Column Empty/Stair";
        markColStairButton.UseVisualStyleBackColor = true;
        markColStairButton.Click += markColStairButton_Click;
        // 
        // markRowStairButton
        // 
        markRowStairButton.Location = new Point(320, 75);
        markRowStairButton.Name = "markRowStairButton";
        markRowStairButton.Size = new Size(280, 48);
        markRowStairButton.TabIndex = 8;
        markRowStairButton.Text = "Mark Row Empty/Stair";
        markRowStairButton.UseVisualStyleBackColor = true;
        markRowStairButton.Click += markRowStairButton_Click;
        // 
        // markSeatsStairButton
        // 
        markSeatsStairButton.Location = new Point(20, 75);
        markSeatsStairButton.Name = "markSeatsStairButton";
        markSeatsStairButton.Size = new Size(280, 48);
        markSeatsStairButton.TabIndex = 7;
        markSeatsStairButton.Text = "Mark Seat(s) Empty/Stair";
        markSeatsStairButton.UseVisualStyleBackColor = true;
        markSeatsStairButton.Click += markSeatsStairButton_Click;
        // 
        // classComboBox
        // 
        classComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        classComboBox.FormattingEnabled = true;
        classComboBox.Location = new Point(1300, 24);
        classComboBox.Name = "classComboBox";
        classComboBox.Size = new Size(200, 40);
        classComboBox.TabIndex = 5;
        classComboBox.SelectedIndexChanged += classComboBox_SelectedIndexChanged;
        // 
        // toggleDamagedRadio
        // 
        toggleDamagedRadio.AutoSize = true;
        toggleDamagedRadio.Location = new Point(1520, 26);
        toggleDamagedRadio.Name = "toggleDamagedRadio";
        toggleDamagedRadio.Size = new Size(298, 36);
        toggleDamagedRadio.TabIndex = 6;
        toggleDamagedRadio.Text = "Toggle Damaged Status";
        toggleDamagedRadio.UseVisualStyleBackColor = true;
        // 
        // assignClassRadio
        // 
        assignClassRadio.AutoSize = true;
        assignClassRadio.Location = new Point(1040, 26);
        assignClassRadio.Name = "assignClassRadio";
        assignClassRadio.Size = new Size(250, 36);
        assignClassRadio.TabIndex = 4;
        assignClassRadio.Text = "Assign Seat Class ->";
        assignClassRadio.UseVisualStyleBackColor = true;
        // 
        // markColNonSeatRadio
        // 
        markColNonSeatRadio.AutoSize = true;
        markColNonSeatRadio.Location = new Point(730, 26);
        markColNonSeatRadio.Name = "markColNonSeatRadio";
        markColNonSeatRadio.Size = new Size(285, 36);
        markColNonSeatRadio.TabIndex = 3;
        markColNonSeatRadio.Text = "Mark Column Empty/Stair";
        markColNonSeatRadio.UseVisualStyleBackColor = true;
        // 
        // markRowNonSeatRadio
        // 
        markRowNonSeatRadio.AutoSize = true;
        markRowNonSeatRadio.Location = new Point(460, 26);
        markRowNonSeatRadio.Name = "markRowNonSeatRadio";
        markRowNonSeatRadio.Size = new Size(255, 36);
        markRowNonSeatRadio.TabIndex = 2;
        markRowNonSeatRadio.Text = "Mark Row Empty/Stair";
        markRowNonSeatRadio.UseVisualStyleBackColor = true;
        // 
        // markNonSeatRadio
        // 
        markNonSeatRadio.AutoSize = true;
        markNonSeatRadio.Location = new Point(180, 26);
        markNonSeatRadio.Name = "markNonSeatRadio";
        markNonSeatRadio.Size = new Size(265, 36);
        markNonSeatRadio.TabIndex = 1;
        markNonSeatRadio.Text = "Mark Seat(s) Empty/Stair";
        markNonSeatRadio.UseVisualStyleBackColor = true;
        // 
        // markSeatRadio
        // 
        markSeatRadio.AutoSize = true;
        markSeatRadio.Checked = true;
        markSeatRadio.Location = new Point(20, 26);
        markSeatRadio.Name = "markSeatRadio";
        markSeatRadio.Size = new Size(145, 36);
        markSeatRadio.TabIndex = 0;
        markSeatRadio.TabStop = true;
        markSeatRadio.Text = "Mark Seat";
        markSeatRadio.UseVisualStyleBackColor = true;
        // 
        // seatMapEditor
        // 
        seatMapEditor.BackColor = Color.FromArgb(240, 242, 245);
        seatMapEditor.Dock = DockStyle.Fill;
        seatMapEditor.Location = new Point(0, 240);
        seatMapEditor.Name = "seatMapEditor";
        seatMapEditor.Size = new Size(1914, 876);
        seatMapEditor.TabIndex = 3;
        seatMapEditor.SelectionChanged += seatMapEditor_SelectionChanged;
        // 
        // statusStrip
        // 
        statusStrip.ImageScalingSize = new Size(32, 32);
        statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
        statusStrip.Location = new Point(0, 1116);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(1914, 42);
        statusStrip.TabIndex = 4;
        // 
        // statusLabel
        // 
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(83, 32);
        statusLabel.Text = "Ready.";
        // 
        // AudiLayoutForm
        // 
        ClientSize = new Size(1914, 1158);
        Controls.Add(seatMapEditor);
        Controls.Add(statusStrip);
        Controls.Add(toolBox);
        Controls.Add(topPanel);
        Controls.Add(headerLabel);
        Name = "AudiLayoutForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Audi Seating Layout Management";
        Shown += AudiLayoutForm_Shown;
        topPanel.ResumeLayout(false);
        topPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)colsNumeric).EndInit();
        ((System.ComponentModel.ISupportInitialize)rowsNumeric).EndInit();
        toolBox.ResumeLayout(false);
        toolBox.PerformLayout();
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
