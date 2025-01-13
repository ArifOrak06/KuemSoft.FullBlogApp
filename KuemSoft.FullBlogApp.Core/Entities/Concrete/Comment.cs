using KuemSoft.FullBlogApp.Core.Entities.Abstracts;

namespace KuemSoft.FullBlogApp.Core.Entities.Concrete
{
    public class Comment : BaseEntity,IEntity
    {
        public string Text { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
        public Guid? AppUserId { get; set; }
        public Article Article { get; set; } = null!;
        public Guid? ArticleId { get; set; }

    }
}
