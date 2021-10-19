using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.Application.Models
{
    public class TaskForUpdateDto
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