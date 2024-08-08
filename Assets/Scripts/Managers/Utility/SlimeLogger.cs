using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utility
{
    public enum LogCategory
    {
        Info, Warning, Errors
    }

    public enum User
    {
        Default,
        Louis,
        Liam,
    }

    public static class SlimeLogger
    {
        private static List<LogCategory> SuppressedCategories = new();
        private static List<User> SuppressedUsers = new();


        public static void Log(string message)
        {
            Log(LogCategory.Info, message);
        }

        public static void Log(LogCategory category, string message)
        {
            Log(User.Default, category, message);
        }

        public static void Log(User user, LogCategory category, string message)
        {
            if (SuppressedUsers.Contains(user))
            {
                return;
            }
            if (SuppressedCategories.Contains(category))
            {
                return;
            }

            string userText = user == User.Default ? "" : user.ToString();
            userText += message;

            switch (category)
            {
                case LogCategory.Info:
                    Debug.Log(userText);
                    break;
                case LogCategory.Warning:
                    Debug.LogWarning(userText);
                    break;
                case LogCategory.Errors:
                    Debug.LogError(userText);
                    break;
            }

        }

        public static void SupressCategory(LogCategory category)
        {
            SuppressedCategories.Add(category);
        }
        public static void SupressUser(User user)
        {
            SuppressedUsers.Add(user);
        }
    }

}
