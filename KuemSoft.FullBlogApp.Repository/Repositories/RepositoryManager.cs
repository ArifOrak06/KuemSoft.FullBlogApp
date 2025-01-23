using KuemSoft.FullBlogApp.Core.Entities.Concrete;
using KuemSoft.FullBlogApp.Core.Repositories;
using KuemSoft.FullBlogApp.Repository.Contexts.EfCore;
using Microsoft.AspNetCore.Identity;

namespace KuemSoft.FullBlogApp.Repository.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _appDbContext;
        private readonly Lazy<IArticleRepository> _articleRepository;
        private readonly Lazy<ITagRepository> _tagRepository;
        private readonly Lazy<IAppUserRepository> _appUserRepository;
        private readonly Lazy<ICommentRepository> _commentRepository;

        public RepositoryManager(AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            _articleRepository = new Lazy<IArticleRepository>(() => new ArticleRepository(appDbContext));
            _tagRepository = new Lazy<ITagRepository>(() => new TagRepository(appDbContext));
            _appUserRepository = new Lazy<IAppUserRepository>(() => new AppUserRepository(appDbContext, userManager));
            _commentRepository = new Lazy<ICommentRepository>(() => new CommentRepository(appDbContext));
        }

        public ICommentRepository CommentRepository => _commentRepository.Value;

        public IAppUserRepository AppUserRepository => _appUserRepository.Value;

        public IArticleRepository ArticleRepository => _articleRepository.Value;    

        public ITagRepository TagRepository => _tagRepository.Value;
    }
}
