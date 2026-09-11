using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.Services;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Forms.Settings
{
    public partial class UserManagementForm : Form
    {
        private AuthService _auth;

        public UserManagementForm()
        {
            InitializeComponent();
            if (DesignTimeHelper.IsDesignMode) return;

            _auth = new AuthService();
            Text = "User Management";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(760, 520);
            MinimumSize = new Size(680, 450);
            BackColor = UiTheme.Background;
            _addButton.Click += (s, e) => OpenEditor(null);
            _resetButton.Click += (s, e) => { var user = SelectedUser; if (user != null) OpenEditor(user); };
            _grid.SelectionChanged += (s, e) => UpdateToggle();
            Load += (s, e) => LoadUsers();
        }

        private User SelectedUser => _grid.SelectedRows.Count == 0 ? null : _grid.SelectedRows[0].DataBoundItem as User;

        private void LoadUsers()
        {
            try { _grid.DataSource = new List<User>(_auth.GetUsersForAdministrator()); UpdateToggle(); }
            catch (Exception ex) { AppLogger.LogException(ex, "Loading system users"); MessageBox.Show(this, ex.Message, "User Management", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void OpenEditor(User user)
        {
            using (var editor = new UserEditorForm(user)) if (editor.ShowDialog(this) == DialogResult.OK) LoadUsers();
        }

        private void Toggle_Click(object sender, EventArgs e)
        {
            var user = SelectedUser; if (user == null) return;
            var action = user.IsActive ? "deactivate" : "activate";
            if (MessageBox.Show(this, "Are you sure you want to " + action + " " + user.Username + "?", "User Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try { _auth.SetUserActive(user.Id, !user.IsActive); LoadUsers(); }
            catch (Exception ex) { AppLogger.LogException(ex, "Changing user active state"); MessageBox.Show(this, ex.Message, "User Management", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void UpdateToggle()
        {
            var user = SelectedUser; _toggle.Enabled = user != null;
            if (user == null) return; _toggle.Text = user.IsActive ? "Deactivate" : "Activate"; _toggle.BackColor = user.IsActive ? UiTheme.Danger : UiTheme.Success;
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var user = _grid.Rows[e.RowIndex].DataBoundItem as User; if (user == null) return;
            if (_grid.Columns[e.ColumnIndex].Name == "Status")
            {
                e.Value = user.IsActive ? "Active" : "Inactive";
                e.CellStyle.ForeColor = user.IsActive ? UiTheme.Success : UiTheme.Danger;
                e.CellStyle.Font = UiTheme.Font(9f, FontStyle.Bold);
            }
            if (_grid.Columns[e.ColumnIndex].Name == "Created") e.Value = user.CreatedAtUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
        }
    }
}
