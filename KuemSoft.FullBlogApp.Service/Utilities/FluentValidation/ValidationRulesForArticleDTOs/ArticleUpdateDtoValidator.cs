using FluentValidation;
using KuemSoft.FullBlogApp.Core.DTOs.ArticleDTOs;

namespace KuemSoft.FullBlogApp.Service.Utilities.FluentValidation.ValidationRulesForArticleDTOs
{
    public class ArticleUpdateDtoValidator : AbstractValidator<ArticleUpdateDto>
    {
        public ArticleUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.");
            RuleFor(x => x.Title).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.").MinimumLength(3).WithMessage("{PropertyName} alanı minimum 3 karakterden oluşturulmalıdır.").MaximumLength(200).WithMessage("{PropertyName} alanı maksimum 200 karakterden oluşturulmalıdır.").WithName("Başlık");

            RuleFor(x => x.Content).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.").MinimumLength(100).WithMessage("{PropertyName} alanı minimum 3 karakterden oluşturulmalıdır.").WithName("İçerik");
            RuleFor(x => x.CategoryId).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.");
            RuleFor(x => x.Photo).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.");
        }
    }
}
