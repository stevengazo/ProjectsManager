using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectsControl.Models
{
    public class Project
    {
        [Key]
        [Required(ErrorMessage ="El Id de Proyecto es requerido")]
        public string ProjectId { get; set; }
        
        public int NumberOfTask { get; set; }
        [Required(ErrorMessage ="El Nombre de Proyecto es requerido")]
        [MinLength(8,ErrorMessage ="El nombre no puede ser menor a 8 caracteres")]
        [MaxLength(50,ErrorMessage ="El nombre no puede ser mayor a 100 caracteres")]
        public string ProjectName { get; set; }
        public string OfferId { get; set; }
        public string OC { get; set; }
        public DateTime OCDate { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Manager { get; set; }
        public string Estatus { get; set; }        
        public bool IsOver { get; set; }
        [Required(ErrorMessage ="Monto de Proyecto Requerido")]
        public float Amount { get; set; }
        public string Currency { get; set; }
        public double PendingAmount { get; set; }
        public string TypeOfJob { get; set; }
        [Required(ErrorMessage ="Detalles del proyecto requeridos")]
        public string Details { get; set; }
        public string Ubication { get; set; }
        /*---------------*/
        public ICollection<Asistance> Asistances { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<Bill> Bills { get; set; }

        // relation with customers
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        // relation with saleman
        public string SalemanId { get; set; }
        public Saleman Saleman { get; set; }
    }
}
