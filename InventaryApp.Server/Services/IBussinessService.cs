using InventaryApp.Server.DataAccess;
using InventaryApp.Server.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventaryApp.Server.Services
{
    public interface IBussinessService 


    {
        Task<IEnumerable<Bussiness>> GetAllBussinesAsync(string userId);
        Task<Bussiness> GetBussinessById(string id, string userId);
        IEnumerable<Bussiness> GetAllBussinessCollectionAsync(int pageSize, int pageNumber, string userId, out int totalBussiness);
        Task<Bussiness> DeleteBussinessAsync(string id, string userId);
        IEnumerable<Bussiness> SearchBussinessAsync(string query, int pageSize, int pageNumber, string userId, out int totalBussiness);
        Task<Bussiness> EditBussinessAsync(string id, string newCode, string newName, string newAddress, string newPhoneNumber, string newEmail, string newOwner, string newOwnerPhone, string userId);
        Task<Bussiness> AddBussinessAsync(string code, string name, string address, string phoneNumber, string email, string owner, string ownerPhone, string userId);
    }

    public class BussinessService : IBussinessService
    {
        private readonly ApplicationDbContext _dbContext;

        public BussinessService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Bussiness>> GetAllBussinesAsync(string userId)
        {
            var allBussiness = await _dbContext.Bussiness
                .Where(p => !p.Status && p.UserId == userId)
                .ToListAsync();
            return allBussiness;
        }
        public async Task<Bussiness> GetBussinessById(string id, string userId)
        {
            var bussiness = await _dbContext.Bussiness.FindAsync(id);
            if (bussiness.UserId != userId || bussiness.Status)
                return null;
            return bussiness;
        }
        public IEnumerable<Bussiness> GetAllBussinessCollectionAsync(int pageSize, int pageNumber, string userId, out int totalBussiness)
        {
            var allBussiness = _dbContext.Bussiness.Where(p => !p.Status && p.UserId == userId);
            totalBussiness = allBussiness.Count();
            var bussines = allBussiness.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArray();
            return bussines;
        }
        public async Task<Bussiness> DeleteBussinessAsync(string id, string userId)
        {
            var bussiness = await _dbContext.Bussiness.FindAsync(id);
            if (bussiness.UserId != userId || bussiness.Status)
                return null;

            bussiness.Status = true;
            bussiness.ModifiedDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return bussiness;
        }
        public IEnumerable<Bussiness> SearchBussinessAsync(string query, int pageSize, int pageNumber, string userId, out int totalBussiness)
        {
            // total plans 
            var allBussiness = _dbContext.Bussiness.Where(b => !b.Status && b.UserId == userId && (b.Code.Contains(query) || b.Name.Contains(query) || b.Owner.Contains(query) ));

            totalBussiness = allBussiness.Count();

            var bussiness = allBussiness.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArray();

            return bussiness;
        }
        public async Task<Bussiness> EditBussinessAsync(string id, string newCode, string newName, string newAddress, string newPhoneNumber, string newEmail, string newOwner, string newOwnerPhone, string userId)
        {
            var bussiness = await _dbContext.Bussiness.FindAsync(id);

            if (bussiness.UserId != userId || bussiness.Status)
                return null;

            bussiness.Code = newCode;
            bussiness.Name = newName;
            bussiness.Address = newAddress;
            bussiness.PhoneNumber = newPhoneNumber;
            bussiness.Email = newEmail;
            bussiness.Owner = newOwner;
            bussiness.OwnerPhone = newOwnerPhone;
            
            bussiness.ModifiedDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();
            return bussiness;
        }
        public async Task<Bussiness> AddBussinessAsync(string code, string name, string address, string phoneNumber, string email, string owner, string ownerPhone, string userId)
        {
            var bussines = new Bussiness
            {
                Code = code,
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                Owner = owner,
                OwnerPhone = ownerPhone,
                UserId = userId
            };

            await _dbContext.AddAsync(bussines);
            await _dbContext.SaveChangesAsync();
            return bussines;
        }
    }
}
