using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectsControl.Models
{
    public class ExtraHour
    {
        [Key]
        [Required]
        public string ExtraHourId { get; set; }
        [Required]
        public DateTime BeginTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public string TypeOfHour { get; set; }
        [Required]
        public string Reason { get; set; }
        public string Notes { get; set; }
        [Required]
        public bool IsPaid { get; set; }

        //Relation with Employee
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        //Relation with Extra Hour
        public string AsistanceId { get; set; }
        public Asistance Asistance { get; set; }
    }
}
