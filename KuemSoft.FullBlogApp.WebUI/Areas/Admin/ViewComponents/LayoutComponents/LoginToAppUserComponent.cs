using KuemSoft.FullBlogApp.Core.Services;
using KuemSoft.FullBlogApp.Service.Extensions.Identity;
using KuemSoft.FullBlogApp.SharedLibrary.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KuemSoft.FullBlogApp.WebUI.Areas.Admin.ViewComponents.LayoutComponents
{
    public class LoginToAppUserComponent : ViewComponent
    {
        private readonly IAppUserService _appUserService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ClaimsPrincipal _claimsPrincipal;

        public LoginToAppUserComponent(IAppUserService appUserService, IHttpContextAccessor contextAccessor)
        {
            _appUserService = appUserService;
            _contextAccessor = contextAccessor;
            _claimsPrincipal = _contextAccessor.HttpContext.User;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loginAppUserId = _claimsPrincipal.GetLoggedInAppUserId();
            var result = await _appUserService.GetAppUserWithRoleAndArticlesAndCommentsAsync(loginAppUserId);
            if (result.ResponseType == ResponseType.Success)
                return View(result.Data);
            return View();
        }
    }
}
