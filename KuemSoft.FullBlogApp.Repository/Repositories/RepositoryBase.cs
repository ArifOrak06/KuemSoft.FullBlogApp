using KuemSoft.FullBlogApp.Core.Entities.Abstracts;
using KuemSoft.FullBlogApp.Core.Repositories;
using KuemSoft.FullBlogApp.Repository.Contexts.EfCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KuemSoft.FullBlogApp.Repository.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntity
    {
        protected readonly AppDbContext _appDbContext;

        protected RepositoryBase(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
            return entity;

        }

        public async void DeleteAsync(T entity) => _appDbContext.Set<T>().Remove(entity);
        public async Task<List<T>> GetAllAsync(bool trackChanges, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _appDbContext.Set<T>();
            if (!trackChanges)
                query = query.AsNoTracking();
            if(predicate != null)
                query = query.Where(predicate);
            if (includeProperties.Any())
                foreach (var property in includeProperties)
                    query = query.Include(property);
            return await query.ToListAsync();
        }

        public IQueryable<T> GetByFilter(bool trackChanges, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _appDbContext.Set<T>();
            if (!trackChanges)
                query = query.AsNoTracking();
            
            query = query.Where(predicate);
            foreach (var property in includeProperties)
                query = query.Include(property);
            return query;

            
        }

        public void UpdateAsync(T entity) => _appDbContext.Set<T>().Update(entity);
        
    }
}
