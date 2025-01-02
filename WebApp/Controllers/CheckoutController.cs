using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApp.Helpers;
using WebApp.Services;
using WebApp.Models.DTOs;

namespace WebApp.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly CheckoutService _checkoutService;

        public CheckoutController(IOptions<MyOptions> myOptions)
        {
            _checkoutService = new CheckoutService(myOptions);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cart()
        {
            int? userID = null;

            var userClaim = User?.FindFirst("UserID")?.Value;
            if (!string.IsNullOrEmpty(userClaim))
            {
                userID = int.Parse(userClaim);
            }

            carrinhoDTO dto = new carrinhoDTO
            {
                userID = userID ?? 0
            };

            var result = _checkoutService.AbrirCarrinho(userID);

            return View();
        }

    }
}
