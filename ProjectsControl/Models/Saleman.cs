using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsControl.Models
{
    public class Saleman 
    {
        [Key]
        [Required]
        public string SalemanId {get;set;}
        [MinLength(10)]
        [MaxLength(120)]
        public string Name { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
