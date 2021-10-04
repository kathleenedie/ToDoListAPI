using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Application.Models;

namespace ToDoList.API.Application.Controllers
{  
    [ApiController]
    [Route("api/tasks")]

    public class TasksController : ControllerBase
    {
        [HttpGet(Name = "GetTasks")]
        public IActionResult GetTasks()
        {
            return Ok(TaskDataStore.Current.Tasks);
        }

        [HttpGet("{id}", Name = "GetTask")]
        public IActionResult GetTask(int id)
        {
            var taskToReturn = TaskDataStore.Current.Tasks.FirstOrDefault(taskDto => taskDto.Id == id);

            if (taskToReturn == null)
            {
                return NotFound();
            }

            return Ok(taskToReturn);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskForCreationDto task)
        {
            var maxTaskId = TaskDataStore.Current.Tasks.Max(t => t.Id);

            var finalTask = new TaskDto()
            {
                Id = ++maxTaskId,
                Category = task.Category,
                Description = task.Description
            };

            TaskDataStore.Current.Tasks.Add(finalTask);

            return CreatedAtRoute("GetTasks",
                new {id = finalTask.Id},
                finalTask);
        }
    }
}
