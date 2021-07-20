﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ProjectsControl.Models
{
    public class Bill
    {
        [Key]
        [Required]
        public string BillId { get; set; }
        [Required]
        public string NumberOfBill { get; set; }
        [Required]
        public DateTime DateOfCreation { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public float cost { get; set; }


        public string ProjectId { get; set; }
        public Project Project { get; set; }
    }
}