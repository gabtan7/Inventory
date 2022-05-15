using BasicInventory.DataAccess.Data;
using BasicInventory.Entities;
using BasicInventory.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasicInventory.DataAccess.Repository.IRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            if (entity.GetType().GetProperty("CreatedDate") != null)
                entity.GetType().GetProperty("CreatedDate").SetValue(entity, DateTime.Now);

            await dbSet.AddAsync(entity);

            return entity;
        }
        public T Update(T entity)
        {
            if (entity.GetType().GetProperty("UpdatedDate") != null)
                entity.GetType().GetProperty("UpdatedDate").SetValue(entity, DateTime.Now);

            dbSet.Update(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            List<T> list = await query.ToListAsync();
            list.Reverse();

            return list;
        }

        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
        {
            IQueryable<T> query;

            if (tracked)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }

            query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public void Remove(T entity) //generic soft delete
        {
            if (entity.GetType().GetProperty("IsActive") != null)
                entity.GetType().GetProperty("IsActive").SetValue(entity, false);

            if (entity.GetType().GetProperty("UpdatedDate") != null)
                entity.GetType().GetProperty("UpdatedDate").SetValue(entity, DateTime.Now);

            dbSet.Update(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            foreach(var t in entity)
            {
                if(t.GetType().GetProperty("IsActive") != null)
                    t.GetType().GetProperty("IsActive").SetValue(t, false);

                if (t.GetType().GetProperty("UpdatedDate") != null)
                    t.GetType().GetProperty("UpdatedDate").SetValue(t, false);

                dbSet.Update(t);
            }
        }

        public void UntrackEntity(T entity)
        {
            _db.Entry(entity).State = EntityState.Detached;
        }

        //public void RemoveRange(IEnumerable<T> entity)
        //{
        //    dbSet.RemoveRange(entity);
        //}
    }
}
