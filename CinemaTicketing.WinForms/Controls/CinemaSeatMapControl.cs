using System.ComponentModel;
using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Enums;
using CinemaTicketing.WinForms.UI.Design;

namespace CinemaTicketing.WinForms.Controls;

public sealed class CinemaSeatMapControl : Control
{
    private readonly HashSet<long> _selectedSeatIds = new();
    private IReadOnlyList<ScreeningSeat> _seats = Array.Empty<ScreeningSeat>();
    private int _totalRows = 10;
    private int _totalCols = 15;

    private bool _isDragging;
    private Point _dragStartPoint;
    private Rectangle _dragRect;

    public CinemaSeatMapControl()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw, true);
        DoubleBuffered = true;
        BackColor = AppColors.FormBackground;
    }

    [Browsable(false)]
    public IReadOnlySet<long> SelectedSeatIds => _selectedSeatIds;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowNonSeatSelection { get; set; }

    public event EventHandler? SelectionChanged;
    public event EventHandler<ScreeningSeat>? SeatClicked;
    public event EventHandler<ScreeningSeat>? SeatDoubleClicked;

    public void LoadSeats(int totalRows, int totalCols, IEnumerable<ScreeningSeat> seats)
    {
        _totalRows = Math.Max(1, totalRows);
        _totalCols = Math.Max(1, totalCols);
        _seats = seats.ToList();
        _selectedSeatIds.Clear();
        Invalidate();
        SelectionChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ClearSelection()
    {
        if (_selectedSeatIds.Count > 0)
        {
            _selectedSeatIds.Clear();
            Invalidate();
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public IReadOnlyList<ScreeningSeat> GetSelectedSeats()
    {
        return _seats.Where(s => _selectedSeatIds.Contains(s.Id)).ToList();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

        // Always render the grid based on configured rows/cols.
        // If no seat data is available for a screening, cells will be
        // treated as non-seat (empty) so the physical layout is still visible.

        const float margin = 5f;

        float availableW = Width - (margin * 2);
        float availableH = Height - (margin * 2);

        float cellW = Math.Max(15f, availableW / _totalCols);
        float cellH = Math.Max(15f, availableH / _totalRows);

        var seatLookup = _seats
            .GroupBy(s => (s.RowIndex, s.ColIndex))
            .ToDictionary(g => g.Key, g => g.First());

        using var centerSf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        // Draw Cells
        for (int r = 0; r < _totalRows; r++)
        {
            for (int c = 0; c < _totalCols; c++)
            {
                float x = margin + (c * cellW);
                float y = margin + (r * cellH);
                var cellRect = new RectangleF(x + 1, y + 1, cellW - 2, cellH - 2);

                if (!seatLookup.TryGetValue((r, c), out var seat))
                {
                    // Fallback seat object if no explicit entity exists in database
                    char rName = (char)('A' + (r % 26));
                    seat = new ScreeningSeat
                    {
                        Id = (r * 1000) + c + 1,
                        RowIndex = r,
                        ColIndex = c,
                        IsSeat = true,
                        RowLabel = rName.ToString(),
                        SeatNumber = $"{rName}{c + 1}",
                        Status = SeatStatus.Available
                    };
                }

                if (!seat.IsSeat)
                {
                    // Explicit Non-seat cell (Stair / Empty Space)
                    bool isSelectedNonSeat = _selectedSeatIds.Contains(seat.Id);
                    using var nonSeatBrush = new SolidBrush(isSelectedNonSeat ? SeatStatusStyles.SelectionFill : SeatStatusStyles.NonSeat);
                    g.FillRectangle(nonSeatBrush, cellRect);

                    using var nonSeatPen = new Pen(isSelectedNonSeat ? SeatStatusStyles.SelectionBorder : Color.FromArgb(0, 130, 200), isSelectedNonSeat ? 2f : 1f);
                    g.DrawRectangle(nonSeatPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);

                    // Render seat.SeatNumber ONLY if explicitly set (e.g. Aisle Row Letter like 'A', 'B', 'D').
                    // If seat.SeatNumber is empty, draw NOTHING (clean solid blue cell).
                    if (!string.IsNullOrWhiteSpace(seat.SeatNumber) && cellW >= 14 && cellH >= 12)
                    {
                        var textColor = isSelectedNonSeat ? Color.Black : Color.White;
                        using var textBrush = new SolidBrush(textColor);
                        var font = cellW < 25 ? AppFonts.SmallText : AppFonts.SeatLabel;
                        g.DrawString(seat.SeatNumber, font, textBrush, cellRect, centerSf);
                    }
                    continue;
                }

                bool isSelected = _selectedSeatIds.Contains(seat.Id);
                var statusColor = SeatStatusStyles.GetStatusColor(seat.Status, seat.IsDamaged);

                using (var fillBrush = new SolidBrush(isSelected ? SeatStatusStyles.SelectionFill : statusColor))
                {
                    g.FillRectangle(fillBrush, cellRect);
                }

                // Border
                using (var borderPen = new Pen(isSelected ? SeatStatusStyles.SelectionBorder : AppColors.BorderColor, isSelected ? 2f : 1f))
                {
                    g.DrawRectangle(borderPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                }

                // Seat Number Text
                if (cellW >= 18 && cellH >= 14)
                {
                    var textColor = isSelected ? Color.Black : (statusColor == SeatStatusStyles.Available ? AppColors.TextDark : Color.White);
                    using var textBrush = new SolidBrush(textColor);
                    var font = cellW < 25 ? AppFonts.SmallText : AppFonts.SeatLabel;
                    g.DrawString(seat.SeatNumber, font, textBrush, cellRect, centerSf);
                }
            }
        }

        // 3. Draw Drag Selection Rectangle if dragging
        if (_isDragging && _dragRect.Width > 0 && _dragRect.Height > 0)
        {
            using var dragPen = new Pen(Color.Navy, 1.5f) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
            using var dragFill = new SolidBrush(Color.FromArgb(40, 0, 120, 215));
            g.FillRectangle(dragFill, _dragRect);
            g.DrawRectangle(dragPen, _dragRect);
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button == MouseButtons.Left)
        {
            _isDragging = true;
            _dragStartPoint = e.Location;
            _dragRect = new Rectangle(e.Location, Size.Empty);
            Invalidate();
        }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (_isDragging)
        {
            int x = Math.Min(_dragStartPoint.X, e.X);
            int y = Math.Min(_dragStartPoint.Y, e.Y);
            int w = Math.Abs(_dragStartPoint.X - e.X);
            int h = Math.Abs(_dragStartPoint.Y - e.Y);
            _dragRect = new Rectangle(x, y, w, h);
            Invalidate();
        }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        if (e.Button == MouseButtons.Left && _isDragging)
        {
            _isDragging = false;

            if (_dragRect.Width < 5 && _dragRect.Height < 5)
            {
                // Single Click
                var seat = HitTestSeat(_dragStartPoint) ?? HitTestSeat(e.Location);
                if (seat is not null)
                {
                    ToggleSeatSelection(seat);
                    SeatClicked?.Invoke(this, seat);
                }
            }
            else
            {
                // Drag Selection
                SelectSeatsInRectangle(_dragRect);
            }

            _dragRect = Rectangle.Empty;
            Invalidate();
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
        base.OnMouseDoubleClick(e);
        if (e.Button == MouseButtons.Left)
        {
            var seat = HitTestSeat(e.Location);
            if (seat is not null)
            {
                SeatDoubleClicked?.Invoke(this, seat);
            }
        }
    }

    private ScreeningSeat? HitTestSeat(Point p)
    {
        const float margin = 5f;

        if (p.X < margin || p.Y < margin)
        {
            return null;
        }

        float availableW = Width - (margin * 2);
        float availableH = Height - (margin * 2);

        if (availableW <= 0 || availableH <= 0) return null;

        float cellW = Math.Max(15f, availableW / _totalCols);
        float cellH = Math.Max(15f, availableH / _totalRows);

        int col = (int)((p.X - margin) / cellW);
        int row = (int)((p.Y - margin) / cellH);

        if (row >= 0 && row < _totalRows && col >= 0 && col < _totalCols)
        {
            var seat = _seats.FirstOrDefault(s => s.RowIndex == row && s.ColIndex == col);
            if (seat is null)
            {
                char rName = (char)('A' + (row % 26));
                seat = new ScreeningSeat
                {
                    Id = (row * 1000) + col + 1,
                    RowIndex = row,
                    ColIndex = col,
                    IsSeat = true,
                    RowLabel = rName.ToString(),
                    SeatNumber = $"{rName}{col + 1}",
                    Status = SeatStatus.Available
                };
            }
            return seat;
        }

        return null;
    }

    private void ToggleSeatSelection(ScreeningSeat seat)
    {
        if (seat.IsDamaged || seat.Status == SeatStatus.Sold)
        {
            return;
        }

        if (!seat.IsSeat && !AllowNonSeatSelection)
        {
            return;
        }

        if (!_selectedSeatIds.Remove(seat.Id))
        {
            _selectedSeatIds.Add(seat.Id);
        }
    }

    private void SelectSeatsInRectangle(Rectangle rect)
    {
        var rectF = (RectangleF)rect;
        foreach (var seat in _seats)
        {
            if (seat.IsDamaged || seat.Status == SeatStatus.Sold)
            {
                continue;
            }

            if (!seat.IsSeat && !AllowNonSeatSelection)
            {
                continue;
            }

            var cellRect = GetSeatRectangle(seat.RowIndex, seat.ColIndex);
            if (rectF.IntersectsWith(cellRect))
            {
                _selectedSeatIds.Add(seat.Id);
            }
        }
    }

    private RectangleF GetSeatRectangle(int rowIndex, int colIndex)
    {
        const float margin = 5f;

        float availableW = Width - (margin * 2);
        float availableH = Height - (margin * 2);

        float cellW = Math.Max(15f, availableW / _totalCols);
        float cellH = Math.Max(15f, availableH / _totalRows);

        float x = margin + (colIndex * cellW);
        float y = margin + (rowIndex * cellH);

        return new RectangleF(x + 1, y + 1, cellW - 2, cellH - 2);
    }
}
