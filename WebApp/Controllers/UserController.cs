using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModels.User;
using WebApp.Models.DTOs;
using WebApp.Services;
using Microsoft.Extensions.Options;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly MyOptions _myOptions;
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IOptions<MyOptions> myOptions, IHttpContextAccessor httpContextAccessor)
        {
            _userService = new UserService(myOptions);
            _httpContextAccessor = httpContextAccessor;
        }
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

            ExecutionResult<utilizadorDTO> result = _userService.Insert(dto);

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
