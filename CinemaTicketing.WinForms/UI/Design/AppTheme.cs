namespace CinemaTicketing.WinForms.UI.Design;

public static class AppTheme
{
    public static void ApplyTheme(Form form)
    {
        form.Font = AppFonts.DefaultText;
        form.BackColor = AppColors.FormBackground;
        ApplyControlStyles(form.Controls);
    }

    private static void ApplyControlStyles(Control.ControlCollection controls)
    {
        foreach (Control control in controls)
        {
            if (control is Button btn)
            {
                ApplyButtonDefaultStyle(btn);
            }
            else if (control is DataGridView dgv)
            {
                ApplyGridDefaultStyle(dgv);
            }
            else if (control is Panel panel)
            {
                ApplyControlStyles(panel.Controls);
            }
            else if (control is GroupBox gb)
            {
                gb.Font = AppFonts.BoldText;
                ApplyControlStyles(gb.Controls);
            }
            else if (control is TabControl tabControl)
            {
                tabControl.Font = AppFonts.DefaultText;
                foreach (TabPage page in tabControl.TabPages)
                {
                    page.BackColor = AppColors.FormBackground;
                    ApplyControlStyles(page.Controls);
                }
            }
        }
    }

    public static void ApplyPrimaryButton(Button button)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 1;
        button.FlatAppearance.BorderColor = AppColors.PrimaryButton;
        button.BackColor = AppColors.PrimaryButton;
        button.ForeColor = AppColors.ButtonText;
        button.Font = AppFonts.BoldText;
    }

    public static void ApplySuccessButton(Button button)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 1;
        button.FlatAppearance.BorderColor = AppColors.SuccessButton;
        button.BackColor = AppColors.SuccessButton;
        button.ForeColor = AppColors.ButtonText;
        button.Font = AppFonts.BoldText;
    }

    public static void ApplyDestructiveButton(Button button)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 1;
        button.FlatAppearance.BorderColor = AppColors.DestructiveButton;
        button.BackColor = AppColors.DestructiveButton;
        button.ForeColor = AppColors.ButtonText;
        button.Font = AppFonts.BoldText;
    }

    public static void ApplyTealButton(Button button)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 1;
        button.FlatAppearance.BorderColor = AppColors.ActionTealButton;
        button.BackColor = AppColors.ActionTealButton;
        button.ForeColor = AppColors.ButtonText;
        button.Font = AppFonts.BoldText;
    }

    public static void ApplyButtonDefaultStyle(Button button)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 1;
        button.FlatAppearance.BorderColor = AppColors.BorderColor;
        button.Font = AppFonts.BoldText;
    }

    public static void ApplyGridDefaultStyle(DataGridView grid)
    {
        grid.EnableHeadersVisualStyles = false;
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.Fixed3D;
        grid.ColumnHeadersDefaultCellStyle.BackColor = AppColors.GridHeaderBackground;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = AppColors.GridHeaderText;
        grid.ColumnHeadersDefaultCellStyle.Font = AppFonts.BoldText;
        grid.DefaultCellStyle.Font = AppFonts.DefaultText;
        grid.DefaultCellStyle.SelectionBackColor = AppColors.GridSelection;
        grid.DefaultCellStyle.SelectionForeColor = AppColors.TextDark;
        grid.RowHeadersVisible = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
    }
}
