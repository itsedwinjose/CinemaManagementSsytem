using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;
using CinemaTicketing.WinForms.UI.Design;

namespace CinemaTicketing.WinForms;

internal partial class SeatClassForm : Form
{
    private readonly ISeatClassRepository _repository;
    private long? _editingId;

    public SeatClassForm(ISeatClassRepository repository)
    {
        _repository = repository;
        InitializeComponent();
        AppTheme.ApplyTheme(this);
        AppTheme.ApplyPrimaryButton(saveButton);
        AppTheme.ApplyDestructiveButton(deleteButton);
    }

    private async void SeatClassForm_Shown(object sender, EventArgs e)
    {
        await LoadGridAsync();
    }

    private async Task LoadGridAsync()
    {
        var items = await _repository.GetAllAsync();
        grid.DataSource = items.Select(x => new
        {
            x.Id,
            x.Name,
            x.DisplayOrder,
            x.IsActive
        }).ToList();
    }

    private async void saveButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(nameTextBox.Text))
        {
            statusLabel.Text = "Please enter a class name.";
            statusLabel.ForeColor = Color.DarkRed;
            return;
        }

        var seatClass = new SeatClass
        {
            Id = _editingId ?? 0,
            Name = nameTextBox.Text.Trim(),
            DisplayOrder = (int)orderNumeric.Value,
            IsActive = activeCheckBox.Checked
        };

        if (_editingId.HasValue)
        {
            await _repository.UpdateAsync(seatClass);
            statusLabel.Text = "Seat class updated.";
        }
        else
        {
            await _repository.CreateAsync(seatClass);
            statusLabel.Text = "Seat class added.";
        }

        statusLabel.ForeColor = Color.DarkGreen;
        ResetForm();
        await LoadGridAsync();
    }

    private async void deleteButton_Click(object sender, EventArgs e)
    {
        if (!_editingId.HasValue)
        {
            statusLabel.Text = "Double-click a row to edit/delete.";
            statusLabel.ForeColor = Color.DarkRed;
            return;
        }

        await _repository.DeleteAsync(_editingId.Value);
        statusLabel.Text = "Seat class deleted.";
        statusLabel.ForeColor = Color.DarkGreen;
        ResetForm();
        await LoadGridAsync();
    }

    private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var row = grid.Rows[e.RowIndex];
        if (row.DataBoundItem is null) return;

        if (row.Cells["Id"].Value is long id) _editingId = id;
        nameTextBox.Text = row.Cells["Name"].Value?.ToString() ?? string.Empty;
        if (row.Cells["DisplayOrder"].Value is int order) orderNumeric.Value = order;
        if (row.Cells["IsActive"].Value is bool active) activeCheckBox.Checked = active;

        saveButton.Text = "Update";
        statusLabel.Text = $"Editing class ID {_editingId}.";
        statusLabel.ForeColor = Color.FromArgb(33, 37, 41);
    }

    private void ResetForm()
    {
        _editingId = null;
        nameTextBox.Clear();
        orderNumeric.Value = 1;
        activeCheckBox.Checked = true;
        saveButton.Text = "Save";
    }
}
