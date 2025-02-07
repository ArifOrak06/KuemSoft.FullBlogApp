namespace KuemSoft.FullBlogApp.Core.DTOs.CategoryDTOs
{
    public class CategoryUpdateDto 
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
