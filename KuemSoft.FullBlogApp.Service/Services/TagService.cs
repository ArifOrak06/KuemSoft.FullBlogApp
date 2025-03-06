using AutoMapper;
using Azure;
using FluentValidation;
using FluentValidation.Results;
using KuemSoft.FullBlogApp.Core.DTOs.TagDTOs;
using KuemSoft.FullBlogApp.Core.Entities.Concrete;
using KuemSoft.FullBlogApp.Core.Repositories;
using KuemSoft.FullBlogApp.Core.Services;
using KuemSoft.FullBlogApp.Core.UnitOfWork;
using KuemSoft.FullBlogApp.Service.Extensions.FluentValidationEx;
using KuemSoft.FullBlogApp.Service.Extensions.Identity;
using KuemSoft.FullBlogApp.SharedLibrary.Enums;
using KuemSoft.FullBlogApp.SharedLibrary.ResponseResultPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KuemSoft.FullBlogApp.Service.Services
{
    public class TagService : ITagService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<TagCreateDto> _createDtoValidator;
        private readonly IValidator<TagUpdateDto> _updateDtoValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _claimsPrincipal;

        public TagService(IRepositoryManager repositoryManager, IUnitOfWork unitOfWork, IMapper mapper, IValidator<TagCreateDto> createDtoValidator, IValidator<TagUpdateDto> updateDtoValidator, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryManager = repositoryManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _httpContextAccessor = httpContextAccessor;
            _claimsPrincipal = _httpContextAccessor.HttpContext.User;
        }

        public async Task<CustomResponseDto<TagCreateDto>> CreateOneTagAsync(TagCreateDto tagCreateDto)
        {
            ValidationResult validationResult = await _createDtoValidator.ValidateAsync(tagCreateDto);
            if (!validationResult.IsValid)
                return CustomResponseDto<TagCreateDto>.ValidationFail(ResponseType.ValidError, validationResult.ConvertToCustomValidationError());
            Tag? newTag = _mapper.Map<Tag>(tagCreateDto);
            newTag.CreatedDate = DateTime.Now;
            newTag.CreatedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            newTag.ModifiedDate = DateTime.Now;
            newTag.ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            if (newTag == null)
                return CustomResponseDto<TagCreateDto>.Fail(ResponseType.Error, $"{tagCreateDto.Text} isimli etiketin dönüştürme aşamasında hata oluştu.");
            await _repositoryManager.TagRepository.CreateAsync(newTag);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<TagCreateDto>.Success(ResponseType.Success, _mapper.Map<TagCreateDto>(newTag),$"{tagCreateDto.Text} isimli etiketin ekleme işlemi başarılı bir şekilde gerçekleştirilmiştir.");
        }

        public async Task<CustomResponseDto<List<TagDto>>> GetAllActivesAndNonDeletedTagsWithArticlesAsync()
        {
            List<Tag>? tags = await _repositoryManager.TagRepository.GetByFilter(false,x => x.IsActive && !x.IsDeleted, x => x.Articles).ToListAsync();
            if (tags is null)
                return CustomResponseDto<List<TagDto>>.Fail(ResponseType.NotFound, "Sistemde kayıtlı aktif veya silinmemiş etiket bulunmamaktadır.");
            return CustomResponseDto<List<TagDto>>.Success(responseType: ResponseType.Success, _mapper.Map<List<TagDto>>(tags), "Sistemde kayıtlı olan aktif ve silinmemiş etiketler başarılı bir şekilde listelenmiştir.");

        }

        public async Task<CustomResponseDto<List<TagDto>>> GetAllActivesTagsByArticleIdAsync(Guid articleId)
        {
            List<ArticleTags>? articleTags = await _repositoryManager.ArticleTagsRepository.GetByFilter(false, x => x.ArticleId.Equals(articleId),x => x.Tag).ToListAsync();
            List<TagDto>? newArticleTagList = new();
            foreach(ArticleTags articleTag in articleTags)
            {
                var tagDto = _mapper.Map<TagDto>(articleTag.Tag);
                newArticleTagList.Add(tagDto);
            }
            return CustomResponseDto<List<TagDto>>.Success(ResponseType.Success, newArticleTagList, $"Makale Id : {articleId} olan makaleye ait etiketler başarılı bir şekilde listelenmiştir.");

                
        }

        public async Task<CustomResponseDto<List<TagDto>>> GetAllDeletedTagsWithArticlesAsync()
        {
            List<Tag>? deletedTags = await _repositoryManager.TagRepository.GetByFilter(false,x => x.IsDeleted&&!x.IsActive,x => x.Articles).ToListAsync();
            if (deletedTags is null)
                return CustomResponseDto<List<TagDto>>.Fail(ResponseType.NotFound, "Sistemde silinmiş etiket bulunmamaktadır.");
            return CustomResponseDto<List<TagDto>>.Success(ResponseType.Success,_mapper.Map<List<TagDto>>(deletedTags),"Sistemde kayıtlı geçici olarak silinmiş tüm makaleler başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseDto<TagDto>> GetOneActiveAndNonDeletedTagWithArticlesByTagIdAsync(Guid tagId)
        {
            Tag? currentTag = await _repositoryManager.TagRepository.GetByFilter(false, x => x.Id.Equals(tagId) && x.IsActive && !x.IsDeleted, x => x.Articles).SingleOrDefaultAsync();
            if (currentTag is null)
                return CustomResponseDto<TagDto>.Fail(ResponseType.NotFound, $"Sistemde {tagId} ile kayıtlı bir etiket bulunmamaktadır.");
            return CustomResponseDto<TagDto>.Success(responseType: ResponseType.Success, _mapper.Map<TagDto>(currentTag), $"Sistemde kayıtlı aktif {tagId}'ye sahip makale başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseDto<NoContentDto>> HardDeleteOneTagAsync(Guid tagId)
        {
            Tag? currentTag = await _repositoryManager.TagRepository.GetByFilter(true, x => x.Id.Equals(tagId), x => x.Articles).SingleOrDefaultAsync();
            if(currentTag is null)
                return CustomResponseDto<NoContentDto>.Fail(ResponseType.NotFound, $"Sistemde {tagId} ile kayıtlı bir etiket bulunmamaktadır.");
            await _repositoryManager.TagRepository.DeleteAsync(currentTag);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(ResponseType.Success, $"Etiket ID : {tagId} olan etiket kalıcı olarak silinmiştir.");
        }

        public async Task<CustomResponseDto<NoContentDto>> SoftDeleteOneTagAsync(Guid tagId)
        {
            Tag? currentTag = await _repositoryManager.TagRepository.GetByFilter(true, x => x.Id.Equals(tagId), x => x.Articles).SingleOrDefaultAsync();
            if (currentTag is null)
                return CustomResponseDto<NoContentDto>.Fail(ResponseType.NotFound, $"Sistemde {tagId} ile kayıtlı bir etiket bulunmamaktadır.");
            currentTag.IsDeleted = true;
            currentTag.IsActive = false;
            currentTag.ModifiedDate = DateTime.Now;
            currentTag.ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(ResponseType.Success, $"Etiket Id : {tagId} olan etiket geçici olarak silinmiş ve pasif hale getirilmiştir.");
        }

        public async Task<CustomResponseDto<NoContentDto>> UndoDeleteOneTagAsync(Guid tagId)
        {
            Tag? currentTag = await _repositoryManager.TagRepository.GetByFilter(true, x => x.Id.Equals(tagId), x => x.Articles).SingleOrDefaultAsync();
            if (currentTag is null)
                return CustomResponseDto<NoContentDto>.Fail(ResponseType.NotFound, $"Sistemde {tagId} ile kayıtlı bir etiket bulunmamaktadır.");
            currentTag.IsDeleted = false;
            currentTag.IsActive = true;
            currentTag.ModifiedDate = DateTime.Now;
            currentTag.ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(ResponseType.Success,$"Etiket Id : {tagId} olan etiket aktif hale getirilmiş yeniden yüklenmiştir.");
        }

        public async Task<CustomResponseDto<TagUpdateDto>> UpdateOneTagAsync(TagUpdateDto tagUpdateDto)
        {
            ValidationResult? validationResult = await _updateDtoValidator.ValidateAsync(tagUpdateDto);
            if (!validationResult.IsValid)
            {
                TagUpdateDto updateDto = _mapper.Map<TagUpdateDto>(await _repositoryManager.TagRepository.GetByFilter(false, x => x.Id.Equals(tagUpdateDto.Id)).SingleOrDefaultAsync());
                return CustomResponseDto<TagUpdateDto>.ValidUpdateFail(ResponseType.ValidError, updateDto, validationResult.ConvertToCustomValidationError());
            }
            Tag? currentTag = await _repositoryManager.TagRepository.GetByFilter(true, x => x.Id.Equals(tagUpdateDto.Id), x => x.Articles).SingleOrDefaultAsync();
            if (currentTag is null)
                return CustomResponseDto<TagUpdateDto>.Fail(responseType: ResponseType.NotFound,$"Etiket Id : {tagUpdateDto.Id}'ye sahip etiket bulunmamaktadır.");
            currentTag.IsActive = currentTag.IsActive;
            currentTag.IsDeleted = currentTag.IsActive ? false : true;
            currentTag.Text = tagUpdateDto.Text;
            currentTag.ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            currentTag.ModifiedDate = DateTime.Now;
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<TagUpdateDto>.Success(ResponseType.Success, _mapper.Map<TagUpdateDto>(currentTag), $"Etiket Id : {tagUpdateDto.Id} etiket başarılı bir şekilde güncellenmiştir.");
                
            


        }
    }
}
