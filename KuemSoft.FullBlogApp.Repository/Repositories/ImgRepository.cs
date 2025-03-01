using KuemSoft.FullBlogApp.Core.Entities.Concrete;
using KuemSoft.FullBlogApp.Core.Repositories;
using KuemSoft.FullBlogApp.Repository.Contexts.EfCore;

namespace KuemSoft.FullBlogApp.Repository.Repositories
{
    public class ImgRepository : RepositoryBase<Img>, IImgRepository
    {
        public ImgRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
