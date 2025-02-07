using FluentValidation;
using KuemSoft.FullBlogApp.Core.DTOs.CategoryDTOs;

namespace KuemSoft.FullBlogApp.Service.Utilities.FluentValidation.ValidationRulesForCategoryDTOs
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Description).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.").WithName("Kategori Adı");
            RuleFor(x => x.Id).NotNull();

        }
    }
}
