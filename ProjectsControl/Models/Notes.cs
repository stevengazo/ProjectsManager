using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectsControl.Models
{
    public class Notes
    {
        [Key]
        [Required(ErrorMessage ="Campo Requerido")]
        public string NotesId { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        public DateTime DateOfCreation { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        [MaxLength(120)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        [MaxLength(340)]
        public string NoteDescription { get; set; }

        #region FOREIGNKEY
        public Project Project { get; set; }
        public string ProjectId { get; set; }

        #endregion

    }
}
