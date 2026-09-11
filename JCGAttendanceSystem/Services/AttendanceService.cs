using System;
using JCGAttendanceSystem.Data;
using JCGAttendanceSystem.Data.Repositories;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.Security;

namespace JCGAttendanceSystem.Services
{
    internal sealed class AttendanceService
    {
        private readonly AttendanceRepository _attendance = new AttendanceRepository();
        private readonly AuditService _audit = new AuditService();

        public AttendanceRecord GetTodayStatus(int studentId)
        {
            return _attendance.GetForDate(studentId, DateTime.Today);
        }

        public AttendanceActionResult TimeIn(Student student, string source)
        {
            EnsureSignedIn();
            ValidateStudentAndSource(student, source);
            if (!student.IsActive) return AttendanceActionResult.Fail("Attendance cannot be recorded for an inactive student.");

            using (var connection = Database.OpenConnection())
            using (var transaction = connection.BeginTransaction())
            {
                var existing = _attendance.GetForDate(connection, transaction, student.Id, DateTime.Today);
                if (existing != null)
                {
                    transaction.Rollback();
                    return existing.TimeOutUtc.HasValue
                        ? AttendanceActionResult.Fail("Attendance is already completed for this student today.", existing)
                        : AttendanceActionResult.Fail("This student is already timed in today.", existing);
                }

                var nowUtc = DateTime.UtcNow;
                var id = _attendance.InsertTimeIn(connection, transaction, student.Id, DateTime.Today, nowUtc, source, SessionContext.CurrentUser.Id);
                transaction.Commit();
                var record = _attendance.GetById(id);
                _audit.Log("Attendance time in", "AttendanceRecord", id.ToString(), "StudentNumber=" + student.StudentNumber + ";Source=" + source);
                return AttendanceActionResult.Ok("Time in recorded successfully.", record);
            }
        }

        public AttendanceActionResult TimeOut(Student student, string source)
        {
            EnsureSignedIn();
            ValidateStudentAndSource(student, source);
            if (!student.IsActive) return AttendanceActionResult.Fail("Attendance cannot be recorded for an inactive student.");

            using (var connection = Database.OpenConnection())
            using (var transaction = connection.BeginTransaction())
            {
                var existing = _attendance.GetForDate(connection, transaction, student.Id, DateTime.Today);
                if (existing == null)
                {
                    transaction.Rollback();
                    return AttendanceActionResult.Fail("Time out cannot be recorded before a time in.");
                }
                if (existing.TimeOutUtc.HasValue)
                {
                    transaction.Rollback();
                    return AttendanceActionResult.Fail("Attendance is already completed for this student today.", existing);
                }

                _attendance.SetTimeOut(connection, transaction, existing.Id, DateTime.UtcNow, SessionContext.CurrentUser.Id);
                transaction.Commit();
                var record = _attendance.GetById(existing.Id);
                _audit.Log("Attendance time out", "AttendanceRecord", existing.Id.ToString(), "StudentNumber=" + student.StudentNumber + ";Source=" + source);
                return AttendanceActionResult.Ok("Time out recorded successfully.", record);
            }
        }

        public void CorrectAttendance(int attendanceId, DateTime timeInLocal, DateTime? timeOutLocal, string notes)
        {
            if (!SessionContext.IsAdministrator)
                throw new UnauthorizedAccessException("Administrator access is required to correct attendance.");
            if (timeOutLocal.HasValue && timeOutLocal.Value < timeInLocal)
                throw new ArgumentException("Time out cannot be earlier than time in.");

            var existing = _attendance.GetById(attendanceId) ?? throw new InvalidOperationException("Attendance record was not found.");
            var timeInUtc = ToUtc(timeInLocal);
            var timeOutUtc = timeOutLocal.HasValue ? (DateTime?)ToUtc(timeOutLocal.Value) : null;
            _attendance.Correct(attendanceId, timeInUtc, timeOutUtc, notes, SessionContext.CurrentUser.Id);
            _audit.Log("Attendance corrected", "AttendanceRecord", attendanceId.ToString(),
                "OldTimeInUtc=" + existing.TimeInUtc.ToString("o") + ";OldTimeOutUtc=" + (existing.TimeOutUtc?.ToString("o") ?? "null"));
        }

        private static void EnsureSignedIn()
        {
            if (!SessionContext.IsAuthenticated)
                throw new UnauthorizedAccessException("You must be signed in to record attendance.");
        }

        private static void ValidateStudentAndSource(Student student, string source)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            if (student.Id <= 0) throw new ArgumentException("A saved student is required.");
            if (source != AttendanceSources.Manual && source != AttendanceSources.Rfid)
                throw new ArgumentException("Attendance source must be Manual or RFID.");
        }

        private static DateTime ToUtc(DateTime local)
        {
            if (local.Kind == DateTimeKind.Utc) return local;
            if (local.Kind == DateTimeKind.Unspecified) local = DateTime.SpecifyKind(local, DateTimeKind.Local);
            return local.ToUniversalTime();
        }
    }
}
