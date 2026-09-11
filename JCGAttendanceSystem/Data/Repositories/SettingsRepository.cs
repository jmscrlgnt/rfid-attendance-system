using System;
using System.Data.SQLite;
using System.Globalization;

namespace JCGAttendanceSystem.Data.Repositories
{
    internal sealed class SettingsRepository
    {
        public string Get(string key, string fallback = null)
        {
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand("SELECT Value FROM AppSettings WHERE Key = @Key LIMIT 1;", connection))
            {
                command.Parameters.AddWithValue("@Key", key);
                var value = command.ExecuteScalar();
                return value == null || value == DBNull.Value ? fallback : Convert.ToString(value, CultureInfo.InvariantCulture);
            }
        }

        public void Set(string key, string value)
        {
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
INSERT INTO AppSettings(Key, Value, UpdatedAtUtc) VALUES(@Key, @Value, @Now)
ON CONFLICT(Key) DO UPDATE SET Value=@Value, UpdatedAtUtc=@Now;", connection))
            {
                command.Parameters.AddWithValue("@Key", key);
                command.Parameters.AddWithValue("@Value", (object)value ?? DBNull.Value);
                command.Parameters.AddWithValue("@Now", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture));
                command.ExecuteNonQuery();
            }
        }
    }
}
