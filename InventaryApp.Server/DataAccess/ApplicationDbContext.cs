using InventaryApp.Server.Entities;
using InventaryApp.Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventaryApp.Server.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ConfigUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Bussiness> Bussiness { get; set; }
        public DbSet <Account> Accounts { get; set; }
        public DbSet <OpenInventary> OpenInventaries { get; set; }
    }


}
