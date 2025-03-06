using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using KuemSoft.FullBlogApp.Core.DTOs.AppUserDTOs;
using KuemSoft.FullBlogApp.Core.Entities.Concrete;
using KuemSoft.FullBlogApp.Core.Repositories;
using KuemSoft.FullBlogApp.Core.Services;
using KuemSoft.FullBlogApp.Core.UnitOfWork;
using KuemSoft.FullBlogApp.Service.Extensions.FluentValidationEx;
using KuemSoft.FullBlogApp.Service.Extensions.Identity;
using KuemSoft.FullBlogApp.Service.Helpers;
using KuemSoft.FullBlogApp.SharedLibrary.Enums;
using KuemSoft.FullBlogApp.SharedLibrary.ResponseResultPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _singInManager;
        private readonly IValidator<AppUserLoginDto> _loginDtoValidator;


        public AppUserService(IRepositoryManager repositoryManager, IMapper mapper, IValidator<AppUserCreateDto> createDtoValidator, IValidator<AppUserUpdateDto> updateDtoValidator, IImgHelper imgHelper, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> singInManager, IValidator<AppUserLoginDto> loginDtoValidator)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _imgHelper = imgHelper;
            _contextAccessor = contextAccessor;
            _claimsPrincipal = _contextAccessor.HttpContext.User;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _singInManager = singInManager;
            _loginDtoValidator = loginDtoValidator;
        }

        public async Task<CustomResponseDto<AppUserCreateDto>> CreateAppUserAsync(AppUserCreateDto appUserCreateDto)
        {
            ValidationResult? validationResult = await _createDtoValidator.ValidateAsync(appUserCreateDto);
            if (!validationResult.IsValid)
                return CustomResponseDto<AppUserCreateDto>.ValidationFail(ResponseType.ValidError, validationResult.ConvertToCustomValidationError());
            AppUser? newAppUser = _mapper.Map<AppUser>(appUserCreateDto);
            newAppUser.UserName = appUserCreateDto.Email;
            if(appUserCreateDto.Photo != null)
            {
                var imgUploadResult = await _imgHelper.UploadImageAsync(appUserCreateDto.FirstName, appUserCreateDto.Photo, ImageType.User);
                if(imgUploadResult.ResponseType == ResponseType.Success)
                {
                    Img? newImg = new Img
                    {
                        FullName = imgUploadResult.Data.FullName,
                        FileType = imgUploadResult.Data.FileType,
                        CreatedDate = DateTime.Now,
                        CreatedBy = _claimsPrincipal.GetLoggerInAppUserEmail(),
                        IsActive = true,
                        IsDeleted = false,
                        ModifiedDate = DateTime.Now,
                        ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail(),
                        
                    };
                    await _repositoryManager.ImgRepository.CreateAsync(newImg);
                    await _unitOfWork.CommitAsync();
                    newAppUser.ImgId = newImg.Id;

                }
                
            }
            IdentityResult? ıdentityCreateAppUserResult = await _userManager.CreateAsync(newAppUser,appUserCreateDto.Password);
            if (ıdentityCreateAppUserResult.Succeeded)
            {
                //Kullanıcı başarılı bir şekilde eklendi ise rolünü de db'ye ekleyelim.
                var isMatchedRole = await _roleManager.FindByIdAsync(appUserCreateDto.AppRoleId.ToString());
                IdentityResult? roleCreateResult  = await _userManager.AddToRoleAsync(newAppUser, isMatchedRole.Name);
                if (roleCreateResult.Succeeded)
                    return CustomResponseDto<AppUserCreateDto>.Success(ResponseType.Success, _mapper.Map<AppUserCreateDto>(newAppUser), $"{appUserCreateDto.FirstName} isimli kullanıcının kayıt işlemi ve role ekleme işlemi başarılı bir şekilde gerçekleşmiştir.");
                return CustomResponseDto<AppUserCreateDto>.IdentityFail(ResponseType.IdentityError, roleCreateResult.ConvertToCustomIdentityError());

            }
            return CustomResponseDto<AppUserCreateDto>.IdentityFail(ResponseType.IdentityError, ıdentityCreateAppUserResult.ConvertToCustomIdentityError());

          
        }

        public async Task<CustomResponseDto<NoContentDto>> DeleteAppUserAsync(Guid appUserId)
        {
            AppUser? currentAppUser = await _repositoryManager.AppUserRepository.GetAppUserAsync(true, x => x.Id.Equals(appUserId));
            if (currentAppUser == null)
                return CustomResponseDto<NoContentDto>.Fail(ResponseType.NotFound, $"AppUser Id : {appUserId}'ye sahip kullanıcı sistemde bulunamamıştır.");
            IdentityResult? deleteResult = await _userManager.DeleteAsync(currentAppUser);
            if (deleteResult.Succeeded)
                return CustomResponseDto<NoContentDto>.Success(ResponseType.Success, $"AppUser Id : {appUserId}'ye sahip kullanıcı silme işlemi başarılı olarak gerçekleştirilmiştir.");
            return CustomResponseDto<NoContentDto>.IdentityFail(ResponseType.IdentityError,deleteResult.ConvertToCustomIdentityError());
        }

        public async Task<CustomResponseDto<List<AppUserDto>>> GetAllAppUsersWithRoleAndArticlesAndCommentsAsync()
        {
            List<AppUser>? appUsers = await _repositoryManager.AppUserRepository.GetAllAppUserAsync(false, null, x => x.Img, x => x.Articles, x => x.Comments);
            if (appUsers == null)
                return CustomResponseDto<List<AppUserDto>>.Fail(ResponseType.NotFound, "Sistemde kayıtlı kullanıcı bulunmamaktadır.");
            List<AppUserDto>? newAppUserDtoList = _mapper.Map<List<AppUserDto>>(appUsers);
            foreach (var appUserDto in newAppUserDtoList)
            {
                var currentAppUserInRole = string.Join("", await _userManager.GetRolesAsync(_mapper.Map<AppUser>(appUserDto)));
                appUserDto.Role = currentAppUserInRole;

            }
            return CustomResponseDto<List<AppUserDto>>.Success(ResponseType.Success, newAppUserDtoList, "Kullanıcılar Rolü,Makaleleri ve Yorumları ile birlikte listelenmiştir.");
        }

        public async Task<CustomResponseDto<AppUserDto>> GetAppUserWithRoleAndArticlesAndCommentsAsync(Guid appUserId)
        {
            AppUser? currentUser  = await _repositoryManager.AppUserRepository.GetAppUserAsync(false,x => x.Id.Equals(appUserId),x => x.Articles,x => x.Img,x =>x.Comments);
            if (currentUser == null)
                return CustomResponseDto<AppUserDto>.Fail(ResponseType.NotFound, $"AppUser Id : {appUserId} olan kullanıcı sistemde bulunamamıştır.");
            AppUserDto? newDto = _mapper.Map<AppUserDto>(currentUser);
            newDto.Role = string.Join("", await _userManager.GetRolesAsync(currentUser));
            return CustomResponseDto<AppUserDto>.Success(ResponseType.Success, newDto, $"AppUser Id : {appUserId}'ye sahip kullanıcı başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseDto<NoContentDto>> LoginToAppUserAsync(AppUserLoginDto appUserLoginDto)
        {
            ValidationResult? validationResult = await _loginDtoValidator.ValidateAsync(appUserLoginDto);
            if (!validationResult.IsValid)
                return CustomResponseDto<NoContentDto>.ValidationFail(ResponseType.ValidError, validationResult.ConvertToCustomValidationError());
            AppUser? currentAppUser = await _userManager.FindByEmailAsync(appUserLoginDto.Email);
            if (currentAppUser == null)
                return CustomResponseDto<NoContentDto>.Fail(ResponseType.NotFound, $"Email veya şifre hatalı.");
            var userPasswordCheck = await _singInManager.PasswordSignInAsync(currentAppUser,appUserLoginDto.Password,appUserLoginDto.RememberMe,false);
            if (userPasswordCheck.Succeeded)
                return CustomResponseDto<NoContentDto>.Success(ResponseType.Success, "Login işlemi başarılı olarak gerçekleştirilmiştir.");
            return CustomResponseDto<NoContentDto>.Fail(ResponseType.Error, "Email veya şifre Hatalı");

        }

        public async Task<CustomResponseDto<AppUserUpdateDto>> UpdateAppUserAsync(AppUserUpdateDto appUserUpdateDto)
        {
            ValidationResult? validationResult = await _updateDtoValidator.ValidateAsync(appUserUpdateDto);
            if (!validationResult.IsValid)
            {
                AppUserUpdateDto newUpdateDto = _mapper.Map<AppUserUpdateDto>(await _repositoryManager.AppUserRepository.GetByFilter(false, x => x.Id.Equals(appUserUpdateDto.Id), x => x.Articles, x => x.Img, x => x.Comments).SingleOrDefaultAsync());
                //newUpdateDto.Role = string.Join("", await _userManager.GetRolesAsync(_mapper.Map<AppUser>(newUpdateDto)));
                return CustomResponseDto<AppUserUpdateDto>.ValidUpdateFail(ResponseType.ValidError, newUpdateDto, validationResult.ConvertToCustomValidationError());
            }
            AppUser? oldUser = await _repositoryManager.AppUserRepository.GetAppUserAsync(true, x => x.Id.Equals(appUserUpdateDto.Id),x => x.Img);
            if (oldUser == null)
                return CustomResponseDto<AppUserUpdateDto>.Fail(ResponseType.NotFound, $"AppUser Id : {appUserUpdateDto.Id}'ye sahip kullanıcı sistemde kayıtlı değildir.");
            oldUser.UserName = appUserUpdateDto.Email;
            oldUser.PhoneNumber = appUserUpdateDto.PhoneNumber;
            oldUser.LastName = appUserUpdateDto.LastName;
            oldUser.FirstName = appUserUpdateDto.FirstName;

            // mevcut kullanıcının yeni rolü eski rolden farklıysa eskisini silip yenisini ekleyelim.
            var currentRole = string.Join("", await _userManager.GetRolesAsync(oldUser));
            if(currentRole != appUserUpdateDto.Role)
            {
                IdentityResult? deleteToRoleResult = await _userManager.RemoveFromRoleAsync(oldUser, currentRole);
                // sildikten sonra yeni rolü ekleyelim.
                if (deleteToRoleResult.Succeeded)
                {
                    //Parametre olarak gelen rolü bulalım.
                    AppRole? findToMatchedRole = await _roleManager.FindByIdAsync(appUserUpdateDto.AppRoleId.ToString());

                    // Yeni rolün eklenmesi.
                    IdentityResult? newRoleAddedOldUserResult = await _userManager.AddToRoleAsync(oldUser, findToMatchedRole.Name);

                    if (!newRoleAddedOldUserResult.Succeeded)
                        return CustomResponseDto<AppUserUpdateDto>.IdentityFail(ResponseType.IdentityError, newRoleAddedOldUserResult.ConvertToCustomIdentityError());
                    
                }
                else
                    return CustomResponseDto<AppUserUpdateDto>.IdentityFail(ResponseType.IdentityError, deleteToRoleResult.ConvertToCustomIdentityError());
                
            }
            if(appUserUpdateDto.Photo != null)
            {
                // Eski Resmi Dizinden Silelim.
                var oldImgDeleteResult = await _imgHelper.RemoveImageAsync(appUserUpdateDto.Img.FileName);
                if(oldImgDeleteResult.ResponseType == ResponseType.Success)
                {
                    // Eski resmi Veritabanından da silelim.
                    await _repositoryManager.ImgRepository.DeleteAsync(oldUser.Img);
                    var imgUploadResult = await _imgHelper.UploadImageAsync(appUserUpdateDto.FirstName, appUserUpdateDto.Photo, ImageType.User);
                    if (imgUploadResult.ResponseType == ResponseType.Success)
                    {
                        Img newImg = new()
                        {
                            FullName = imgUploadResult.Data.FullName,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            CreatedBy = _claimsPrincipal.GetLoggerInAppUserEmail(),
                            ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail(),
                            FileType = imgUploadResult.Data.FileType,
                            IsActive = true,
                            IsDeleted = false,

                        };
                        await _repositoryManager.ImgRepository.CreateAsync(newImg);
                        await _unitOfWork.CommitAsync();
                        oldUser.ImgId = newImg.Id;
                    }
                }
                
            }
            IdentityResult? appUserUpdateResult = await _userManager.UpdateAsync(oldUser);
            if (!appUserUpdateResult.Succeeded)
                return CustomResponseDto<AppUserUpdateDto>.IdentityFail(ResponseType.IdentityError, appUserUpdateResult.ConvertToCustomIdentityError());
            await _userManager.UpdateSecurityStampAsync(oldUser);
            return CustomResponseDto<AppUserUpdateDto>.Success(ResponseType.Success, _mapper.Map<AppUserUpdateDto>(oldUser), $"AppUser Id : {appUserUpdateDto.Id} olan kullanıcı bilgileri başarılı bir şekilde güncellenmiştir.");
                

        }
    }
}
