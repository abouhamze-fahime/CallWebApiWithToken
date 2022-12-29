using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrderStore.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(object id);
        //Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includes = "");
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task Delete(object id);
        Task AddRangeAsync(IEnumerable<T> entities);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void RemoveRange(IEnumerable<T> entities);

    }
}
