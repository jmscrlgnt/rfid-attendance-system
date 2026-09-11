using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JCGAttendanceSystem.Forms.Attendance;
using JCGAttendanceSystem.Forms.Dashboard;
using JCGAttendanceSystem.Forms.Records;
using JCGAttendanceSystem.Forms.Settings;
using JCGAttendanceSystem.Forms.Students;
using JCGAttendanceSystem.Security;
using JCGAttendanceSystem.Services;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Forms.Main
{
    public partial class MainShellForm : Form
    {
        private AuthService _auth;
        private readonly Dictionary<Button, string> _titles = new Dictionary<Button, string>();
        private Form _activePage;
        private Button _activeButton;

        public bool LoggedOut { get; private set; }

        public MainShellForm()
        {
            InitializeComponent();
            if (DesignTimeHelper.IsDesignMode) return;

            _auth = new AuthService();
            RegisterNavigation(_dashboardButton, "Dashboard");
            RegisterNavigation(_attendanceButton, "Attendance");
            RegisterNavigation(_studentsButton, "Students");
            RegisterNavigation(_recordsButton, "Records");
            RegisterNavigation(_settingsButton, "Settings");

            var user = SessionContext.CurrentUser;
            _signedInUserLabel.Text = (user?.Username ?? "Unknown") + Environment.NewLine + (user?.Role ?? string.Empty);
        }

        private void RegisterNavigation(Button button, string title)
        {
            _titles[button] = title;
            button.MouseEnter += NavButton_MouseEnter;
            button.MouseLeave += NavButton_MouseLeave;
        }

        private void MainShellForm_Shown(object sender, EventArgs e)
        {
            _dashboardButton.PerformClick();
        }

        private void DashboardButton_Click(object sender, EventArgs e) => Navigate(new DashboardForm(), _dashboardButton);
        private void AttendanceButton_Click(object sender, EventArgs e) => Navigate(new AttendanceForm(), _attendanceButton);
        private void StudentsButton_Click(object sender, EventArgs e) => Navigate(new StudentsForm(), _studentsButton);
        private void RecordsButton_Click(object sender, EventArgs e) => Navigate(new RecordsForm(), _recordsButton);
        private void SettingsButton_Click(object sender, EventArgs e) => Navigate(new SettingsForm(), _settingsButton);

        private void NavButton_MouseEnter(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null && button != _activeButton) button.BackColor = UiTheme.SidebarHover;
        }

        private void NavButton_MouseLeave(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null && button != _activeButton) button.BackColor = UiTheme.Sidebar;
        }

        public void Navigate(Form page, Button navButton)
        {
            if (page == null) return;
            _activePage?.Close();
            _activePage?.Dispose();
            if (_activeButton != null) _activeButton.BackColor = UiTheme.Sidebar;
            _activeButton = navButton;
            _activeButton.BackColor = UiTheme.SidebarActive;
            _pageTitle.Text = _titles.ContainsKey(navButton) ? _titles[navButton] : page.Text;

            page.TopLevel = false;
            page.FormBorderStyle = FormBorderStyle.None;
            page.Dock = DockStyle.Fill;
            _content.Controls.Clear();
            _content.Controls.Add(page);
            _activePage = page;
            page.Show();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Sign out of this session?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            _auth.Logout();
            LoggedOut = true;
            Close();
        }
    }
}
