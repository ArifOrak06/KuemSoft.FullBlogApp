using KuemSoft.FullBlogApp.Core.Entities.Abstracts;

namespace KuemSoft.FullBlogApp.Core.Entities.Concrete
{
    public class ArticleTags : BaseEntity,IEntity
    {
        public Guid? ArticleId { get; set; }
        public Article Article { get; set; }
        public Guid? TagId { get; set; }
        public Tag Tag { get; set; }

    }
}
