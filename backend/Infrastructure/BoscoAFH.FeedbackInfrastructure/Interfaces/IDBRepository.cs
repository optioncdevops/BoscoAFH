using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BoscoAFH.FeedbackInfrastructure.Interfaces
{
    public interface IDBRepository<T> where T : class
    {
        // Add new entity
        Task<T> AddAsync(T entity);

        Task<int> SaveAsync(T entity);

        Task<int> AddRangeAsync(List<T> entity);

        Task<T> FetchDefaultAsync(Expression<Func<T, bool>> predicate);

        // Get by Id
        Task<T> GetByIdAsync(long id);

        Task<T> GetByIdAsync(Guid id);

        // Remove entity
        Task<int> RemoveAsync(T entity);

        // Get all entities
        Task<IEnumerable<T>> GetAllAsync(bool include = false);

        // Get all needed entities with filter
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);

        // Get all needed entities with selected columns
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> selector);

        // Find entities based on a predicate (using Func<>)
        Task<IEnumerable<T>> FindListAsync(Func<T, bool> predicate);

        Task<T> FindAsync(Func<T, bool> predicate);

        // Update entity
        Task<int> UpdateAsync(T entity);

        Task<int> UpdateRangeAsync(List<T> entity);

        // Delete entity
        Task<int> DeleteAsync(int id);

        Task<int> RemoveRangeAsync(List<T> entity);

        Task<int> DeleteAsync(Guid id);

        Task<IEnumerable<TDTO>> ExecuteQueryAsync<TDTO>(string sQuery, object parameters);

        IQueryable<T> Query();
    }

}
