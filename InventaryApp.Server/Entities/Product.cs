using InventaryApp.Server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventaryApp.Server.Entities
{
    public partial class Product : Record
    {
        public Product()
        {
            Code = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Cost = 0;
            Price = 0;
        }
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        public double Cost { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [ForeignKey("Brand")]
        public string BrandId { get; set; }
        [Required]
        [ForeignKey("Category")]
        public string CategoryId { get; set; }

    }
}
