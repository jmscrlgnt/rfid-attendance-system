using System;
using System.Collections.Generic;
using JCGAttendanceSystem.Data.Repositories;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.Security;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Services
{
    internal sealed class StudentService
    {
        private readonly StudentRepository _students = new StudentRepository();
        private readonly AuditService _audit = new AuditService();

        public IList<Student> Search(string query, bool includeInactive) => _students.Search(query, includeInactive);
        public Student GetById(int id) => _students.GetById(id);
        public Student GetByStudentNumber(string studentNumber) => _students.GetByStudentNumber((studentNumber ?? string.Empty).Trim());
        public Student GetByRfidTag(string rfidTag) => _students.GetByRfidTag((rfidTag ?? string.Empty).Trim());

        public Student Save(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            if (!SessionContext.IsAuthenticated) throw new UnauthorizedAccessException("You must be signed in to manage students.");

            Normalize(student);
            ValidateStudent(student);
            EnsureUnique(student);

            if (student.Id == 0)
            {
                student.IsActive = true;
                student.Id = _students.Insert(student);
                _audit.Log("Student created", "Student", student.Id.ToString(), "StudentNumber=" + student.StudentNumber);
            }
            else
            {
                if (_students.GetById(student.Id) == null) throw new InvalidOperationException("Student was not found.");
                _students.Update(student);
                _audit.Log("Student updated", "Student", student.Id.ToString(), "StudentNumber=" + student.StudentNumber);
            }
            return _students.GetById(student.Id);
        }

        public void SetActive(int studentId, bool isActive)
        {
            if (!SessionContext.IsAuthenticated) throw new UnauthorizedAccessException("You must be signed in to manage students.");
            var student = _students.GetById(studentId) ?? throw new InvalidOperationException("Student was not found.");
            _students.SetActive(studentId, isActive);
            _audit.Log(isActive ? "Student reactivated" : "Student deactivated", "Student", studentId.ToString(), "StudentNumber=" + student.StudentNumber);
        }

        private static void Normalize(Student student)
        {
            student.StudentNumber = Validation.NormalizeRequired(student.StudentNumber);
            student.FirstName = Validation.NormalizeRequired(student.FirstName);
            student.LastName = Validation.NormalizeRequired(student.LastName);
            student.Gender = Validation.NormalizeOptional(student.Gender);
            student.Email = Validation.NormalizeOptional(student.Email);
            student.Course = Validation.NormalizeRequired(student.Course);
            student.Section = Validation.NormalizeRequired(student.Section);
            student.RfidTag = Validation.NormalizeOptional(student.RfidTag);
        }

        private static void ValidateStudent(Student student)
        {
            if (student.StudentNumber == null) throw new ArgumentException("Student number is required.");
            if (student.FirstName == null) throw new ArgumentException("First name is required.");
            if (student.LastName == null) throw new ArgumentException("Last name is required.");
            if (student.Course == null) throw new ArgumentException("Course is required.");
            if (student.Section == null) throw new ArgumentException("Section is required.");
            if (student.YearLevel < 1 || student.YearLevel > 4) throw new ArgumentException("Year level must be from 1 to 4.");
            if (!Validation.IsValidEmail(student.Email)) throw new ArgumentException("Email address is not valid.");
        }

        private void EnsureUnique(Student student)
        {
            var byNumber = _students.GetByStudentNumber(student.StudentNumber);
            if (byNumber != null && byNumber.Id != student.Id)
                throw new InvalidOperationException("Another student already uses that student number.");

            if (!string.IsNullOrEmpty(student.Email))
            {
                var byEmail = _students.GetByEmail(student.Email);
                if (byEmail != null && byEmail.Id != student.Id)
                    throw new InvalidOperationException("Another student already uses that email.");
            }

            if (!string.IsNullOrEmpty(student.RfidTag))
            {
                var byTag = _students.GetByRfidTag(student.RfidTag);
                if (byTag != null && byTag.Id != student.Id)
                    throw new InvalidOperationException("Another student already uses that RFID tag.");
            }
        }
    }
}
