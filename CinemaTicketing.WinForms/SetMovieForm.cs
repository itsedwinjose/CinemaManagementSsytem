using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;
using CinemaTicketing.Core.Services;
using CinemaTicketing.WinForms.UI.Design;

namespace CinemaTicketing.WinForms;

internal partial class SetMovieForm : Form
{
    private readonly IScreeningRepository _screeningRepository;
    private readonly IMovieService _movieService;
    private readonly ITheatreSettingsService _theatreSettingsService;
    private readonly long _cinemaId;

    private IReadOnlyList<CinemaInfo> _cinemas = Array.Empty<CinemaInfo>();
    private IReadOnlyList<ShowType> _showTypes = Array.Empty<ShowType>();
    private IReadOnlyList<Movie> _movies = Array.Empty<Movie>();
    private IReadOnlyList<Screening> _screenings = Array.Empty<Screening>();

    private long? _editingMovieId;

    public SetMovieForm(
        IScreeningRepository screeningRepository,
        IMovieService movieService,
        ITheatreSettingsService theatreSettingsService,
        long cinemaId = 1)
    {
        _screeningRepository = screeningRepository;
        _movieService = movieService;
        _theatreSettingsService = theatreSettingsService;
        _cinemaId = cinemaId;

        InitializeComponent();
        SetupMainGridColumns();
    }

    private async void SetMovieForm_Shown(object sender, EventArgs e)
    {
        if (Owner is not null)
        {
            Width = (int)(Owner.Width * 0.75);
            Height = (int)(Owner.Height * 0.75);
            CenterToParent();
        }

        await LoadLookupsAsync();
        await LoadScreeningsGridAsync();
        await LoadMovieMasterGridAsync();
    }

