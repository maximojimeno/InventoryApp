using InventaryApp.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventaryApp.Server.Entities
{
    public class Bussiness : Record
    {
        public Bussiness()
        {
            Accounts = new List<Account>();
        }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Owner { get; set; }
        public string OwnerPhone { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
