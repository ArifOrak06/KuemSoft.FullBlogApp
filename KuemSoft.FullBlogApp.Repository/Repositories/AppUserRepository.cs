using KuemSoft.FullBlogApp.Core.Entities.Concrete;
using KuemSoft.FullBlogApp.Core.Repositories;
using KuemSoft.FullBlogApp.Repository.Contexts.EfCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KuemSoft.FullBlogApp.Repository.Repositories
{
    public class AppUserRepository : RepositoryBase<AppUser>, IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public AppUserRepository(AppDbContext appDbContext, UserManager<AppUser> userManager) : base(appDbContext)
        {
            _userManager = userManager;
        }

        public async Task<List<AppUser>> GetAllAppUserWithRoleAndArticlesAndCommentsAsync(bool trackChanges, Expression<Func<AppUser, bool>> predicate = null, params Expression<Func<AppUser, object>>[] includeProperties)
        {
            var query = _userManager.Users;
            if (!trackChanges)
                query = query.AsNoTracking();
            if(predicate != null)
                query = query.Where(predicate);
            foreach (var property in includeProperties)
                query = query.Include(property);
            return await query.ToListAsync();
        }

        public async Task<AppUser> GetAppUserWithRoleAndArticlesAndCommentsAsync(bool trackChanges, Expression<Func<AppUser,bool>> predicate = null, params Expression<Func<AppUser, object>>[] includeProperties)
        {
            var query = _userManager.Users;
            if(!trackChanges)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            if(includeProperties.Any())
                foreach (var property in includeProperties)
                    query = query.Include(property);
            return await query.SingleOrDefaultAsync();
        }
    }
}
