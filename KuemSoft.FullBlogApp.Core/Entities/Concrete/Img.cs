using KuemSoft.FullBlogApp.Core.Entities.Abstracts;

namespace KuemSoft.FullBlogApp.Core.Entities.Concrete
{
    public class Img : BaseEntity, IEntity
    {
        public string FullName { get; set; }
        public string FileType { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
    }
}
