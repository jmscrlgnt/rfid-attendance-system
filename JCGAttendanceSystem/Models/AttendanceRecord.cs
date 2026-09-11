using System;

namespace JCGAttendanceSystem.Models
{
    internal sealed class AttendanceRecord
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime TimeInUtc { get; set; }
        public DateTime? TimeOutUtc { get; set; }
        public string Source { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime UpdatedAtUtc { get; set; }
    }
}
