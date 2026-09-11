using System;
using System.Drawing;
using System.Windows.Forms;
using JCGAttendanceSystem.Services;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Forms.Settings
{
    public partial class ChangePasswordForm : Form
    {
        private AuthService _auth;

        public ChangePasswordForm()
        {
            InitializeComponent();
            if (DesignTimeHelper.IsDesignMode) return;

            _auth = new AuthService();
            Text = "Change Password";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(460, 390);
            BackColor = UiTheme.Background;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            _error.Text = string.Empty;
            if (_newPassword.Text != _confirm.Text) { _error.Text = "New password confirmation does not match."; return; }
            try
            {
                _auth.ChangeOwnPassword(_current.Text, _newPassword.Text);
                MessageBox.Show(this, "Password updated successfully.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, "Changing own password");
                _error.Text = ex.Message;
            }
        }
    }
}
