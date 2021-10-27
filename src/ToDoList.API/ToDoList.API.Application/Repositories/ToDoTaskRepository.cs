using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Application.Models;

namespace ToDoList.API.Application.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly ToDoTaskContext _toDoTaskContext;

        public ToDoTaskRepository(ToDoTaskContext context)
        {
            _toDoTaskContext = context;
        }

        public async Task <ToDoTask> Create(ToDoTask toDoTask)
        {
            _toDoTaskContext.ToDoTasks.Add(toDoTask);
            await _toDoTaskContext.SaveChangesAsync();

            return toDoTask;
        }

        public async Task Delete(int id)
        {
            var toDoTaskToDelete = await _toDoTaskContext.ToDoTasks.FindAsync(id);
            _toDoTaskContext.ToDoTasks.Remove(toDoTaskToDelete);
            await _toDoTaskContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDoTask>> Get()
        {
            return await _toDoTaskContext.ToDoTasks.ToListAsync();
        }

        public async Task<ToDoTask> Get(int id)
        {
            return await _toDoTaskContext.ToDoTasks.FindAsync(id);
        }

        public async Task Update(ToDoTask toDoTask)
        {
            _toDoTaskContext.Entry(toDoTask).State = EntityState.Modified;
            await _toDoTaskContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDoTask>> GetCompleted()
        {
            var toDoTasksCompleted = await _toDoTaskContext.ToDoTasks.Where(t => t.Completed == true).ToListAsync();
            return toDoTasksCompleted;
        }

        public async Task<IEnumerable<ToDoTask>> GetToDo()
        {
            var toDoTasksToDo = await _toDoTaskContext.ToDoTasks.Where(t => t.Completed == false).ToListAsync();
            return toDoTasksToDo;
        }

        public async Task<IEnumerable<ToDoTask>> CategoryFilter(string category)
        {
            var categoryTasks = await _toDoTaskContext.ToDoTasks.Where(t => t.Category == category).ToListAsync();
            return categoryTasks;
        }

        public Task<List<ToDoTask>> CategoryStatus(string category, bool completed)
        {
            return _toDoTaskContext.ToDoTasks
                .Where(t => t.Category == category && t.Completed == completed).ToListAsync();
        }
    }
}