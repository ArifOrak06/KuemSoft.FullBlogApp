using KuemSoft.FullBlogApp.Core.Entities.Concrete;
using System.Linq.Expressions;

namespace KuemSoft.FullBlogApp.Core.Repositories
{
    public interface IAppUserRepository : IRepositoryBase<AppUser>
    {
        Task<List<AppUser>> GetAllAppUserWithRoleAndArticlesAndCommentsAsync(bool trackChanges, Expression<Func<AppUser,bool>> predicate = null,params Expression<Func<AppUser, object>>[] includeProperties);
        Task<AppUser> GetAppUserWithRoleAndArticlesAndCommentsAsync(bool trackChanges, Expression<Func<AppUser,bool>> predicate = null, params Expression<Func<AppUser, object>>[] includeProperties);
        
    }
}
