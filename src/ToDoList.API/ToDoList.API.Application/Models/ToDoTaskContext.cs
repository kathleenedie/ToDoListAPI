using Microsoft.EntityFrameworkCore;

namespace ToDoList.API.Application.Models
{
    public class ToDoTaskContext : DbContext
    {
        public ToDoTaskContext(DbContextOptions<ToDoTaskContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoTask>().HasData(
                new ToDoTask {Category = "home", Completed = false, Description = "hang up washing", Id = 1},
                new ToDoTask {Category = "home", Completed = true, Description = "cook dinner for Mum and Dad", Id = 2},
                new ToDoTask {Category = "garden", Completed = false, Description = "pack away pizza oven", Id = 3},
                new ToDoTask {Category = "volunteering", Completed = false, Description = "finish handbook updates", Id = 4}
            );
        }

        public DbSet<ToDoTask> ToDoTasks { get; set; }
    }
}