    private void SetupMainGridColumns()
    {
        mainGrid.Columns.Clear();

        mainGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Theatre", HeaderText = "Theatre", Width = 90, ReadOnly = true });
        mainGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ShowType", HeaderText = "Show Type", Width = 100, ReadOnly = true });
        mainGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ShowTime", HeaderText = "Show Time", Width = 85, ReadOnly = true });
        mainGrid.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Is3D", HeaderText = "3 D", Width = 45, ReadOnly = true });
        mainGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "MovieName", HeaderText = "Movie Name", Width = 160, ReadOnly = true });

        var selectCol = new DataGridViewCheckBoxColumn
        {
            Name = "Sele",
            HeaderText = "Sele",
            Width = 45
        };
        mainGrid.Columns.Add(selectCol);

        mainGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Class1", HeaderText = "Class1", Width = 60, ReadOnly = true });
        mainGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Class2", HeaderText = "Class2", Width = 60, ReadOnly = true });
        mainGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Class3", HeaderText = "Class3", Width = 60, ReadOnly = true });
        mainGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Class4", HeaderText = "Class4", Width = 60, ReadOnly = true });
    }

    private async Task LoadLookupsAsync()
    {
        _cinemas = await _theatreSettingsService.GetCinemasAsync();
        _showTypes = await _theatreSettingsService.GetShowTypesAsync();
        _movies = await _movieService.GetMoviesAsync();

        var cinemaList = new List<CinemaInfo> { new CinemaInfo { Id = 0, Name = "-- All --" } };
        cinemaList.AddRange(_cinemas);
        theatreComboBox.DataSource = cinemaList;
        theatreComboBox.DisplayMember = nameof(CinemaInfo.Name);
        theatreComboBox.ValueMember = nameof(CinemaInfo.Id);

        var showTypeList = new List<ShowType> { new ShowType { Id = 0, Name = "-- All --" } };
        showTypeList.AddRange(_showTypes);
        showTypeComboBox.DataSource = showTypeList;
        showTypeComboBox.DisplayMember = nameof(ShowType.Name);
        showTypeComboBox.ValueMember = nameof(ShowType.Id);

        movieComboBox.DataSource = _movies.ToList();
        movieComboBox.DisplayMember = nameof(Movie.Name);
        movieComboBox.ValueMember = nameof(Movie.Id);
    }

    private async Task LoadScreeningsGridAsync()
    {
        var date = DateOnly.FromDateTime(datePicker.Value);
        dateHeaderLabel.Text = date.ToString("dd-MM-yyyy");

        _screenings = await _screeningRepository.GetScreeningsAsync(_cinemaId, date);

        var selectedTheatreId = theatreComboBox.SelectedValue is long tId ? tId : 0;
        var selectedShowTypeId = showTypeComboBox.SelectedValue is long stId ? stId : 0;

        var filtered = _screenings.AsEnumerable();
        if (selectedTheatreId > 0)
        {
            filtered = filtered.Where(s => s.CinemaId == selectedTheatreId);
        }
        if (selectedShowTypeId > 0)
        {
            filtered = filtered.Where(s => s.ShowTypeId == selectedShowTypeId);
        }

        mainGrid.Rows.Clear();
        foreach (var s in filtered)
        {
            int rowIndex = mainGrid.Rows.Add(
                s.AudiName,
                s.ShowTypeName,
                DateTime.Today.Add(s.ShowTime).ToString("hh:mmtt").ToUpper(),
                s.IsMovie3D,
                s.MovieName.ToUpper(),
                false,
                $"{s.DefaultPrice:F0}",
                "0",
                "0",
                "0"
            );
            mainGrid.Rows[rowIndex].Tag = s;
        }

        statusLabel.Text = $"Loaded {mainGrid.Rows.Count} show screenings for {date:dd-MM-yyyy}.";
    }

    private async Task LoadMovieMasterGridAsync()
    {
        _movies = await _movieService.GetMoviesAsync();
        masterGrid.AutoGenerateColumns = false;
        masterGrid.DataSource = _movies.Select(m => new
        {
            m.Id,
            Movie = m.Name,
            Is3D = m.Is3D
        }).ToList();
    }

    private async void Filter_Changed(object sender, EventArgs e)
    {
        await LoadScreeningsGridAsync();
    }

    private async void showAllButton_Click(object sender, EventArgs e)
    {
        if (theatreComboBox.Items.Count > 0) theatreComboBox.SelectedIndex = 0;
        if (showTypeComboBox.Items.Count > 0) showTypeComboBox.SelectedIndex = 0;
        await LoadScreeningsGridAsync();
    }

    private async void setSameMovieButton_Click(object sender, EventArgs e)
    {
        if (movieComboBox.SelectedItem is not Movie selectedMovie)
        {
            statusLabel.Text = "Please select a movie to set.";
            statusLabel.ForeColor = Color.DarkRed;
            return;
        }

        var date = DateOnly.FromDateTime(datePicker.Value);
        int updateCount = 0;

        foreach (DataGridViewRow row in mainGrid.Rows)
        {
            bool isChecked = Convert.ToBoolean(row.Cells["Sele"].Value);
            if (isChecked && row.Tag is Screening screening)
            {
                await _screeningRepository.SetMovieAssignmentAsync(
                    screening.CinemaId,
                    date,
                    screening.AudiId,
                    screening.ShowTypeId,
                    screening.ShowTime,
                    selectedMovie.Id);
                updateCount++;
            }
        }

        if (updateCount == 0)
        {
            statusLabel.Text = "No rows selected. Please check the 'Sele' box on rows to assign movie.";
            statusLabel.ForeColor = Color.DarkOrange;
            return;
        }

        statusLabel.Text = $"Successfully assigned '{selectedMovie.Name}' to {updateCount} screenings.";
        statusLabel.ForeColor = Color.DarkGreen;
        await LoadScreeningsGridAsync();
    }

    private async void mAddButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(mNameTextBox.Text))
        {
            statusLabel.Text = "Enter movie name.";
            statusLabel.ForeColor = Color.DarkRed;
            return;
        }

        var movie = new Movie
        {
            Id = _editingMovieId ?? 0,
            Name = mNameTextBox.Text.Trim(),
            Is3D = m3dCheckBox.Checked,
            IsActive = true
        };

        await _movieService.SaveMovieAsync(movie);
        statusLabel.Text = _editingMovieId.HasValue ? "Movie updated." : "Movie added.";
        statusLabel.ForeColor = Color.DarkGreen;

        ResetMasterForm();
        await LoadMovieMasterGridAsync();
        await LoadLookupsAsync();
    }

    private async void masterGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        var row = masterGrid.Rows[e.RowIndex];
        if (row.Cells["mgMovieColumn"].Value is not null && masterGrid.Columns[e.ColumnIndex].Name == "mgDeleteColumn")
        {
            if (_movies.Count > e.RowIndex)
            {
                var movie = _movies[e.RowIndex];
                await _movieService.DeleteMovieAsync(movie.Id);
                statusLabel.Text = $"Deleted movie '{movie.Name}'.";
                statusLabel.ForeColor = Color.DarkGreen;

                ResetMasterForm();
                await LoadMovieMasterGridAsync();
                await LoadLookupsAsync();
            }
        }
    }

    private void masterGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var row = masterGrid.Rows[e.RowIndex];

        if (row.Cells["mgMovieColumn"].Value is not null)
        {
            if (_movies.Count > e.RowIndex)
            {
                var movie = _movies[e.RowIndex];
                _editingMovieId = movie.Id;
                mNameTextBox.Text = movie.Name;
                m3dCheckBox.Checked = movie.Is3D;
                mAddButton.Text = "Update";
                statusLabel.Text = $"Editing '{movie.Name}'.";
                statusLabel.ForeColor = Color.FromArgb(30, 30, 30);
            }
        }
    }

    private void ResetMasterForm()
    {
        _editingMovieId = null;
        mNameTextBox.Clear();
        m3dCheckBox.Checked = false;
        mAddButton.Text = "Add";
    }
}
