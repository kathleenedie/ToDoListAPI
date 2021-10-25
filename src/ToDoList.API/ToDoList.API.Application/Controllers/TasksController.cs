using System;
using System.Collections.Generic;
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

        [HttpDelete(Name = "DeleteTasks")]
        public async Task<ActionResult> Delete(int id)
        {
            var taskToDelete = await _toDoTaskRepository.Get(id);
            if (taskToDelete == null)
                return NotFound();

            await _toDoTaskRepository.Delete(taskToDelete.Id);
            return NoContent();
        }
    }
}
