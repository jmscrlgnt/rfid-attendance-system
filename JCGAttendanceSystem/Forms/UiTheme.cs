using System.Drawing;
using System.Windows.Forms;

namespace JCGAttendanceSystem.Forms
{
    internal static class UiTheme
    {
        public static readonly Color Sidebar = Color.FromArgb(11, 31, 51);
        public static readonly Color SidebarHover = Color.FromArgb(19, 51, 79);
        public static readonly Color SidebarActive = Color.FromArgb(24, 66, 101);
        public static readonly Color Accent = Color.FromArgb(30, 136, 229);
        public static readonly Color AccentDark = Color.FromArgb(21, 101, 192);
        public static readonly Color Success = Color.FromArgb(46, 125, 50);
        public static readonly Color Warning = Color.FromArgb(245, 124, 0);
        public static readonly Color Danger = Color.FromArgb(198, 40, 40);
        public static readonly Color Background = Color.FromArgb(244, 247, 250);
        public static readonly Color Card = Color.White;
        public static readonly Color Border = Color.FromArgb(222, 228, 235);
        public static readonly Color Text = Color.FromArgb(31, 41, 55);
        public static readonly Color Muted = Color.FromArgb(107, 114, 128);

        public static Font Font(float size = 10f, FontStyle style = FontStyle.Regular)
        {
            return new Font("Segoe UI", size, style, GraphicsUnit.Point);
        }

        public static Button PrimaryButton(string text)
        {
            return new Button
            {
                Text = text,
                Height = 40,
                AutoSize = false,
                FlatStyle = FlatStyle.Flat,
                BackColor = Accent,
                ForeColor = Color.White,
                Font = Font(10f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };
        }

        public static Button SecondaryButton(string text)
        {
            return new Button
            {
                Text = text,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Text,
                Font = Font(10f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderColor = Border, BorderSize = 1 }
            };
        }

        public static Button DangerButton(string text)
        {
            var button = PrimaryButton(text);
            button.BackColor = Danger;
            return button;
        }

        public static Label Title(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = Font(20f, FontStyle.Bold),
                ForeColor = Text
            };
        }

        public static Label MutedLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = Font(9.5f),
                ForeColor = Muted
            };
        }

        public static TextBox TextBox(bool password = false)
        {
            return new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Font = Font(11f),
                Height = 32,
                UseSystemPasswordChar = password
            };
        }

        public static Panel CardPanel()
        {
            return new Panel
            {
                BackColor = Card,
                Padding = new Padding(18),
                Margin = new Padding(8),
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        public static void ConfigureGrid(DataGridView grid)
        {
            grid.BackgroundColor = Card;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Border;
            grid.RowHeadersVisible = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AutoGenerateColumns = false;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(238, 243, 248);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Text;
            grid.ColumnHeadersDefaultCellStyle.Font = Font(9.5f, FontStyle.Bold);
            grid.DefaultCellStyle.Font = Font(9.5f);
            grid.DefaultCellStyle.ForeColor = Text;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(222, 235, 247);
            grid.DefaultCellStyle.SelectionForeColor = Text;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            grid.ColumnHeadersHeight = 36;
            grid.RowTemplate.Height = 34;
        }
    }
}
