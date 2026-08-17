using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Services;

namespace CinemaTicketing.WinForms;

internal partial class TheatreSettingsForm : Form
{
    private readonly ITheatreSettingsService _theatreSettingsService;
    private IReadOnlyList<CinemaInfo> _cinemas = Array.Empty<CinemaInfo>();
    private IReadOnlyList<ShowType> _showTypes = Array.Empty<ShowType>();
    private long? _editingId;

    public TheatreSettingsForm(ITheatreSettingsService theatreSettingsService)
    {
        _theatreSettingsService = theatreSettingsService;
        InitializeComponent();
    }

    private async void TheatreSettingsForm_Shown(object sender, EventArgs e)
    {
        if (Owner is not null)
        {
            Width = (int)(Owner.Width * 0.75);
            Height = (int)(Owner.Height * 0.75);
            CenterToParent();
        }

        await LoadLookupsAsync();
        await LoadGridAsync();
        await Load3DChargeAsync();
        await LoadShowTypesGridAsync();
    }

    private async Task Load3DChargeAsync()
    {
        var charge = await _theatreSettingsService.Get3DChargeAsync();
        threeDPriceNumeric.Value = charge;
    }

    private async Task LoadShowTypesGridAsync()
    {
        _showTypes = await _theatreSettingsService.GetShowTypesAsync();
        showTypeGrid.AutoGenerateColumns = false;
        showTypeGrid.DataSource = _showTypes.Select(x => new
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }

    private async void saveButton_Click(object sender, EventArgs e)
    {
        await SaveAsync();
    }

    private async void settingsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        var row = settingsGrid.Rows[e.RowIndex];
        var item = row.DataBoundItem as TheatreSettingGridItem;
        if (item is null) return;

        if (settingsGrid.Columns[e.ColumnIndex].Name == "editButtonColumn")
        {
            _editingId = item.Id;
            cinemaComboBox.SelectedValue = item.CinemaId;
            showTypeComboBox.SelectedValue = item.ShowTypeId;
            showTimeTextBox.Text = item.ShowTime;
            priceNumericUpDown.Value = item.Price;
            saveButton.Text = "Update";
            statusLabel.Text = $"Editing setting for {item.Theatre} - {item.ShowType}.";
            statusLabel.ForeColor = Color.FromArgb(30, 30, 30);
        }
        else if (settingsGrid.Columns[e.ColumnIndex].Name == "deleteButtonColumn")
        {
            await _theatreSettingsService.DeleteAsync(item.Id);
            ResetForm();
            await LoadGridAsync();
            statusLabel.Text = "Deleted theatre setting.";
            statusLabel.ForeColor = Color.DarkGreen;
        }
    }

