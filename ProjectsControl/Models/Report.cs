using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ProjectsControl.Models { 

    public class Report
    {
        [Key]
        [Required]
        public string ReportId { get; set; }
        [Required]
        public string NumberOfReport { get; set; }
        [MinLength(6)]
        public string Author { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public string Status { get; set; }
        public string Notes { get; set; }

        /// relation with project
        /// 

        public string ProjectId { get; set; }
        public Project Project { get; set; }

    }
}
