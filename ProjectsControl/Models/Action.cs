using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectsControl.Models
{
    public class Action
    {
        [Key]
        [Required]
        public string ActionId { get; set; }
        [Required]
        [MinLength(10)]
        public string Title { get; set; }
        [Required]
        public DateTime DateOfCreation { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string TypeOfAction { get; set; }
        [Required]
        [MaxLength(340)]
        public string Description { get; set; }
        public string IsActive { get; set; }

      
    }
}
