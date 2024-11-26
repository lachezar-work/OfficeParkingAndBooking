using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OfficeAndParking.Services.Repositories.Contracts
{
    public interface IBaseRepository<T, in TId> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(TId id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
    }
}