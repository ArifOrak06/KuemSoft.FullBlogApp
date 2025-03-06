using KuemSoft.FullBlogApp.Core.Entities.Concrete;
using KuemSoft.FullBlogApp.Core.Repositories;
using KuemSoft.FullBlogApp.Repository.Contexts.EfCore;

namespace KuemSoft.FullBlogApp.Repository.Repositories
{
    public class ArticleTagsRepository : RepositoryBase<ArticleTags>, IArticleTagsRepository
    {
        public ArticleTagsRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
