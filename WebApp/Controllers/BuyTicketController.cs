using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class BuyTicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Pay()
        {
            return View();
        }
    }
}
