using KuemSoft.FullBlogApp.Core.DTOs.TagDTOs;
using KuemSoft.FullBlogApp.SharedLibrary.ResponseResultPattern;

namespace KuemSoft.FullBlogApp.Core.Services
{
    public interface ITagService
    {
        Task<CustomResponseDto<List<TagDto>>> GetAllActivesAndNonDeletedTagsWithArticlesAsync();
        Task<CustomResponseDto<List<TagDto>>> GetAllDeletedTagsWithArticlesAsync();
        Task<CustomResponseDto<TagDto>> GetOneActiveAndNonDeletedTagWithArticlesByTagIdAsync(Guid tagId);
        Task<CustomResponseDto<TagCreateDto>> CreateOneTagAsync(TagCreateDto tagCreateDto);
        Task<CustomResponseDto<TagUpdateDto>> UpdateOneTagAsync(TagUpdateDto tagUpdateDto);
        Task<CustomResponseDto<NoContentDto>> SoftDeleteOneTagAsync(Guid tagId);
        Task<CustomResponseDto<NoContentDto>> HardDeleteOneTagAsync(Guid tagId);
        Task<CustomResponseDto<NoContentDto>> UndoDeleteOneTagAsync(Guid tagId);
        Task<CustomResponseDto<List<TagDto>>> GetAllActivesTagsByArticleIdAsync(Guid articleId);
    }
}
