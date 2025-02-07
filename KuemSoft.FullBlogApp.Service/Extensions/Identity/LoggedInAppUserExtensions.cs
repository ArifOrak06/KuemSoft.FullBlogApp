using System.Security.Claims;

namespace KuemSoft.FullBlogApp.Service.Extensions.Identity
{
    public static class LoggedInAppUserExtensions
    {

        public static Guid GetLoggedInAppUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
        }
        public static string GetLoggerInAppUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        }

    }
}
