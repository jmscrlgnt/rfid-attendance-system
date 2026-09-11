using System.Collections.Generic;

namespace JCGAttendanceSystem.Models
{
    internal sealed class LegacyImportSummary
    {
        public int ImportedStudents { get; set; }
        public int SkippedStudents { get; set; }
        public int ImportedAttendance { get; set; }
        public int SkippedAttendance { get; set; }
        public IList<string> Messages { get; } = new List<string>();

        public override string ToString()
        {
            return "Students imported: " + ImportedStudents +
                   ", skipped: " + SkippedStudents +
                   ". Attendance imported: " + ImportedAttendance +
                   ", skipped: " + SkippedAttendance + ".";
        }
    }
}
