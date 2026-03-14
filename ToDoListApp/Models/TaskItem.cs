using System;
using System.Collections.Generic;
using System.Text;
//This is the class for each ITEMS in the list
namespace ToDoListApp.Models
{
    public class TaskItem
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
    }
}
