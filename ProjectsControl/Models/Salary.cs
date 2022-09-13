using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ProjectsControl.Models
{
    public class Salary
    {
        [Key]
        [Required(ErrorMessage = "Id requerido")]
        public string SalaryId { get; set; }
        [Required]
        public double SalaryAmount { get; set; }
        [Required]
        public DateTime DayOfApplication { get;set; }        
        public string notes { get; set; }
        public bool isActive { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }



    }
}
