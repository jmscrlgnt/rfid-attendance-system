using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Text;
using JCGAttendanceSystem.Models;

namespace JCGAttendanceSystem.Data.Repositories
{
    internal sealed class AttendanceRepository
    {
        public AttendanceRecord GetForDate(int studentId, DateTime localDate)
        {
            using (var connection = Database.OpenConnection())
                return GetForDate(connection, null, studentId, localDate);
        }

        public AttendanceRecord GetById(int id)
        {
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
SELECT Id, StudentId, AttendanceDate, TimeInUtc, TimeOutUtc, Source, CreatedByUserId,
       UpdatedByUserId, Notes, CreatedAtUtc, UpdatedAtUtc
FROM AttendanceRecords WHERE Id = @Id LIMIT 1;", connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                    return reader.Read() ? Map(reader) : null;
            }
        }

        public AttendanceRecord GetForDate(SQLiteConnection connection, SQLiteTransaction transaction, int studentId, DateTime localDate)
        {
            using (var command = new SQLiteCommand(@"
SELECT Id, StudentId, AttendanceDate, TimeInUtc, TimeOutUtc, Source, CreatedByUserId,
       UpdatedByUserId, Notes, CreatedAtUtc, UpdatedAtUtc
FROM AttendanceRecords
WHERE StudentId = @StudentId AND AttendanceDate = @AttendanceDate
LIMIT 1;", connection, transaction))
            {
                command.Parameters.AddWithValue("@StudentId", studentId);
                command.Parameters.AddWithValue("@AttendanceDate", localDate.ToString(DatabaseInitializer.AttendanceDateFormat, CultureInfo.InvariantCulture));
                using (var reader = command.ExecuteReader())
                    return reader.Read() ? Map(reader) : null;
            }
        }

        public int InsertTimeIn(SQLiteConnection connection, SQLiteTransaction transaction, int studentId, DateTime localDate, DateTime timeInUtc, string source, int? userId)
        {
            var now = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
            using (var command = new SQLiteCommand(@"
INSERT INTO AttendanceRecords(StudentId, AttendanceDate, TimeInUtc, TimeOutUtc, Source, CreatedByUserId, UpdatedByUserId, Notes, CreatedAtUtc, UpdatedAtUtc)
VALUES(@StudentId, @AttendanceDate, @TimeInUtc, NULL, @Source, @UserId, @UserId, NULL, @Now, @Now);
SELECT last_insert_rowid();", connection, transaction))
            {
                command.Parameters.AddWithValue("@StudentId", studentId);
                command.Parameters.AddWithValue("@AttendanceDate", localDate.ToString(DatabaseInitializer.AttendanceDateFormat, CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@TimeInUtc", timeInUtc.ToString("o", CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@Source", source);
                command.Parameters.AddWithValue("@UserId", (object)userId ?? DBNull.Value);
                command.Parameters.AddWithValue("@Now", now);
                return Convert.ToInt32((long)command.ExecuteScalar(), CultureInfo.InvariantCulture);
            }
        }

        public void SetTimeOut(SQLiteConnection connection, SQLiteTransaction transaction, int attendanceId, DateTime timeOutUtc, int? userId)
        {
            using (var command = new SQLiteCommand(@"
UPDATE AttendanceRecords
SET TimeOutUtc = @TimeOutUtc, UpdatedByUserId = @UserId, UpdatedAtUtc = @UpdatedAtUtc
WHERE Id = @Id AND TimeOutUtc IS NULL;", connection, transaction))
            {
                command.Parameters.AddWithValue("@TimeOutUtc", timeOutUtc.ToString("o", CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@UserId", (object)userId ?? DBNull.Value);
                command.Parameters.AddWithValue("@UpdatedAtUtc", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@Id", attendanceId);
                if (command.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("Attendance record could not be updated because its state changed.");
            }
        }

        public void Correct(int attendanceId, DateTime timeInUtc, DateTime? timeOutUtc, string notes, int userId)
        {
            using (var connection = Database.OpenConnection())
            using (var transaction = connection.BeginTransaction())
            using (var command = new SQLiteCommand(@"
UPDATE AttendanceRecords
SET TimeInUtc=@TimeInUtc, TimeOutUtc=@TimeOutUtc, Notes=@Notes,
    UpdatedByUserId=@UserId, UpdatedAtUtc=@UpdatedAtUtc
WHERE Id=@Id;", connection, transaction))
            {
                command.Parameters.AddWithValue("@TimeInUtc", timeInUtc.ToString("o", CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@TimeOutUtc", timeOutUtc.HasValue ? (object)timeOutUtc.Value.ToString("o", CultureInfo.InvariantCulture) : DBNull.Value);
                command.Parameters.AddWithValue("@Notes", string.IsNullOrWhiteSpace(notes) ? (object)DBNull.Value : notes.Trim());
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@UpdatedAtUtc", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@Id", attendanceId);
                if (command.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("Attendance record was not found.");
                transaction.Commit();
            }
        }

        public DashboardSummary GetDashboardSummary(DateTime localDate)
        {
            var date = localDate.ToString(DatabaseInitializer.AttendanceDateFormat, CultureInfo.InvariantCulture);
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
SELECT
    (SELECT COUNT(1) FROM Students WHERE IsActive = 1) AS TotalActiveStudents,
    (SELECT COUNT(1) FROM AttendanceRecords WHERE AttendanceDate = @AttendanceDate) AS PresentToday,
    (SELECT COUNT(1) FROM AttendanceRecords WHERE AttendanceDate = @AttendanceDate AND TimeOutUtc IS NULL) AS CurrentlyTimedIn,
    (SELECT COUNT(1) FROM AttendanceRecords WHERE AttendanceDate = @AttendanceDate AND TimeOutUtc IS NOT NULL) AS CompletedToday;", connection))
            {
                command.Parameters.AddWithValue("@AttendanceDate", date);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return new DashboardSummary();
                    return new DashboardSummary
                    {
                        TotalActiveStudents = Convert.ToInt32(reader["TotalActiveStudents"], CultureInfo.InvariantCulture),
                        PresentToday = Convert.ToInt32(reader["PresentToday"], CultureInfo.InvariantCulture),
                        CurrentlyTimedIn = Convert.ToInt32(reader["CurrentlyTimedIn"], CultureInfo.InvariantCulture),
                        CompletedToday = Convert.ToInt32(reader["CompletedToday"], CultureInfo.InvariantCulture)
                    };
                }
            }
        }

        public IList<AttendanceView> GetRecentActivity(int limit)
        {
            var items = new List<AttendanceView>();
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
SELECT ar.Id AS AttendanceId, s.Id AS StudentId, s.StudentNumber,
       s.FirstName || ' ' || s.LastName AS StudentName, s.Course, s.YearLevel, s.Section,
       ar.AttendanceDate, ar.TimeInUtc, ar.TimeOutUtc, ar.Source, ar.Notes,
       'Time In' AS EventType, ar.TimeInUtc AS EventTimeUtc
FROM AttendanceRecords ar JOIN Students s ON s.Id = ar.StudentId
UNION ALL
SELECT ar.Id AS AttendanceId, s.Id AS StudentId, s.StudentNumber,
       s.FirstName || ' ' || s.LastName AS StudentName, s.Course, s.YearLevel, s.Section,
       ar.AttendanceDate, ar.TimeInUtc, ar.TimeOutUtc, ar.Source, ar.Notes,
       'Time Out' AS EventType, ar.TimeOutUtc AS EventTimeUtc
FROM AttendanceRecords ar JOIN Students s ON s.Id = ar.StudentId
WHERE ar.TimeOutUtc IS NOT NULL
ORDER BY EventTimeUtc DESC
LIMIT @Limit;", connection))
            {
                command.Parameters.AddWithValue("@Limit", Math.Max(1, Math.Min(limit, 100)));
                using (var reader = command.ExecuteReader())
                    while (reader.Read()) items.Add(MapView(reader, true));
            }
            return items;
        }

        public IList<AttendanceView> SearchRecords(AttendanceFilter filter)
        {
            filter = filter ?? new AttendanceFilter();
            var sql = new StringBuilder(@"
SELECT ar.Id AS AttendanceId, s.Id AS StudentId, s.StudentNumber,
       s.FirstName || ' ' || s.LastName AS StudentName, s.Course, s.YearLevel, s.Section,
       ar.AttendanceDate, ar.TimeInUtc, ar.TimeOutUtc, ar.Source, ar.Notes
FROM AttendanceRecords ar
JOIN Students s ON s.Id = ar.StudentId
WHERE 1=1");

            if (filter.DateFrom.HasValue) sql.Append(" AND ar.AttendanceDate >= @DateFrom");
            if (filter.DateTo.HasValue) sql.Append(" AND ar.AttendanceDate <= @DateTo");
            if (!string.IsNullOrWhiteSpace(filter.StudentQuery))
                sql.Append(" AND (s.StudentNumber LIKE @StudentQuery COLLATE NOCASE OR s.FirstName LIKE @StudentQuery COLLATE NOCASE OR s.LastName LIKE @StudentQuery COLLATE NOCASE OR (s.FirstName || ' ' || s.LastName) LIKE @StudentQuery COLLATE NOCASE)");
            if (!string.IsNullOrWhiteSpace(filter.Course)) sql.Append(" AND s.Course = @Course COLLATE NOCASE");
            if (filter.YearLevel.HasValue) sql.Append(" AND s.YearLevel = @YearLevel");
            if (!string.IsNullOrWhiteSpace(filter.Section)) sql.Append(" AND s.Section = @Section COLLATE NOCASE");
            if (string.Equals(filter.Status, "Timed In", StringComparison.OrdinalIgnoreCase)) sql.Append(" AND ar.TimeOutUtc IS NULL");
            if (string.Equals(filter.Status, "Completed", StringComparison.OrdinalIgnoreCase)) sql.Append(" AND ar.TimeOutUtc IS NOT NULL");
            sql.Append(" ORDER BY ar.AttendanceDate DESC, ar.TimeInUtc DESC;");

            var items = new List<AttendanceView>();
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(sql.ToString(), connection))
            {
                if (filter.DateFrom.HasValue) command.Parameters.AddWithValue("@DateFrom", filter.DateFrom.Value.ToString(DatabaseInitializer.AttendanceDateFormat, CultureInfo.InvariantCulture));
                if (filter.DateTo.HasValue) command.Parameters.AddWithValue("@DateTo", filter.DateTo.Value.ToString(DatabaseInitializer.AttendanceDateFormat, CultureInfo.InvariantCulture));
                if (!string.IsNullOrWhiteSpace(filter.StudentQuery)) command.Parameters.AddWithValue("@StudentQuery", "%" + filter.StudentQuery.Trim() + "%");
                if (!string.IsNullOrWhiteSpace(filter.Course)) command.Parameters.AddWithValue("@Course", filter.Course.Trim());
                if (filter.YearLevel.HasValue) command.Parameters.AddWithValue("@YearLevel", filter.YearLevel.Value);
                if (!string.IsNullOrWhiteSpace(filter.Section)) command.Parameters.AddWithValue("@Section", filter.Section.Trim());
                using (var reader = command.ExecuteReader())
                    while (reader.Read()) items.Add(MapView(reader, false));
            }
            return items;
        }

        private static AttendanceRecord Map(SQLiteDataReader reader)
        {
            return new AttendanceRecord
            {
                Id = Convert.ToInt32(reader["Id"], CultureInfo.InvariantCulture),
                StudentId = Convert.ToInt32(reader["StudentId"], CultureInfo.InvariantCulture),
                AttendanceDate = DateTime.ParseExact(Convert.ToString(reader["AttendanceDate"], CultureInfo.InvariantCulture), DatabaseInitializer.AttendanceDateFormat, CultureInfo.InvariantCulture),
                TimeInUtc = DateTime.Parse(Convert.ToString(reader["TimeInUtc"], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                TimeOutUtc = reader["TimeOutUtc"] == DBNull.Value ? (DateTime?)null : DateTime.Parse(Convert.ToString(reader["TimeOutUtc"], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                Source = Convert.ToString(reader["Source"], CultureInfo.InvariantCulture),
                CreatedByUserId = reader["CreatedByUserId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CreatedByUserId"], CultureInfo.InvariantCulture),
                UpdatedByUserId = reader["UpdatedByUserId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["UpdatedByUserId"], CultureInfo.InvariantCulture),
                Notes = reader["Notes"] == DBNull.Value ? null : Convert.ToString(reader["Notes"], CultureInfo.InvariantCulture),
                CreatedAtUtc = DateTime.Parse(Convert.ToString(reader["CreatedAtUtc"], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                UpdatedAtUtc = DateTime.Parse(Convert.ToString(reader["UpdatedAtUtc"], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
            };
        }

        private static AttendanceView MapView(SQLiteDataReader reader, bool hasEventColumns)
        {
            var view = new AttendanceView
            {
                AttendanceId = Convert.ToInt32(reader["AttendanceId"], CultureInfo.InvariantCulture),
                StudentId = Convert.ToInt32(reader["StudentId"], CultureInfo.InvariantCulture),
                StudentNumber = Convert.ToString(reader["StudentNumber"], CultureInfo.InvariantCulture),
                StudentName = Convert.ToString(reader["StudentName"], CultureInfo.InvariantCulture),
                Course = Convert.ToString(reader["Course"], CultureInfo.InvariantCulture),
                YearLevel = Convert.ToInt32(reader["YearLevel"], CultureInfo.InvariantCulture),
                Section = Convert.ToString(reader["Section"], CultureInfo.InvariantCulture),
                AttendanceDate = DateTime.ParseExact(Convert.ToString(reader["AttendanceDate"], CultureInfo.InvariantCulture), DatabaseInitializer.AttendanceDateFormat, CultureInfo.InvariantCulture),
                TimeInUtc = DateTime.Parse(Convert.ToString(reader["TimeInUtc"], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                TimeOutUtc = reader["TimeOutUtc"] == DBNull.Value ? (DateTime?)null : DateTime.Parse(Convert.ToString(reader["TimeOutUtc"], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                Source = Convert.ToString(reader["Source"], CultureInfo.InvariantCulture),
                Notes = reader["Notes"] == DBNull.Value ? null : Convert.ToString(reader["Notes"], CultureInfo.InvariantCulture)
            };
            if (hasEventColumns)
            {
                view.EventType = Convert.ToString(reader["EventType"], CultureInfo.InvariantCulture);
                view.EventTimeUtc = DateTime.Parse(Convert.ToString(reader["EventTimeUtc"], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }
            return view;
        }
    }
}
