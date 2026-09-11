using System;
using System.Drawing;
using System.Windows.Forms;
using JCGAttendanceSystem.Forms.Main;
using JCGAttendanceSystem.Services;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Forms.Authentication
{
    public partial class LoginForm : Form
    {
        private AuthService _auth;

        public LoginForm()
        {
            InitializeComponent();
            if (DesignTimeHelper.IsDesignMode) return;

            _auth = new AuthService();
            Text = "JCG Attendance System - Sign In";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(900, 580);
            ClientSize = new Size(1040, 650);
            BackColor = UiTheme.Background;
        }

        private void ShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            _password.UseSystemPasswordChar = !_showPassword.Checked;
        }

        private void SignIn_Click(object sender, EventArgs e)
        {
            _error.Text = string.Empty;
            try
            {
                var user = _auth.Authenticate(_username.Text, _password.Text);
                if (user == null)
                {
                    _error.Text = "Invalid username/password or the account is inactive.";
                    _password.Clear();
                    _password.Focus();
                    return;
                }

                Hide();
                using (var shell = new MainShellForm())
                {
                    shell.ShowDialog(this);
                    if (shell.LoggedOut)
                    {
                        _password.Clear();
                        Show();
                        Activate();
                    }
                    else
                    {
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, "Signing in");
                _error.Text = "The sign-in operation could not be completed. Please try again.";
            }
        }
    }
}
