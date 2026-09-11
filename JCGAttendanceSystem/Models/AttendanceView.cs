using System;

namespace JCGAttendanceSystem.Models
{
    public sealed class AttendanceView
    {
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public string StudentNumber { get; set; }
        public string StudentName { get; set; }
        public string Course { get; set; }
        public int YearLevel { get; set; }
        public string Section { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime TimeInUtc { get; set; }
        public DateTime? TimeOutUtc { get; set; }
        public string Source { get; set; }
        public string Notes { get; set; }
        public string EventType { get; set; }
        public DateTime? EventTimeUtc { get; set; }
        public string Status => TimeOutUtc.HasValue ? "Completed" : "Timed In";
    }
}
