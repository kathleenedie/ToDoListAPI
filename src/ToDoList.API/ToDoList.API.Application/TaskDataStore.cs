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

        public List<TaskDto> Tasks { get; set; }

        public TaskDataStore()
        {
            Tasks = new List<TaskDto>()
            {
                new TaskDto()
                {
                    Id = 1,
                    Category = "Home",
                    Description = "Load dishwasher",
                    Completed = false
                },

                new TaskDto()
                {
                    Id = 2,
                    Category = "Study",
                    Description = "Update OneNote with UML connector notes",
                    Completed = false
                },

                new TaskDto()
                {
                    Id = 3,
                    Category = "Garden",
                    Description = "Buy sharp sand for the patio extension",
                    Completed = false
                },

                new TaskDto()
                {
                    Id = 4,
                    Category = "Garden",
                    Description = "Pick up slabs from Jean for patio extension",
                    Completed = true
                },

                new TaskDto()
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
