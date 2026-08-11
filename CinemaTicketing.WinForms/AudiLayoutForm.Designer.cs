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
    private System.Windows.Forms.RadioButton assignClassRadio;
    private System.Windows.Forms.RadioButton toggleDamagedRadio;
    private System.Windows.Forms.ComboBox classComboBox;
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
        headerLabel = new System.Windows.Forms.Label();
        topPanel = new System.Windows.Forms.Panel();
        manageClassesButton = new System.Windows.Forms.Button();
        saveButton = new System.Windows.Forms.Button();
        resizeButton = new System.Windows.Forms.Button();
        colsNumeric = new System.Windows.Forms.NumericUpDown();
        colsLabel = new System.Windows.Forms.Label();
        rowsNumeric = new System.Windows.Forms.NumericUpDown();
        rowsLabel = new System.Windows.Forms.Label();
        audiComboBox = new System.Windows.Forms.ComboBox();
        audiLabel = new System.Windows.Forms.Label();
        toolBox = new System.Windows.Forms.GroupBox();
        classComboBox = new System.Windows.Forms.ComboBox();
        toggleDamagedRadio = new System.Windows.Forms.RadioButton();
        assignClassRadio = new System.Windows.Forms.RadioButton();
        markNonSeatRadio = new System.Windows.Forms.RadioButton();
        markSeatRadio = new System.Windows.Forms.RadioButton();
        seatMapEditor = new CinemaTicketing.WinForms.Controls.CinemaSeatMapControl();
        statusStrip = new System.Windows.Forms.StatusStrip();
        statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
        topPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)colsNumeric).BeginInit();
        ((System.ComponentModel.ISupportInitialize)rowsNumeric).BeginInit();
        toolBox.SuspendLayout();
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
        headerLabel.Text = " Audi Seating Layout Management";
        headerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
        topPanel.Dock = System.Windows.Forms.DockStyle.Top;
        topPanel.Location = new System.Drawing.Point(0, 35);
        topPanel.Name = "topPanel";
        topPanel.Size = new System.Drawing.Size(984, 45);
        topPanel.TabIndex = 1;
        // 
        // manageClassesButton
        // 
        manageClassesButton.Location = new System.Drawing.Point(820, 10);
        manageClassesButton.Name = "manageClassesButton";
        manageClassesButton.Size = new System.Drawing.Size(145, 26);
        manageClassesButton.TabIndex = 8;
        manageClassesButton.Text = "Seat Classes...";
        manageClassesButton.UseVisualStyleBackColor = true;
        manageClassesButton.Click += manageClassesButton_Click;
        // 
        // saveButton
        // 
        saveButton.Location = new System.Drawing.Point(710, 10);
        saveButton.Name = "saveButton";
        saveButton.Size = new System.Drawing.Size(95, 26);
        saveButton.TabIndex = 7;
        saveButton.Text = "Save Layout";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += saveButton_Click;
        // 
        // resizeButton
        // 
        resizeButton.Location = new System.Drawing.Point(595, 10);
        resizeButton.Name = "resizeButton";
        resizeButton.Size = new System.Drawing.Size(105, 26);
        resizeButton.TabIndex = 6;
        resizeButton.Text = "Resize Grid";
        resizeButton.UseVisualStyleBackColor = true;
        resizeButton.Click += resizeButton_Click;
        // 
        // colsNumeric
        // 
        colsNumeric.Location = new System.Drawing.Point(520, 12);
        colsNumeric.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
        colsNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        colsNumeric.Name = "colsNumeric";
        colsNumeric.Size = new System.Drawing.Size(60, 23);
        colsNumeric.TabIndex = 5;
        colsNumeric.Value = new decimal(new int[] { 15, 0, 0, 0 });
        // 
        // colsLabel
        // 
        colsLabel.AutoSize = true;
        colsLabel.Location = new System.Drawing.Point(460, 15);
        colsLabel.Name = "colsLabel";
        colsLabel.Size = new System.Drawing.Size(58, 15);
        colsLabel.TabIndex = 4;
        colsLabel.Text = "Columns:";
        // 
        // rowsNumeric
        // 
        rowsNumeric.Location = new System.Drawing.Point(390, 12);
        rowsNumeric.Maximum = new decimal(new int[] { 40, 0, 0, 0 });
        rowsNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        rowsNumeric.Name = "rowsNumeric";
        rowsNumeric.Size = new System.Drawing.Size(60, 23);
        rowsNumeric.TabIndex = 3;
        rowsNumeric.Value = new decimal(new int[] { 10, 0, 0, 0 });
        // 
        // rowsLabel
        // 
        rowsLabel.AutoSize = true;
        rowsLabel.Location = new System.Drawing.Point(345, 15);
        rowsLabel.Name = "rowsLabel";
        rowsLabel.Size = new System.Drawing.Size(38, 15);
        rowsLabel.TabIndex = 2;
        rowsLabel.Text = "Rows:";
        // 
        // audiComboBox
        // 
        audiComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        audiComboBox.FormattingEnabled = true;
        audiComboBox.Location = new System.Drawing.Point(85, 12);
        audiComboBox.Name = "audiComboBox";
        audiComboBox.Size = new System.Drawing.Size(240, 23);
        audiComboBox.TabIndex = 1;
        audiComboBox.SelectedIndexChanged += audiComboBox_SelectedIndexChanged;
        // 
        // audiLabel
        // 
        audiLabel.AutoSize = true;
        audiLabel.Location = new System.Drawing.Point(12, 15);
        audiLabel.Name = "audiLabel";
        audiLabel.Size = new System.Drawing.Size(70, 15);
        audiLabel.TabIndex = 0;
        audiLabel.Text = "Select Audi:";
        // 
        // toolBox
        // 
        toolBox.Controls.Add(classComboBox);
        toolBox.Controls.Add(toggleDamagedRadio);
        toolBox.Controls.Add(assignClassRadio);
        toolBox.Controls.Add(markNonSeatRadio);
        toolBox.Controls.Add(markSeatRadio);
        toolBox.Dock = System.Windows.Forms.DockStyle.Top;
        toolBox.Location = new System.Drawing.Point(0, 80);
        toolBox.Name = "toolBox";
        toolBox.Size = new System.Drawing.Size(984, 50);
        toolBox.TabIndex = 2;
        toolBox.TabStop = false;
        toolBox.Text = "Editing Action Tool (Apply to Selected Cells in Grid Below)";
        // 
        // classComboBox
        // 
        classComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        classComboBox.FormattingEnabled = true;
        classComboBox.Location = new System.Drawing.Point(375, 19);
        classComboBox.Name = "classComboBox";
        classComboBox.Size = new System.Drawing.Size(150, 23);
        classComboBox.TabIndex = 4;
        // 
        // toggleDamagedRadio
        // 
        toggleDamagedRadio.AutoSize = true;
        toggleDamagedRadio.Location = new System.Drawing.Point(545, 20);
        toggleDamagedRadio.Name = "toggleDamagedRadio";
        toggleDamagedRadio.Size = new System.Drawing.Size(147, 19);
        toggleDamagedRadio.TabIndex = 3;
        toggleDamagedRadio.Text = "Toggle Damaged Status";
        toggleDamagedRadio.UseVisualStyleBackColor = true;
        // 
        // assignClassRadio
        // 
        assignClassRadio.AutoSize = true;
        assignClassRadio.Location = new System.Drawing.Point(240, 20);
        assignClassRadio.Name = "assignClassRadio";
        assignClassRadio.Size = new System.Drawing.Size(130, 19);
        assignClassRadio.TabIndex = 2;
        assignClassRadio.Text = "Assign Seat Class ->";
        assignClassRadio.UseVisualStyleBackColor = true;
        // 
        // markNonSeatRadio
        // 
        markNonSeatRadio.AutoSize = true;
        markNonSeatRadio.Location = new System.Drawing.Point(115, 20);
        markNonSeatRadio.Name = "markNonSeatRadio";
        markNonSeatRadio.Size = new System.Drawing.Size(107, 19);
        markNonSeatRadio.TabIndex = 1;
        markNonSeatRadio.Text = "Mark Non-Seat";
        markNonSeatRadio.UseVisualStyleBackColor = true;
        // 
        // markSeatRadio
        // 
        markSeatRadio.AutoSize = true;
        markSeatRadio.Checked = true;
        markSeatRadio.Location = new System.Drawing.Point(15, 20);
        markSeatRadio.Name = "markSeatRadio";
        markSeatRadio.Size = new System.Drawing.Size(79, 19);
        markSeatRadio.TabIndex = 0;
        markSeatRadio.TabStop = true;
        markSeatRadio.Text = "Mark Seat";
        markSeatRadio.UseVisualStyleBackColor = true;
        // 
        // seatMapEditor
        // 
        seatMapEditor.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
        seatMapEditor.Dock = System.Windows.Forms.DockStyle.Fill;
        seatMapEditor.Location = new System.Drawing.Point(0, 130);
        seatMapEditor.Name = "seatMapEditor";
        seatMapEditor.Size = new System.Drawing.Size(984, 400);
        seatMapEditor.TabIndex = 3;
        seatMapEditor.SelectionChanged += seatMapEditor_SelectionChanged;
        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { statusLabel });
        statusStrip.Location = new System.Drawing.Point(0, 530);
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
        // AudiLayoutForm
        // 
        ClientSize = new System.Drawing.Size(984, 552);
        Controls.Add(seatMapEditor);
        Controls.Add(statusStrip);
        Controls.Add(toolBox);
        Controls.Add(topPanel);
        Controls.Add(headerLabel);
        Name = "AudiLayoutForm";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
