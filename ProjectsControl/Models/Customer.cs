using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsControl.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public string CustomerId { get; set; }
        [MinLength(10)]
        [MaxLength(120)]
        [Required]
        public string Name { get; set; }
        public string Sector { get; set; }
        
        public ICollection<Project> Projects { get; set; }
    }
}
