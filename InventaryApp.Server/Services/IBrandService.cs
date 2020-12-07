using InventaryApp.Server.DataAccess;
using InventaryApp.Server.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace InventaryApp.Server.Services
{
    public interface IBrandService
    {
        Task<Brand> AddBrandAsync(string name, string userId);
        Task<Brand> EditBrandAsync(string id, string newName, string userId);
        Task<Brand> GetBrandById(string id, string userId);
        Task<Brand> DeleteBrandAsync(string id, string userId);
        Task<IEnumerable<Brand>> GetAllBrandAsync(string userId);
        IEnumerable<Brand> SearchBrandAsync(string query, int pageSize, int pageNumber, string userId, out int totalBrand);
        IEnumerable<Brand> GetAllBrandCollectionAsync(int pageSize, int pageNumber, string userId, out int totalBrand);
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
        public async Task<Brand> AddBrandAsync(string name, string userId)
        {
            var brand = new Brand
            {
                Name = name,
                UserId = userId
            };

            await _dbContext.AddAsync(brand);
            await _dbContext.SaveChangesAsync();
            return brand;
        }
        public async Task<Brand> EditBrandAsync(string id, string newName, string userId)
        {
            var brand = await _dbContext.brands.FindAsync(id);

            if (brand.UserId != userId || brand.Status)
                return null;

            brand.Name = newName;
            brand.ModifiedDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();
            return brand;
        }
        public async Task<Brand> GetBrandById(string id, string userId)
        {
            var brand = await _dbContext.brands.FindAsync(id);
            if (brand.UserId != userId || brand.Status)
                return null;
            return brand;
        }
        public async Task<Brand> DeleteBrandAsync(string id, string userId)
        {
            var brand = await _dbContext.brands.FindAsync(id);
            if (brand.UserId != userId || brand.Status)
                return null;

            brand.Status = true;
            brand.ModifiedDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return brand;
        }
        public IEnumerable<Brand> SearchBrandAsync(string query, int pageSize, int pageNumber, string userId, out int totalBrand)
        {

            var allBrands = _dbContext.brands.Where(c => !c.Status && c.UserId == userId && (c.Name.Contains(query)));

            totalBrand = allBrands.Count();

            var brands = allBrands.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArray();

            return brands;
        }
        public IEnumerable<Brand> GetAllBrandCollectionAsync(int pageSize, int pageNumber, string userId, out int totalBrand)
        {
            var allBrands = _dbContext.brands.Where(p => !p.Status && p.UserId == userId);
            totalBrand = allBrands.Count();
            var brands = allBrands.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArray();
            return brands;
        }
    }
}
