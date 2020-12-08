using InventaryApp.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventaryApp.Server.Entities
{
    public class InventaryDetails : Record
    {
        public InventaryDetails()
        {
            Count = 0;
            Cost = 0;
            Total = 0;

        }
        public Inventary Inventary { get; set; }
        [ForeignKey("Inventary")]
        [Required]
        public string InventaryId { get; set; }
        public Product product { get; set; }
        [ForeignKey("Product")]
        [Required]
        public string ProductId { get; set; }
        [Required]
        public double Count { get; set; }
        [Required]
        public double Cost { get; set; }
        [Required]
        public double Total { get; set; }

    }
}
