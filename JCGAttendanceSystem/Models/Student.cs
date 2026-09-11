using System;

namespace JCGAttendanceSystem.Models
{
    public sealed class Student
    {
        public int Id { get; set; }
        public string StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Course { get; set; }
        public int YearLevel { get; set; }
        public string Section { get; set; }
        public string RfidTag { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime UpdatedAtUtc { get; set; }
        public string FullName => ((FirstName ?? string.Empty) + " " + (LastName ?? string.Empty)).Trim();
    }
}
