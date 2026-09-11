using System;

namespace JCGAttendanceSystem.Models
{
    internal sealed class AuditLogEntry
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string Details { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
