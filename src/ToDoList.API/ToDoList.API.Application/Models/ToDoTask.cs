using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.API.Application.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}
