using System;
using System.Drawing;
using System.Windows.Forms;
using JCGAttendanceSystem.Services;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Forms.Authentication
{
    public partial class SetupAdminForm : Form
    {
        private AuthService _auth;

        public SetupAdminForm()
        {
            InitializeComponent();
            if (DesignTimeHelper.IsDesignMode) return;

            _auth = new AuthService();
            Text = "First-run administrator setup";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(500, 500);
            BackColor = UiTheme.Background;
        }

        private void Create_Click(object sender, EventArgs e)
        {
            _error.Text = string.Empty;
            if (_password.Text != _confirm.Text)
            {
                _error.Text = "Password confirmation does not match.";
                return;
            }

            try
            {
                _auth.CreateInitialAdministrator(_username.Text, _password.Text);
                MessageBox.Show(this, "Administrator account created. You can now sign in.", "Setup complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, "Creating initial administrator");
                _error.Text = ex.Message;
            }
        }
    }
}
