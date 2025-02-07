using FluentValidation.Results;
using KuemSoft.FullBlogApp.SharedLibrary.ResponseResultPattern;

namespace KuemSoft.FullBlogApp.Service.Extensions.FluentValidationEx
{
    public static class FluentValidationExtension
    {
        public static List<CustomValidationError> ConvertToCustomValidationError(this ValidationResult validationResult)
        {
            List<CustomValidationError>? customErrors = new();
            foreach (var validError in validationResult.Errors)
                customErrors.Add(new CustomValidationError
                {
                    ErrorMessage = validError.ErrorMessage,
                    PropertyName = validError.PropertyName,
                });
            return customErrors;
        }
    }
}
