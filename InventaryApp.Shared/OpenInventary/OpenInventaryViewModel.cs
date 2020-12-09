using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventaryApp.Shared.OpenInventary
{
    public class OpenInventaryViewModel 
    {
        public OpenInventaryViewModel()
        {
            OpenDate = DateTime.Now;
            
        }
        public string Id { get; set; }
        [Required]
        public DateTime OpenDate { get; set; }
        [Required]
        public DateTime CloseDate { get; set; }
        [Required]
        public string BussinessId { get; set; }
        public bool StatusInventary { get; set; }
        [Required]
        public double OldAmountInventary { get; set; }
        [Required]
        public double ActualAmountInventary { get; set; }
    }
}
