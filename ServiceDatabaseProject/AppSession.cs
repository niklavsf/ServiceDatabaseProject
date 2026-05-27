using System;

namespace ServiceDatabaseProject
{
    public static class AppSession
    {
        public static UserAuthenticationForm.AppUser CurrentUser { get; set; }

        public static bool IsAdmin
        {
            get
            {
                return CurrentUser != null &&
                       string.Equals(CurrentUser.UserType, "ADMIN", StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
