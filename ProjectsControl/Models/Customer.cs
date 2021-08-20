using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsControl.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public string CustomerId { get; set; }
        
        /// <summary>
        /// Number of DNI of the client
        /// </summary>
        [Required(ErrorMessage ="Numero de Cedula Requerido")]
        public int DNIOfCustomer { get; set; }
        /// <summary>
        /// Name of the client 
        /// </summary>
        [MinLength(10)]
        [MaxLength(120)]
        [Required(ErrorMessage ="Name of the customer")]
        public string Name { get; set; }
        /// <summary>
        /// Sector of the customer. Public or Private
        /// </summary>
        [Required(ErrorMessage ="Tipo de sector requerido")]
        public string Sector { get; set; }

        #region ForeignKey
        public ICollection<Project> Projects { get; set; }
        public ICollection<Offer> Offers { get; set; }
        #endregion
    }
}
