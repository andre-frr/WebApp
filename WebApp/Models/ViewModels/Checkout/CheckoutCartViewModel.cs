namespace WebApp.Models.ViewModels.Checkout
{
    public class CheckoutCartViewModel
    {
        public int carrinho_id { get; set; }
        public decimal total { get; set; } 
        public int? userID { get; set; }
    }
}
