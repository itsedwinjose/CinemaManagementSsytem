using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;
using CinemaTicketing.Core.Services;
using CinemaTicketing.WinForms.UI.Design;

namespace CinemaTicketing.WinForms;

internal partial class AudiLayoutForm : Form
{
    private readonly IAudiLayoutService _layoutService;
    private readonly ISeatClassRepository _seatClassRepository;
    private readonly long _cinemaId;

    private IReadOnlyList<Audi> _audis = Array.Empty<Audi>();
    private IReadOnlyList<SeatClass> _seatClasses = Array.Empty<SeatClass>();
    private List<AudiLayoutCell> _currentCells = new();
    private Audi? _selectedAudi;

    public AudiLayoutForm(IAudiLayoutService layoutService, ISeatClassRepository seatClassRepository, long cinemaId = 1)
    {
        _layoutService = layoutService;
        _seatClassRepository = seatClassRepository;
        _cinemaId = cinemaId;

        InitializeComponent();
        seatMapEditor.AllowNonSeatSelection = true;
        seatMapEditor.SeatDoubleClicked += seatMapEditor_SeatDoubleClicked;

        AppTheme.ApplyTheme(this);
        AppTheme.ApplyPrimaryButton(saveButton);
        AppTheme.ApplyTealButton(resizeButton);
        AppTheme.ApplyTealButton(markSeatsStairButton);
        AppTheme.ApplyTealButton(markRowStairButton);
        AppTheme.ApplyTealButton(markColStairButton);
        AppTheme.ApplyTealButton(setAisleLabelButton);
        AppTheme.ApplyTealButton(autoRenumberButton);
        AppTheme.ApplyPrimaryButton(applySeatNameButton);
    }

    private async void AudiLayoutForm_Shown(object sender, EventArgs e)
    {
        await LoadLookupsAsync();
    }

    private async Task LoadLookupsAsync()
    {
        _audis = await _layoutService.GetAudisAsync(_cinemaId);
        _seatClasses = await _layoutService.GetSeatClassesAsync();

        audiComboBox.DataSource = _audis.ToList();
        audiComboBox.DisplayMember = nameof(Audi.Name);
        audiComboBox.ValueMember = nameof(Audi.Id);

        classComboBox.DataSource = _seatClasses.ToList();
        classComboBox.DisplayMember = nameof(SeatClass.Name);
        classComboBox.ValueMember = nameof(SeatClass.Id);

        if (_audis.Count > 0)
        {
            audiComboBox.SelectedIndex = 0;
            await LoadAudiLayoutAsync(_audis[0]);
        }
    }

