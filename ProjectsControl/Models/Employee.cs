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
        public int EmployeeDNI { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(60)]
        public string Name { get; set; }
        [Required(ErrorMessage ="Dato Requerido")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Dato Requerido")]
        public DateTime DateofHiring { get; set; }
        [Required(ErrorMessage = "Dato Requerido")]
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfFired { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Dato Requerido")]
        public int MobileNumber { get; set; }
        [Required(ErrorMessage = "Dato Requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Dato Requerido")]
        public float Salary { get; set; }


        public ICollection<ExtraHour> ExtraHours { get; set; }
        public ICollection<Asistance> Asistances { get; set; } 
        public ICollection<Action> Actions { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
   
}

