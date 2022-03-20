using Microsoft.EntityFrameworkCore;
using MySnippet_Data;
using MySnippet_DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MySnippet_DataAccess.Repository
{
    public class Repository<T> where T : class
    {
        private readonly MySnippetDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(MySnippetDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        protected void Add(T entity)
        {
            dbSet.Add(entity);
        }

        protected T Find(int id)
        {
            return dbSet.Find(id);
        }

        protected T Find(long id)
        {
            return dbSet.Find(id);
        }

        protected T Find(string id)
        {
            return dbSet.Find(id);
        }

        protected T FirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }

            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return query.FirstOrDefault();
        }

        protected IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return query.ToList();
        }

        protected void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        protected void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        protected void Update(T entity)
        {
            dbSet.Update(entity);
        }

        protected void Save()
        {
            _db.SaveChanges();
        }
    }
}
