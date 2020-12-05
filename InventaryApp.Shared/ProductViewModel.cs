﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventaryApp.Shared
{
   public class ProductViewModel
    {
        public string Id { get; set; }

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
       
        public string BrandId { get; set; }
        [Required]
        [StringLength(20)]
        public string CategoryId { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
