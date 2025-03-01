using KuemSoft.FullBlogApp.Core.DTOs.CategoryDTOs;
using KuemSoft.FullBlogApp.SharedLibrary.ResponseResultPattern;

namespace KuemSoft.FullBlogApp.Core.Services
{
    public interface ICategoryService
    {
        Task<CustomResponseDto<List<CategoryDto>>> GetAllActivesAndNonDeletedCategoriesWithArticlesAsync();
        Task<CustomResponseDto<List<CategoryDto>>> GetAllDeletedCategoriesWithArticlesAsync();
        Task<CustomResponseDto<CategoryDto>> GetOneActiveAndNonDeletedCategoryByIdWithArticlesAsync(Guid categoryId);
        Task<CustomResponseDto<CategoryCreateDto>> CreateOneCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task<CustomResponseDto<CategoryUpdateDto>> UpdateOneCategoryAsync(CategoryUpdateDto categoryUpdateDto);
        Task<CustomResponseDto<NoContentDto>> SoftDeleteOneCategoryAsync(Guid categoryId);
        Task<CustomResponseDto<NoContentDto>> UndoDeleteOneCategoryAsync(Guid categoryId);
        Task<CustomResponseDto<NoContentDto>> HardDeleteOneCategoryAsync(Guid categoryId);

    }
}
