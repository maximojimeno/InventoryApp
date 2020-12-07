using InventaryApp.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventaryApp.Server.Entities
{
    public class Account : Record
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        public virtual Bussiness Bussiness { get; set; }
        [ForeignKey("Bussiness")]
        public string BussinnessId { get; set; }
    }
}
