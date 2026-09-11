using System;
using System.Data.SQLite;
using System.Globalization;

namespace JCGAttendanceSystem.Data
{
    internal static class DatabaseInitializer
    {
        public const string AttendanceDateFormat = "yyyy-MM-dd";

        public static void Initialize()
        {
            using (var connection = Database.OpenConnection())
            using (var transaction = connection.BeginTransaction())
            {
                Execute(connection, transaction, @"
CREATE TABLE IF NOT EXISTS Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Username TEXT NOT NULL UNIQUE COLLATE NOCASE,
    PasswordHash TEXT NOT NULL,
    PasswordSalt TEXT NOT NULL,
    Role TEXT NOT NULL CHECK(Role IN ('Administrator','Staff')),
    IsActive INTEGER NOT NULL DEFAULT 1,
    CreatedAtUtc TEXT NOT NULL,
    UpdatedAtUtc TEXT NOT NULL
);");

                Execute(connection, transaction, @"
CREATE TABLE IF NOT EXISTS Students (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    StudentNumber TEXT NOT NULL UNIQUE COLLATE NOCASE,
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    BirthDate TEXT NULL,
    Gender TEXT NULL,
    Email TEXT NULL UNIQUE COLLATE NOCASE,
    Course TEXT NOT NULL,
    YearLevel INTEGER NOT NULL CHECK(YearLevel BETWEEN 1 AND 4),
    Section TEXT NOT NULL,
    RfidTag TEXT NULL UNIQUE COLLATE NOCASE,
    IsActive INTEGER NOT NULL DEFAULT 1,
    CreatedAtUtc TEXT NOT NULL,
    UpdatedAtUtc TEXT NOT NULL
);");

                Execute(connection, transaction, @"
CREATE TABLE IF NOT EXISTS AttendanceRecords (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    StudentId INTEGER NOT NULL,
    AttendanceDate TEXT NOT NULL, -- yyyy-MM-dd local attendance day
    TimeInUtc TEXT NOT NULL,
    TimeOutUtc TEXT NULL,
    Source TEXT NOT NULL CHECK(Source IN ('Manual','RFID')),
    CreatedByUserId INTEGER NULL,
    UpdatedByUserId INTEGER NULL,
    Notes TEXT NULL,
    CreatedAtUtc TEXT NOT NULL,
    UpdatedAtUtc TEXT NOT NULL,
    UNIQUE(StudentId, AttendanceDate),
    FOREIGN KEY(StudentId) REFERENCES Students(Id),
    FOREIGN KEY(CreatedByUserId) REFERENCES Users(Id),
    FOREIGN KEY(UpdatedByUserId) REFERENCES Users(Id)
);");

                Execute(connection, transaction, @"
CREATE TABLE IF NOT EXISTS AuditLogs (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NULL,
    Action TEXT NOT NULL,
    EntityType TEXT NULL,
    EntityId TEXT NULL,
    Details TEXT NULL,
    CreatedAtUtc TEXT NOT NULL,
    FOREIGN KEY(UserId) REFERENCES Users(Id)
);");

                Execute(connection, transaction, @"
CREATE TABLE IF NOT EXISTS AppSettings (
    Key TEXT PRIMARY KEY,
    Value TEXT NULL,
    UpdatedAtUtc TEXT NOT NULL
);");

                Execute(connection, transaction, "CREATE INDEX IF NOT EXISTS IX_Students_Name ON Students(LastName, FirstName);");
                Execute(connection, transaction, "CREATE INDEX IF NOT EXISTS IX_Students_RfidTag ON Students(RfidTag);");
                Execute(connection, transaction, "CREATE INDEX IF NOT EXISTS IX_Attendance_Date ON AttendanceRecords(AttendanceDate);");
                Execute(connection, transaction, "CREATE INDEX IF NOT EXISTS IX_Attendance_Student ON AttendanceRecords(StudentId, AttendanceDate);");
                Execute(connection, transaction, "CREATE INDEX IF NOT EXISTS IX_Audit_CreatedAt ON AuditLogs(CreatedAtUtc DESC);");

                using (var command = new SQLiteCommand(@"
INSERT OR IGNORE INTO AppSettings(Key, Value, UpdatedAtUtc)
VALUES('SchemaVersion', '2', @UpdatedAtUtc);", connection, transaction))
                {
                    command.Parameters.AddWithValue("@UpdatedAtUtc", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture));
                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        }

        private static void Execute(SQLiteConnection connection, SQLiteTransaction transaction, string sql)
        {
            using (var command = new SQLiteCommand(sql, connection, transaction))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
