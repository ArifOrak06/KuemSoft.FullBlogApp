using System.Linq.Expressions;

namespace KuemSoft.FullBlogApp.Core.Repositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetByFilter(bool trackChanges, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> GetAllAsync(bool trackChanges, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> CreateAsync(T entity);
        void DeleteAsync(T entity);
        void UpdateAsync(T entity);

    }
}
