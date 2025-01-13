using KuemSoft.FullBlogApp.Core.Entities.Abstracts;

namespace KuemSoft.FullBlogApp.Core.Entities.Concrete
{
    public class Tag : BaseEntity,IEntity
    {
        public string? Text { get; set; }
        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
