namespace KuemSoft.FullBlogApp.Core.DTOs.TagDTOs
{
    public class TagUpdateDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string Text { get; set; }
    }
}
