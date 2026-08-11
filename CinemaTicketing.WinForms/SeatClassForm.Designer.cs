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
        headerLabel = new System.Windows.Forms.Label();
        grid = new System.Windows.Forms.DataGridView();
        nameLabel = new System.Windows.Forms.Label();
        nameTextBox = new System.Windows.Forms.TextBox();
        orderLabel = new System.Windows.Forms.Label();
        orderNumeric = new System.Windows.Forms.NumericUpDown();
        activeCheckBox = new System.Windows.Forms.CheckBox();
        saveButton = new System.Windows.Forms.Button();
        deleteButton = new System.Windows.Forms.Button();
        statusLabel = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        ((System.ComponentModel.ISupportInitialize)orderNumeric).BeginInit();
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
        headerLabel.Size = new System.Drawing.Size(464, 35);
        headerLabel.TabIndex = 0;
        headerLabel.Text = " Seat Class Management";
        headerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // grid
        // 
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        grid.Location = new System.Drawing.Point(12, 45);
        grid.Name = "grid";
        grid.ReadOnly = true;
        grid.RowHeadersVisible = false;
        grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        grid.Size = new System.Drawing.Size(440, 170);
        grid.TabIndex = 1;
        grid.CellDoubleClick += grid_CellDoubleClick;
        // 
        // nameLabel
        // 
        nameLabel.AutoSize = true;
        nameLabel.Location = new System.Drawing.Point(12, 230);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new System.Drawing.Size(72, 15);
        nameLabel.TabIndex = 2;
        nameLabel.Text = "Class Name:";
        // 
        // nameTextBox
        // 
        nameTextBox.Location = new System.Drawing.Point(90, 227);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.Size = new System.Drawing.Size(140, 23);
        nameTextBox.TabIndex = 3;
        // 
        // orderLabel
        // 
        orderLabel.AutoSize = true;
        orderLabel.Location = new System.Drawing.Point(245, 230);
        orderLabel.Name = "orderLabel";
        orderLabel.Size = new System.Drawing.Size(40, 15);
        orderLabel.TabIndex = 4;
        orderLabel.Text = "Order:";
        // 
        // orderNumeric
        // 
        orderNumeric.Location = new System.Drawing.Point(290, 227);
        orderNumeric.Name = "orderNumeric";
        orderNumeric.Size = new System.Drawing.Size(60, 23);
        orderNumeric.TabIndex = 5;
        orderNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // activeCheckBox
        // 
        activeCheckBox.AutoSize = true;
        activeCheckBox.Checked = true;
        activeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
        activeCheckBox.Location = new System.Drawing.Point(365, 229);
        activeCheckBox.Name = "activeCheckBox";
        activeCheckBox.Size = new System.Drawing.Size(59, 19);
        activeCheckBox.TabIndex = 6;
        activeCheckBox.Text = "Active";
        activeCheckBox.UseVisualStyleBackColor = true;
        // 
        // saveButton
        // 
        saveButton.Location = new System.Drawing.Point(280, 265);
        saveButton.Name = "saveButton";
        saveButton.Size = new System.Drawing.Size(80, 28);
        saveButton.TabIndex = 7;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += saveButton_Click;
        // 
        // deleteButton
        // 
        deleteButton.Location = new System.Drawing.Point(372, 265);
        deleteButton.Name = "deleteButton";
        deleteButton.Size = new System.Drawing.Size(80, 28);
        deleteButton.TabIndex = 8;
        deleteButton.Text = "Delete";
        deleteButton.UseVisualStyleBackColor = true;
        deleteButton.Click += deleteButton_Click;
        // 
        // statusLabel
        // 
        statusLabel.AutoSize = true;
        statusLabel.Location = new System.Drawing.Point(12, 272);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new System.Drawing.Size(42, 15);
        statusLabel.TabIndex = 9;
        statusLabel.Text = "Ready.";
        // 
        // SeatClassForm
        // 
        ClientSize = new System.Drawing.Size(464, 305);
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
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SeatClassForm";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "Seat Class Management";
        Shown += SeatClassForm_Shown;
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ((System.ComponentModel.ISupportInitialize)orderNumeric).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
