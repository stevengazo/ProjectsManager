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
        /// Id of the Quotation PK for internal Use
        /// </summary>
        public string QuotationId { get; set; }
        /// <summary>
        /// Type of Quotation
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Description of the Quotation
        /// </summary>
        public string Description { get; set; }

        public float MaterialAmount { get; set; }
        public float MecsaAmountMaterial { get; set; }
        public float ViaticsAmount{ get; set; }
        public float UnexpectedAmount { get; set; }
        public float KMAmount { get; set; }
        public float TotalAmount { get; set; }
        /// <summary>
        /// Total of Days in the Quotation
        /// </summary>
        public int QuantityOfDays { get; set; }
        /// <summary>
        /// Totak of Emplooyes in the  Quotation
        /// </summary>
        public int QuantityOfEmployees { get; set; }
        /// <summary>
        /// Author of the Quotation
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Last modification by an user
        /// </summary>
        public string UserModification { get; set; }
        /// <summary>
        /// Date of creation
        /// </summary>
        public DateTime DateCreation { get; set; }
        /// <summary>
        /// Date of last modification
        /// </summary>
        public DateTime LastModification { get; set; }

        #region Foreign Keys
        public ICollection<Of_Quo> Of_Quos { get; set; }
        
        #endregion

    }
}
