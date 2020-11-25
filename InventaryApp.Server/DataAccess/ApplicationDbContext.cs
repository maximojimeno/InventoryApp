using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventaryApp.Server.Models;

namespace InventaryApp.Server.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ConfigUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customers> Customers { get; set; }
    }
}
