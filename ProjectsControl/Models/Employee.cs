using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectsControl.Models
{
    public class Employee
    {
        [Key]
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(60)]
        public string Name { get; set; }
        public DateTime DateofHiring { get; set; }
        public string Position { get; set; }

        public ICollection<ExtraHour> ExtraHours { get; set; }
        public ICollection<Asistance> Asistances { get; set; } }
}

