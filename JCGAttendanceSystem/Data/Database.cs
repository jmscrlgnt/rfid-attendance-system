using System.Data.SQLite;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Data
{
    internal static class Database
    {
        public static SQLiteConnection OpenConnection()
        {
            AppPaths.EnsureDirectories();
            var connection = new SQLiteConnection(
                "Data Source=" + AppPaths.DatabasePath + ";Version=3;Foreign Keys=True;Journal Mode=WAL;");
            connection.Open();

            using (var command = new SQLiteCommand("PRAGMA foreign_keys = ON;", connection))
            {
                command.ExecuteNonQuery();
            }

            return connection;
        }
    }
}
