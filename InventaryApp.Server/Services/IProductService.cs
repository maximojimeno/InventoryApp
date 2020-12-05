using InventaryApp.Server.DataAccess;
using InventaryApp.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventaryApp.Server.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProductAsync(int pageSize, int pageNumber, string userId, out int totalProduct);
        Task<Product> AddProductAsync(string code, string name, string description, string brandId, string categoryId, decimal cost, decimal price, string userId);
    }   


    public class ProductService : IProductService
    {

        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Product> AddProductAsync(string code, string name, string description, string brandId, string categoryId, decimal cost, decimal price, string userId)
        {
            var product = new Product
            {
                Code = code,
                Name = name,
                Description = description,
                BrandId = brandId,
                CategoryId = categoryId,
                Cost = cost,
                Price = price,
                UserId = userId
            };

            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public IEnumerable<Product> GetAllProductAsync(int pageSize, int pageNumber, string userId, out int totalProduct)
        {
            var allProducts = _dbContext.products.Where(p => !p.Status && p.UserId == userId);
                totalProduct = allProducts.Count();
            var product = allProducts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArray();
            return product;
        }

    }
}
