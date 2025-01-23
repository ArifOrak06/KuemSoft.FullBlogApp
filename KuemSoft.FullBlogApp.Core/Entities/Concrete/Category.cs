using KuemSoft.FullBlogApp.Core.Entities.Abstracts;

namespace KuemSoft.FullBlogApp.Core.Entities.Concrete
{
    public class Category : BaseEntity,IEntity
    {
        public string Description { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
