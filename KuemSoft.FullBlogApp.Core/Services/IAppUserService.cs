using KuemSoft.FullBlogApp.Core.DTOs.AppUserDTOs;
using KuemSoft.FullBlogApp.SharedLibrary.ResponseResultPattern;

namespace KuemSoft.FullBlogApp.Core.Services
{
    public interface IAppUserService
    {
        Task<CustomResponseDto<List<AppUserDto>>> GetAllAppUsersWithRoleAndArticlesAndCommentsAsync();
        Task<CustomResponseDto<AppUserDto>> GetAppUserWithRoleAndArticlesAndCommentsAsync(Guid appUserId);
        Task<CustomResponseDto<AppUserCreateDto>> CreateAppUserAsync(AppUserCreateDto appUserCreateDto);
        Task<CustomResponseDto<AppUserUpdateDto>> UpdateAppUserAsync(AppUserUpdateDto appUserUpdateDto);
        Task<CustomResponseDto<NoContentDto>> DeleteAppUserAsync(Guid appUserId);
    }
}
