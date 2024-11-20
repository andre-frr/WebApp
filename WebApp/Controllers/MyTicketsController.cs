using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class MyTicketsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
