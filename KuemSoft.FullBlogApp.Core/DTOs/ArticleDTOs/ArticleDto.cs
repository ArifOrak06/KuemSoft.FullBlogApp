using KuemSoft.FullBlogApp.Core.DTOs.AppUserDTOs;
using KuemSoft.FullBlogApp.Core.DTOs.CategoryDTOs;
using KuemSoft.FullBlogApp.Core.DTOs.CommentDTOs;
using KuemSoft.FullBlogApp.Core.DTOs.ImgDTOs;
using KuemSoft.FullBlogApp.Core.DTOs.TagDTOs;

namespace KuemSoft.FullBlogApp.Core.DTOs.ArticleDTOs
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Guid? AppUserId { get; set; }
        public AppUserDto AppUser { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
        public ICollection<TagDto> Tags { get; set; }
        public CategoryDto Category { get; set; }
        public Guid ImgId { get; set; }
        public ImgDto ImgDto { get; set; }

    }
}
