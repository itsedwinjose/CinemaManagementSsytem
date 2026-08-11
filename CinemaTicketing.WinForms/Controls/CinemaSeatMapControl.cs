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

    public event EventHandler? SelectionChanged;
    public event EventHandler<ScreeningSeat>? SeatClicked;

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

        if (_seats.Count == 0)
        {
            var font = AppFonts.BoldText;
            using var brush = new SolidBrush(AppColors.TextMuted);
            g.DrawString("No layout or screening selected.", font, brush, 20, 20);
            return;
        }

        const float rowHeaderWidth = 25f;
        const float margin = 5f;

        float availableW = Width - rowHeaderWidth - (margin * 2);
        float availableH = Height - (margin * 2);

        float cellW = Math.Max(15f, availableW / _totalCols);
        float cellH = Math.Max(15f, availableH / _totalRows);

        var seatLookup = _seats.ToDictionary(s => (s.RowIndex, s.ColIndex));

        using var centerSf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        // 1. Draw Row Labels
        var labelFont = AppFonts.SmallBold;
        using (var labelBrush = new SolidBrush(AppColors.TextDark))
        {
            for (int r = 0; r < _totalRows; r++)
            {
                float y = margin + (r * cellH);
                string rowName = ((char)('A' + (r % 26))).ToString();
                g.DrawString(rowName, labelFont, labelBrush, new RectangleF(margin, y, rowHeaderWidth - 5, cellH), centerSf);
            }
        }

        // 2. Draw Cells
        for (int r = 0; r < _totalRows; r++)
        {
            for (int c = 0; c < _totalCols; c++)
            {
                float x = margin + rowHeaderWidth + (c * cellW);
                float y = margin + (r * cellH);
                var cellRect = new RectangleF(x + 1, y + 1, cellW - 2, cellH - 2);

                if (!seatLookup.TryGetValue((r, c), out var seat) || !seat.IsSeat)
                {
                    // Non-seat cell
                    using var nonSeatBrush = new SolidBrush(SeatStatusStyles.NonSeat);
                    g.FillRectangle(nonSeatBrush, cellRect);
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
                var seat = HitTestSeat(e.Location);
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

    private ScreeningSeat? HitTestSeat(Point p)
    {
        const float rowHeaderWidth = 25f;
        const float margin = 5f;

        float availableW = Width - rowHeaderWidth - (margin * 2);
        float availableH = Height - (margin * 2);

        float cellW = Math.Max(15f, availableW / _totalCols);
        float cellH = Math.Max(15f, availableH / _totalRows);

        int col = (int)((p.X - margin - rowHeaderWidth) / cellW);
        int row = (int)((p.Y - margin) / cellH);

        if (row >= 0 && row < _totalRows && col >= 0 && col < _totalCols)
        {
            return _seats.FirstOrDefault(s => s.RowIndex == row && s.ColIndex == col);
        }

        return null;
    }

    private void ToggleSeatSelection(ScreeningSeat seat)
    {
        if (!seat.IsSeat || seat.IsDamaged || seat.Status != SeatStatus.Available)
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
        foreach (var seat in _seats)
        {
            if (!seat.IsSeat || seat.IsDamaged || seat.Status != SeatStatus.Available)
            {
                continue;
            }

            var pt = GetSeatCenterPoint(seat);
            if (rect.Contains(pt))
            {
                _selectedSeatIds.Add(seat.Id);
            }
        }
    }

    private Point GetSeatCenterPoint(ScreeningSeat seat)
    {
        const float rowHeaderWidth = 25f;
        const float margin = 5f;

        float availableW = Width - rowHeaderWidth - (margin * 2);
        float availableH = Height - (margin * 2);

        float cellW = Math.Max(15f, availableW / _totalCols);
        float cellH = Math.Max(15f, availableH / _totalRows);

        float cx = margin + rowHeaderWidth + (seat.ColIndex * cellW) + (cellW / 2);
        float cy = margin + (seat.RowIndex * cellH) + (cellH / 2);

        return new Point((int)cx, (int)cy);
    }
}
