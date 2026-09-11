using System;

namespace JCGAttendanceSystem.Models
{
    internal sealed class AttendanceFilter
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string StudentQuery { get; set; }
        public string Course { get; set; }
        public int? YearLevel { get; set; }
        public string Section { get; set; }
        public string Status { get; set; }
    }
}
