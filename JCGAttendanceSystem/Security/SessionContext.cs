using JCGAttendanceSystem.Models;

namespace JCGAttendanceSystem.Security
{
    internal sealed class SessionUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }

    internal static class SessionContext
    {
        public static SessionUser CurrentUser { get; private set; }
        public static bool IsAuthenticated => CurrentUser != null;
        public static bool IsAdministrator => CurrentUser != null && CurrentUser.Role == UserRoles.Administrator;

        public static void SignIn(User user)
        {
            CurrentUser = user == null
                ? null
                : new SessionUser
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = user.Role
                };
        }

        public static void SignOut()
        {
            CurrentUser = null;
        }
    }

    internal static class UserRoles
    {
        public const string Administrator = "Administrator";
        public const string Staff = "Staff";
    }
}
