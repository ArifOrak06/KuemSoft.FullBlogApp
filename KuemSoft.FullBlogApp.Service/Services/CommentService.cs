using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using KuemSoft.FullBlogApp.Core.DTOs.CommentDTOs;
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
    public class CommentService : ICommentService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CommentCreateDto> _createDtoValidator;
        private readonly IValidator<CommentUpdateDto> _updateDtoValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _claimsPrincipal;

        public CommentService(IRepositoryManager repositoryManager, IUnitOfWork unitOfWork, IMapper mapper, IValidator<CommentCreateDto> createDtoValidator, IValidator<CommentUpdateDto> updateDtoValidator, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryManager = repositoryManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _httpContextAccessor = httpContextAccessor;
            _claimsPrincipal = _httpContextAccessor.HttpContext.User;
        }

        public async Task<CustomResponseDto<CommentCreateDto>> CreateOneCommentAsync(CommentCreateDto commentCreateDto)
        {
            ValidationResult? validationResult = await _createDtoValidator.ValidateAsync(commentCreateDto);
            if(!validationResult.IsValid)
                return CustomResponseDto<CommentCreateDto>.ValidationFail(ResponseType.ValidError,validationResult.ConvertToCustomValidationError());
            Comment? newComment = _mapper.Map<Comment>(commentCreateDto);
            newComment.AppUserId = _claimsPrincipal.GetLoggedInAppUserId();
            newComment.CreatedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            newComment.ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            newComment.CreatedDate = DateTime.Now;
            newComment.ModifiedDate = DateTime.Now;
            newComment.IsActive = true;
            newComment.IsDeleted = false;
            await _repositoryManager.CommentRepository.CreateAsync(newComment);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<CommentCreateDto>.Success(ResponseType.Success,_mapper.Map<CommentCreateDto>(newComment),"Yorum ekleme işlemi başarılı bir şekilde gerçekleştirilmiştir.");
        }

        public async Task<CustomResponseDto<List<CommentDto>>> GetAllActivesAndNonDeletedCommentsWithAppUsersAndArticlesAsync()
        {
            List<Comment>? currentComments = await _repositoryManager.CommentRepository.GetByFilter(false, x => x.IsActive && !x.IsDeleted, x => x.Article, x => x.AppUser).ToListAsync();
            if (currentComments != null)
                return CustomResponseDto<List<CommentDto>>.Fail(ResponseType.NotFound, $"Sistemde aktif veya silinmemiş herhangi bir yorum bulunmamaktadır.");
            return CustomResponseDto<List<CommentDto>>.Success(ResponseType.Success, _mapper.Map<List<CommentDto>>(currentComments), "Sistemde kayıtlı aktif ve silinmemiş yorumlar başarılı bir şekilde listelenmiştir.");

        }

        public async Task<CustomResponseDto<List<CommentDto>>> GetAllDeletedCommentsWithAppUsersAndArticlesAsync()
        {
            List<Comment>? deletedComments = await _repositoryManager.CommentRepository.GetByFilter(false, x => x.IsDeleted && !x.IsActive, x => x.Article, x => x.AppUser).ToListAsync();
            if (deletedComments != null)
                return CustomResponseDto<List<CommentDto>>.Fail(ResponseType.NotFound, "Sistemde pasif veya silinmiş yorum bulunmamaktadır.");
            return CustomResponseDto<List<CommentDto>>.Success(ResponseType.Success, _mapper.Map<List<CommentDto>>(deletedComments), "Silinmiş ve pasif yorumlar başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseDto<CommentDto>> GetOneActiveAndNonDeletedCommentByIdWithAppUserAndArticleAsync(Guid commentId)
        {
            Comment? currentComment = await _repositoryManager.CommentRepository.GetByFilter(false,x=> x.Id.Equals(commentId)&&x.IsActive&&!x.IsDeleted,x =>x.Article,x=>x.AppUser).SingleOrDefaultAsync();
            if (currentComment == null)
                return CustomResponseDto<CommentDto>.Fail(ResponseType.NotFound, $"Sistemde Yorum ID : {commentId} olan yorum bulunamamıştır.");
            return CustomResponseDto<CommentDto>.Success(ResponseType.Success, _mapper.Map<CommentDto>(currentComment), $"Yorum Id : {commentId} olan yorum başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseDto<NoContentDto>> HardDeleteOneCommentAsync(Guid commentId)
        {
            Comment? currentComment = await _repositoryManager.CommentRepository.GetByFilter(true, x => x.Id.Equals(commentId) && x.IsActive && !x.IsDeleted, x => x.Article, x => x.AppUser).SingleOrDefaultAsync();
            if (currentComment == null)
                return CustomResponseDto<NoContentDto>.Fail(ResponseType.NotFound, $"Sistemde Yorum ID : {commentId} olan yorum bulunamamıştır.");
            await _repositoryManager.CommentRepository.DeleteAsync(currentComment);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(ResponseType.Success, $"Yorum Id : {commentId} olan yorum kalıcı olarak silinmiştir.");
        }

        public async Task<CustomResponseDto<NoContentDto>> SoftDeleteOneCommentAsync(Guid commentId)
        {
            Comment? currentComment = await _repositoryManager.CommentRepository.GetByFilter(true, x => x.Id.Equals(commentId) && x.IsActive && !x.IsDeleted, x => x.Article, x => x.AppUser).SingleOrDefaultAsync();
            if (currentComment == null)
                return CustomResponseDto<NoContentDto>.Fail(ResponseType.NotFound, $"Sistemde Yorum ID : {commentId} olan yorum bulunamamıştır.");
            currentComment.IsDeleted = true;
            currentComment.IsActive = false;
            currentComment.ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            currentComment.ModifiedDate = DateTime.Now;
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(ResponseType.Success, $"Yorum Id : {commentId} olan yorum arayüzden başarılı bir şekilde geçici olarak silinmiş pasif hale getirilmiştir.");
        }

        public async Task<CustomResponseDto<NoContentDto>> UndoDeleteOneCommentAsync(Guid commentId)
        {
            Comment? currentComment = await _repositoryManager.CommentRepository.GetByFilter(true, x => x.Id.Equals(commentId) && x.IsActive && !x.IsDeleted, x => x.Article, x => x.AppUser).SingleOrDefaultAsync();
            if (currentComment == null)
                return CustomResponseDto<NoContentDto>.Fail(ResponseType.NotFound, $"Sistemde Yorum ID : {commentId} olan yorum bulunamamıştır.");
            currentComment.IsDeleted = false;
            currentComment.IsActive = true;
            currentComment.ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            currentComment.ModifiedDate = DateTime.Now;
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(ResponseType.Success, $"Yorum Id : {commentId} olan yorum  başarılı bir şekilde aktif hale getirilmiştir.");
        }

        public async Task<CustomResponseDto<CommentUpdateDto>> UpdateOneCommentAsync(CommentUpdateDto commentUpdateDto)
        {
            ValidationResult? validationResult  = await _updateDtoValidator.ValidateAsync(commentUpdateDto);
            if (!validationResult.IsValid)
            {
                CommentUpdateDto? newUpdateDto = _mapper.Map<CommentUpdateDto>(await _repositoryManager.CommentRepository.GetByFilter(false, x => x.Id.Equals(commentUpdateDto.Id), x => x.Article, x => x.AppUser).SingleOrDefaultAsync());
                return CustomResponseDto<CommentUpdateDto>.ValidUpdateFail(ResponseType.ValidError, newUpdateDto, validationResult.ConvertToCustomValidationError());
            }
            Comment? currentComment = await _repositoryManager.CommentRepository.GetByFilter(true, x => x.Id.Equals(commentUpdateDto.Id), x => x.Article, x => x.AppUser).SingleOrDefaultAsync();
            if (currentComment == null)
                return CustomResponseDto<CommentUpdateDto>.Fail(responseType: ResponseType.NotFound, errorMessage: $"Yorum Id : {commentUpdateDto.Id}'ye sahip yorum sistemde bulunamamıştır.");
            currentComment.Text = commentUpdateDto.Text;
            currentComment.IsActive = commentUpdateDto.IsActive;
            if (commentUpdateDto.IsActive) currentComment.IsDeleted = false; else currentComment.IsDeleted = true;
            currentComment.ModifiedDate = DateTime.Now;
            currentComment.ModifiedBy = _claimsPrincipal.GetLoggerInAppUserEmail();
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<CommentUpdateDto>.Success(ResponseType.Success, _mapper.Map<CommentUpdateDto>(currentComment), $"Yorum Id : {commentUpdateDto.Id} olan yorum başarılı bir şekilde güncellenmiştir.");
        }
    }
}
