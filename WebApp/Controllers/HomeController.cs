using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApp.Helpers;
using WebApp.Models.ViewModels.Home;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService _homeService;

        public HomeController(IOptions<MyOptions> myOptions)
        {
            _homeService = new HomeService(myOptions);
        }
        public IActionResult Index()
        {
            var eventos = _homeService.GetEventos();
            var viewModel = new HomeIndexViewModel
            {
                Eventos = eventos
            };

            return View(viewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}