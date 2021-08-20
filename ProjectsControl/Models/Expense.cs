using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectsControl.Models
{
    public class Expensive
    {
        [Key]
        [Required(ErrorMessage ="Dato requerido")]
        public string ExpensiveId { get; set; }
        [Required(ErrorMessage = "Dato requerido")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Dato requerido")]
        public DateTime LastModification { get; set; }
        [Required(ErrorMessage = "Dato requerido")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Dato requerido")]
        public float  Amount { get; set; }
        [Required(ErrorMessage = "Dato requerido")]
        public string Currency { get; set; }

        public string Note { get; set; }
        #region Foreign Key
        /// RELATION WITH PROJECT
        public string ProjectId { get; set; }
        public Project Project { get; set; }
        #endregion
    }
}
