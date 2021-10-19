using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Application.Models;
using Microsoft.AspNetCore.JsonPatch;

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

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskForUpdateDto task)
        {
            var taskToUpdate = TaskDataStore.Current.Tasks.FirstOrDefault(taskDto => taskDto.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            taskToUpdate.Category = task.Category;
            taskToUpdate.Description = task.Description;
            taskToUpdate.Completed = task.Completed;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateTask(int id, [FromBody] JsonPatchDocument<TaskForUpdateDto> patchDoc)
        {
            var taskFromStore = TaskDataStore.Current.Tasks.FirstOrDefault(taskDto => taskDto.Id == id);
            if (taskFromStore == null)
            {
                return NotFound();
            }

            var taskToPatch = new TaskForUpdateDto()
            {
                Category = taskFromStore.Category,
                Description = taskFromStore.Description,
                Completed = taskFromStore.Completed
            };

            patchDoc.ApplyTo(taskToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(taskToPatch))
            {
                return BadRequest(ModelState);
            }

            taskFromStore.Category = taskToPatch.Category;
            taskFromStore.Description = taskToPatch.Description;
            taskFromStore.Completed = taskToPatch.Completed;

            return NoContent();
        }
    }
}
