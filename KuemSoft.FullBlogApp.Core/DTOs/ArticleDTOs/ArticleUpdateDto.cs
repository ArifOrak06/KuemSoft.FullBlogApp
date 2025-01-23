using KuemSoft.FullBlogApp.Core.DTOs.AppUserDTOs;
using KuemSoft.FullBlogApp.Core.DTOs.CategoryDTOs;
using KuemSoft.FullBlogApp.Core.DTOs.ImgDTOs;
using Microsoft.AspNetCore.Http;

namespace KuemSoft.FullBlogApp.Core.DTOs.ArticleDTOs
{
    public class ArticleUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid ImgId { get; set; }
        public ImgDto Img { get; set; }
        public IFormFile Photo { get; set; }
        public IList<CategoryDto> Categories { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
