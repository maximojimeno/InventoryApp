using InventaryApp.Server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventaryApp.Server.Entities
{
    public class Account : Record
    {
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string Type { get; set; }
        public virtual Bussiness Bussiness { get; set; }
        [ForeignKey("Bussiness")]
        public string BussinessId { get; set; }
    }
}
