using System;
using System.Collections.Generic;
using JCGAttendanceSystem.Data.Repositories;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.Security;

namespace JCGAttendanceSystem.Services
{
    internal sealed class DashboardService
    {
        private readonly AttendanceRepository _attendance = new AttendanceRepository();

        public DashboardSummary GetTodaySummary()
        {
            EnsureSignedIn();
            return _attendance.GetDashboardSummary(DateTime.Today);
        }

        public IList<AttendanceView> GetRecentActivity(int limit = 10)
        {
            EnsureSignedIn();
            return _attendance.GetRecentActivity(limit);
        }

        private static void EnsureSignedIn()
        {
            if (!SessionContext.IsAuthenticated)
                throw new UnauthorizedAccessException("You must be signed in to view the dashboard.");
        }
    }
}
