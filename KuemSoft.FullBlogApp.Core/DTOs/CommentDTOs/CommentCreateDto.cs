using KuemSoft.FullBlogApp.Core.DTOs.AppUserDTOs;
using KuemSoft.FullBlogApp.Core.DTOs.ArticleDTOs;

namespace KuemSoft.FullBlogApp.Core.DTOs.CommentDTOs
{
    public class CommentCreateDto
    {
        public string Text { get; set; }
        public Guid? AppUserId { get; set; }
        public AppUserDto AppUser { get; set; }
        public Guid? ArticleId { get; set; }
        public ArticleDto Article { get; set; }
    }
}
