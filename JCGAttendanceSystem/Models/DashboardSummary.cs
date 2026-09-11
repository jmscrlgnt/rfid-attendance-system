namespace JCGAttendanceSystem.Models
{
    internal sealed class DashboardSummary
    {
        public int TotalActiveStudents { get; set; }
        public int PresentToday { get; set; }
        public int CurrentlyTimedIn { get; set; }
        public int CompletedToday { get; set; }
    }
}
