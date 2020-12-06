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
    }
}
