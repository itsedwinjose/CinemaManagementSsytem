namespace CinemaTicketing.WinForms;

internal sealed class TicketPreviewDialog : Form
{
    public TicketPreviewDialog(string ticketText)
    {
        Text = "Ticket Preview";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MinimizeBox = false;
        MaximizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(440, 620);

        var ticketTextBox = new TextBox
        {
            Multiline = true,
            ReadOnly = true,
            ScrollBars = ScrollBars.Vertical,
            WordWrap = false,
            Font = new Font(FontFamily.GenericMonospace, 10F),
            Dock = DockStyle.Fill,
            Text = ticketText
        };

        var closeButton = new Button
        {
            Text = "Done",
            DialogResult = DialogResult.OK,
            Dock = DockStyle.Bottom,
            Height = 42
        };

        Controls.Add(ticketTextBox);
        Controls.Add(closeButton);
        AcceptButton = closeButton;
    }
}
