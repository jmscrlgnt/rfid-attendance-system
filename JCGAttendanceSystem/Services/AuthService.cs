using System;
using System.Collections.Generic;
using JCGAttendanceSystem.Data.Repositories;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.Security;

namespace JCGAttendanceSystem.Services
{
    internal sealed class AuthService
    {
        private readonly UserRepository _users = new UserRepository();
        private readonly AuditService _audit = new AuditService();

        public bool NeedsInitialAdminSetup()
        {
            return _users.CountUsers() == 0;
        }

        public User CreateInitialAdministrator(string username, string password)
        {
            if (_users.CountUsers() != 0)
                throw new InvalidOperationException("Initial administrator setup has already been completed.");

            ValidateUsernameAndPassword(username, password);
            var result = PasswordHasher.HashPassword(password);
            var id = _users.Insert(username.Trim(), result.HashBase64, result.SaltBase64, UserRoles.Administrator);
            _audit.LogAs(id, "Initial administrator created", "User", id.ToString(), "Role=Administrator");
            return _users.GetById(id);
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _users.GetByUsername(username.Trim());
            if (user == null || !user.IsActive || !PasswordHasher.Verify(password, user.PasswordSalt, user.PasswordHash))
            {
                _audit.LogAs(user?.Id, "Login failed", "User", user?.Id.ToString(), "Username=" + username.Trim());
                return null;
            }

            SessionContext.SignIn(user);
            _audit.Log("Login successful", "User", user.Id.ToString(), "Role=" + user.Role);
            return user;
        }

        public void Logout()
        {
            if (SessionContext.CurrentUser != null)
                _audit.Log("Logout", "User", SessionContext.CurrentUser.Id.ToString());
            SessionContext.SignOut();
        }

        public IList<User> GetUsersForAdministrator()
        {
            EnsureAdministrator();
            return _users.GetAll();
        }

        public User CreateStaff(string username, string password)
        {
            EnsureAdministrator();
            ValidateUsernameAndPassword(username, password);
            if (_users.GetByUsername(username.Trim()) != null)
                throw new InvalidOperationException("That username is already in use.");

            var result = PasswordHasher.HashPassword(password);
            var id = _users.Insert(username.Trim(), result.HashBase64, result.SaltBase64, UserRoles.Staff);
            _audit.Log("Staff user created", "User", id.ToString(), "Username=" + username.Trim());
            return _users.GetById(id);
        }

        public void SetUserActive(int userId, bool isActive)
        {
            EnsureAdministrator();
            var target = _users.GetById(userId) ?? throw new InvalidOperationException("User was not found.");

            if (!isActive && SessionContext.CurrentUser != null && target.Id == SessionContext.CurrentUser.Id)
                throw new InvalidOperationException("You cannot deactivate your own signed-in account.");

            if (!isActive && target.Role == UserRoles.Administrator && _users.CountActiveAdministrators() <= 1)
                throw new InvalidOperationException("The last active administrator cannot be deactivated.");

            _users.SetActive(userId, isActive);
            _audit.Log(isActive ? "User activated" : "User deactivated", "User", userId.ToString(), "Username=" + target.Username);
        }

        public void ResetUserPassword(int userId, string newPassword)
        {
            EnsureAdministrator();
            ValidatePassword(newPassword);
            var target = _users.GetById(userId) ?? throw new InvalidOperationException("User was not found.");
            var result = PasswordHasher.HashPassword(newPassword);
            _users.UpdatePassword(userId, result.HashBase64, result.SaltBase64);
            if (SessionContext.CurrentUser != null && SessionContext.CurrentUser.Id == userId)
                SessionContext.SignIn(_users.GetById(userId));
            _audit.Log("User password reset", "User", userId.ToString(), "Username=" + target.Username);
        }

        public void ChangeOwnPassword(string currentPassword, string newPassword)
        {
            if (!SessionContext.IsAuthenticated)
                throw new UnauthorizedAccessException("You must be signed in to change your password.");
            var currentUser = _users.GetById(SessionContext.CurrentUser.Id)
                ?? throw new InvalidOperationException("The signed-in account could not be found.");
            if (!PasswordHasher.Verify(currentPassword, currentUser.PasswordSalt, currentUser.PasswordHash))
                throw new InvalidOperationException("Current password is incorrect.");
            ValidatePassword(newPassword);

            var result = PasswordHasher.HashPassword(newPassword);
            _users.UpdatePassword(currentUser.Id, result.HashBase64, result.SaltBase64);
            SessionContext.SignIn(_users.GetById(currentUser.Id));
            _audit.Log("Password changed", "User", currentUser.Id.ToString());
        }

        private static void ValidateUsernameAndPassword(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Trim().Length < 3)
                throw new ArgumentException("Username must contain at least 3 characters.");
            ValidatePassword(password);
        }

        private static void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
                throw new ArgumentException("Password must contain at least 8 characters.");
        }

        private static void EnsureAdministrator()
        {
            if (!SessionContext.IsAdministrator)
                throw new UnauthorizedAccessException("Administrator access is required for this operation.");
        }
    }
}
