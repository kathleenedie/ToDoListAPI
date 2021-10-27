using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Application.Models;
using Microsoft.AspNetCore.JsonPatch;
using ToDoList.API.Application.Repositories;

namespace ToDoList.API.Application.Controllers
{
    [ApiController]
    [Route("api/tasks")]

    public class TasksController : ControllerBase
    {
        private readonly IToDoTaskRepository _toDoTaskRepository;

        public TasksController(IToDoTaskRepository toDoTaskRepository)
        {
            _toDoTaskRepository = toDoTaskRepository;
        }

        [HttpGet(Name = "GetTasks")]
        public async Task<IEnumerable<ToDoTask>> GetTasks()
        {
            return await _toDoTaskRepository.Get();
        }

        [HttpGet("{id}", Name = "GetTask")]
        public async Task<ActionResult<ToDoTask>> GetTask(int id)
        {
            return await _toDoTaskRepository.Get(id);
        }

        [HttpPost(Name = "PostTasks")]
        public async Task<ActionResult<ToDoTask>> PostTask([FromBody] ToDoTask toDoTask)
        {
            var newTask = await _toDoTaskRepository.Create(toDoTask);
            return CreatedAtAction(nameof(GetTasks), new {id = newTask.Id}, newTask);
        }

        [HttpPut(Name = "UpdateTasks")]
        public async Task<ActionResult<ToDoTask>> UpdateTask(int id, [FromBody] ToDoTask toDoTask)
        {
            if (id != toDoTask.Id)
            {
                return BadRequest();
            }

            await _toDoTaskRepository.Update(toDoTask);
            return NoContent();
        }

        [HttpPatch("{id}", Name = "PatchTask")]
        public async Task<ActionResult<ToDoTask>> PatchTask(int id, [FromBody] JsonPatchDocument<ToDoTask> patchDoc)
        {
            var originalTask = await _toDoTaskRepository.Get(id);

            if (id != originalTask.Id)
            {
                return NotFound();
            }

            var patchTask = new ToDoTask()
            {
                Category = originalTask.Category,
                Description = originalTask.Description,
                Completed = originalTask.Completed
            };

            patchDoc.ApplyTo(originalTask, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(patchTask))
            {
                return BadRequest(ModelState);
            }

            await _toDoTaskRepository.Update(originalTask);

            return NoContent();
        }

        [HttpDelete(Name = "DeleteTasks")]
        public async Task<ActionResult> Delete(int id)
        {
            var taskToDelete = await _toDoTaskRepository.Get(id);
            if (taskToDelete == null)
                return NotFound();

            await _toDoTaskRepository.Delete(taskToDelete.Id);
            return NoContent();
        }

        [HttpGet("done", Name = "Completed")]
        public async Task<IEnumerable<ToDoTask>> GetCompleted()
        {
            return await _toDoTaskRepository.GetCompleted();
        }

        [HttpGet("todo", Name = "StillToDo")]
        public async Task<IEnumerable<ToDoTask>> GetToDo()
        {
            return await _toDoTaskRepository.GetToDo();
        }

        [HttpGet("category/{category}", Name = "CategoryFilter")]
        public async Task<IEnumerable<ToDoTask>> GetByCategory(string category)
        {
            return await _toDoTaskRepository.CategoryFilter(category);
        }

        [HttpGet("category/{category}/{completed}", Name = "CategoryStatus")]
        public async Task<List<ToDoTask>> GetStatusByCategory(string category, bool completed)
        {
            return await _toDoTaskRepository.CategoryStatus(category, completed);
        }
    }
}
