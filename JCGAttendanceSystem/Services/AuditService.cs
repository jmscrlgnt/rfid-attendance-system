using System.Collections.Generic;
using JCGAttendanceSystem.Data.Repositories;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.Security;

namespace JCGAttendanceSystem.Services
{
    internal sealed class AuditService
    {
        private readonly AuditLogRepository _repository = new AuditLogRepository();

        public void Log(string action, string entityType = null, string entityId = null, string details = null)
        {
            _repository.Add(SessionContext.CurrentUser?.Id, action, entityType, entityId, details);
        }

        public void LogAs(int? userId, string action, string entityType = null, string entityId = null, string details = null)
        {
            _repository.Add(userId, action, entityType, entityId, details);
        }

        public IList<AuditLogEntry> GetRecentForAdministrator(int limit)
        {
            if (!SessionContext.IsAdministrator)
                throw new System.UnauthorizedAccessException("Administrator access is required to view audit history.");
            return _repository.GetRecent(limit);
        }
    }
}
