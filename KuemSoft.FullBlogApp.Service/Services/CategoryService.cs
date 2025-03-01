using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using KuemSoft.FullBlogApp.Core.DTOs.CategoryDTOs;
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
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CategoryCreateDto> _createDtoValidator;
        private readonly IValidator<CategoryUpdateDto> _updateDtoValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _claimsPrincipal;

        public CategoryService(IRepositoryManager repositoryManager, IUnitOfWork unitOfWork, IMapper mapper, IValidator<CategoryCreateDto> createDtoValidator, IValidator<CategoryUpdateDto> updateDtoValidator, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryManager = repositoryManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _httpContextAccessor = httpContextAccessor;
            _claimsPrincipal = _httpContextAccessor.HttpContext.User;
        }

        public async Task<CustomResponseDto<CategoryCreateDto>> CreateOneCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            ValidationResult? validationResult = await _createDtoValidator.ValidateAsync(categoryCreateDto);
            if (!validationResult.IsValid)
                return CustomResponseDto<CategoryCreateDto>.ValidationFail(ResponseType.ValidError, validationResult.ConvertToCustomValidationError());
            Category? newCategory = _mapper.Map<Category>(categoryCreateDto);
            if (newCategory == null)
                return CustomResponseDto<CategoryCreateDto>.Fail(ResponseType.Error, $"Kategori adı : {categoryCreateDto.Description} olan kategori ekleme işlemi varlık dönüştürme hatası nedeniyle sisteme eklenememiştir.");
            newCategory.CreatedDate = DateTime.Now;
            newCategory.ModifiedDate = DateTime.Now;
            newCategory.IsActive = true;
            newCategory.IsDeleted = false;
            newCategory.ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            newCategory.CreatedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            await _repositoryManager.CategoryRepository.CreateAsync(newCategory);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<CategoryCreateDto>.Success(ResponseType.Success,_mapper.Map<CategoryCreateDto>(newCategory),$"Kategori Adı : {categoryCreateDto.Description} olan kategori sisteme eklenmiştir.");
        }

        public async Task<CustomResponseDto<List<CategoryDto>>> GetAllActivesAndNonDeletedCategoriesWithArticlesAsync()
        {
            List<Category>? categories = await _repositoryManager.CategoryRepository.GetByFilter(false, x => x.IsActive && !x.IsDeleted, x => x.Articles).ToListAsync();
            if (categories == null)
                return CustomResponseDto<List<CategoryDto>>.Fail(ResponseType.NotFound, "Sistemde Aktif veya Silinmemiş kategori bulunmamaktadır.");
            return CustomResponseDto<List<CategoryDto>>.Success(ResponseType.Success, _mapper.Map<List<CategoryDto>>(categories), "Sistemde kayıtlı aktif ve silinmemiş tüm kategoriler başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseDto<List<CategoryDto>>> GetAllDeletedCategoriesWithArticlesAsync()
        {
            List<Category>? deletedCategories = await _repositoryManager.CategoryRepository.GetByFilter(false, x => !x.IsActive && x.IsDeleted, x => x.Articles).ToListAsync();
            if (deletedCategories == null)
                return CustomResponseDto<List<CategoryDto>>.Fail(ResponseType.NotFound, "Sistemde pasif veya silinmiş kategori bulunmamaktadır.");
            return CustomResponseDto<List<CategoryDto>>.Success(ResponseType.Success, _mapper.Map<List<CategoryDto>>(deletedCategories), "Sistemde kayıtlı pasif ve silinmiş tüm kategoriler başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseDto<CategoryDto>> GetOneActiveAndNonDeletedCategoryByIdWithArticlesAsync(Guid categoryId)
        {
            Category? currentCategory = await _repositoryManager.CategoryRepository.GetByFilter(false, x => x.Id.Equals(categoryId), x => x.Articles).SingleOrDefaultAsync();
            if (currentCategory == null)
                return CustomResponseDto<CategoryDto>.Fail(ResponseType.NotFound, $"Sistemde Kategori ID : {categoryId}'ye sahip kategori bulunamamıştır.");
            return CustomResponseDto<CategoryDto>.Success(ResponseType.Success, _mapper.Map<CategoryDto>(currentCategory), $"Sistemde kayıtlı Kategori Id : {categoryId} olan kategori başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseDto<NoContentDto>> HardDeleteOneCategoryAsync(Guid categoryId)
        {
            Category? currentCategory = await _repositoryManager.CategoryRepository.GetByFilter(true, x => x.Id.Equals(categoryId), x => x.Articles).SingleOrDefaultAsync();
            if (currentCategory == null)
                return CustomResponseDto<NoContentDto>.Fail(ResponseType.NotFound, $"Sistemde Kategori ID : {categoryId}'ye sahip kategori bulunamamıştır.");
            await _repositoryManager.CategoryRepository.DeleteAsync(currentCategory);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(responseType: ResponseType.Success, $"Kategori Id : {categoryId} olan kategori kalıcı olarak silinmiştir.");
        }

        public async Task<CustomResponseDto<NoContentDto>> SoftDeleteOneCategoryAsync(Guid categoryId)
        {
            Category? currentCategory = await _repositoryManager.CategoryRepository.GetByFilter(true, x => x.Id.Equals(categoryId), x => x.Articles).SingleOrDefaultAsync();
            if (currentCategory == null)
                return CustomResponseDto<NoContentDto>.Fail(ResponseType.NotFound, $"Sistemde Kategori ID : {categoryId}'ye sahip kategori bulunamamıştır.");
            currentCategory.IsDeleted = true;
            currentCategory.IsActive = false;
            currentCategory.ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            currentCategory.ModifiedDate = DateTime.Now;
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(ResponseType.Success, $"Kategori Id : {categoryId} olan kategori geçici olarak arayüzden silinmiştir.");
        }

        public async Task<CustomResponseDto<NoContentDto>> UndoDeleteOneCategoryAsync(Guid categoryId)
        {
            Category? currentCategory = await _repositoryManager.CategoryRepository.GetByFilter(true, x => x.Id.Equals(categoryId), x => x.Articles).SingleOrDefaultAsync();
            if (currentCategory == null)
                return CustomResponseDto<NoContentDto>.Fail(ResponseType.NotFound, $"Sistemde Kategori ID : {categoryId}'ye sahip kategori bulunamamıştır.");
            currentCategory.IsDeleted = false;
            currentCategory.IsActive = true;
            currentCategory.ModifiedDate = DateTime.Now;
            currentCategory.ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail() ?? "Adminastration";
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(ResponseType.Success, $"Kategori Id: {categoryId} olan kategori aktif hale getirilmiştir.");
        }

        public async Task<CustomResponseDto<CategoryUpdateDto>> UpdateOneCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            ValidationResult? validationResult = await _updateDtoValidator.ValidateAsync(categoryUpdateDto);
            if (!validationResult.IsValid)
            {
                CategoryUpdateDto? newUpdateDto = _mapper.Map<CategoryUpdateDto>(await _repositoryManager.CategoryRepository.GetByFilter(false, x => x.Id.Equals(categoryUpdateDto.Id), x => x.Articles).SingleOrDefaultAsync());
                return CustomResponseDto<CategoryUpdateDto>.ValidUpdateFail(ResponseType.ValidError, newUpdateDto, validationResult.ConvertToCustomValidationError());

            }
            Category? currentCategory = await _repositoryManager.CategoryRepository.GetByFilter(true, x => x.Id.Equals(categoryUpdateDto.Id), x => x.Articles).SingleOrDefaultAsync();
            if (currentCategory == null)
                return CustomResponseDto<CategoryUpdateDto>.Fail(ResponseType.Error, $"Kategori Id : {categoryUpdateDto.Id} olan kategori sistemde kayıtlı değildir.");
            currentCategory.Description = categoryUpdateDto.Description;
            currentCategory.IsActive = categoryUpdateDto.IsActive;
            currentCategory.IsDeleted = categoryUpdateDto.IsActive ? false : true;
            currentCategory.ModifiedDate = DateTime.Now;
            currentCategory.ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<CategoryUpdateDto>.Success(ResponseType.Success, _mapper.Map<CategoryUpdateDto>(currentCategory), $"Kategori Adı : {categoryUpdateDto.Description} olan makalenin güncelleme işlemi başarılı bir şekilde gerçekleşmiştir.");

        }
    }
}
