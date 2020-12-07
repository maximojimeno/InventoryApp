using InventaryApp.Server.DataAccess;
using InventaryApp.Server.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace InventaryApp.Server.Services
{
   public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategotyAsync(string userId);
        Task<Category> GetCategoryById(string id, string userId);
        Task<Category> AddCategoryAsync(string name, string userId);
        Task<Category> DeleteProductAsync(string id, string userId);
        Task<Category> EditCategoryAsync(string id, string newName, string userId);
        IEnumerable<Category> SearchProductAsync(string query, int pageSize, int pageNumber, string userId, out int totalCategory);
        IEnumerable<Category> GetAllCategoryCollectionAsync(int pageSize, int pageNumber, string userId, out int totalCategory);

    }

    public class CategotyService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public CategotyService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAllCategotyAsync(string userId)
        {
            var allCategory = await _dbContext.Categories
                .Where(p => !p.Status && p.UserId == userId)
                .ToListAsync();
                                        
            
            return allCategory;
        }
        public async Task<Category> AddCategoryAsync(string name, string userId)
        {
            var category = new Category
            {
                Name = name,
                UserId = userId
            };

            await _dbContext.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }
        public async Task<Category> EditCategoryAsync(string id,string newName,string userId)
        {
            var category = await _dbContext.Categories.FindAsync(id);

            if (category.UserId != userId || category.Status)
                return null;

            category.Name = newName;
            category.ModifiedDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();
            return category;
        }
        public async Task<Category> GetCategoryById(string id, string userId)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category.UserId != userId || category.Status)
                return null;
            return category;
        }
        public async Task<Category> DeleteProductAsync(string id, string userId)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category.UserId != userId || category.Status)
                return null;

            category.Status = true;
            category.ModifiedDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return category;
        }
        public IEnumerable<Category> SearchProductAsync(string query, int pageSize, int pageNumber, string userId, out int totalCategory)
        {
            // total plans 
            var allCategories = _dbContext.Categories.Where(c => !c.Status && c.UserId == userId && (c.Name.Contains(query)));

            totalCategory = allCategories.Count();

            var categories = allCategories.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArray();

            return categories;
        }
        public IEnumerable<Category> GetAllCategoryCollectionAsync(int pageSize, int pageNumber, string userId, out int totalCategory)
        {
            var allCategories = _dbContext.Categories.Where(p => !p.Status && p.UserId == userId);
            totalCategory = allCategories.Count();
            var category = allCategories.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArray();
            return category;
        }
    }
}
