using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Username
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public string Email { get; set; }
    }
}
