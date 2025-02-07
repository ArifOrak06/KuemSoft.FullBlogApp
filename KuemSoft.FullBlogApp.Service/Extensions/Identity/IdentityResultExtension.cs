using KuemSoft.FullBlogApp.SharedLibrary.ResponseResultPattern;
using Microsoft.AspNetCore.Identity;

namespace KuemSoft.FullBlogApp.Service.Extensions.Identity
{
    public static class IdentityResultExtension
    {
        public static List<CustomIdentityError> ConvertToCustomIdentityError(this IdentityResult identityResult)
        {
            List<CustomIdentityError>? customErrors = new();
            foreach (var identityError in identityResult.Errors)
            {
                customErrors.Add(new CustomIdentityError()
                {
                    Description = identityError.Description,
                });
            }
            return customErrors;
        }
    }
}
