namespace WebApp.Models.DTOs
{
    public class eventoDTO
    {
        public int evento_id { get; set; }
        public string? titulo { get; set; }
        public string ?imagem { get; set; }
        public string ?tipo { get; set; }
        public string ?classificacao { get; set; }
        public DateTime data_hora { get; set; }
        public string ?local_evento { get; set; }
        public string ?descricao { get; set; }
        public decimal preco { get; set; }
        public int lotacao { get; set; }
        public DateTime created_at { get; set; }
        public DateTime modified_at { get; set; }
    }
}
