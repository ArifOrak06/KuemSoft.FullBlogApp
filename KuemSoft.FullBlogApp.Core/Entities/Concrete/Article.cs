using KuemSoft.FullBlogApp.Core.Entities.Abstracts;

namespace KuemSoft.FullBlogApp.Core.Entities.Concrete
{
    public class Article : BaseEntity,IEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Guid? AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
