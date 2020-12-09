using InventaryApp.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventaryApp.Server.Entities
{
    public class OpenInventary : Record
    {
        public OpenInventary()
        {
            OpenDate = DateTime.Now;
            CloseDate = DateTime.Now;
            StatusInventary = true;
            OldAmountInventary = 0;
            ActualAmountInventary = 0;
        }

        [Required]
        public DateTime? OpenDate { get; set; }
        [Required]
        public DateTime? CloseDate { get; set; }
        public virtual Bussiness Bussiness { get; set; }
        [Required]
        [ForeignKey("Bussiness")]
        public string BussinessId { get; set; }
        public bool StatusInventary { get; set; }
        [Required]
        public double OldAmountInventary { get; set; }
        [Required]
        public double ActualAmountInventary { get; set; }

    }
}
