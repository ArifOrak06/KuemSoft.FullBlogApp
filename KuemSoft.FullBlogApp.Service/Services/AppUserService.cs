using AutoMapper;
using FluentValidation;
using KuemSoft.FullBlogApp.Core.DTOs.AppUserDTOs;
using KuemSoft.FullBlogApp.Core.Repositories;
using KuemSoft.FullBlogApp.Core.Services;
using KuemSoft.FullBlogApp.Service.Helpers;
using KuemSoft.FullBlogApp.SharedLibrary.ResponseResultPattern;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace KuemSoft.FullBlogApp.Service.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IValidator<AppUserCreateDto> _createDtoValidator;
        private readonly IValidator<AppUserUpdateDto> _updateDtoValidator;
        private readonly IImgHelper _imgHelper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ClaimsPrincipal _claimsPrincipal;

        public AppUserService(IRepositoryManager repositoryManager, IMapper mapper, IValidator<AppUserCreateDto> createDtoValidator, IValidator<AppUserUpdateDto> updateDtoValidator, IImgHelper imgHelper, IHttpContextAccessor contextAccessor)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _imgHelper = imgHelper;
            _contextAccessor = contextAccessor;
            _claimsPrincipal = _contextAccessor.HttpContext.User;
        }

        public Task<CustomResponseDto<AppUserCreateDto>> CreateAppUserAsync(AppUserCreateDto appUserCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<NoContentDto>> DeleteAppUserAsync(Guid appUserId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<List<AppUserDto>>> GetAllAppUsersWithRoleAndArticlesAndCommentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<AppUserDto>> GetAppUserWithRoleAndArticlesAndCommentsAsync(Guid appUserId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<AppUserUpdateDto>> UpdateAppUserAsync(AppUserUpdateDto appUserUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
