namespace WebApp.Models.DTOs
{
    public class encomendaDTO
    {
        public int encomenda_id { get; set; }
        public decimal total { get; set; }
        public string estado_pagamento { get; set; }
        public DateTime created_at { get; set; }
        public int userID { get; set; }
        public int carrinho_id { get; set; }
    }
}
