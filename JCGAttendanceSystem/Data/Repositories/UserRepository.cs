using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using JCGAttendanceSystem.Models;

namespace JCGAttendanceSystem.Data.Repositories
{
    internal sealed class UserRepository
    {
        public int CountUsers()
        {
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand("SELECT COUNT(1) FROM Users;", connection))
                return Convert.ToInt32(command.ExecuteScalar(), CultureInfo.InvariantCulture);
        }

        public int CountActiveAdministrators()
        {
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand("SELECT COUNT(1) FROM Users WHERE Role = @Role AND IsActive = 1;", connection))
            {
                command.Parameters.AddWithValue("@Role", Security.UserRoles.Administrator);
                return Convert.ToInt32(command.ExecuteScalar(), CultureInfo.InvariantCulture);
            }
        }

        public User GetByUsername(string username)
        {
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
SELECT Id, Username, PasswordHash, PasswordSalt, Role, IsActive, CreatedAtUtc, UpdatedAtUtc
FROM Users WHERE Username = @Username COLLATE NOCASE LIMIT 1;", connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                using (var reader = command.ExecuteReader())
                    return reader.Read() ? Map(reader) : null;
            }
        }

        public User GetById(int id)
        {
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
SELECT Id, Username, PasswordHash, PasswordSalt, Role, IsActive, CreatedAtUtc, UpdatedAtUtc
FROM Users WHERE Id = @Id LIMIT 1;", connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                    return reader.Read() ? Map(reader) : null;
            }
        }

        public IList<User> GetAll()
        {
            var users = new List<User>();
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
SELECT Id, Username, PasswordHash, PasswordSalt, Role, IsActive, CreatedAtUtc, UpdatedAtUtc
FROM Users ORDER BY Username COLLATE NOCASE;", connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read()) users.Add(Map(reader));
            }
            return users;
        }

        public int Insert(string username, string passwordHash, string passwordSalt, string role)
        {
            var now = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
INSERT INTO Users(Username, PasswordHash, PasswordSalt, Role, IsActive, CreatedAtUtc, UpdatedAtUtc)
VALUES(@Username, @PasswordHash, @PasswordSalt, @Role, 1, @Now, @Now);
SELECT last_insert_rowid();", connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                command.Parameters.AddWithValue("@PasswordSalt", passwordSalt);
                command.Parameters.AddWithValue("@Role", role);
                command.Parameters.AddWithValue("@Now", now);
                return Convert.ToInt32((long)command.ExecuteScalar(), CultureInfo.InvariantCulture);
            }
        }

        public void UpdatePassword(int id, string passwordHash, string passwordSalt)
        {
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
UPDATE Users SET PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt, UpdatedAtUtc = @UpdatedAtUtc
WHERE Id = @Id;", connection))
            {
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                command.Parameters.AddWithValue("@PasswordSalt", passwordSalt);
                command.Parameters.AddWithValue("@UpdatedAtUtc", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        public void SetActive(int id, bool isActive)
        {
            using (var connection = Database.OpenConnection())
            using (var command = new SQLiteCommand(@"
UPDATE Users SET IsActive = @IsActive, UpdatedAtUtc = @UpdatedAtUtc WHERE Id = @Id;", connection))
            {
                command.Parameters.AddWithValue("@IsActive", isActive ? 1 : 0);
                command.Parameters.AddWithValue("@UpdatedAtUtc", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        private static User Map(SQLiteDataReader reader)
        {
            return new User
            {
                Id = Convert.ToInt32(reader["Id"], CultureInfo.InvariantCulture),
                Username = Convert.ToString(reader["Username"], CultureInfo.InvariantCulture),
                PasswordHash = Convert.ToString(reader["PasswordHash"], CultureInfo.InvariantCulture),
                PasswordSalt = Convert.ToString(reader["PasswordSalt"], CultureInfo.InvariantCulture),
                Role = Convert.ToString(reader["Role"], CultureInfo.InvariantCulture),
                IsActive = Convert.ToInt32(reader["IsActive"], CultureInfo.InvariantCulture) == 1,
                CreatedAtUtc = DateTime.Parse(Convert.ToString(reader["CreatedAtUtc"], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                UpdatedAtUtc = DateTime.Parse(Convert.ToString(reader["UpdatedAtUtc"], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
            };
        }
    }
}
