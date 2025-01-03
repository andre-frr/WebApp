namespace WebApp.Models.ViewModels.Events
{
    public class EventsViewModel
    {
        public string? titulo { get; set; }
        public string? imagem { get; set; }
        public string? tipo { get; set; }
        public string? classificacao { get; set; }
        public DateTime data_hora { get; set; }
        public string? local_evento { get; set; }
        public string? descricao { get; set; }
        public decimal preco { get; set; }
        public int lotacao { get; set; }
    }
}