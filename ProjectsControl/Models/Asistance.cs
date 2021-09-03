using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ProjectsControl.Models
{
    public class Asistance
    {
        public string AsistanceId { get; set; }
        public DateTime DateOfBegin { get; set; }
        public DateTime DateOfEnd { get; set; }


        // Relation with Employee
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        // Relation with Projects
        public string ProjectId { get; set; }
        public Project Project { get; set; }

        public string WeekId { get; set; }
        public Week Week { get; set; }
        public ICollection<ExtraHour> ExtraHours { get; set; } 
    }
}

