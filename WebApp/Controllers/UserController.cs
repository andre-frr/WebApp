using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModels.User;
using WebApp.Models.DTOs;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            UserRegisterViewModel model = new UserRegisterViewModel();

            return View(model);
        }
        [HttpPost]
        public IActionResult Register(UserRegisterViewModel model)
        {
            utilizadorDTO dto = new utilizadorDTO();
            dto.nome = model.nome;
            dto.apelido = model.apelido;
            dto.email = model.email;
            dto.pass = model.pass;

            return View("Profile", GetProfileViewModel());

        }
        public IActionResult Profile()
        {
            return View();
        }
        private UserProfileViewModel GetProfileViewModel()
        {
            UserProfileViewModel model = new UserProfileViewModel();
            return model;
        }
    }
}
