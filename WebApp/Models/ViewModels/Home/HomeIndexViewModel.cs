namespace WebApp.Models.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public List<HomeEventViewModel> Eventos { get; set; }
    }

    public class HomeEventViewModel
    {
        public string titulo { get; set; }
        public string imagem { get; set; }
        public DateTime data_hora { get; set; }
        public string local_evento { get; set; }
    }
}