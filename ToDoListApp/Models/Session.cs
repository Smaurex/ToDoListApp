using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListApp.Models
{
    public static class Session
    {
        public static User CurrentUser { get; set; }

        public static bool IsLoggedIn
        {
            get { return CurrentUser != null; }
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
