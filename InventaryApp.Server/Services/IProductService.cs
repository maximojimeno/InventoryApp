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
        Task<Product> AddProductAsync(string code, string name, string description, string brand, string category, decimal cost, decimal price, string userId);
    }


    public class ProductService : IProductService
    {

        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Product> AddProductAsync(string code, string name, string description, string brand, string category, decimal cost, decimal price, string userId)
        {
            var product = new Product
            {
                Code = code,
                Name = name,
                Description = description,
                Brand = brand,
                Category = category,
                Cost = cost,
                Price = price,
                UserId = userId
            };

            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}
