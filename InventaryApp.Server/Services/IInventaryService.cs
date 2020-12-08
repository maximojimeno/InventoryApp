using InventaryApp.Server.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventaryApp.Server.Services
{
    public interface IInventaryService
    {

    }

    public class InventaryService : IInventaryService
    {
        private readonly ApplicationDbContext _dbContext;

        public InventaryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


    }

}
