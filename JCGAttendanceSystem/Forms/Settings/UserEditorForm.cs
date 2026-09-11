using System;
using System.Drawing;
using System.Windows.Forms;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.Services;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Forms.Settings
{
    public partial class UserEditorForm : Form
    {
        private AuthService _auth;
        private User _resetTarget;

        public UserEditorForm()
        {
            InitializeComponent();
        }

        public UserEditorForm(User resetTarget)
        {
            InitializeComponent();
            _auth = new AuthService();
            _resetTarget = resetTarget;
            Text = resetTarget == null ? "Add Staff User" : "Reset User Password";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(450, 390);
            BackColor = UiTheme.Background;
            _headingLabel.Text = _resetTarget == null ? "Create Staff Account" : "Reset Password";
            _subtitleLabel.Text = _resetTarget == null ? "Staff can manage attendance and students, but not administrator tools." : "Set a new password for " + _resetTarget.Username + ".";
            _username.Text = _resetTarget?.Username ?? string.Empty;
            _username.ReadOnly = _resetTarget != null;
            _saveButton.Text = _resetTarget == null ? "Create Staff" : "Reset Password";
        }

        private void Save_Click(object sender, EventArgs e)
        {
            _error.Text = string.Empty;
            if (_password.Text != _confirm.Text) { _error.Text = "Password confirmation does not match."; return; }
            try
            {
                if (_resetTarget == null) _auth.CreateStaff(_username.Text, _password.Text);
                else _auth.ResetUserPassword(_resetTarget.Id, _password.Text);
                DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, "Saving system user");
                _error.Text = ex.Message;
            }
        }
    }
}
