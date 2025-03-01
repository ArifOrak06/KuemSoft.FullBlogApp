using KuemSoft.FullBlogApp.Core.DTOs.ArticleDTOs;
using KuemSoft.FullBlogApp.SharedLibrary.ResponseResultPattern;

namespace KuemSoft.FullBlogApp.Core.Services
{
    public interface IArticleService
    {
        Task<CustomResponseDto<List<ArticleDto>>> GetAllActivesAndNonDeletedArticlesWithAssociatedEntitiesAsync();
        Task<CustomResponseDto<ArticleDto>> GetOneActiveAndNonDeletedArticleWithAssocieatedEntitiesByArticleIdAsync(Guid articleId);
        Task<CustomResponseDto<List<ArticleDto>>> GetAllActivesAndNonDeletedArticlesWithAssociatedEntitiesByCategoryIdAsync(Guid categoryId);
        Task<CustomResponseDto<List<ArticleDto>>> GetAllDeletedArticlesAsync();
        Task<CustomResponseDto<ArticleCreateDto>> CreateOneArticleAsync(ArticleCreateDto articleCreateDto);
        Task<CustomResponseDto<ArticleUpdateDto>> UpdateOneArticleAsync(ArticleUpdateDto articleUpdateDto);
        Task<CustomResponseDto<NoContentDto>> SoftDeleteArticleAsync(Guid articleId);
        Task<CustomResponseDto<NoContentDto>> HardDeleteArticleAsync(Guid articleId);
        Task<CustomResponseDto<NoContentDto>> UndoDeleteOneArticleAsync(Guid articleId);

    }
}
