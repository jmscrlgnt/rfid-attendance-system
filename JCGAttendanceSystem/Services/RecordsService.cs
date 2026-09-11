using System;
using System.Collections.Generic;
using JCGAttendanceSystem.Data.Repositories;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.Security;

namespace JCGAttendanceSystem.Services
{
    internal sealed class RecordsService
    {
        private readonly AttendanceRepository _repository = new AttendanceRepository();

        public IList<AttendanceView> Search(AttendanceFilter filter)
        {
            if (!SessionContext.IsAuthenticated)
                throw new UnauthorizedAccessException("You must be signed in to view attendance records.");
            return _repository.SearchRecords(filter);
        }
    }
}
