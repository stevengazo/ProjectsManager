using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectsControl.Models
{
    public class Of_Quo
    {
        [Key][Required(ErrorMessage ="Valor Requerido")]
        public string Of_QuoId { get; set; }

        public bool IsModicable { get; set; }

        //RELATION WITH OFFER
        public string OfferId { get; set; }
        public Offer Offer { get; set; }
        //RELATION WITH QUOTATION
        public string QuotationId { get; set; }

        public Quotation Quotation { get; set; }
    }
}
