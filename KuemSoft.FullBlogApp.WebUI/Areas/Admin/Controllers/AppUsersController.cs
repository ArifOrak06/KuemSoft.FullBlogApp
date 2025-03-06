using KuemSoft.FullBlogApp.Core.Services;
using KuemSoft.FullBlogApp.SharedLibrary.Enums;
using Microsoft.AspNetCore.Mvc;

namespace KuemSoft.FullBlogApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppUsersController : Controller
    {
        private readonly IAppUserService _appUserService;

        public AppUsersController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _appUserService.GetAllAppUsersWithRoleAndArticlesAndCommentsAsync();
            if(result.ResponseType == ResponseType.Success)            
                return View(result.Data);
            
            if (result.ResponseType == ResponseType.NotFound)
                return NotFound();
            return View();
        }

    }
}
