namespace KuemSoft.FullBlogApp.Core.Repositories
{
    public interface IRepositoryManager
    {
        ICommentRepository CommentRepository { get; }
        IAppUserRepository AppUserRepository { get; }
        IArticleRepository ArticleRepository { get; }
        ITagRepository TagRepository { get; }
        
    }
}
