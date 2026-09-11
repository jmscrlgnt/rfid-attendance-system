using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using JCGAttendanceSystem.Data.Legacy;
using JCGAttendanceSystem.Security;
using JCGAttendanceSystem.Services;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Forms.Settings
{
    public partial class SettingsForm : Form
    {
        private BackupService _backup;
        private SettingsService _settings;

        public SettingsForm()
        {
            InitializeComponent();
            if (DesignTimeHelper.IsDesignMode) return;

            _backup = new BackupService();
            _settings = new SettingsService();
            Text = "Settings"; BackColor = UiTheme.Background; AutoScroll = true;
            _changePasswordButton.Click += (s, e) => { using (var form = new ChangePasswordForm()) form.ShowDialog(this); };
            _userManagementButton.Click += (s, e) => { using (var form = new UserManagementForm()) form.ShowDialog(this); };
            _auditButton.Click += (s, e) => { using (var form = new AuditLogForm()) form.ShowDialog(this); };
            _adminToolsPanel.Visible = SessionContext.IsAdministrator;
            _saveOrganizationButton.Enabled = SessionContext.IsAdministrator;
            try { _organization.Text = _settings.GetOrganizationName(); } catch { _organization.Text = "JCG Attendance System"; }
            _databasePathLabel.Text = "Database: " + AppPaths.DatabasePath;
        }

        private void SaveOrganization_Click(object sender, EventArgs e)
        {
            if (!SessionContext.IsAdministrator) return;
            try { _settings.SetOrganizationName(_organization.Text); MessageBox.Show(this, "Organization name saved.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            catch (Exception ex) { AppLogger.LogException(ex, "Saving organization name"); MessageBox.Show(this, ex.Message, "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Backup_Click(object sender, EventArgs e)
        {
            if (!SessionContext.IsAdministrator) return;
            using (var dialog = new SaveFileDialog { Filter = "JCG database backup (*.db)|*.db", FileName = "jcg-backup-" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".db" })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                try { _backup.CreateBackup(dialog.FileName); MessageBox.Show(this, "Backup created successfully.", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                catch (Exception ex) { AppLogger.LogException(ex, "Creating database backup"); MessageBox.Show(this, ex.Message, "Backup", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void Restore_Click(object sender, EventArgs e)
        {
            if (!SessionContext.IsAdministrator) return;
            using (var dialog = new OpenFileDialog { Filter = "JCG database backup (*.db)|*.db|All files (*.*)|*.*" })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                if (MessageBox.Show(this, "Restore this database? The current database will first be copied to an automatic safety backup. The application must be restarted afterward.", "Restore Database", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                try
                {
                    var safety = _backup.Restore(dialog.FileName);
                    MessageBox.Show(this, "Restore completed. Safety backup: " + safety + "\n\nPlease restart JCG Attendance System before continuing.", "Restore Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { AppLogger.LogException(ex, "Restoring database"); MessageBox.Show(this, ex.Message, "Restore Database", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void Legacy_Click(object sender, EventArgs e)
        {
            if (!SessionContext.IsAdministrator) return;
            using (var dialog = new OpenFileDialog { Filter = "Microsoft Access database (*.mdb)|*.mdb", InitialDirectory = Directory.Exists(AppPaths.LegacyDirectory) ? AppPaths.LegacyDirectory : string.Empty })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    var result = new LegacyImportService().Import(dialog.FileName);
                    var detail = string.Join("\n", result.Messages.Take(8));
                    MessageBox.Show(this, result + (string.IsNullOrWhiteSpace(detail) ? "" : "\n\nDetails:\n" + detail), "Legacy Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { AppLogger.LogException(ex, "Importing legacy database"); MessageBox.Show(this, ex.Message, "Legacy Import", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
