using FluentValidation;
using KuemSoft.FullBlogApp.Core.DTOs.CategoryDTOs;

namespace KuemSoft.FullBlogApp.Service.Utilities.FluentValidation.ValidationRulesForCategoryDTOs
{
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(x => x.Description).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.").WithName("Kategori Adı");

        }
    }
}
