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
        public async Task<IActionResult> AhAmáliaLIVINGEXPERIENCE()
        {
            var evento = await _eventsService.GetEventoByTituloAsync("Ah Amália - Living Experience");

            if (evento == null) return NotFound();

            var viewModel = new EventsViewModel
            {
                titulo = evento.titulo,
                imagem = evento.imagem,
                tipo = evento.tipo,
                classificacao = evento.classificacao,
                data_hora = evento.data_hora,
                local_evento = evento.local_evento,
                descricao = evento.descricao, 
                preco = evento.preco,
                lotacao = evento.lotacao
            };

            return View(viewModel);
        }

        public async Task<IActionResult> FicaComigoEstaNoite()
        {
            var evento = await _eventsService.GetEventoByTituloAsync("Fica Comigo Esta Noite");

            if (evento == null) return NotFound();

            var viewModel = new EventsViewModel
            {
                titulo = evento.titulo,
                imagem = evento.imagem,
                tipo = evento.tipo,
                classificacao = evento.classificacao,
                data_hora = evento.data_hora,
                local_evento = evento.local_evento,
                descricao = evento.descricao,
                preco = evento.preco,
                lotacao = evento.lotacao
            };

            return View(viewModel);
        }

        public async Task<IActionResult> QueridoEvanHansen()
        {
            var evento = await _eventsService.GetEventoByTituloAsync("Querido Evan Hansen");

            if (evento == null) return NotFound();

            var viewModel = new EventsViewModel
            {
                titulo = evento.titulo,
                imagem = evento.imagem,
                tipo = evento.tipo,
                classificacao = evento.classificacao,
                data_hora = evento.data_hora,
                local_evento = evento.local_evento,
                descricao = evento.descricao,
                preco = evento.preco,
                lotacao = evento.lotacao
            };

            return View(viewModel);
        }

        public async Task<IActionResult> SolteiraCasadaViuvaDivorciada()
        {
            var evento = await _eventsService.GetEventoByTituloAsync("Solteira Casada Viúva Divorciada");

            if (evento == null) return NotFound();

            string descricao = string.Empty;
            if (!string.IsNullOrEmpty(evento.descricao))
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), evento.descricao);
                if (System.IO.File.Exists(filePath))
                {
                    descricao = System.IO.File.ReadAllText(filePath);
                }
            }
            else
            {
                descricao = "Descrição não disponível.";
            }

            var viewModel = new EventsViewModel
            {
                titulo = evento.titulo,
                imagem = evento.imagem,
                tipo = evento.tipo,
                classificacao = evento.classificacao,
                data_hora = evento.data_hora,
                local_evento = evento.local_evento,
                descricao = descricao,
                preco = evento.preco,
                lotacao = evento.lotacao
            };

            return View(viewModel);
        }

        public async Task<IActionResult> AMorteDoCorvo()
         {
            var evento = await _eventsService.GetEventoByTituloAsync("A Morte do Corvo");

            if (evento == null) return NotFound();

            string descricao = string.Empty;
            if (!string.IsNullOrEmpty(evento.descricao))
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), evento.descricao);
                if (System.IO.File.Exists(filePath))
                {
                    descricao = System.IO.File.ReadAllText(filePath);
                }
            }
            else
            {
                descricao = "Descrição não disponível.";
            }

            var viewModel = new EventsViewModel
            {
                titulo = evento.titulo,
                imagem = evento.imagem,
                tipo = evento.tipo,
                classificacao = evento.classificacao,
                data_hora = evento.data_hora,
                local_evento = evento.local_evento,
                descricao = descricao,
                preco = evento.preco,
                lotacao = evento.lotacao
            };

            return View(viewModel);
        }
        public IActionResult BuyTicket()
        {
            return View();
        }
    }
}
