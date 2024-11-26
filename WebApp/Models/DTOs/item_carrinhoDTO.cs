namespace WebApp.Models.DTOs
{
    public class item_carrinhoDTO
    {
        public int item_carrinho_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime modified_at { get; set; }
        public int carrinho_id { get; set; }
        public int bilhete_id { get; set; }
    }
}
