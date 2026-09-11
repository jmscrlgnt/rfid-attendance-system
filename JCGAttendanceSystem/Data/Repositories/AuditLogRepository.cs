using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using JCGAttendanceSystem.Models;

namespace JCGAttendanceSystem.Data.Repositories
{
    internal sealed class AuditLogRepository
    {
        public void Add(int? userId, string action, string entityType, string entityId, string details)
        {
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
INSERT INTO AuditLogs(UserId, Action, EntityType, EntityId, Details, CreatedAtUtc)
VALUES(@UserId, @Action, @EntityType, @EntityId, @Details, @CreatedAtUtc);", connection))
            {
                command.Parameters.AddWithValue("@UserId", (object)userId ?? DBNull.Value);
                command.Parameters.AddWithValue("@Action", action);
                command.Parameters.AddWithValue("@EntityType", (object)entityType ?? DBNull.Value);
                command.Parameters.AddWithValue("@EntityId", (object)entityId ?? DBNull.Value);
                command.Parameters.AddWithValue("@Details", (object)details ?? DBNull.Value);
                command.Parameters.AddWithValue("@CreatedAtUtc", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture));
                command.ExecuteNonQuery();
            }
        }

        public IList<AuditLogEntry> GetRecent(int limit)
        {
            var items = new List<AuditLogEntry>();
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
SELECT a.Id, a.UserId, u.Username, a.Action, a.EntityType, a.EntityId, a.Details, a.CreatedAtUtc
FROM AuditLogs a
LEFT JOIN Users u ON u.Id = a.UserId
ORDER BY a.Id DESC LIMIT @Limit;", connection))
            {
                command.Parameters.AddWithValue("@Limit", Math.Max(1, Math.Min(limit, 1000)));
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new AuditLogEntry
                        {
                            Id = Convert.ToInt32(reader["Id"], CultureInfo.InvariantCulture),
                            UserId = reader["UserId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["UserId"], CultureInfo.InvariantCulture),
                            Username = reader["Username"] == DBNull.Value ? "System" : Convert.ToString(reader["Username"], CultureInfo.InvariantCulture),
                            Action = Convert.ToString(reader["Action"], CultureInfo.InvariantCulture),
                            EntityType = reader["EntityType"] == DBNull.Value ? null : Convert.ToString(reader["EntityType"], CultureInfo.InvariantCulture),
                            EntityId = reader["EntityId"] == DBNull.Value ? null : Convert.ToString(reader["EntityId"], CultureInfo.InvariantCulture),
                            Details = reader["Details"] == DBNull.Value ? null : Convert.ToString(reader["Details"], CultureInfo.InvariantCulture),
                            CreatedAtUtc = DateTime.Parse(Convert.ToString(reader["CreatedAtUtc"], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
                        });
                    }
                }
            }
            return items;
        }
    }
}
