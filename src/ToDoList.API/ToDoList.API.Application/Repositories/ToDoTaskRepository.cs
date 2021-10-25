using System.Collections.Generic;
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
    }
}