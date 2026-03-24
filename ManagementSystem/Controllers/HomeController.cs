using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Models;

namespace ManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(string message)
        {
            var model = new ErrorView { Message = message };
            return View("Error", model);
        }
    }
}
