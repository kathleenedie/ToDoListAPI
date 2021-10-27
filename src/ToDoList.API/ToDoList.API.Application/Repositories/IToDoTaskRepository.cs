using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.API.Application.Models;

namespace ToDoList.API.Application.Repositories
{
    public interface IToDoTaskRepository
    {
        Task<IEnumerable<ToDoTask>> Get();
        Task<ToDoTask> Get(int id);
        Task<ToDoTask> Create(ToDoTask toDoTask);
        Task Update(ToDoTask toDoTask);
        Task Delete(int id);
        Task<IEnumerable<ToDoTask>> GetCompleted();
        Task<IEnumerable<ToDoTask>> GetToDo();
        Task<IEnumerable<ToDoTask>> CategoryFilter(string category);
        Task<List<ToDoTask>> CategoryStatus(string category, bool completed);
    }
}