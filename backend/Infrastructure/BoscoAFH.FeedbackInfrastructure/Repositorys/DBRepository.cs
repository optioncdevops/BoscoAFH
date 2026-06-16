using BoscoAFH.FeedbackInfrastructure.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace BoscoAFH.FeedbackInfrastructure.Repositorys
{
    public class DBRepository<T> : IDBRepository<T> where T : class
    {
        private readonly DbContext _context;  // Use DbContext directly
        private readonly DbSet<T> _dbSet;

        public DBRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        // Add new entity
        public async Task<T> AddAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<int> SaveAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(List<T> entities)
        {
            // Check for null to avoid exceptions
            ArgumentNullException.ThrowIfNull(entities);

            // Use AddRange instead of AddRangeAsync
            _dbSet.AddRange(entities);

            // Save changes asynchronously
            return await _context.SaveChangesAsync();
        }

        public async Task<T> FetchDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        // Get by Id
        public async Task<T> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Get all entities
        public async Task<IEnumerable<T>> GetAllAsync(bool include = false)
        {
            if (include)
            {
                return await _dbSet.IgnoreQueryFilters().ToListAsync();
            }
            else
            {
                // As No Tracking
                return await _dbSet.AsNoTracking().ToListAsync();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"> Predicate to filter results</param>
        /// <param name="selector">Selector to map the result </param>
        /// <returns></returns>
        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(predicate)  // Apply WHERE condition
                .Select(selector)   // Select the projection
                .ToListAsync();     // Execute query and return results
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            return await _dbSet
                .AsNoTracking()
                .AsNoTracking() // Improves performance by not tracking the entities
                .Select(selector)
                .ToListAsync();
        }

        // Find entities based on a predicate
        public async Task<IEnumerable<T>> FindListAsync(Func<T, bool> predicate)
        {
            return await Task.Run(() => _dbSet.AsNoTracking().Where(predicate).ToList());
        }

        // Find entities based on a predicate
        public async Task<T> FindAsync(Func<T, bool> predicate)
        {
            return await Task.Run(() => _dbSet.AsNoTracking().FirstOrDefault(predicate));
        }

        // Update entity
        public async Task<int> UpdateAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateRangeAsync(List<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            _dbSet.UpdateRange(entities);
            return await _context.SaveChangesAsync();
        }

        // Remove entity
        public async Task<int> RemoveAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            T entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveRangeAsync(List<T> entities)
        {
            // Check if the entities list is null or empty to avoid unnecessary database operations
            if (entities == null || entities.Count == 0)
            {
                throw new ArgumentNullException(nameof(entities), "The entities list cannot be null or empty.");
            }

            // Remove the entities from the DbSet
            _dbSet.RemoveRange(entities);

            // Save changes asynchronously and return the number of affected rows
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            T entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        //public async Task<IEnumerable<TDTO>> ExecuteSPAsync<TDTO>(string storedProcedure, object parameters)
        //{
        //    var result = new List<TDTO>();

        //    using (var dbConnection = _context.Database.GetDbConnection())
        //    {
        //        dbConnection.Open();
        //        // Execute the stored procedure using Dapper
        //        var entities = await dbConnection.QueryAsync<TDTO>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        //        result = entities.AsList(); // Convert to List<TDTO>
        //    }
        //    return result;
        //}

        public async Task<IEnumerable<TDTO>> ExecuteQueryAsync<TDTO>(string sQuery, object parameters)
        {
            var result = new List<TDTO>();
            using (var dbConnection = _context.Database.GetDbConnection())
            {
                await dbConnection.OpenAsync();
                // Execute the Query using Dapper
                var entities = await dbConnection.QueryAsync<TDTO>(sQuery, parameters, commandType: CommandType.Text);
                result = entities.AsList(); // Convert to List<TDTO>
            }
            return result;
        }
        public IQueryable<T> Query()
        {
            return _dbSet.AsNoTracking(); // ✅ Ensures better performance for read-only queries
        }

    }
}
