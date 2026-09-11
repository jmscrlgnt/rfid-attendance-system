using System;
using System.IO;

namespace JCGAttendanceSystem.Utilities
{
    internal static class AppPaths
    {
        public static string RootDirectory => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "JCGAttendanceSystem");

        public static string DataDirectory => Path.Combine(RootDirectory, "Data");
        public static string DatabasePath => Path.Combine(DataDirectory, "jcg_attendance.db");
        public static string BackupsDirectory => Path.Combine(RootDirectory, "Backups");
        public static string LogsDirectory => Path.Combine(RootDirectory, "Logs");
        public static string LegacyDirectory => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Legacy");

        public static void EnsureDirectories()
        {
            Directory.CreateDirectory(RootDirectory);
            Directory.CreateDirectory(DataDirectory);
            Directory.CreateDirectory(BackupsDirectory);
            Directory.CreateDirectory(LogsDirectory);
        }
    }
}
