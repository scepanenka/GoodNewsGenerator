using Microsoft.AspNetCore.Mvc;

namespace GoodNews.MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}