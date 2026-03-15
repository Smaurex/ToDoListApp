using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListApp.Models
{
    public static class UserRepository
    {
        private static List<User> users = new List<User>();

        public static void AddUser(User user)
        {
            users.Add(user);
        }

        public static User GetUserByEmail(string email)
        {
            return users.FirstOrDefault(u => u.Email == email);
        }
    }
}
