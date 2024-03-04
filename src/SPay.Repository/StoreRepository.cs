using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;

namespace SPay.Repository
{
    public interface IStoreRepository
    {
        Task<IList<StoreOwner>> GetAllStoreInfo();
        Task<IList<StoreOwner>> SearchByNameAsync(string name);

    }
    public class StoreRepository : IStoreRepository
    {
        private readonly SPayDbContext _context;
        public StoreRepository(SPayDbContext _context)
        {
            this._context = _context;
        }
        public async Task<IList<StoreOwner>> GetAllStoreInfo()
        {
            return await _context.StoreOwners
                .Include(so => so.StoreKeyNavigation)
                .Include(so => so.StoreKeyNavigation.CategoryKeyNavigation)
                .Include(so => so.StoreKeyNavigation.Wallets)
                .ToListAsync();
        }

        public async Task<IList<StoreOwner>> SearchByNameAsync(string name)
        {
            return await _context.StoreOwners
                .Where(so => so.StoreKeyNavigation.Name.ToLower().Contains(name.ToLower()))
                .Include(so => so.StoreKeyNavigation)
                .Include(so => so.StoreKeyNavigation.CategoryKeyNavigation)
                .Include(so => so.StoreKeyNavigation.Wallets)
                .ToListAsync();
        }
    }
}
