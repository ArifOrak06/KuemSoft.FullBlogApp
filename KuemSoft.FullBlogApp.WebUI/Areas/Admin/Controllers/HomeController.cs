using Microsoft.AspNetCore.Mvc;

namespace KuemSoft.FullBlogApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        // AdminLayout Main Page
        public IActionResult Index()
        {
            return View();
        }
    }
}
