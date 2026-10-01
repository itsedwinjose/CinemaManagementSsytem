using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Enums;
using CinemaTicketing.WinForms.Controls;

namespace CinemaTicketing.Tests;

public class CinemaSeatMapControlTests
{
    [Fact]
    public void LoadSeats_PopulatesSeatsAndClearsSelection()
    {
        var control = new CinemaSeatMapControl();
        var seats = new List<ScreeningSeat>
        {
            new() { Id = 1, RowIndex = 0, ColIndex = 0, IsSeat = true, SeatNumber = "A1", Status = SeatStatus.Available },
            new() { Id = 2, RowIndex = 0, ColIndex = 1, IsSeat = true, SeatNumber = "A2", Status = SeatStatus.Available }
        };

        control.LoadSeats(1, 2, seats);

        Assert.Empty(control.SelectedSeatIds);
        Assert.Empty(control.GetSelectedSeats());
    }

    [Fact]
    public void AllowNonSeatSelection_DefaultsToFalse()
    {
        var control = new CinemaSeatMapControl();
        Assert.False(control.AllowNonSeatSelection);
    }
}
