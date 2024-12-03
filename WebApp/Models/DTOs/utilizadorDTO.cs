namespace WebApp.Models.DTOs
{
    public class utilizadorDTO
    {
        public int userID { get; set; }
        public string nome { get; set; }
        public string apelido { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public DateOnly dt_nascimento { get; set; }
        public string morada { get; set; }
        public int nif { get; set; }
        public string cidade { get; set; }
        public string? cod_postal { get; set; }
        public DateTime created_at { get; set; }
        public DateTime modified_at { get; set; }
    }
}
