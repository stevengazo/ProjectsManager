using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectsControl.Models
{
    public class Action
    {
        /// <summary>
        /// Primary for internal use 
        /// </summary>
        [Key]
        [Required]        
        public string ActionId { get; set; }
        /// <summary>
        /// Title of the Action
        /// </summary>
        [Required]
        [MinLength(10)]
        public string Title { get; set; }
        /// <summary>
        /// Date of Creation
        /// </summary>
        [Required]
        public DateTime DateOfCreation { get; set; }
        [Required]       
        public string Author { get; set; }
        [Required]
        public string TypeOfAction { get; set; }
        [Required]
        [MaxLength(340)]
        public string Description { get; set; }
        public bool IsActive { get; set; }

        #region ForeignKey
        public Employee Employee { get; set; }

        public string EmployeeId { get; set; }

        #endregion

    }
}
