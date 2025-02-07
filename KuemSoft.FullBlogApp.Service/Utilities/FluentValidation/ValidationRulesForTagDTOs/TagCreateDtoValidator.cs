using FluentValidation;
using KuemSoft.FullBlogApp.Core.DTOs.TagDTOs;

namespace KuemSoft.FullBlogApp.Service.Utilities.FluentValidation.ValidationRulesForTagDTOs
{
    public class TagCreateDtoValidator  : AbstractValidator<TagCreateDto>
    {
        public TagCreateDtoValidator()
        {
            RuleFor(x => x.Text).NotNull().WithMessage("{PropertyName} alanı boş bırakılamaz.!").MinimumLength(3).WithMessage("{PropertyName} alanı minimum 3 karakterten oluşturulmalıdır.");
        }
    }
}
