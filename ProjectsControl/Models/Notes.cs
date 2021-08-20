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
        [Required]
        public string NotesId { get; set; }
        [Required]
        public string Author { get; set; }
        public string DateOfCreation { get; set; }
        [Required]
        [MaxLength(120)]
        public string Title { get; set; }
        [Required]
        [MaxLength(340)]
        public string NoteDescription { get; set; }

        
    }
}