    private async void showTypeGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        var row = showTypeGrid.Rows[e.RowIndex];
        if (row.Cells["stIdColumn"].Value is long id)
        {
            if (showTypeGrid.Columns[e.ColumnIndex].Name == "stEditButtonColumn")
            {
                newShowTypeTextBox.Text = row.Cells["stNameColumn"].Value?.ToString() ?? "";
            }
            else if (showTypeGrid.Columns[e.ColumnIndex].Name == "stDeleteButtonColumn")
            {
                await _theatreSettingsService.DeleteShowTypeAsync(id);
                await LoadLookupsAsync();
                await LoadShowTypesGridAsync();
                statusLabel.Text = "Deleted show type.";
                statusLabel.ForeColor = Color.DarkGreen;
            }
        }
    }

    private async void threeDAddButton_Click(object sender, EventArgs e)
    {
        await _theatreSettingsService.Save3DChargeAsync(threeDPriceNumeric.Value);
        statusLabel.Text = $"3D charge updated to ₹{threeDPriceNumeric.Value:F0}.";
        statusLabel.ForeColor = Color.DarkGreen;
    }

    private async void addShowTypeButton_Click(object sender, EventArgs e)
    {
        string name = newShowTypeTextBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            statusLabel.Text = "Enter a show type name.";
            statusLabel.ForeColor = Color.DarkRed;
            return;
        }

        await _theatreSettingsService.CreateShowTypeAsync(name);
        newShowTypeTextBox.Clear();
        await LoadLookupsAsync();
        await LoadShowTypesGridAsync();
        statusLabel.Text = $"Added show type '{name}'.";
        statusLabel.ForeColor = Color.DarkGreen;
    }

    private async Task LoadLookupsAsync()
    {
        _cinemas = await _theatreSettingsService.GetCinemasAsync();
        _showTypes = await _theatreSettingsService.GetShowTypesAsync();

        cinemaComboBox.DataSource = _cinemas.ToList();
        cinemaComboBox.DisplayMember = nameof(CinemaInfo.Name);
        cinemaComboBox.ValueMember = nameof(CinemaInfo.Id);

        showTypeComboBox.DataSource = _showTypes.ToList();
        showTypeComboBox.DisplayMember = nameof(ShowType.Name);
        showTypeComboBox.ValueMember = nameof(ShowType.Id);
    }

    private async Task LoadGridAsync()
    {
        var items = await _theatreSettingsService.GetSettingsAsync();
        settingsGrid.AutoGenerateColumns = false;
        settingsGrid.DataSource = items.Select(x => new TheatreSettingGridItem
        {
            Id = x.Id,
            CinemaId = x.CinemaId,
            ShowTypeId = x.ShowTypeId,
            Theatre = x.CinemaName,
            ShowType = x.ShowTypeName,
            ShowTime = DateTime.Today.Add(x.ShowTime).ToString("hh:mmtt").ToUpper(),
            Price = x.Price
        }).ToList();
    }

    private async Task SaveAsync()
    {
        try
        {
            saveButton.Enabled = false;
            statusLabel.Text = "Saving theatre setting...";
            statusLabel.ForeColor = Color.FromArgb(33, 37, 41);

            string rawTime = showTimeTextBox.Text.Trim().Replace(".", ":");
            if (!DateTime.TryParse(rawTime, out var dt))
            {
                statusLabel.Text = "Enter valid show time (e.g. 11:30 AM or 02:30 PM).";
                statusLabel.ForeColor = Color.DarkRed;
                return;
            }

            TimeSpan showTime = dt.TimeOfDay;

            var request = new TheatreSettingSaveRequest(
                _editingId,
                cinemaComboBox.SelectedValue is long cinemaId ? cinemaId : 0,
                showTypeComboBox.SelectedValue is long showTypeId ? showTypeId : 0,
                showTime,
                priceNumericUpDown.Value);

            var result = await _theatreSettingsService.SaveAsync(request);
            statusLabel.Text = result.Message;
            statusLabel.ForeColor = result.IsSuccess ? Color.DarkGreen : Color.DarkRed;

            if (!result.IsSuccess)
            {
                return;
            }

            ResetForm();
            await LoadGridAsync();
        }
        finally
        {
            saveButton.Enabled = true;
        }
    }

    private void ResetForm()
    {
        _editingId = null;
        if (cinemaComboBox.Items.Count > 0)
        {
            cinemaComboBox.SelectedIndex = 0;
        }
        if (showTypeComboBox.Items.Count > 0)
        {
            showTypeComboBox.SelectedIndex = 0;
        }
        showTimeTextBox.Text = "11:30 AM";
        priceNumericUpDown.Value = 150;
        saveButton.Text = "Save";
    }

    private sealed class TheatreSettingGridItem
    {
        public long Id { get; init; }
        public long CinemaId { get; init; }
        public long ShowTypeId { get; init; }
        public string Theatre { get; init; } = string.Empty;
        public string ShowType { get; init; } = string.Empty;
        public string ShowTime { get; init; } = string.Empty;
        public decimal Price { get; init; }
    }

    private void showTypeLabel_Click(object sender, EventArgs e)
    {

    }
}
