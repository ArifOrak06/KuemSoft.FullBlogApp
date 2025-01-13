using KuemSoft.FullBlogApp.Core.Entities.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace KuemSoft.FullBlogApp.Core.Entities.Concrete
{
    public class AppUser : IdentityUser<Guid>,IEntity
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ICollection<Article> Articles { get; set; } = new List<Article>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
