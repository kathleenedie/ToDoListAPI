using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.API.Application.Models;

namespace ToDoList.API.Application
{
    public class TaskDataStore
    {
        public static TaskDataStore Current { get; } = new TaskDataStore();

        public List<ToDoTask> Tasks { get; set; }

        public TaskDataStore()
        {
            Tasks = new List<ToDoTask>()
            {
                new ToDoTask()
                {
                    Id = 1,
                    Category = "Home",
                    Description = "Load dishwasher",
                    Completed = false
                },

                new ToDoTask()
                {
                    Id = 2,
                    Category = "Study",
                    Description = "Update OneNote with UML connector notes",
                    Completed = false
                },

                new ToDoTask()
                {
                    Id = 3,
                    Category = "Garden",
                    Description = "Buy sharp sand for the patio extension",
                    Completed = false
                },

                new ToDoTask()
                {
                    Id = 4,
                    Category = "Garden",
                    Description = "Pick up slabs from Jean for patio extension",
                    Completed = true
                },

                new ToDoTask()
                {
                    Id = 5,
                    Category = "Work",
                    Description = "Write an API for a to do list",
                    Completed = false
                }

            };
        }
    }
}
