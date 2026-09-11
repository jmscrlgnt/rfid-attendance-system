using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.Security;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Services
{
    internal sealed class RecordExportService
    {
        private readonly AuditService _audit = new AuditService();

        public void ExportCsv(string path, IEnumerable<AttendanceView> records)
        {
            if (!SessionContext.IsAuthenticated)
                throw new UnauthorizedAccessException("You must be signed in to export records.");
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("A destination file is required.", nameof(path));

            var rows = (records ?? Enumerable.Empty<AttendanceView>()).Select(r => new[]
            {
                r.AttendanceDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                r.StudentNumber,
                r.StudentName,
                r.Course,
                r.YearLevel.ToString(CultureInfo.InvariantCulture),
                r.Section,
                r.TimeInUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                r.TimeOutUtc.HasValue ? r.TimeOutUtc.Value.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) : string.Empty,
                r.Status,
                r.Source,
                r.Notes ?? string.Empty
            }).ToList();

            CsvWriter.Write(path,
                new[] { "Date", "Student Number", "Student Name", "Course", "Year", "Section", "Time In", "Time Out", "Status", "Source", "Notes" },
                rows);
            _audit.Log("Attendance records exported", "Export", null, "File=" + Path.GetFileName(path) + ";Rows=" + rows.Count.ToString(CultureInfo.InvariantCulture));
        }
    }
}
