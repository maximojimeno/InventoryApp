using InventaryApp.Server.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventaryApp.Server.Entities
{
    public class Inventary : Record
    {
        public Inventary()
        {
            InventaryDetails = new List<InventaryDetails>();
        }
       
        public virtual OpenInventary OpenInventary { get; set; }
        [Required]
        [ForeignKey("OpenInventary")]
        public string OpenInventaryId { get; set; }
        [Required]
        public double Total { get; set; }

        public virtual ICollection<InventaryDetails> InventaryDetails { get; set; }

        private double CalculateTotal()
        {
            Total = 0;
            foreach (var item in InventaryDetails)
            {
                Total += item.Total;
            }

            return Total;
        }

    }
}
