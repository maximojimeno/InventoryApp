using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventaryApp.Shared
{
   public class RegisterViewModel
    {
        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 6)]
        public string ConfirmPassword { get; set; }
    }
}
