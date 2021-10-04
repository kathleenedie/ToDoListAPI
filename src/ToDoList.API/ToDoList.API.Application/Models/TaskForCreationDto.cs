using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.API.Application.Models
{
    public class TaskForCreationDto
    {
        [MaxLength(20)]
        public string Category { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [DefaultValue(false)]
        public bool Completed { get; set; }
    }
}
