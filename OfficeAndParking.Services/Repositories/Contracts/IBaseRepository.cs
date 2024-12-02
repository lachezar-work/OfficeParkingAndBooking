using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OfficeAndParking.Services.Repositories.Contracts
{
    public interface IBaseRepository<TEntity, in TEntityIdType> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> GetByIdAsync(TEntityIdType id);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task SaveChangesAsync();
    }
}