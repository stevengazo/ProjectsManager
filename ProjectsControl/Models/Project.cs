using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectsControl.Models
{
    public class Project
    {
        /// <summary>
        /// Primary key for internal use
        /// </summary>
        [Key]
        [Required(ErrorMessage ="El Id de Proyecto es requerido")]        
        public string ProjectId { get; set; }
        /// <summary>
        /// Number of Project for employees use
        /// </summary>
        public int NumberOfProject { get; set; }
        /// <summary>
        /// Number Of Task in Bitrix
        /// </summary>
        public int NumberOfTask { get; set; }
        /// <summary>
        /// Name of the Project
        /// </summary>
        [Required(ErrorMessage ="El Nombre de Proyecto es requerido")]
        [MinLength(8,ErrorMessage ="El nombre no puede ser menor a 8 caracteres")]
        [MaxLength(50,ErrorMessage ="El nombre no puede ser mayor a 100 caracteres")]
        public string ProjectName { get; set; }
        /// <summary>
        /// Id of Order of Buy for the client
        /// </summary>
        public string OC { get; set; }
        /// <summary>
        /// Date of the Order of Buy
        /// </summary>
        public DateTime OCDate { get; set; }
        /// <summary>
        /// Date of Begin of the project
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// Date of end of the Project
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Enginneer manager of the project
        /// </summary>
        public string Manager { get; set; }
        /// <summary>
        /// Technician of the project
        /// </summary>
        public string Technician { get; set; }
        /// <summary>
        /// Estatus of the Project
        /// </summary>
        public string Estatus { get; set; }        
        /// <summary>
        /// True if the project was ending 
        /// </summary>
        public bool IsOver { get; set; }
        /// <summary>
        /// Total amount of the project
        /// </summary>
        [Required(ErrorMessage ="Monto de Proyecto Requerido")]
        public float Amount { get; set; }
        /// <summary>
        /// Type of currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Pending Amont of the project
        /// </summary>
        public double PendingAmount { get; set; }
        /// <summary>
        /// Type of job of the project
        /// </summary>
        public string TypeOfJob { get; set; }
        /// <summary>
        /// Description of the project
        /// </summary>
        [Required(ErrorMessage ="Detalles del proyecto requeridos")]       
        public string Details { get; set; }
        /// <summary>
        /// Ubication to execute the project
        /// </summary>
        [Required(ErrorMessage ="Ubicacion de la ejecución del proyecto requerida")]
        public string Ubication { get; set; }

        public string NumberOfOffer { get; set; }

        #region Foreign Keys
        /*---------------*/
        public ICollection<Asistance> Asistances { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<Bill> Bills { get; set; }

        public ICollection<Notes> Notes { get; set; }
        
        public ICollection<Expensive> Expensives { get; set; }

        // relation with Employees (Saleman)
        
        /// <summary>
        /// Saleman Id of the project
        /// </summary>
        public string EmployeeId { get; set; }
        /// <summary>
        /// Saleman of the Project
        /// </summary>
        public Employee Employee { get; set; }        

        // relation with CUSTOMERS
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }



        #endregion
    }
}
