using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApp.Helpers;
using WebApp.Models.ViewModels.Events;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventsService _eventsService;

        public EventsController(IOptions<MyOptions> myOptions)
        {
            _eventsService = new EventsService(myOptions);
        }
        public IActionResult FicaComigoEstaNoite()
        {
            return View();
        }
        public IActionResult QueridoEvanHansen()
        {
            return View();
        }
        public async Task<IActionResult> AhAmáliaLIVINGEXPERIENCE()
        {
            var evento = await _eventsService.GetEventoByTituloAsync("Ah Amália - Living Experience");
            if (evento == null) return NotFound();

            var viewModel = new EventsViewModel { Evento = evento };
            return View(viewModel);
        }
        public IActionResult SolteiraCasadaViuvaDivorciada()
        {
            return View();
        }
        public IActionResult AMorteDoCorvo()
        {
            return View();
        }
        public IActionResult BuyTicket()
        {
            return View();
        }
    }
}
