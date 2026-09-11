using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace JCGAttendanceSystem.Utilities
{
    internal static class AppLogger
    {
        private static readonly object Sync = new object();

        public static void LogException(Exception exception, string context)
        {
            if (exception == null) return;
            try
            {
                AppPaths.EnsureDirectories();
                var file = Path.Combine(AppPaths.LogsDirectory, "jcg-" + DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + ".log");
                var text = new StringBuilder()
                    .AppendLine("[" + DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture) + "] " + (context ?? "Unexpected error"))
                    .AppendLine(exception.GetType().FullName + ": " + exception.Message)
                    .AppendLine(exception.StackTrace)
                    .AppendLine()
                    .ToString();
                lock (Sync) File.AppendAllText(file, text, Encoding.UTF8);
            }
            catch
            {
                // Logging must never crash the application.
            }
        }
    }
}
