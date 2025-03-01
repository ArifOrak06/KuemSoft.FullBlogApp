using KuemSoft.FullBlogApp.Core.Entities.Concrete;
using KuemSoft.FullBlogApp.Core.Repositories;
using KuemSoft.FullBlogApp.Repository.Contexts.EfCore;

namespace KuemSoft.FullBlogApp.Repository.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {  
        }
    }
}
