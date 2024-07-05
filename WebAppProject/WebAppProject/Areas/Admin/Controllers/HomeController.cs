using Microsoft.AspNetCore.Mvc;

namespace WebAppProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageItem()
        {
            return View();
        }

        public IActionResult AddProduct()
        {
            return View();
        }


    }
}
