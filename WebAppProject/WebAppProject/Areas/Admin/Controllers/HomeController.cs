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

        public IActionResult AddEmployee()
        {
            return View();
        }
        public IActionResult ManageEmployee()
        {
            return View();
        }

        public IActionResult ManageCustomerAcc()
        {
            return View();
        }

        public IActionResult ManageWebsite()
        {
            return View();
        }

        public IActionResult Edit ()
        {
            return View("CustomerEditForm");
        }

    }
}
