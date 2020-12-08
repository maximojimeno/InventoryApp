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
        Task<Product> DeleteProductAsync(string id, string userId);
        Task<Product> GetProductById(string id, string userId);
        IEnumerable<Product> GetAllProductAsync(int pageSize, int pageNumber, string userId, out int totalProduct);
        IEnumerable<Product> SearchProductAsync(string query, int pageSize, int pageNumber, string userId, out int totalProducts);
        Task<Product> AddProductAsync(string code, string name, string description, string brandId, string categoryId, double cost, double price, string userId);
        Task<Product> EditProductAsync(string id, string newCode, string newName, string newDescription, string newBrandId, string newCategoryId, double newCost, double newPrice, string userId);

    }   


    public class ProductService : IProductService
    {

        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Product> AddProductAsync(string code, string name, string description, string brandId, string categoryId, double cost, double price, string userId)
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
            var allProducts = _dbContext.Products.Where(p => !p.Status && p.UserId == userId);
                totalProduct = allProducts.Count();
            var product = allProducts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArray();

            foreach (var item in product)
            {
                item.BrandId = _dbContext.Brands
                    .Where(i => !i.Status && i.Id == item.BrandId)
                    .Select(s => s.Name)
                    .FirstOrDefault();
                item.CategoryId = _dbContext.Categories
                    .Where(i => !i.Status && i.Id == item.CategoryId)
                    .Select(s => s.Name)
                    .FirstOrDefault();
            }
            return product;
        }
        public async Task<Product> EditProductAsync(string id,string newCode, string newName, string newDescription, string newBrandId, string newCategoryId, double newCost, double newPrice, string userId)
        {
            var product = await _dbContext.Products.FindAsync(id);
            
            if (product.UserId != userId || product.Status)
                return null;

            product.Code = newCode;
            product.Name = newName;
            product.Description = newDescription;
            product.BrandId = newBrandId;
            product.CategoryId = newCategoryId;
            product.Cost = newCost;
            product.Price = newPrice;
            product.ModifiedDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();
            return product;
        }
        public async Task<Product> GetProductById(string id, string userId)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product.UserId != userId || product.Status)
                return null;
            return product;
        }
        public async Task<Product> DeleteProductAsync(string id, string userId)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product.UserId != userId || product.Status)
                return null;

            product.Status = true;
            product.ModifiedDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return product;
        }
        public IEnumerable<Product> SearchProductAsync(string query, int pageSize, int pageNumber, string userId, out int totalProducts)
        {
            // total plans 
            var allProducts = _dbContext.Products.Where(p => !p.Status && p.UserId == userId && (p.Code.Contains(query) || p.Name.Contains(query)|| p.Category.Name.Contains(query) || p.Brand.Name.Contains(query)));

            totalProducts = allProducts.Count();

            var products = allProducts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArray();
            foreach (var item in products)
            {
                item.BrandId = _dbContext.Brands
                    .Where(i => !i.Status && i.Id == item.BrandId)
                    .Select(s => s.Name)
                    .FirstOrDefault();
                item.CategoryId = _dbContext.Categories
                    .Where(i => !i.Status && i.Id == item.CategoryId)
                    .Select(s => s.Name)
                    .FirstOrDefault();
            }
            return products;
        }
    }
}
