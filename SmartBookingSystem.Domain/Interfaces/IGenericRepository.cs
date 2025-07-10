using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[]? includes);
        Task<T?> GetByIdAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[]? includes);
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task SoftDeleteAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
