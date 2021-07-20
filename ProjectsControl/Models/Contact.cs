using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ProjectsControl.Models
{
    public class Contact
    {
        [Key]
        [Required]
        public string ContactId { get; set; }
        [Required]
        [MinLength(10)]
        public string Name { get; set; }
        [Required]
        [MinLength(8)]
        public int PhoneNumber { get; set; }
        [Required]
        [MinLength(10)]
        public string Email { get; set; }
        public string Position { get; set; }

        // Relation with Customer
        public string CustomerId { get; set; }
        public Customer Customer {get; set;}
    }
}
