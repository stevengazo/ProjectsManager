using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectsControl.Models
{
    public class Week
    {
        [Key]
        [Required]
        public string WeekId { get; set; }
        
        public string NumberOfWeek { get; set; }
        public DateTime BeginOfWeek { get; set; }
        public DateTime EndOfWeek { get; set; }

        public ICollection<Asistance> Asistances { get; set; }
        public ICollection<ExtraHour> ExtraHours { get; set; }


    }
}
