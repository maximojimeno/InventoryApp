using InventaryApp.Server.DataAccess;
using InventaryApp.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventaryApp.Server.Services
{
    public interface IAccountService
    {
        Task<Account> AddAccountAsync(string code, string name, string type, string bussinessId, string userId);
        Task<Account> EditAccountAsync(string id, string newCode, string newName, string newType, string newBussinessId, string userId);
        Task<Account> GetAccountById(string id, string userId);
        Task<Account> DeleteAccountAsync(string id, string userId);
        IEnumerable<Account> GetAllAccountAsync(int pageSize, int pageNumber, string userId, out int totalAccounts);
        IEnumerable<Account> SearchAccountAsync(string query, int pageSize, int pageNumber, string userId, out int totalAccounts);
    }

    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _dbContext;
        public AccountService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Account> AddAccountAsync(string code, string name,string type,string bussinessId, string userId)
        {
            var account = new Account
            {
                Code = code,
                Name = name,
                Type = type,
                BussinessId = bussinessId,
                UserId = userId
            };

            await _dbContext.AddAsync(account);
            await _dbContext.SaveChangesAsync();
            return account;
        }
        public async Task<Account> EditAccountAsync(string id,string newCode, string newName,string newType,string newBussinessId, string userId)
        {
            var account = await _dbContext.Accounts.FindAsync(id);

            if (account.UserId != userId || account.Status)
                return null;
            account.Code = newCode;
            account.Name = newName;
            account.Type = newType;
            account.Type = newBussinessId;
            account.ModifiedDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();
            return account;
        }
        public async Task<Account> GetAccountById(string id, string userId)
        {
            var account = await _dbContext.Accounts.FindAsync(id);
            if (account.UserId != userId || account.Status)
                return null;
            return account;
        }
        public async Task<Account> DeleteAccountAsync(string id, string userId)
        {
            var account = await _dbContext.Accounts.FindAsync(id);
            if (account.UserId != userId || account.Status)
                return null;

            account.Status = true;
            account.ModifiedDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return account;
        }
        public IEnumerable<Account> GetAllAccountAsync(int pageSize, int pageNumber, string userId, out int totalAccounts)
        {
            var allAccounts = _dbContext.Accounts.Where(a => !a.Status && a.UserId == userId);
            totalAccounts = allAccounts.Count();
            var accounts = allAccounts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArray();

            foreach (var item in accounts)
            {
                item.BussinessId = _dbContext.Bussiness
                    .Where(i => !i.Status && i.Id == item.BussinessId)
                    .Select(s => s.Name)
                    .FirstOrDefault();
            }
            return accounts;
        }
        public IEnumerable<Account> SearchAccountAsync(string query, int pageSize, int pageNumber, string userId, out int totalAccounts)
        {
            var allAccounts = _dbContext.Accounts.Where(a => !a.Status && a.UserId == userId && (a.Code.Contains(query) || a.Name.Contains(query) || a.Type.Contains(query)));

            totalAccounts = allAccounts.Count();

            var account = allAccounts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArray();
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
