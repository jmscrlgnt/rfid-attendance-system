namespace JCGAttendanceSystem.Models
{
    internal sealed class AttendanceActionResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public AttendanceRecord Record { get; set; }

        public static AttendanceActionResult Ok(string message, AttendanceRecord record)
        {
            return new AttendanceActionResult { Success = true, Message = message, Record = record };
        }

        public static AttendanceActionResult Fail(string message, AttendanceRecord record = null)
        {
            return new AttendanceActionResult { Success = false, Message = message, Record = record };
        }
    }

    internal static class AttendanceSources
    {
        public const string Manual = "Manual";
        public const string Rfid = "RFID";
    }
}
