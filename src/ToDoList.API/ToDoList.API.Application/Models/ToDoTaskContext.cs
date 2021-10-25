using Microsoft.EntityFrameworkCore;

namespace ToDoList.API.Application.Models
{
    public class ToDoTaskContext : DbContext
    {
        public ToDoTaskContext(DbContextOptions<ToDoTaskContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ToDoTask> ToDoTasks { get; set; }
    }
}