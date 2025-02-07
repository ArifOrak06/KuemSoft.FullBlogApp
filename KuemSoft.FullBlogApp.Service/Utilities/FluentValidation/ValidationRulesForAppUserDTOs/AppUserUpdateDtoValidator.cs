using FluentValidation;
using KuemSoft.FullBlogApp.Core.DTOs.AppUserDTOs;

namespace KuemSoft.FullBlogApp.Service.Utilities.FluentValidation.ValidationRulesForAppUserDTOs
{
    public class AppUserUpdateDtoValidator : AbstractValidator<AppUserUpdateDto>
    {
        public AppUserUpdateDtoValidator()
        {
            RuleFor(x => x.AppRoleId).NotNull().WithMessage("{PropertyName} alanı seçim yapılması zorunlu bir alandır.").WithName("Rol");
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Email).NotNull().WithMessage("{PropertyName} alanı doldurulması zorunlu bir alandır.").MinimumLength(11).WithMessage("{PropertyName} alanı minimum 11 karakterden oluşturulabilir.").WithName("E-Posta");
            
            RuleFor(x => x.FirstName).NotNull().WithMessage("{PropertyName} alanı doldurulması zorunlu bir alandır.").MinimumLength(3).WithMessage("{PropertyName} alanı minimum 3 karakterden oluşturulabilir.").MaximumLength(100).WithMessage("{PropertyName} alanı maksimum 100 karakterden oluşturulabilir.").WithName("Adınız");
            RuleFor(x => x.LastName).NotNull().WithMessage("{PropertyName} alanı doldurulması zorunlu bir alandır.").MinimumLength(3).WithMessage("{PropertyName} alanı minimum 3 karakterden oluşturulabilir.").MaximumLength(100).WithMessage("{PropertyName} alanı maksimum 100 karakterden oluşturulabilir.").WithName("Soyadınız");
        }
    }
}
