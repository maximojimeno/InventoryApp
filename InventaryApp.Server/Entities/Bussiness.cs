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
            Code = string.Empty;
            Name = string.Empty;
            Address = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            Owner = string.Empty;
            OwnerPhone = string.Empty;
            Accounts = new List<Account>();
            OpenInventaries = new List<OpenInventary>();
        }
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Owner { get; set; }
        [StringLength(15)]
        public string OwnerPhone { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<OpenInventary> OpenInventaries { get; set; }
    }
}
