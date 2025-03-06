using KuemSoft.FullBlogApp.Core.DTOs.AppUserDTOs;
using KuemSoft.FullBlogApp.Core.Entities.Concrete;
using KuemSoft.FullBlogApp.Core.Services;
using KuemSoft.FullBlogApp.SharedLibrary.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KuemSoft.FullBlogApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAppUserService _appUserService;

        public AuthController(SignInManager<AppUser> signInManager, IAppUserService appUserService)
        {
            _signInManager = signInManager;
            _appUserService = appUserService;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AppUserLoginDto appUserLoginDto)
        {
            var result =await _appUserService.LoginToAppUserAsync(appUserLoginDto);
            if(result.ResponseType == ResponseType.Success)
                return RedirectToAction("Index", "Home", new {Area="Admin"});
            if (result.ResponseType == ResponseType.ValidError)
                foreach (var error in result.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName,error.ErrorMessage);

            if (result.ResponseType == ResponseType.Error)
                return NotFound();
            return View();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
