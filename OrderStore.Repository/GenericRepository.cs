using Microsoft.EntityFrameworkCore;
using OrderStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrderStore.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private DbSet<T> _dbset;/// برای انجام عملیات 

        protected GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public virtual async Task<T> GetAsync(object id)
        {
            return await _dbset.FindAsync(id);
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includes = "")
        {

            //return context.Set<T>().ToList();
            // آی کوئری ایبل تا زمان فراخوانی اجرا نمی شود
            IQueryable<T> query = _dbset;
            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            if (includes != "")
            {
                foreach (string include in includes.Split(','))
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();

           
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
            return entity;
        }
        public virtual   void Update(T entity)
        {
          //  _dbset.Update(entity);
            _dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
           // _dbset.Remove(entity);

            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbset.Attach(entity);// اتچ یعنی چه 
            }
            _dbset.Remove(entity);
        }
        public virtual async  Task  Delete(object id)
        {
            var en = await GetAsync(id);
            Delete(en);
        }
        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbset.AddRangeAsync(entities);
        }
        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return  _dbset.Where(expression);
        }
        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _dbset.RemoveRange(entities);
        }
    }
}
