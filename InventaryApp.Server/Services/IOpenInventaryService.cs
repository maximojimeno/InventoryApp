using InventaryApp.Server.DataAccess;
using InventaryApp.Server.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventaryApp.Server.Services
{
    public interface IOpenInventaryService
    {
        Task<IEnumerable<OpenInventary>> GetAllOpenInventaryAsync(string userId);
        Task<OpenInventary> AddOpenInventaryAsync(DateTime openDate, DateTime closeDate, string bussinessId, bool statusInventary, double oldAmountInventary, double actualAmountInventary, string userId);
        Task<OpenInventary> EditOpenInventaryAsync(string id, DateTime newOpenDate, DateTime newCloseDate, string newBussinessId, bool newStatusInventary, double newOldAmountInventary, double newActualAmountInventary, string userId);
        IEnumerable<OpenInventary> SearchOpenInventaryAsync(string query, int pageSize, int pageNumber, string userId, out int totalOpenInventary);
        IEnumerable<OpenInventary> GetAllOpenInventaryCollectionAsync(int pageSize, int pageNumber, string userId, out int totalOpenInventary);
    }

    public class OpenInventaryService : IOpenInventaryService
    {
        private readonly ApplicationDbContext _dbContext;
        public OpenInventaryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OpenInventary>> GetAllOpenInventaryAsync(string userId)
        {
            var allOpenInventary = await _dbContext.OpenInventaries
                .Where(p => !p.Status && p.UserId == userId)
                .ToListAsync();
            return allOpenInventary;
        }
        public async Task<OpenInventary> AddOpenInventaryAsync(DateTime openDate, DateTime closeDate, string bussinessId, bool statusInventary, double oldAmountInventary, double actualAmountInventary, string userId)
        {
            var openInventary = new OpenInventary
            {
                OpenDate = openDate,
                CloseDate = closeDate,
                BussinessId = bussinessId,
                StatusInventary = statusInventary,
                OldAmountInventary = oldAmountInventary,
                ActualAmountInventary = actualAmountInventary,
                UserId = userId
            };

            await _dbContext.AddAsync(openInventary);
            await _dbContext.SaveChangesAsync();
            return openInventary;
        }
        public async Task<OpenInventary> EditOpenInventaryAsync(string id, DateTime newOpenDate, DateTime newCloseDate, string newBussinessId, bool newStatusInventary, double newOldAmountInventary, double newActualAmountInventary, string userId)
        {
            var openInventary = await _dbContext.OpenInventaries.FindAsync(id);

            if (openInventary.UserId != userId || openInventary.Status)
                return null;
            openInventary.OpenDate = newOpenDate;
            openInventary.CloseDate = newCloseDate;
            openInventary.StatusInventary = newStatusInventary;
            openInventary.OldAmountInventary = newOldAmountInventary;
            openInventary.ActualAmountInventary = newActualAmountInventary;
            openInventary.ModifiedDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();
            return openInventary;
        }
        
        public IEnumerable<OpenInventary> GetAllOpenInventaryCollectionAsync(int pageSize, int pageNumber, string userId, out int totalOpenInventary)
        {
            var allOpenInventary = _dbContext.OpenInventaries.Where(a => !a.Status && a.UserId == userId);
            totalOpenInventary = allOpenInventary.Count();
            var openInventary = allOpenInventary.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArray();

            foreach (var item in openInventary)
            {
                item.BussinessId = _dbContext.Bussiness
                    .Where(i => !i.Status && i.Id == item.BussinessId)
                    .Select(s => s.Name)
                    .FirstOrDefault();
            }
            return openInventary;
        }
        public IEnumerable<OpenInventary> SearchOpenInventaryAsync(string query, int pageSize, int pageNumber, string userId, out int totalOpenInventary)
        {
            var allOpenInventary = _dbContext.OpenInventaries.Where(o => !o.Status && o.UserId == userId && (o.Bussiness.Name.Contains(query)));

            totalOpenInventary = allOpenInventary.Count();

            var account = allOpenInventary.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArray();
            foreach (var item in account)
            {
                item.BussinessId = _dbContext.Bussiness
                     .Where(i => !i.Status && i.Id == item.BussinessId)
                     .Select(s => s.Name)
                     .FirstOrDefault();
            }
            return account;
        }

    }
}
