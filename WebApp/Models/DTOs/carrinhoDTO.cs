namespace WebApp.Models.DTOs
{
    public class carrinhoDTO
    {
        public int carrinho_id { get; set; }
        public decimal total { get; set; }
        public DateTime created_at { get; set; }
        public DateTime modified_at { get; set; }
        public int userID { get; set; }
    }
}
