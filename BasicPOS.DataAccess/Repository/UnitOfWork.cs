using BasicInventory.DataAccess.Data;
using BasicInventory.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicInventory.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IItemRepository Item { get; private set; }
        public IStockRepository Stock { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Item = new ItemRepository(_db);
            Stock = new StockRepository(_db);
        }
        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
