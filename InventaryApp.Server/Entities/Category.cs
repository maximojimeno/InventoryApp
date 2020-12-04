using InventaryApp.Server.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventaryApp.Server.Entities
{
    public class Category : Record
    {
        public Category()
        {
            Products = new List<Product>();
        }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
