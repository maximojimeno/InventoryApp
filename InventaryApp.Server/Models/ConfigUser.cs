using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventaryApp.Server.Models
{
    public class ConfigUser : IdentityUser
    {

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
    }
}
