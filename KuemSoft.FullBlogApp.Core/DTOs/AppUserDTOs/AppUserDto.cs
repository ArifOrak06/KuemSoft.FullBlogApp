using KuemSoft.FullBlogApp.Core.DTOs.ArticleDTOs;

namespace KuemSoft.FullBlogApp.Core.DTOs.AppUserDTOs
{
    public class AppUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string Role { get; set; }
        public bool EmailConfirmed { get; set; }
        public ICollection<ArticleDto> Articles { get; set; }

    }
}
