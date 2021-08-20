using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectsControl.Models
{
    public class Offer
    {
        /// <summary>
        /// Primary key for internal use of the app
        /// </summary>
        [Key]        
        [Required(ErrorMessage= "El Id de la oferta no puede ser nulo")]
        public string OfferId { get; set; }
        /// <summary>
        /// Number of offer for the employees
        /// </summary>
        [Required(ErrorMessage ="Numero de oferta requerido")]
        public int NumberOfOffer { get; set; }
        /// <summary>
        /// Title of the offer
        /// </summary>
        [Required(ErrorMessage ="Titulo de la oferta requerido")]
        [MinLength(10,ErrorMessage ="Longitud menor a 10 caracteres")]
        public string Title { get; set; }
        /// <summary>
        /// Type of offer
        /// </summary>
        [Required(ErrorMessage ="Tipo de Oferta requerido")]
        public string Type { get; set; }
        /// <summary>
        /// Description of the offer. Indicates all the important information 
        /// </summary>
        [Required(ErrorMessage ="Informacion requerida")]
        public string Description { get; set; }
        /// <summary>
        /// Author of the offert 
        /// </summary>
        [Required(ErrorMessage ="Indique el autor de la oferta")]
        public string Author { get; set; }
        [Required(ErrorMessage ="Indique el vendedor encargado de esta oferta")]
        public string SaleManName { get; set; }
        /// <summary>
        /// Date of the creation of the offer
        /// </summary>
        [Required(ErrorMessage ="Fecha de creacion requerida")]
        public DateTime DateOfCreation { get; set; }
        /// <summary>
        /// Date of last Modification
        /// </summary>
        [Required(ErrorMessage ="Fecha de ultima modificacion requerida")]
        public DateTime LastEdition { get; set; }

        #region ForeingKey
        /// RELATION WITH CUSTOMER
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
        #endregion
    }
}
