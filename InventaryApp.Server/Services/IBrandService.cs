using InventaryApp.Server.DataAccess;
using InventaryApp.Server.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace InventaryApp.Server.Services
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllBrandAsync(string userId);
    }

   

    public class BrandServices : IBrandService
    {
        private readonly ApplicationDbContext _dbContext;

        public BrandServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Brand>> GetAllBrandAsync(string userId)
        {
            var allBrands = await _dbContext.brands
                .Where(p => !p.Status && p.UserId == userId)
                .ToListAsync();

            return allBrands;
        }
    }
}
