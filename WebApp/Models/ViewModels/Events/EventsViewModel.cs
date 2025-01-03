using WebApp.Models.DTOs;

namespace WebApp.Models.ViewModels.Events
{
    public class EventsViewModel
    {
        public eventoDTO Evento { get; set; } // Para exibir detalhes de um evento específico
        public List<eventoDTO> Eventos { get; set; } // Para listar múltiplos eventos (se necessário)
    }
}