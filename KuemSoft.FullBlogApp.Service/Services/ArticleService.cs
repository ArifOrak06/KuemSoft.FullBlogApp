using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using KuemSoft.FullBlogApp.Core.DTOs.ArticleDTOs;
using KuemSoft.FullBlogApp.Core.DTOs.ImgDTOs;
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
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KuemSoft.FullBlogApp.Service.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImgHelper _imgHelper;
        private readonly IValidator<ArticleCreateDto> _createDtoValidator;
        private readonly IValidator<ArticleUpdateDto> _updateDtoValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _claimsPrincipal;

        public ArticleService(IRepositoryManager repositoryManager, IUnitOfWork unitOfWork, IMapper mapper, IImgHelper imgHelper, IValidator<ArticleCreateDto> createDtoValidator, IValidator<ArticleUpdateDto> updateDtoValidator, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryManager = repositoryManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imgHelper = imgHelper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _httpContextAccessor = httpContextAccessor;
            _claimsPrincipal = _httpContextAccessor.HttpContext.User;
        }

        public async Task<CustomResponseDto<ArticleCreateDto>> CreateOneArticleAsync(ArticleCreateDto articleCreateDto)
        {
            ValidationResult? validationResult = await _createDtoValidator.ValidateAsync(articleCreateDto);
            if (!validationResult.IsValid)
                return CustomResponseDto<ArticleCreateDto>.ValidationFail(ResponseType.ValidError, validationResult.ConvertToCustomValidationError());
            Article? newArticle = _mapper.Map<Article>(articleCreateDto);
            if(articleCreateDto.Photo != null)
            {
                CustomResponseDto<ImgUploadDto>? imgUploadResult = await _imgHelper.UploadImageAsync(articleCreateDto.Title,articleCreateDto.Photo,ImageType.Article);
                if(imgUploadResult.ResponseType == ResponseType.Success)
                {
                    Img newImg = new()
                    {
                        CreatedDate = DateTime.Now,
                        FullName = imgUploadResult.Data.FullName,
                        FileType = imgUploadResult.Data.FileType,
                        ModifiedDate = DateTime.Now,
                        CreatedBy = _claimsPrincipal.GetLoggerInAppUserEmail(),
                        ModifiedBy = _claimsPrincipal?.GetLoggerInAppUserEmail(),
                        IsActive = true,
                        IsDeleted = false,
                    };

                    await _repositoryManager.ImgRepository.CreateAsync(newImg);
                    newArticle.ImgId = newImg.Id;
                }
            }
            newArticle.IsActive = true;
            newArticle.CreatedDate = DateTime.Now;
            newArticle.ModifiedDate = DateTime.Now;
            newArticle.IsDeleted = false;
            newArticle.AppUserId = _claimsPrincipal.GetLoggedInAppUserId();
            newArticle.CreatedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            newArticle.ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            await _repositoryManager.ArticleRepository.CreateAsync(newArticle);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<ArticleCreateDto>.Success(ResponseType.Success, _mapper.Map<ArticleCreateDto>(newArticle), $"Makale Başlığı : {articleCreateDto.Title} başlıklı makale başarılı bir şekilde oluşturulmuştur.");
        }

        public async Task<CustomResponseDto<List<ArticleDto>>> GetAllActivesAndNonDeletedArticlesWithAssociatedEntitiesByCategoryIdAsync(Guid categoryId)
        {
            List<Article>? currentArticles = await _repositoryManager.ArticleRepository.GetByFilter(false, x => x.CategoryId.Equals(categoryId) && x.IsActive && !x.IsDeleted, x => x.Img, x => x.Tags, x => x.AppUser, x => x.Comments, x => x.Category).ToListAsync();
            if (currentArticles == null)
                return CustomResponseDto<List<ArticleDto>>.Fail(ResponseType.NotFound, $"Sistemde Kategori Id : {categoryId}'ye ait aktif bir makale bulunmamaktadır.");
            return CustomResponseDto<List<ArticleDto>>.Success(ResponseType.Success, _mapper.Map<List<ArticleDto>>(currentArticles), $"Kategori Id : {categoryId}'ye ait aktif tüm makaleler başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseDto<List<ArticleDto>>> GetAllActivesAndNonDeletedArticlesWithAssociatedEntitiesAsync()
        {
            List<Article>? currentArticles = await _repositoryManager.ArticleRepository.GetByFilter(false,x => x.IsActive && !x.IsDeleted, x => x.Img, x => x.Tags, x => x.AppUser, x => x.Comments, x => x.Category).ToListAsync();
            if (currentArticles == null)
                return CustomResponseDto<List<ArticleDto>>.Fail(ResponseType.NotFound, "Sistemde aktif bir makale bulunmamaktadır.");
            return CustomResponseDto<List<ArticleDto>>.Success(ResponseType.Success, _mapper.Map<List<ArticleDto>>(currentArticles), "Sistemde kayıtlı olan tüm aktif makaleler başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseDto<List<ArticleDto>>> GetAllDeletedArticlesAsync()
        {
            List<Article>? currentArticles = await _repositoryManager.ArticleRepository.GetByFilter(false, x => x.IsDeleted && !x.IsActive, x => x.Img, x => x.Tags, x => x.AppUser, x => x.Comments, x => x.Category).ToListAsync();
            if (currentArticles == null)
                return CustomResponseDto<List<ArticleDto>>.Fail(ResponseType.NotFound, "Sistemde silinmiş pasif bir makale bulunmamaktadır.");
            return CustomResponseDto<List<ArticleDto>>.Success(ResponseType.Success, _mapper.Map<List<ArticleDto>>(currentArticles), "Sistemde kayıtlı olan tüm silinmiş ve pasif makaleler başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseDto<ArticleDto>> GetOneActiveAndNonDeletedArticleWithAssocieatedEntitiesByArticleIdAsync(Guid articleId)
        {
            Article? currentArticle = await _repositoryManager.ArticleRepository.GetByFilter(false,x => x.Id.Equals(articleId)&&x.IsActive&&!x.IsDeleted,x=>x.Category,x=>x.AppUser,x=>x.Tags,x=>x.Comments,x=>x.Img).SingleOrDefaultAsync();
            if (currentArticle == null)
                return CustomResponseDto<ArticleDto>.Fail(ResponseType.NotFound, $"Sistemde Article ID : {articleId}'ye kayıtlı böyle birç makale bulunmamaktadır.");
            return CustomResponseDto<ArticleDto>.Success(ResponseType.Success, _mapper.Map<ArticleDto>(currentArticle), $"Sistemde kayıtlı Article Id : {articleId}'ye sahip makale başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseDto<NoContentDto>> HardDeleteArticleAsync(Guid articleId)
        {
            Article? currentArticle = await _repositoryManager.ArticleRepository.GetByFilter(false, x => x.Id.Equals(articleId) && x.IsActive && !x.IsDeleted, x => x.Category, x => x.AppUser, x => x.Tags, x => x.Comments, x => x.Img).SingleOrDefaultAsync();
            if (currentArticle == null)
                return CustomResponseDto<NoContentDto>.Fail(ResponseType.NotFound, $"Sistemde Article ID : {articleId}'ye kayıtlı böyle birç makale bulunmamaktadır.");
            await _repositoryManager.ArticleRepository.DeleteAsync(currentArticle);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(ResponseType.Success, $"Article Id : {articleId}'ye sahip makale kalıcı olarak silinmiştir.");
        }

        public async Task<CustomResponseDto<NoContentDto>> SoftDeleteArticleAsync(Guid articleId)
        {
            Article? currentArticle = await _repositoryManager.ArticleRepository.GetByFilter(true, x => x.Id.Equals(articleId) && x.IsActive && !x.IsDeleted).SingleOrDefaultAsync();
            if (currentArticle == null)
                return CustomResponseDto<NoContentDto>.Fail(ResponseType.NotFound, $"Sistemde Article ID : {articleId}'ye kayıtlı böyle birç makale bulunmamaktadır.");
            currentArticle.IsDeleted = true;
            currentArticle.IsActive = false;
            currentArticle.ModifiedDate = DateTime.Now;
            currentArticle.ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(ResponseType.Success, $"Article Id : {articleId} olan makale arayüzden silme işlemi başarılı olarak gerçekleşmiştir.");
        }

        public async Task<CustomResponseDto<NoContentDto>> UndoDeleteOneArticleAsync(Guid articleId)
        {
            Article? currentArticle = await _repositoryManager.ArticleRepository.GetByFilter(true, x => x.Id.Equals(articleId) && x.IsActive && !x.IsDeleted).SingleOrDefaultAsync();
            if (currentArticle == null)
                return CustomResponseDto<NoContentDto>.Fail(ResponseType.NotFound, $"Sistemde Article ID : {articleId}'ye kayıtlı böyle birç makale bulunmamaktadır.");
            currentArticle.IsDeleted = false;
            currentArticle.IsActive = true;
            currentArticle.ModifiedDate = DateTime.Now;
            currentArticle.ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(ResponseType.Success, $"Article Id : {articleId} olan makale geri yükleme işlemi başarılı olarak gerçekleşmiştir.");
        }

        public async Task<CustomResponseDto<ArticleUpdateDto>> UpdateOneArticleAsync(ArticleUpdateDto articleUpdateDto)
        {
            ValidationResult? validationResult = await _updateDtoValidator.ValidateAsync(articleUpdateDto);
            if (!validationResult.IsValid)
            {
                ArticleUpdateDto? newUpdateDto = _mapper.Map<ArticleUpdateDto>(await _repositoryManager.ArticleRepository.GetByFilter(false, x => x.Id.Equals(articleUpdateDto.Id), x => x.Img, x => x.Comments, x => x.AppUser, x => x.Tags, x => x.Category).SingleOrDefaultAsync());
                return CustomResponseDto<ArticleUpdateDto>.ValidUpdateFail(ResponseType.ValidError, newUpdateDto, validationResult.ConvertToCustomValidationError());
            }
            Article? currentArticle = await _repositoryManager.ArticleRepository.GetByFilter(true, x => x.Id.Equals(articleUpdateDto.Id), x => x.Img, x => x.Tags, x => x.AppUser, x => x.Category, x => x.Comments).SingleOrDefaultAsync();
            if (currentArticle == null)
                return CustomResponseDto<ArticleUpdateDto>.Fail(ResponseType.NotFound, $"Makale Id : {articleUpdateDto.Id}'ye sahip bir makale sistemde bulunamamıştır.");
                
            
            currentArticle.Title = articleUpdateDto.Title;
            currentArticle.Content = articleUpdateDto.Content;
            currentArticle.ModifiedDate = DateTime.Now;
            currentArticle.ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            currentArticle.IsActive = articleUpdateDto.IsActive;
            if (currentArticle.IsActive) currentArticle.IsDeleted = false; else currentArticle.IsDeleted = true;
            currentArticle.CategoryId = currentArticle.CategoryId;
            if(articleUpdateDto.Photo != null)
            {
                CustomResponseDto<ImgUploadDto>? imgUploadResult = await _imgHelper.UploadImageAsync(articleUpdateDto.Title, articleUpdateDto.Photo, ImageType.Article);
                if(imgUploadResult.ResponseType == ResponseType.Success)
                {
                    Img newImg = new()
                    {
                        CreatedDate = DateTime.Now,
                        FullName = imgUploadResult.Data.FullName,
                        FileType = imgUploadResult.Data.FileType,
                        CreatedBy = _claimsPrincipal.GetLoggerInAppUserEmail(),
                        ModifiedDate = DateTime.Now,
                        ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail(),
                        IsActive = true,
                        IsDeleted = false
                    };

                    await _repositoryManager.ImgRepository.CreateAsync(newImg);
                    currentArticle.ImgId = newImg.Id;

                }
            }
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<ArticleUpdateDto>.Success(ResponseType.Success, _mapper.Map<ArticleUpdateDto>(currentArticle), $"Makale ID : {articleUpdateDto.Id}'ye sahip makalenin güncelleme işlemi başarılı bir şekilde gerçekleşmiştir.");


        }
    }
}
