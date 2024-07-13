using Microsoft.AspNetCore.Mvc;

namespace WebAppProject.Controllers
{
    public class Login : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
