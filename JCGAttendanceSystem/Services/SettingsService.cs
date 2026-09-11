using System;
using JCGAttendanceSystem.Data.Repositories;
using JCGAttendanceSystem.Security;

namespace JCGAttendanceSystem.Services
{
    internal sealed class SettingsService
    {
        private readonly SettingsRepository _settings = new SettingsRepository();
        private readonly AuditService _audit = new AuditService();

        public string GetOrganizationName()
        {
            EnsureSignedIn();
            return _settings.Get("OrganizationName", "JCG Attendance System");
        }

        public void SetOrganizationName(string value)
        {
            if (!SessionContext.IsAdministrator)
                throw new UnauthorizedAccessException("Administrator access is required to change organization settings.");
            var normalized = string.IsNullOrWhiteSpace(value) ? "JCG Attendance System" : value.Trim();
            _settings.Set("OrganizationName", normalized);
            _audit.Log("Organization name changed", "Setting", "OrganizationName", "Value=" + normalized);
        }

        private static void EnsureSignedIn()
        {
            if (!SessionContext.IsAuthenticated)
                throw new UnauthorizedAccessException("You must be signed in to view settings.");
        }
    }
}
