using BasicInventory.DataAccess.Data;
using BasicInventory.Entities;
using BasicInventory.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicInventory.DataAccess.Repository.IRepository
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(ApplicationDbContext db) : base(db)
        {

        }
    }
}