using InventaryApp.Server.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventaryApp.Server.Entities
{
    public class Brand : Record
    {
        public Brand()
        {
            Products = new List<Product>();
        }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
