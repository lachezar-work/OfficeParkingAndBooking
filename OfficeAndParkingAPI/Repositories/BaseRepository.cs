using Microsoft.EntityFrameworkCore;
using OfficeAndParkingAPI.Repositories.Contracts;
using OfficeAndParking.Data;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace OfficeAndParkingAPI.Repositories
{
    public class BaseRepository<T, TId> : IBaseRepository<T, TId> where T : class
    {
        protected readonly OfficeParkingDbContext _dbContext;

        public BaseRepository(OfficeParkingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(TId id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}