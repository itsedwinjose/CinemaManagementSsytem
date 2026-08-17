namespace CinemaTicketing.WinForms;

partial class SeatClassForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label headerLabel;
    private System.Windows.Forms.DataGridView grid;
    private System.Windows.Forms.Label nameLabel;
    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.Label orderLabel;
    private System.Windows.Forms.NumericUpDown orderNumeric;
    private System.Windows.Forms.CheckBox activeCheckBox;
    private System.Windows.Forms.Button saveButton;
    private System.Windows.Forms.Button deleteButton;
    private System.Windows.Forms.Label statusLabel;

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
        grid = new DataGridView();
        nameLabel = new Label();
        nameTextBox = new TextBox();
        orderLabel = new Label();
        orderNumeric = new NumericUpDown();
        activeCheckBox = new CheckBox();
        saveButton = new Button();
        deleteButton = new Button();
        statusLabel = new Label();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        ((System.ComponentModel.ISupportInitialize)orderNumeric).BeginInit();
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
        headerLabel.Size = new Size(821, 35);
        headerLabel.TabIndex = 0;
        headerLabel.Text = " Seat Class Management";
        headerLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // grid
        // 
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        grid.Location = new Point(12, 45);
        grid.Name = "grid";
        grid.ReadOnly = true;
        grid.RowHeadersVisible = false;
        grid.RowHeadersWidth = 82;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.Size = new Size(797, 268);
        grid.TabIndex = 1;
        grid.CellDoubleClick += grid_CellDoubleClick;
        // 
        // nameLabel
        // 
        nameLabel.AutoSize = true;
        nameLabel.Location = new Point(12, 341);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new Size(143, 32);
        nameLabel.TabIndex = 2;
        nameLabel.Text = "Class Name:";
        // 
        // nameTextBox
        // 
        nameTextBox.Location = new Point(192, 341);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.Size = new Size(353, 39);
        nameTextBox.TabIndex = 3;
        // 
        // orderLabel
        // 
        orderLabel.AutoSize = true;
        orderLabel.Location = new Point(192, 414);
        orderLabel.Name = "orderLabel";
        orderLabel.Size = new Size(80, 32);
        orderLabel.TabIndex = 4;
        orderLabel.Text = "Order:";
        // 
        // orderNumeric
        // 
        orderNumeric.Location = new Point(331, 412);
        orderNumeric.Name = "orderNumeric";
        orderNumeric.Size = new Size(60, 39);
        orderNumeric.TabIndex = 5;
        orderNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // activeCheckBox
        // 
        activeCheckBox.AutoSize = true;
        activeCheckBox.Checked = true;
        activeCheckBox.CheckState = CheckState.Checked;
        activeCheckBox.Location = new Point(434, 415);
        activeCheckBox.Name = "activeCheckBox";
        activeCheckBox.Size = new Size(111, 36);
        activeCheckBox.TabIndex = 6;
        activeCheckBox.Text = "Active";
        activeCheckBox.UseVisualStyleBackColor = true;
        // 
        // saveButton
        // 
        saveButton.Location = new Point(454, 495);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(160, 71);
        saveButton.TabIndex = 7;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += saveButton_Click;
        // 
        // deleteButton
        // 
        deleteButton.Location = new Point(644, 495);
        deleteButton.Name = "deleteButton";
        deleteButton.Size = new Size(156, 71);
        deleteButton.TabIndex = 8;
        deleteButton.Text = "Delete";
        deleteButton.UseVisualStyleBackColor = true;
        deleteButton.Click += deleteButton_Click;
        // 
        // statusLabel
        // 
        statusLabel.AutoSize = true;
        statusLabel.Location = new Point(24, 514);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(83, 32);
        statusLabel.TabIndex = 9;
        statusLabel.Text = "Ready.";
        // 
        // SeatClassForm
        // 
        ClientSize = new Size(821, 591);
        Controls.Add(statusLabel);
        Controls.Add(deleteButton);
        Controls.Add(saveButton);
        Controls.Add(activeCheckBox);
        Controls.Add(orderNumeric);
        Controls.Add(orderLabel);
        Controls.Add(nameTextBox);
        Controls.Add(nameLabel);
        Controls.Add(grid);
        Controls.Add(headerLabel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SeatClassForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Seat Class Management";
        Shown += SeatClassForm_Shown;
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ((System.ComponentModel.ISupportInitialize)orderNumeric).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
