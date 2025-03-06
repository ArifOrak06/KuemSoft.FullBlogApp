using FluentValidation;
using KuemSoft.FullBlogApp.Core.DTOs.AppUserDTOs;

namespace KuemSoft.FullBlogApp.Service.Utilities.FluentValidation.ValidationRulesForAppUserDTOs
{
    public class AppUserLoginDtoValidator : AbstractValidator<AppUserLoginDto>
    {
        public AppUserLoginDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("{PropertyName} alanı  bilgi girilmesi zorunlu bir alandır.").MaximumLength(100).WithMessage("{PropertyName} alanı maksimum 100 karakterden oluşturulabilir.").MinimumLength(11).WithMessage("{PropertyName} alanı minimum 11 karakterden oluşturulabilir.").WithName("E-Posta");
            RuleFor(x =>x.Password).NotNull().WithMessage("{PropertyName} alanı  bilgi girilmesi zorunlu bir alandır.").MaximumLength(11).WithMessage("{PropertyName} alanı maksimum 11 karakterden oluşturulabilir.").MinimumLength(4).WithMessage("{PropertyName} alanı minimum 4 karakterden oluşturulabilir.").WithName("Parola");
        }
    }
}
