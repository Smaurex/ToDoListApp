
using System;
using System.Collections.Generic;
using System.Text;


namespace ToDoList.Models
{
    public static class TaskRepository
    {
        public static List<TaskItem> taskList = new List<TaskItem>()
        {
            new TaskItem { TaskId = 1, Title = "Buy groceries", Detail = "Milk, Bread, Eggs" },
            new TaskItem { TaskId = 2,Title = "Call Mom", Detail = "Her birthday is coming up" },
            new TaskItem { TaskId = 3, Title = "Finish project", Detail = "Due next week" }

        };


        public static List<TaskItem> GetTask() => taskList;
    
        public static TaskItem GetTaskById(int taskId)
        {
            var taskSelected = taskList.FirstOrDefault(t => t.TaskId == taskId);
            if (taskSelected != null)
            {
                return new TaskItem
                {
                    TaskId = taskSelected.TaskId,
                    Title = taskSelected.Title,
                    Detail = taskSelected.Detail
                };
            }
            return null;
        }

        public static void AddTask(TaskItem newTaskPopulated)
        {
            taskList.Add(newTaskPopulated);
        }

        public static void UpdateTask(int taskId, TaskItem taskPopulated)
        {
            if (taskId != taskPopulated.TaskId) return;

            var taskToUpdate = taskList.FirstOrDefault(t => t.TaskId == taskId);
            if (taskToUpdate != null)
            {
                taskToUpdate.Title = taskPopulated.Title;
                taskToUpdate.Detail = taskPopulated.Detail;
            }
        }

        public static void DeleteTask(int taskId)
        {
            var taskToDelete = taskList.FirstOrDefault(t => t.TaskId == taskId); //lamentory expression
            if (taskToDelete != null)
            {
                taskList.Remove(taskToDelete);
            }
        }

        public static int NewId()
        {
            return taskList.Any() ? taskList.Max(x => x.TaskId) + 1 : 1;
        }
    }
}
