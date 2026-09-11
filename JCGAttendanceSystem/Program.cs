using System;
using System.Windows.Forms;
using JCGAttendanceSystem.Data;
using JCGAttendanceSystem.Forms.Authentication;
using JCGAttendanceSystem.Services;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                AppPaths.EnsureDirectories();
                DatabaseInitializer.Initialize();
                var auth = new AuthService();
                if (auth.NeedsInitialAdminSetup())
                {
                    using (var setup = new SetupAdminForm())
                    {
                        if (setup.ShowDialog() != DialogResult.OK) return;
                    }
                }
                Application.Run(new LoginForm());
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, "Application startup");
                MessageBox.Show("JCG Attendance System could not start. Check the local log file for details.", "Startup error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
