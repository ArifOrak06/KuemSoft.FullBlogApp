using FluentValidation;
using KuemSoft.FullBlogApp.Core.DTOs.CommentDTOs;

namespace KuemSoft.FullBlogApp.Service.Utilities.FluentValidation.ValidationRulesForCommentDTOs
{
    public class CommentCreateDtoValidator : AbstractValidator<CommentCreateDto>
    {
        public CommentCreateDtoValidator()
        {
            RuleFor(x => x.Text).NotNull().WithMessage("{PropertyName} alanı boş olamaz.");
          
        }
    }
}
