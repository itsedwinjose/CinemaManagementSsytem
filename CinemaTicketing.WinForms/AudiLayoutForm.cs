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
        AppTheme.ApplyTheme(this);
        AppTheme.ApplyPrimaryButton(saveButton);
        AppTheme.ApplyTealButton(resizeButton);
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
    }

    private void seatMapEditor_SelectionChanged(object sender, EventArgs e)
    {
        var selectedScreeningSeats = seatMapEditor.GetSelectedSeats();
        if (selectedScreeningSeats.Count == 0) return;

        bool isSeatVal = markSeatRadio.Checked;
        bool isNonSeatVal = markNonSeatRadio.Checked;
        bool isAssignClass = assignClassRadio.Checked;
        bool isToggleDamaged = toggleDamagedRadio.Checked;

        SeatClass? selectedClass = classComboBox.SelectedItem as SeatClass;

        var cellDict = _currentCells.ToDictionary(c => (c.RowIndex, c.ColIndex));

        foreach (var ss in selectedScreeningSeats)
        {
            if (cellDict.TryGetValue((ss.RowIndex, ss.ColIndex), out var cell))
            {
                int index = _currentCells.IndexOf(cell);
                if (index < 0) continue;

                if (isSeatVal)
                {
                    _currentCells[index] = new AudiLayoutCell
                    {
                        Id = cell.Id,
                        AudiId = cell.AudiId,
                        RowIndex = cell.RowIndex,
                        ColIndex = cell.ColIndex,
                        IsSeat = true,
                        RowLabel = cell.RowLabel,
                        SeatNumber = cell.SeatNumber,
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
                        SeatNumber = cell.SeatNumber,
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
