using KuemSoft.FullBlogApp.Core.DTOs.ArticleDTOs;

namespace KuemSoft.FullBlogApp.Core.DTOs.TagDTOs
{
    public class TagDto 
    {
        public Guid Id { get; set; }
        public ICollection<ArticleDto> Articles { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }



    }
}
