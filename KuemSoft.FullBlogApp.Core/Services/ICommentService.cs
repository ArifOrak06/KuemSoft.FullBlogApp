using KuemSoft.FullBlogApp.Core.DTOs.CommentDTOs;
using KuemSoft.FullBlogApp.SharedLibrary.ResponseResultPattern;

namespace KuemSoft.FullBlogApp.Core.Services
{
    public interface ICommentService
    {
        Task<CustomResponseDto<List<CommentDto>>> GetAllActivesAndNonDeletedCommentsWithAppUsersAndArticlesAsync();
        Task<CustomResponseDto<List<CommentDto>>> GetAllDeletedCommentsWithAppUsersAndArticlesAsync();
        Task<CustomResponseDto<CommentDto>> GetOneActiveAndNonDeletedCommentByIdWithAppUserAndArticleAsync(Guid commentId);
        Task<CustomResponseDto<CommentCreateDto>> CreateOneCommentAsync(CommentCreateDto commentCreateDto);
        Task<CustomResponseDto<CommentUpdateDto>> UpdateOneCommentAsync(CommentUpdateDto commentUpdateDto);
        Task<CustomResponseDto<NoContentDto>> SoftDeleteOneCommentAsync(Guid commentId);
        Task<CustomResponseDto<NoContentDto>> HardDeleteOneCommentAsync(Guid commentId);
        Task<CustomResponseDto<NoContentDto>> UndoDeleteOneCommentAsync(Guid commentId);

    }
}
