using System;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using JCGAttendanceSystem.Data.Repositories;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.Security;
using JCGAttendanceSystem.Services;

namespace JCGAttendanceSystem.Data.Legacy
{
    internal sealed class LegacyImportService
    {
        private readonly StudentService _students = new StudentService();
        private readonly StudentRepository _studentRepository = new StudentRepository();
        private readonly AuditService _audit = new AuditService();

        public LegacyImportSummary Import(string path)
        {
            if (!SessionContext.IsAdministrator)
                throw new UnauthorizedAccessException("Administrator access is required to import legacy data.");
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                throw new FileNotFoundException("Select the legacy Data.mdb file.", path);

            var summary = new LegacyImportSummary();
            using (var connection = OpenLegacyConnection(path))
            {
                ImportStudents(connection, summary);
                ImportAttendance(connection, summary);
            }
            _audit.Log("Legacy Access data imported", "LegacyImport", null, summary.ToString());
            return summary;
        }

        private void ImportStudents(OleDbConnection connection, LegacyImportSummary summary)
        {
            using (var command = new OleDbCommand(@"
SELECT Fname, Lname, [Date of Birth], Gender, Email, Course, SecYrlvl, UserID
FROM Data1;", connection))
            using (var reader = command.ExecuteReader())
            {
                var rowNumber = 0;
                while (reader != null && reader.Read())
                {
                    rowNumber++;
                    try
                    {
                        var legacyId = Value(reader, "UserID");
                        var studentNumber = string.IsNullOrWhiteSpace(legacyId)
                            ? "LEGACY-" + rowNumber.ToString("D6", CultureInfo.InvariantCulture)
                            : legacyId.Trim();
                        if (_studentRepository.GetByStudentNumber(studentNumber) != null)
                        {
                            summary.SkippedStudents++;
                            summary.Messages.Add("Student " + studentNumber + " already exists and was skipped.");
                            continue;
                        }

                        int year;
                        string section;
                        ParseYearSection(Value(reader, "SecYrlvl"), out year, out section);
                        var student = new Student
                        {
                            StudentNumber = studentNumber,
                            FirstName = Fallback(Value(reader, "Fname"), "Legacy"),
                            LastName = Fallback(Value(reader, "Lname"), "Student"),
                            BirthDate = ParseDate(Value(reader, "Date of Birth")),
                            Gender = NullIfEmpty(Value(reader, "Gender")),
                            Email = NullIfEmpty(Value(reader, "Email")),
                            Course = Fallback(Value(reader, "Course"), "UNKNOWN"),
                            YearLevel = year,
                            Section = section,
                            IsActive = true
                        };
                        _students.Save(student);
                        summary.ImportedStudents++;
                    }
                    catch (Exception ex)
                    {
                        summary.SkippedStudents++;
                        summary.Messages.Add("Student row " + rowNumber + " skipped: " + ex.Message);
                    }
                }
            }
        }

        private void ImportAttendance(OleDbConnection connection, LegacyImportSummary summary)
        {
            OleDbCommand command = null;
            try
            {
                command = new OleDbCommand("SELECT Email, TimeIn, TimeOut, [Day] FROM [Time];", connection);
                ImportAttendanceRows(command, summary);
            }
            catch (OleDbException)
            {
                command?.Dispose();
                command = new OleDbCommand("SELECT Email, TimeIn, TimeOut, DOT FROM [Time];", connection);
                ImportAttendanceRows(command, summary);
            }
            finally
            {
                command?.Dispose();
            }
        }

        private void ImportAttendanceRows(OleDbCommand command, LegacyImportSummary summary)
        {
            using (var reader = command.ExecuteReader())
            {
                var rowNumber = 0;
                while (reader != null && reader.Read())
                {
                    rowNumber++;
                    try
                    {
                        var email = NullIfEmpty(Value(reader, "Email"));
                        var student = email == null ? null : _studentRepository.GetByEmail(email);
                        if (student == null)
                        {
                            summary.SkippedAttendance++;
                            summary.Messages.Add("Attendance row " + rowNumber + " skipped because its student/email could not be resolved.");
                            continue;
                        }

                        var dateText = reader.FieldCount > 3 && reader.GetName(3).Equals("Day", StringComparison.OrdinalIgnoreCase)
                            ? Value(reader, "Day") : Value(reader, "DOT");
                        DateTime attendanceDay;
                        if (!DateTime.TryParse(dateText, CultureInfo.CurrentCulture, DateTimeStyles.None, out attendanceDay) &&
                            !DateTime.TryParse(dateText, CultureInfo.InvariantCulture, DateTimeStyles.None, out attendanceDay))
                            throw new FormatException("Attendance date could not be parsed.");

                        var timeInLocal = CombineDateAndTime(attendanceDay, Value(reader, "TimeIn"));
                        var timeOutText = NullIfEmpty(Value(reader, "TimeOut"));
                        DateTime? timeOutLocal = timeOutText == null ? (DateTime?)null : CombineDateAndTime(attendanceDay, timeOutText);
                        InsertLegacyAttendance(student.Id, attendanceDay.Date, timeInLocal.ToUniversalTime(), timeOutLocal?.ToUniversalTime());
                        summary.ImportedAttendance++;
                    }
                    catch (Exception ex)
                    {
                        summary.SkippedAttendance++;
                        summary.Messages.Add("Attendance row " + rowNumber + " skipped: " + ex.Message);
                    }
                }
            }
        }

        private static void InsertLegacyAttendance(int studentId, DateTime date, DateTime timeInUtc, DateTime? timeOutUtc)
        {
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
INSERT OR IGNORE INTO AttendanceRecords
(StudentId, AttendanceDate, TimeInUtc, TimeOutUtc, Source, CreatedByUserId, UpdatedByUserId, Notes, CreatedAtUtc, UpdatedAtUtc)
VALUES(@StudentId, @AttendanceDate, @TimeInUtc, @TimeOutUtc, 'Manual', @UserId, @UserId, 'Imported from legacy Access database', @Now, @Now);", connection))
            {
                var now = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
                command.Parameters.AddWithValue("@StudentId", studentId);
                command.Parameters.AddWithValue("@AttendanceDate", date.ToString(DatabaseInitializer.AttendanceDateFormat, CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@TimeInUtc", timeInUtc.ToString("o", CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@TimeOutUtc", timeOutUtc.HasValue ? (object)timeOutUtc.Value.ToString("o", CultureInfo.InvariantCulture) : DBNull.Value);
                command.Parameters.AddWithValue("@UserId", SessionContext.CurrentUser.Id);
                command.Parameters.AddWithValue("@Now", now);
                command.ExecuteNonQuery();
            }
        }

        private static OleDbConnection OpenLegacyConnection(string path)
        {
            Exception lastError = null;
            foreach (var provider in new[] { "Microsoft.ACE.OLEDB.12.0", "Microsoft.Jet.OLEDB.4.0" })
            {
                try
                {
                    var connection = new OleDbConnection("Provider=" + provider + ";Data Source=" + path + ";Persist Security Info=False;");
                    connection.Open();
                    return connection;
                }
                catch (Exception ex)
                {
                    lastError = ex;
                }
            }
            throw new InvalidOperationException("The Microsoft Access database provider is not installed. Install the Microsoft Access Database Engine or continue with a fresh SQLite database.", lastError);
        }

        private static string Value(OleDbDataReader reader, string name)
        {
            var value = reader[name];
            return value == DBNull.Value ? string.Empty : Convert.ToString(value, CultureInfo.CurrentCulture);
        }

        private static string Fallback(string value, string fallback) => string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();
        private static string NullIfEmpty(string value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

        private static DateTime? ParseDate(string value)
        {
            DateTime parsed;
            return DateTime.TryParse(value, out parsed) ? parsed.Date : (DateTime?)null;
        }

        private static void ParseYearSection(string value, out int year, out string section)
        {
            year = 1;
            section = "A";
            if (string.IsNullOrWhiteSpace(value)) return;
            var match = Regex.Match(value, @"(?<year>[1-4]).*?(?<section>[1-4]?[A-Za-z])\s*$");
            if (!match.Success) return;
            int parsedYear;
            if (int.TryParse(match.Groups["year"].Value, out parsedYear)) year = parsedYear;
            section = match.Groups["section"].Value.ToUpperInvariant();
        }

        private static DateTime CombineDateAndTime(DateTime date, string timeText)
        {
            DateTime parsed;
            if (!DateTime.TryParse(timeText, CultureInfo.CurrentCulture, DateTimeStyles.None, out parsed) &&
                !DateTime.TryParse(timeText, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed))
                throw new FormatException("Attendance time could not be parsed.");
            return date.Date.Add(parsed.TimeOfDay);
        }
    }
}
