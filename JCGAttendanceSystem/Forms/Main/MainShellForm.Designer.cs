using System.Drawing;
using System.Windows.Forms;

namespace JCGAttendanceSystem.Forms.Main
{
    partial class MainShellForm
    {
        private Panel _sidebar;
        private Label _logoLabel;
        private FlowLayoutPanel _navPanel;
        private Button _dashboardButton;
        private Button _attendanceButton;
        private Button _studentsButton;
        private Button _recordsButton;
        private Button _settingsButton;
        private Panel _userPanel;
        private Label _signedInUserLabel;
        private Button _logoutButton;
        private Panel _header;
        private Label _pageTitle;
        private Panel _content;

        private void InitializeComponent()
        {
            this._sidebar = new Panel();
            this._logoLabel = new Label();
            this._navPanel = new FlowLayoutPanel();
            this._dashboardButton = new Button();
            this._attendanceButton = new Button();
            this._studentsButton = new Button();
            this._recordsButton = new Button();
            this._settingsButton = new Button();
            this._userPanel = new Panel();
            this._signedInUserLabel = new Label();
            this._logoutButton = new Button();
            this._header = new Panel();
            this._pageTitle = new Label();
            this._content = new Panel();
            this._sidebar.SuspendLayout();
            this._navPanel.SuspendLayout();
            this._userPanel.SuspendLayout();
            this._header.SuspendLayout();
            this.SuspendLayout();
            // sidebar
            this._sidebar.BackColor = Color.FromArgb(11, 31, 51);
            this._sidebar.Controls.Add(this._userPanel);
            this._sidebar.Controls.Add(this._navPanel);
            this._sidebar.Controls.Add(this._logoLabel);
            this._sidebar.Dock = DockStyle.Left;
            this._sidebar.Location = new Point(0, 0);
            this._sidebar.Name = "_sidebar";
            this._sidebar.Padding = new Padding(0, 24, 0, 18);
            this._sidebar.Size = new Size(238, 760);
            // logo
            this._logoLabel.Dock = DockStyle.Top;
            this._logoLabel.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            this._logoLabel.ForeColor = Color.White;
            this._logoLabel.Location = new Point(0, 24);
            this._logoLabel.Name = "_logoLabel";
            this._logoLabel.Padding = new Padding(20, 8, 0, 0);
            this._logoLabel.Size = new Size(238, 92);
            this._logoLabel.Text = "JCG\r\nATTENDANCE";
            this._logoLabel.TextAlign = ContentAlignment.MiddleLeft;
            // nav
            this._navPanel.AutoSize = true;
            this._navPanel.Controls.Add(this._dashboardButton);
            this._navPanel.Controls.Add(this._attendanceButton);
            this._navPanel.Controls.Add(this._studentsButton);
            this._navPanel.Controls.Add(this._recordsButton);
            this._navPanel.Controls.Add(this._settingsButton);
            this._navPanel.Dock = DockStyle.Top;
            this._navPanel.FlowDirection = FlowDirection.TopDown;
            this._navPanel.Location = new Point(0, 116);
            this._navPanel.Name = "_navPanel";
            this._navPanel.Padding = new Padding(12, 18, 12, 0);
            this._navPanel.Size = new Size(238, 300);
            this._navPanel.WrapContents = false;
            // dashboard button
            this._dashboardButton.BackColor = Color.FromArgb(11, 31, 51);
            this._dashboardButton.Cursor = Cursors.Hand;
            this._dashboardButton.FlatAppearance.BorderSize = 0;
            this._dashboardButton.FlatStyle = FlatStyle.Flat;
            this._dashboardButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this._dashboardButton.ForeColor = Color.FromArgb(216, 225, 233);
            this._dashboardButton.Margin = new Padding(0, 0, 0, 6);
            this._dashboardButton.Name = "_dashboardButton";
            this._dashboardButton.Size = new Size(214, 48);
            this._dashboardButton.Text = "  Dashboard";
            this._dashboardButton.TextAlign = ContentAlignment.MiddleLeft;
            this._dashboardButton.UseVisualStyleBackColor = false;
            this._dashboardButton.Click += new System.EventHandler(this.DashboardButton_Click);
            // attendance button
            this._attendanceButton.BackColor = Color.FromArgb(11, 31, 51);
            this._attendanceButton.Cursor = Cursors.Hand;
            this._attendanceButton.FlatAppearance.BorderSize = 0;
            this._attendanceButton.FlatStyle = FlatStyle.Flat;
            this._attendanceButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this._attendanceButton.ForeColor = Color.FromArgb(216, 225, 233);
            this._attendanceButton.Margin = new Padding(0, 0, 0, 6);
            this._attendanceButton.Name = "_attendanceButton";
            this._attendanceButton.Size = new Size(214, 48);
            this._attendanceButton.Text = "  Attendance";
            this._attendanceButton.TextAlign = ContentAlignment.MiddleLeft;
            this._attendanceButton.UseVisualStyleBackColor = false;
            this._attendanceButton.Click += new System.EventHandler(this.AttendanceButton_Click);
            // students button
            this._studentsButton.BackColor = Color.FromArgb(11, 31, 51);
            this._studentsButton.Cursor = Cursors.Hand;
            this._studentsButton.FlatAppearance.BorderSize = 0;
            this._studentsButton.FlatStyle = FlatStyle.Flat;
            this._studentsButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this._studentsButton.ForeColor = Color.FromArgb(216, 225, 233);
            this._studentsButton.Margin = new Padding(0, 0, 0, 6);
            this._studentsButton.Name = "_studentsButton";
            this._studentsButton.Size = new Size(214, 48);
            this._studentsButton.Text = "  Students";
            this._studentsButton.TextAlign = ContentAlignment.MiddleLeft;
            this._studentsButton.UseVisualStyleBackColor = false;
            this._studentsButton.Click += new System.EventHandler(this.StudentsButton_Click);
            // records button
            this._recordsButton.BackColor = Color.FromArgb(11, 31, 51);
            this._recordsButton.Cursor = Cursors.Hand;
            this._recordsButton.FlatAppearance.BorderSize = 0;
            this._recordsButton.FlatStyle = FlatStyle.Flat;
            this._recordsButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this._recordsButton.ForeColor = Color.FromArgb(216, 225, 233);
            this._recordsButton.Margin = new Padding(0, 0, 0, 6);
            this._recordsButton.Name = "_recordsButton";
            this._recordsButton.Size = new Size(214, 48);
            this._recordsButton.Text = "  Records";
            this._recordsButton.TextAlign = ContentAlignment.MiddleLeft;
            this._recordsButton.UseVisualStyleBackColor = false;
            this._recordsButton.Click += new System.EventHandler(this.RecordsButton_Click);
            // settings button
            this._settingsButton.BackColor = Color.FromArgb(11, 31, 51);
            this._settingsButton.Cursor = Cursors.Hand;
            this._settingsButton.FlatAppearance.BorderSize = 0;
            this._settingsButton.FlatStyle = FlatStyle.Flat;
            this._settingsButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this._settingsButton.ForeColor = Color.FromArgb(216, 225, 233);
            this._settingsButton.Margin = new Padding(0, 0, 0, 6);
            this._settingsButton.Name = "_settingsButton";
            this._settingsButton.Size = new Size(214, 48);
            this._settingsButton.Text = "  Settings";
            this._settingsButton.TextAlign = ContentAlignment.MiddleLeft;
            this._settingsButton.UseVisualStyleBackColor = false;
            this._settingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // user panel
            this._userPanel.BackColor = Color.FromArgb(11, 31, 51);
            this._userPanel.Controls.Add(this._signedInUserLabel);
            this._userPanel.Controls.Add(this._logoutButton);
            this._userPanel.Dock = DockStyle.Bottom;
            this._userPanel.Location = new Point(0, 630);
            this._userPanel.Name = "_userPanel";
            this._userPanel.Padding = new Padding(18, 8, 18, 10);
            this._userPanel.Size = new Size(238, 112);
            // signed in user
            this._signedInUserLabel.Dock = DockStyle.Top;
            this._signedInUserLabel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this._signedInUserLabel.ForeColor = Color.White;
            this._signedInUserLabel.Location = new Point(18, 8);
            this._signedInUserLabel.Name = "_signedInUserLabel";
            this._signedInUserLabel.Size = new Size(202, 46);
            // logout
            this._logoutButton.BackColor = Color.FromArgb(34, 54, 73);
            this._logoutButton.Dock = DockStyle.Bottom;
            this._logoutButton.FlatAppearance.BorderSize = 0;
            this._logoutButton.FlatStyle = FlatStyle.Flat;
            this._logoutButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this._logoutButton.ForeColor = Color.White;
            this._logoutButton.Location = new Point(18, 64);
            this._logoutButton.Name = "_logoutButton";
            this._logoutButton.Size = new Size(202, 38);
            this._logoutButton.Text = "Logout";
            this._logoutButton.UseVisualStyleBackColor = false;
            this._logoutButton.Click += new System.EventHandler(this.Logout_Click);
            // header
            this._header.BackColor = Color.White;
            this._header.Controls.Add(this._pageTitle);
            this._header.Dock = DockStyle.Top;
            this._header.Location = new Point(238, 0);
            this._header.Name = "_header";
            this._header.Padding = new Padding(30, 0, 30, 0);
            this._header.Size = new Size(1042, 76);
            // title
            this._pageTitle.Dock = DockStyle.Fill;
            this._pageTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this._pageTitle.ForeColor = Color.FromArgb(31, 41, 55);
            this._pageTitle.Name = "_pageTitle";
            this._pageTitle.TextAlign = ContentAlignment.MiddleLeft;
            // content
            this._content.BackColor = Color.FromArgb(244, 247, 250);
            this._content.Dock = DockStyle.Fill;
            this._content.Location = new Point(238, 76);
            this._content.Name = "_content";
            this._content.Padding = new Padding(24);
            this._content.Size = new Size(1042, 684);
            // form
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(244, 247, 250);
            this.ClientSize = new Size(1280, 760);
            this.Controls.Add(this._content);
            this.Controls.Add(this._header);
            this.Controls.Add(this._sidebar);
            this.MinimumSize = new Size(1180, 700);
            this.Name = "MainShellForm";
            this.Text = "JCG Attendance System";
            this.WindowState = FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.MainShellForm_Shown);
            this._sidebar.ResumeLayout(false);
            this._sidebar.PerformLayout();
            this._navPanel.ResumeLayout(false);
            this._userPanel.ResumeLayout(false);
            this._header.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