    private async void audiComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (audiComboBox.SelectedItem is Audi audi)
        {
            await LoadAudiLayoutAsync(audi);
        }
    }

    private async Task LoadAudiLayoutAsync(Audi audi)
    {
        _selectedAudi = audi;
        rowsNumeric.Value = audi.TotalRows;
        colsNumeric.Value = audi.TotalCols;

        var existingCells = await _layoutService.GetLayoutAsync(audi.Id);
        _currentCells = BuildOrUpdateCellGrid(audi.TotalRows, audi.TotalCols, existingCells);

        RenderSeatMap();
        statusLabel.Text = $"Loaded layout for {audi.Name}: {_currentCells.Count(c => c.IsSeat)} seats configured.";
        statusLabel.ForeColor = Color.FromArgb(30, 30, 30);
    }

    private List<AudiLayoutCell> BuildOrUpdateCellGrid(int rows, int cols, IReadOnlyList<AudiLayoutCell> existing)
    {
        var existingDict = existing.ToDictionary(c => (c.RowIndex, c.ColIndex));
        var defaultClassId = _seatClasses.Count > 0 ? _seatClasses[0].Id : (long?)null;
        var defaultClassName = _seatClasses.Count > 0 ? _seatClasses[0].Name : "";

        var list = new List<AudiLayoutCell>();

        for (int r = 0; r < rows; r++)
        {
            char rowChar = (char)('A' + (r % 26));
            string rowLabel = rowChar.ToString();

            for (int c = 0; c < cols; c++)
            {
                if (existingDict.TryGetValue((r, c), out var old))
                {
                    list.Add(old);
                }
                else
                {
                    string seatNum = $"{rowLabel}{c + 1}";
                    list.Add(new AudiLayoutCell
                    {
                        AudiId = _selectedAudi?.Id ?? 0,
                        RowIndex = r,
                        ColIndex = c,
                        IsSeat = true,
                        RowLabel = rowLabel,
                        SeatNumber = seatNum,
                        SeatClassId = defaultClassId,
                        SeatClassName = defaultClassName,
                        IsDamaged = false
                    });
                }
            }
        }

        return list;
    }

    private void RenderSeatMap()
    {
        int rows = (int)rowsNumeric.Value;
        int cols = (int)colsNumeric.Value;

        var screeningSeats = _currentCells.Select(c => new ScreeningSeat
        {
            Id = c.Id > 0 ? c.Id : (c.RowIndex * 1000 + c.ColIndex + 1),
            RowIndex = c.RowIndex,
            ColIndex = c.ColIndex,
            IsSeat = c.IsSeat,
            RowLabel = c.RowLabel,
            SeatNumber = c.SeatNumber,
            SeatClassId = c.SeatClassId,
            SeatClassName = c.SeatClassName,
            IsDamaged = c.IsDamaged,
            Status = Core.Enums.SeatStatus.Available
        }).ToList();

        seatMapEditor.LoadSeats(rows, cols, screeningSeats);
    }

    private void resizeButton_Click(object sender, EventArgs e)
    {
        int rows = (int)rowsNumeric.Value;
        int cols = (int)colsNumeric.Value;
        _currentCells = BuildOrUpdateCellGrid(rows, cols, _currentCells);
        RenderSeatMap();
        statusLabel.Text = $"Grid resized to {rows} x {cols}. Click Save Layout to persist.";
        statusLabel.ForeColor = Color.FromArgb(30, 30, 30);
    }

    private void seatMapEditor_SelectionChanged(object sender, EventArgs e)
    {
        var selectedScreeningSeats = seatMapEditor.GetSelectedSeats();

        if (selectedScreeningSeats.Count == 1)
        {
            var target = selectedScreeningSeats[0];
            seatNameTextBox.Text = target.SeatNumber;
            seatNameTextBox.Enabled = true;
            applySeatNameButton.Enabled = true;
        }
        else
        {
            seatNameTextBox.Text = string.Empty;
            seatNameTextBox.Enabled = false;
            applySeatNameButton.Enabled = false;
        }

        if (selectedScreeningSeats.Count == 0) return;

        bool isSeatVal = markSeatRadio.Checked;
        bool isNonSeatVal = markNonSeatRadio.Checked;
        bool isRowNonSeatVal = markRowNonSeatRadio.Checked;
        bool isColNonSeatVal = markColNonSeatRadio.Checked;
        bool isAssignClass = assignClassRadio.Checked;
        bool isToggleDamaged = toggleDamagedRadio.Checked;

        SeatClass? selectedClass = classComboBox.SelectedItem as SeatClass;

        if (isRowNonSeatVal)
        {
            var targetRows = selectedScreeningSeats.Select(s => s.RowIndex).ToHashSet();
            for (int i = 0; i < _currentCells.Count; i++)
            {
                var cell = _currentCells[i];
                if (targetRows.Contains(cell.RowIndex))
                {
                    _currentCells[i] = new AudiLayoutCell
                    {
                        Id = cell.Id,
                        AudiId = cell.AudiId,
                        RowIndex = cell.RowIndex,
                        ColIndex = cell.ColIndex,
                        IsSeat = false,
                        RowLabel = cell.RowLabel,
                        SeatNumber = string.Empty,
                        SeatClassId = cell.SeatClassId,
                        SeatClassName = cell.SeatClassName,
                        IsDamaged = cell.IsDamaged
                    };
                }
            }

            RenderSeatMap();
            statusLabel.Text = $"Marked row(s) {string.Join(", ", targetRows.Select(r => (char)('A' + (r % 26))))} as Empty/Stair space.";
            statusLabel.ForeColor = Color.DarkGreen;
            return;
        }

        if (isColNonSeatVal)
        {
            var targetCols = selectedScreeningSeats.Select(s => s.ColIndex).ToHashSet();
            for (int i = 0; i < _currentCells.Count; i++)
            {
                var cell = _currentCells[i];
                if (targetCols.Contains(cell.ColIndex))
                {
                    _currentCells[i] = new AudiLayoutCell
                    {
                        Id = cell.Id,
                        AudiId = cell.AudiId,
                        RowIndex = cell.RowIndex,
                        ColIndex = cell.ColIndex,
                        IsSeat = false,
                        RowLabel = cell.RowLabel,
                        SeatNumber = string.Empty,
                        SeatClassId = cell.SeatClassId,
                        SeatClassName = cell.SeatClassName,
                        IsDamaged = cell.IsDamaged
                    };
                }
            }

            RenderSeatMap();
            statusLabel.Text = $"Marked column(s) {string.Join(", ", targetCols.Select(c => c + 1))} as Empty/Stair space.";
            statusLabel.ForeColor = Color.DarkGreen;
            return;
        }

        var cellDict = _currentCells.ToDictionary(c => (c.RowIndex, c.ColIndex));

        foreach (var ss in selectedScreeningSeats)
        {
            if (cellDict.TryGetValue((ss.RowIndex, ss.ColIndex), out var cell))
            {
                int index = _currentCells.IndexOf(cell);
                if (index < 0) continue;

                if (isSeatVal)
                {
                    string rowLabel = string.IsNullOrWhiteSpace(cell.RowLabel) ? ((char)('A' + (cell.RowIndex % 26))).ToString() : cell.RowLabel;
                    string defaultNum = $"{rowLabel}{cell.ColIndex + 1}";
                    _currentCells[index] = new AudiLayoutCell
                    {
                        Id = cell.Id,
                        AudiId = cell.AudiId,
                        RowIndex = cell.RowIndex,
                        ColIndex = cell.ColIndex,
                        IsSeat = true,
                        RowLabel = rowLabel,
                        SeatNumber = string.IsNullOrWhiteSpace(cell.SeatNumber) ? defaultNum : cell.SeatNumber,
                        SeatClassId = cell.SeatClassId,
                        SeatClassName = cell.SeatClassName,
                        IsDamaged = cell.IsDamaged
                    };
                }
                else if (isNonSeatVal)
                {
                    _currentCells[index] = new AudiLayoutCell
                    {
                        Id = cell.Id,
                        AudiId = cell.AudiId,
                        RowIndex = cell.RowIndex,
                        ColIndex = cell.ColIndex,
                        IsSeat = false,
                        RowLabel = cell.RowLabel,
                        SeatNumber = string.Empty,
                        SeatClassId = cell.SeatClassId,
                        SeatClassName = cell.SeatClassName,
                        IsDamaged = cell.IsDamaged
                    };
                }
                else if (isAssignClass && selectedClass is not null)
                {
                    _currentCells[index] = new AudiLayoutCell
                    {
                        Id = cell.Id,
                        AudiId = cell.AudiId,
                        RowIndex = cell.RowIndex,
                        ColIndex = cell.ColIndex,
                        IsSeat = cell.IsSeat,
                        RowLabel = cell.RowLabel,
                        SeatNumber = cell.SeatNumber,
                        SeatClassId = selectedClass.Id,
                        SeatClassName = selectedClass.Name,
                        IsDamaged = cell.IsDamaged
                    };
                }
                else if (isToggleDamaged)
                {
                    _currentCells[index] = new AudiLayoutCell
                    {
                        Id = cell.Id,
                        AudiId = cell.AudiId,
                        RowIndex = cell.RowIndex,
                        ColIndex = cell.ColIndex,
                        IsSeat = cell.IsSeat,
                        RowLabel = cell.RowLabel,
                        SeatNumber = cell.SeatNumber,
                        SeatClassId = cell.SeatClassId,
                        SeatClassName = cell.SeatClassName,
                        IsDamaged = !cell.IsDamaged
                    };
                }
            }
        }

        RenderSeatMap();
        statusLabel.Text = $"Applied modification to {selectedScreeningSeats.Count} cells.";
        statusLabel.ForeColor = Color.FromArgb(30, 30, 30);
    }

    private void markSeatsStairButton_Click(object sender, EventArgs e)
    {
        var selectedScreeningSeats = seatMapEditor.GetSelectedSeats();
        if (selectedScreeningSeats.Count == 0)
        {
            MessageBox.Show("Please select the seat(s) you wish to mark as Empty/Stair.", "Mark Seat(s) Empty/Stair", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var targetDict = selectedScreeningSeats.ToDictionary(s => (s.RowIndex, s.ColIndex));
        for (int i = 0; i < _currentCells.Count; i++)
        {
            var cell = _currentCells[i];
            if (targetDict.ContainsKey((cell.RowIndex, cell.ColIndex)))
            {
                _currentCells[i] = new AudiLayoutCell
                {
                    Id = cell.Id,
                    AudiId = cell.AudiId,
                    RowIndex = cell.RowIndex,
                    ColIndex = cell.ColIndex,
                    IsSeat = false,
                    RowLabel = cell.RowLabel,
                    SeatNumber = string.Empty,
                    SeatClassId = cell.SeatClassId,
                    SeatClassName = cell.SeatClassName,
                    IsDamaged = cell.IsDamaged
                };
            }
        }

        RenderSeatMap();
        statusLabel.Text = $"Marked {selectedScreeningSeats.Count} selected seat(s) as Empty/Stair space.";
        statusLabel.ForeColor = Color.DarkGreen;
    }

    private void markRowStairButton_Click(object sender, EventArgs e)
    {
        var selectedScreeningSeats = seatMapEditor.GetSelectedSeats();
        if (selectedScreeningSeats.Count == 0)
        {
            MessageBox.Show("Please select at least one cell in the row(s) you wish to mark as Empty/Stair.", "Mark Row Empty/Stair", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var targetRows = selectedScreeningSeats.Select(s => s.RowIndex).ToHashSet();
        for (int i = 0; i < _currentCells.Count; i++)
        {
            var cell = _currentCells[i];
            if (targetRows.Contains(cell.RowIndex))
            {
                _currentCells[i] = new AudiLayoutCell
                {
                    Id = cell.Id,
                    AudiId = cell.AudiId,
                    RowIndex = cell.RowIndex,
                    ColIndex = cell.ColIndex,
                    IsSeat = false,
                    RowLabel = cell.RowLabel,
                    SeatNumber = string.Empty,
                    SeatClassId = cell.SeatClassId,
                    SeatClassName = cell.SeatClassName,
                    IsDamaged = cell.IsDamaged
                };
            }
        }

        RenderSeatMap();
        statusLabel.Text = $"Marked row(s) {string.Join(", ", targetRows.Select(r => (char)('A' + (r % 26))))} as Empty/Stair space.";
        statusLabel.ForeColor = Color.DarkGreen;
    }

    private void markColStairButton_Click(object sender, EventArgs e)
    {
        var selectedScreeningSeats = seatMapEditor.GetSelectedSeats();
        if (selectedScreeningSeats.Count == 0)
        {
            MessageBox.Show("Please select at least one cell in the column(s) you wish to mark as Empty/Stair.", "Mark Column Empty/Stair", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var targetCols = selectedScreeningSeats.Select(s => s.ColIndex).ToHashSet();
        for (int i = 0; i < _currentCells.Count; i++)
        {
            var cell = _currentCells[i];
            if (targetCols.Contains(cell.ColIndex))
            {
                _currentCells[i] = new AudiLayoutCell
                {
                    Id = cell.Id,
                    AudiId = cell.AudiId,
                    RowIndex = cell.RowIndex,
                    ColIndex = cell.ColIndex,
                    IsSeat = false,
                    RowLabel = cell.RowLabel,
                    SeatNumber = string.Empty,
                    SeatClassId = cell.SeatClassId,
                    SeatClassName = cell.SeatClassName,
                    IsDamaged = cell.IsDamaged
                };
            }
        }

        RenderSeatMap();
        statusLabel.Text = $"Marked column(s) {string.Join(", ", targetCols.Select(c => c + 1))} as Empty/Stair space.";
        statusLabel.ForeColor = Color.DarkGreen;
    }

    private void setAisleLabelButton_Click(object sender, EventArgs e)
    {
        var selectedScreeningSeats = seatMapEditor.GetSelectedSeats();
        if (selectedScreeningSeats.Count == 0)
        {
            MessageBox.Show("Please select non-seat cell(s) in an aisle where you want to show row letters (e.g. A, B, D).", "Set Aisle Row Label", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var cellDict = _currentCells.ToDictionary(c => (c.RowIndex, c.ColIndex));

        foreach (var ss in selectedScreeningSeats)
        {
            if (cellDict.TryGetValue((ss.RowIndex, ss.ColIndex), out var cell))
            {
                int index = _currentCells.IndexOf(cell);
                if (index < 0) continue;

                string rowLabel = string.IsNullOrWhiteSpace(cell.RowLabel) ? ((char)('A' + (cell.RowIndex % 26))).ToString() : cell.RowLabel;

                _currentCells[index] = new AudiLayoutCell
                {
                    Id = cell.Id,
                    AudiId = cell.AudiId,
                    RowIndex = cell.RowIndex,
                    ColIndex = cell.ColIndex,
                    IsSeat = false,
                    RowLabel = rowLabel,
                    SeatNumber = rowLabel,
                    SeatClassId = cell.SeatClassId,
                    SeatClassName = cell.SeatClassName,
                    IsDamaged = cell.IsDamaged
                };
            }
        }

        RenderSeatMap();
        statusLabel.Text = $"Set aisle row label on {selectedScreeningSeats.Count} non-seat cells.";
        statusLabel.ForeColor = Color.DarkGreen;
    }

    private void applySeatNameButton_Click(object sender, EventArgs e)
    {
        var selectedSeats = seatMapEditor.GetSelectedSeats();
        if (selectedSeats.Count != 1)
        {
            MessageBox.Show("Please select a single seat to rename.", "Rename Seat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        string newName = seatNameTextBox.Text.Trim();

        var target = selectedSeats[0];
        int index = _currentCells.FindIndex(c => c.RowIndex == target.RowIndex && c.ColIndex == target.ColIndex);
        if (index >= 0)
        {
            var cell = _currentCells[index];
            _currentCells[index] = new AudiLayoutCell
            {
                Id = cell.Id,
                AudiId = cell.AudiId,
                RowIndex = cell.RowIndex,
                ColIndex = cell.ColIndex,
                IsSeat = cell.IsSeat,
                RowLabel = cell.RowLabel,
                SeatNumber = newName,
                SeatClassId = cell.SeatClassId,
                SeatClassName = cell.SeatClassName,
                IsDamaged = cell.IsDamaged
            };

            RenderSeatMap();
            statusLabel.Text = string.IsNullOrEmpty(newName) ? "Cleared cell label." : $"Updated seat name to '{newName}'.";
            statusLabel.ForeColor = Color.DarkGreen;
        }
    }

    private void autoRenumberButton_Click(object sender, EventArgs e)
    {
        int rows = (int)rowsNumeric.Value;

        var updatedList = new List<AudiLayoutCell>(_currentCells.Count);

        for (int r = 0; r < rows; r++)
        {
            char rowChar = (char)('A' + (r % 26));
            string defaultRowLabel = rowChar.ToString();

            var rowCells = _currentCells
                .Where(c => c.RowIndex == r)
                .OrderBy(c => c.ColIndex)
                .ToList();

            int seatCounter = 1;
            foreach (var cell in rowCells)
            {
                string rowLabel = string.IsNullOrWhiteSpace(cell.RowLabel) ? defaultRowLabel : cell.RowLabel;
                string seatNum = cell.IsSeat ? $"{rowLabel}{seatCounter++}" : cell.SeatNumber;

                updatedList.Add(new AudiLayoutCell
                {
                    Id = cell.Id,
                    AudiId = cell.AudiId,
                    RowIndex = cell.RowIndex,
                    ColIndex = cell.ColIndex,
                    IsSeat = cell.IsSeat,
                    RowLabel = rowLabel,
                    SeatNumber = seatNum,
                    SeatClassId = cell.SeatClassId,
                    SeatClassName = cell.SeatClassName,
                    IsDamaged = cell.IsDamaged
                });
            }
        }

        _currentCells = updatedList;
        RenderSeatMap();
        statusLabel.Text = "Auto-renumbered active seats sequentially per row (preserved aisle labels & empty spaces).";
        statusLabel.ForeColor = Color.DarkGreen;
    }

    private void seatMapEditor_SeatDoubleClicked(object? sender, ScreeningSeat seat)
    {
        string? newName = PromptInput($"Enter new seat name for cell (Row {seat.RowIndex + 1}, Col {seat.ColIndex + 1}):", "Rename Seat", seat.SeatNumber);
        if (newName is not null)
        {
            newName = newName.Trim();
            int index = _currentCells.FindIndex(c => c.RowIndex == seat.RowIndex && c.ColIndex == seat.ColIndex);
            if (index >= 0)
            {
                var cell = _currentCells[index];
                _currentCells[index] = new AudiLayoutCell
                {
                    Id = cell.Id,
                    AudiId = cell.AudiId,
                    RowIndex = cell.RowIndex,
                    ColIndex = cell.ColIndex,
                    IsSeat = cell.IsSeat,
                    RowLabel = cell.RowLabel,
                    SeatNumber = newName,
                    SeatClassId = cell.SeatClassId,
                    SeatClassName = cell.SeatClassName,
                    IsDamaged = cell.IsDamaged
                };

                RenderSeatMap();
                statusLabel.Text = string.IsNullOrEmpty(newName) ? "Cleared cell label." : $"Renamed seat to '{newName}'.";
                statusLabel.ForeColor = Color.DarkGreen;
            }
        }
    }

    private static string? PromptInput(string prompt, string title, string defaultValue)
    {
        using var form = new Form
        {
            Width = 380,
            Height = 170,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            Text = title,
            StartPosition = FormStartPosition.CenterParent,
            MaximizeBox = false,
            MinimizeBox = false
        };
        var lbl = new Label { Left = 20, Top = 15, Width = 320, Text = prompt };
        var txt = new TextBox { Left = 20, Top = 45, Width = 320, Text = defaultValue };
        var btnOk = new Button { Text = "OK", Left = 150, Width = 90, Height = 35, Top = 85, DialogResult = DialogResult.OK };
        var btnCancel = new Button { Text = "Cancel", Left = 250, Width = 90, Height = 35, Top = 85, DialogResult = DialogResult.Cancel };

        form.Controls.Add(lbl);
        form.Controls.Add(txt);
        form.Controls.Add(btnOk);
        form.Controls.Add(btnCancel);
        form.AcceptButton = btnOk;
        form.CancelButton = btnCancel;

        AppTheme.ApplyTheme(form);
        AppTheme.ApplyPrimaryButton(btnOk);

        return form.ShowDialog() == DialogResult.OK ? txt.Text : null;
    }

    private async void saveButton_Click(object sender, EventArgs e)
    {
        if (_selectedAudi is null) return;

        try
        {
            saveButton.Enabled = false;
            int rows = (int)rowsNumeric.Value;
            int cols = (int)colsNumeric.Value;

            await _layoutService.SaveLayoutAsync(_selectedAudi.Id, rows, cols, _currentCells);
            statusLabel.Text = $"Successfully saved layout for {_selectedAudi.Name} ({_currentCells.Count(c => c.IsSeat)} seats).";
            statusLabel.ForeColor = Color.DarkGreen;
        }
        catch (Exception ex)
        {
            statusLabel.Text = $"Error saving layout: {ex.Message}";
            statusLabel.ForeColor = Color.DarkRed;
        }
        finally
        {
            saveButton.Enabled = true;
        }
    }

    private async void manageClassesButton_Click(object sender, EventArgs e)
    {
        using var form = new SeatClassForm(_seatClassRepository);
        form.ShowDialog(this);
        _seatClasses = await _layoutService.GetSeatClassesAsync();
        classComboBox.DataSource = _seatClasses.ToList();
    }

    private void classComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
