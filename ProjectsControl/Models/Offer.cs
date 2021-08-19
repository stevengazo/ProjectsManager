using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectsControl.Models
{
    public class Offer
    {
        [Key]        
        [Required(ErrorMessage= "El Id de la oferta no puede ser nulo")]
        public string OfferId { get; set; }

        public int NumberOfOffer { get; set; }
        [Required(ErrorMessage ="El Titulo de la oferta es requerido")]
        public string Title { get; set; }
        [Required(ErrorMessage ="El tipo de oferta es requerido")]
        [MinLength(8,ErrorMessage ="La descripcion es muy corta")]
        [MaxLength(100,ErrorMessage ="La descripcion es muy larga")]
        public string Type { get; set; }
        [Required(ErrorMessage ="Monto del proyecto requerido Requerida")]
        public double Amount { get; set; }        
        [Required(ErrorMessage = "Descripcion Requerida")]
        public string Description { get; set; }
        [Required(ErrorMessage ="Cantidadd de Dias requerida")]
        public int QuantityOfDays { get; set; }
        [Required(ErrorMessage ="Cantidad de Personas Requerida")]
        public int QuantityOfEmployees { get; set; }
        [Required(ErrorMessage ="Introduzca el nombre del autor")]
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
        
    }
}
