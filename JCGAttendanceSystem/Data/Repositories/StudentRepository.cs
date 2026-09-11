using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using JCGAttendanceSystem.Models;

namespace JCGAttendanceSystem.Data.Repositories
{
    internal sealed class StudentRepository
    {
        public IList<Student> Search(string query, bool includeInactive)
        {
            var items = new List<Student>();
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
SELECT Id, StudentNumber, FirstName, LastName, BirthDate, Gender, Email, Course,
       YearLevel, Section, RfidTag, IsActive, CreatedAtUtc, UpdatedAtUtc
FROM Students
WHERE (@IncludeInactive = 1 OR IsActive = 1)
  AND (@Query = '' OR StudentNumber LIKE @LikeQuery COLLATE NOCASE
       OR FirstName LIKE @LikeQuery COLLATE NOCASE
       OR LastName LIKE @LikeQuery COLLATE NOCASE
       OR (FirstName || ' ' || LastName) LIKE @LikeQuery COLLATE NOCASE
       OR IFNULL(Email, '') LIKE @LikeQuery COLLATE NOCASE
       OR Course LIKE @LikeQuery COLLATE NOCASE
       OR CAST(YearLevel AS TEXT) LIKE @LikeQuery
       OR Section LIKE @LikeQuery COLLATE NOCASE)
ORDER BY LastName COLLATE NOCASE, FirstName COLLATE NOCASE;", connection))
            {
                var normalized = (query ?? string.Empty).Trim();
                command.Parameters.AddWithValue("@IncludeInactive", includeInactive ? 1 : 0);
                command.Parameters.AddWithValue("@Query", normalized);
                command.Parameters.AddWithValue("@LikeQuery", "%" + normalized + "%");
                using (var reader = command.ExecuteReader())
                    while (reader.Read()) items.Add(Map(reader));
            }
            return items;
        }

        public Student GetById(int id) => GetSingle("Id = @Value", id);
        public Student GetByStudentNumber(string value) => GetSingle("StudentNumber = @Value COLLATE NOCASE", value);
        public Student GetByEmail(string value) => string.IsNullOrWhiteSpace(value) ? null : GetSingle("Email = @Value COLLATE NOCASE", value.Trim());
        public Student GetByRfidTag(string value) => string.IsNullOrWhiteSpace(value) ? null : GetSingle("RfidTag = @Value COLLATE NOCASE", value.Trim());

        public int Insert(Student student)
        {
            var now = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
INSERT INTO Students(StudentNumber, FirstName, LastName, BirthDate, Gender, Email, Course, YearLevel, Section, RfidTag, IsActive, CreatedAtUtc, UpdatedAtUtc)
VALUES(@StudentNumber, @FirstName, @LastName, @BirthDate, @Gender, @Email, @Course, @YearLevel, @Section, @RfidTag, @IsActive, @Now, @Now);
SELECT last_insert_rowid();", connection))
            {
                AddParameters(command, student);
                command.Parameters.AddWithValue("@Now", now);
                return Convert.ToInt32((long)command.ExecuteScalar(), CultureInfo.InvariantCulture);
            }
        }

        public void Update(Student student)
        {
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
UPDATE Students
SET StudentNumber=@StudentNumber, FirstName=@FirstName, LastName=@LastName, BirthDate=@BirthDate,
    Gender=@Gender, Email=@Email, Course=@Course, YearLevel=@YearLevel, Section=@Section,
    RfidTag=@RfidTag, IsActive=@IsActive, UpdatedAtUtc=@UpdatedAtUtc
WHERE Id=@Id;", connection))
            {
                AddParameters(command, student);
                command.Parameters.AddWithValue("@UpdatedAtUtc", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@Id", student.Id);
                command.ExecuteNonQuery();
            }
        }

        public void SetActive(int id, bool isActive)
        {
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand("UPDATE Students SET IsActive = @IsActive, UpdatedAtUtc = @UpdatedAtUtc WHERE Id = @Id;", connection))
            {
                command.Parameters.AddWithValue("@IsActive", isActive ? 1 : 0);
                command.Parameters.AddWithValue("@UpdatedAtUtc", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        private static void AddParameters(SQLiteCommand command, Student student)
        {
            command.Parameters.AddWithValue("@StudentNumber", student.StudentNumber);
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@BirthDate", student.BirthDate.HasValue ? (object)student.BirthDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : DBNull.Value);
            command.Parameters.AddWithValue("@Gender", (object)student.Gender ?? DBNull.Value);
            command.Parameters.AddWithValue("@Email", (object)student.Email ?? DBNull.Value);
            command.Parameters.AddWithValue("@Course", student.Course);
            command.Parameters.AddWithValue("@YearLevel", student.YearLevel);
            command.Parameters.AddWithValue("@Section", student.Section);
            command.Parameters.AddWithValue("@RfidTag", (object)student.RfidTag ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsActive", student.IsActive ? 1 : 0);
        }

        private Student GetSingle(string predicate, object value)
        {
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
SELECT Id, StudentNumber, FirstName, LastName, BirthDate, Gender, Email, Course,
       YearLevel, Section, RfidTag, IsActive, CreatedAtUtc, UpdatedAtUtc
FROM Students WHERE " + predicate + " LIMIT 1;", connection))
            {
                command.Parameters.AddWithValue("@Value", value);
                using (var reader = command.ExecuteReader())
                    return reader.Read() ? Map(reader) : null;
            }
        }

        private static Student Map(SQLiteDataReader reader)
        {
            return new Student
            {
                Id = Convert.ToInt32(reader["Id"], CultureInfo.InvariantCulture),
                StudentNumber = Convert.ToString(reader["StudentNumber"], CultureInfo.InvariantCulture),
                FirstName = Convert.ToString(reader["FirstName"], CultureInfo.InvariantCulture),
                LastName = Convert.ToString(reader["LastName"], CultureInfo.InvariantCulture),
                BirthDate = reader["BirthDate"] == DBNull.Value ? (DateTime?)null : DateTime.ParseExact(Convert.ToString(reader["BirthDate"], CultureInfo.InvariantCulture), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Gender = reader["Gender"] == DBNull.Value ? null : Convert.ToString(reader["Gender"], CultureInfo.InvariantCulture),
                Email = reader["Email"] == DBNull.Value ? null : Convert.ToString(reader["Email"], CultureInfo.InvariantCulture),
                Course = Convert.ToString(reader["Course"], CultureInfo.InvariantCulture),
                YearLevel = Convert.ToInt32(reader["YearLevel"], CultureInfo.InvariantCulture),
                Section = Convert.ToString(reader["Section"], CultureInfo.InvariantCulture),
                RfidTag = reader["RfidTag"] == DBNull.Value ? null : Convert.ToString(reader["RfidTag"], CultureInfo.InvariantCulture),
                IsActive = Convert.ToInt32(reader["IsActive"], CultureInfo.InvariantCulture) == 1,
                CreatedAtUtc = DateTime.Parse(Convert.ToString(reader["CreatedAtUtc"], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                UpdatedAtUtc = DateTime.Parse(Convert.ToString(reader["UpdatedAtUtc"], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
            };
        }
    }
}
