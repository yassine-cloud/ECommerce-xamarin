using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ECommerce.models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace ECommerce.auth
{
    class SessionManager
    {
        private const string TokenKey = "UserToken";
        private const string UserKey = "UserInfo";
        private const string UsernameKey = "Username";
        private const string UserRoleKey = "UserRole";
        private const string UserIdKey = "UserId";
        private const string UserConnected = "UserConnected";

        // Save token and preferences
        public static void SaveSession(string token, UserRead user)
        {
            SecureStorage.SetAsync(TokenKey, token);
            var userJson = JsonConvert.SerializeObject(user);
            Preferences.Set(UserKey, userJson);
            Preferences.Set(UsernameKey, user.nom + " " + user.prenom);
            Preferences.Set(UserRoleKey, user.role.ToString());
            Preferences.Set(UserIdKey, user.id);
            Preferences.Set(UserConnected, true);
        }

        // Retrieve token
        public static async Task<string> GetTokenAsync()
        {
            return await SecureStorage.GetAsync(TokenKey);
        }

        public static UserRead GetUser()
        {
            var userJson = Preferences.Get(UserKey, string.Empty);
            if (string.IsNullOrEmpty(userJson))
            {
                return null;  // No user info stored
            }
            return JsonConvert.DeserializeObject<UserRead>(userJson);  // Deserialize to UserRead object
        }

        public static string GetUserId()
        {
            return Preferences.Get(UserIdKey, string.Empty);
        }

        public static string GetUserRole()
        {
            return Preferences.Get(UserRoleKey, string.Empty);
        }

        public static bool IsUserConnected()
        {
            return Preferences.Get(UserConnected, false);
        }

        // Retrieve username
        public static string GetUsername()
        {
            return Preferences.Get(UsernameKey, string.Empty);
        }

        // Clear session
        public static void ClearSession()
        {
            SecureStorage.Remove(TokenKey);
            Preferences.Remove(UserKey);
            Preferences.Remove(UsernameKey);
            Preferences.Remove(UserRoleKey);
            Preferences.Remove(UserIdKey);
            Preferences.Set(UserConnected, false);

        }
    }
}
