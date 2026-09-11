using System;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using JCGAttendanceSystem.Security;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Services
{
    internal sealed class BackupService
    {
        private readonly AuditService _audit = new AuditService();
        private static readonly string[] RequiredTables = { "Users", "Students", "AttendanceRecords", "AuditLogs", "AppSettings" };

        public void CreateBackup(string destinationPath)
        {
            EnsureAdministrator();
            if (string.IsNullOrWhiteSpace(destinationPath))
                throw new ArgumentException("A backup destination is required.", nameof(destinationPath));
            AppPaths.EnsureDirectories();
            if (!File.Exists(AppPaths.DatabasePath))
                throw new FileNotFoundException("The live database does not exist yet.", AppPaths.DatabasePath);

            CheckpointDatabase();
            var fullDestination = Path.GetFullPath(destinationPath);
            if (string.Equals(fullDestination, Path.GetFullPath(AppPaths.DatabasePath), StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Choose a destination other than the live database file.");
            Directory.CreateDirectory(Path.GetDirectoryName(fullDestination));
            File.Copy(AppPaths.DatabasePath, fullDestination, true);
            ValidateDatabase(fullDestination);
            _audit.Log("Database backup created", "Database", null, "File=" + Path.GetFileName(fullDestination));
        }

        public string Restore(string sourcePath)
        {
            EnsureAdministrator();
            if (string.IsNullOrWhiteSpace(sourcePath) || !File.Exists(sourcePath))
                throw new FileNotFoundException("Select an existing JCG database backup.", sourcePath);

            ValidateDatabase(sourcePath);
            AppPaths.EnsureDirectories();

            var safetyBackup = Path.Combine(
                AppPaths.BackupsDirectory,
                "pre-restore-safety-" + DateTime.Now.ToString("yyyyMMdd-HHmmss", CultureInfo.InvariantCulture) + ".db");

            if (File.Exists(AppPaths.DatabasePath))
            {
                CheckpointDatabase();
                File.Copy(AppPaths.DatabasePath, safetyBackup, true);
            }

            var tempPath = AppPaths.DatabasePath + ".restore.tmp";
            File.Copy(sourcePath, tempPath, true);
            ValidateDatabase(tempPath);

            DeleteIfExists(AppPaths.DatabasePath + "-wal");
            DeleteIfExists(AppPaths.DatabasePath + "-shm");
            File.Copy(tempPath, AppPaths.DatabasePath, true);
            DeleteIfExists(tempPath);

            _audit.LogAs(null, "Database restored", "Database", null,
                "Source=" + Path.GetFileName(sourcePath) + ";SafetyBackup=" + Path.GetFileName(safetyBackup));
            return safetyBackup;
        }

        public void ValidateDatabase(string path)
        {
            using (var connection = new SQLiteConnection("Data Source=" + path + ";Version=3;Read Only=True;"))
            {
                connection.Open();
                foreach (var table in RequiredTables)
                {
                    using (var command = new SQLiteCommand("SELECT COUNT(1) FROM sqlite_master WHERE type='table' AND name=@Name;", connection))
                    {
                        command.Parameters.AddWithValue("@Name", table);
                        if (Convert.ToInt32(command.ExecuteScalar(), CultureInfo.InvariantCulture) != 1)
                            throw new InvalidDataException("The selected file is not a valid JCG Attendance System database. Missing table: " + table + ".");
                    }
                }
            }
        }

        private static void CheckpointDatabase()
        {
            using (var connection = Data.Database.OpenConnection())
            using (var command = new SQLiteCommand("PRAGMA wal_checkpoint(FULL);", connection))
                command.ExecuteNonQuery();
        }

        private static void DeleteIfExists(string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }

        private static void EnsureAdministrator()
        {
            if (!SessionContext.IsAdministrator)
                throw new UnauthorizedAccessException("Administrator access is required for backup and restore.");
        }
    }
}
