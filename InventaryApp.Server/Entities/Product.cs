using InventaryApp.Server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventaryApp.Server.Entities
{
    public partial class Product : Record
    {
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        [Required]
        public virtual Brand Brand { get; set; }
        [Required]
        public virtual Category Category { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("Brand")]
        public string BrandId { get; set; }
        [ForeignKey("Category")]
        public string CategoryId { get; set; }

    }
}
