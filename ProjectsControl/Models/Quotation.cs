using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectsControl.Models
{
    public class Quotation
    {
        /// <summary>
        /// Internal id of the quotation
        /// </summary>
        [Key]
        [Required(ErrorMessage = "Dato requerido")]
        public string QuotationId { get; set; }
        /// <summary>
        /// Amount used in solid materials by mecsa
        /// </summary>
        public float MecsaMaterialsAmount { get; set; }
        /// <summary>
        /// Amount used in solid materials by others companies
        /// </summary>
        public float GeneralMaterialsAmount { get; set; }
        /// <summary>
        /// Mount expected to used in workforce to the project
        /// </summary>
        public float workforceAmount { get; set; }
        /// <summary>
        /// Viactics calculated
        /// </summary>
        public float viaticsAmount { get; set; }      
        public float unExpectedAmount { get; set; }
        public float kilometersAmount { get; set; }
        public float lastPersonModification { get; set; }
        public DateTime lastModificationDate { get; set; }
        public string offerId { get; set; }

     }

}
