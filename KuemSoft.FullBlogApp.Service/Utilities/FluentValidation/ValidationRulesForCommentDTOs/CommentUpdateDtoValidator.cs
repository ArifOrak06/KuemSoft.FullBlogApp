using FluentValidation;
using KuemSoft.FullBlogApp.Core.DTOs.CommentDTOs;

namespace KuemSoft.FullBlogApp.Service.Utilities.FluentValidation.ValidationRulesForCommentDTOs
{
    public class CommentUpdateDtoValidator : AbstractValidator<CommentUpdateDto>
    {
        public CommentUpdateDtoValidator()
        {
            RuleFor(x => x.Text).NotNull().WithMessage("{PropertyName} alanı boş olamaz.").MinimumLength(20).WithMessage("{PropertyName} alanı mininmuym 30 karakterden oluşturulmalıdır.").WithName("Yorum");
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} alanı boş olamaz");
        }
    }
}
