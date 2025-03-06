using KuemSoft.FullBlogApp.Core.DTOs.ArticleDTOs;
using KuemSoft.FullBlogApp.Core.Services;
using KuemSoft.FullBlogApp.SharedLibrary.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KuemSoft.FullBlogApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
  
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        public ArticlesController(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _articleService.GetAllActivesAndNonDeletedArticlesWithAssociatedEntitiesAsync();
            if(result.ResponseType == ResponseType.Success)
                return View(result.Data);
            if (result.ResponseType == ResponseType.NotFound)
                return View();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateArticle()
        {
            var categoryDtos = (await _categoryService.GetAllActivesAndNonDeletedCategoriesWithArticlesAsync()).Data;
            
            return View(new ArticleCreateDto
            {
                Categories = categoryDtos,
            });
        }
        [HttpPost]
        public async Task<IActionResult> CreateArticle(ArticleCreateDto articleCreateDto)
        {
            // Makaleye Etiket Atama TagController içerisinde yapılacak.! Makale ve Etiket arasında çoka çok ilişki var !!
            var result = await _articleService.CreateOneArticleAsync(articleCreateDto);
            if (result.ResponseType == ResponseType.Success)
                return RedirectToAction("Index", "Articles", new { Area = "Admin" });
            if (result.ResponseType == ResponseType.ValidError)
                foreach (var validError in result.ValidationErrors)
                    ModelState.AddModelError(validError.PropertyName, validError.ErrorMessage);
            var categoryDtos = (await _categoryService.GetAllActivesAndNonDeletedCategoriesWithArticlesAsync()).Data;
            return View(new ArticleCreateDto
            {
                Categories = categoryDtos,
            });
        }
    }
}
