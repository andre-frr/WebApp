using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModels.User;
using WebApp.Models.DTOs;
using WebApp.Services;
using Microsoft.Extensions.Options;
using WebApp.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.email) || string.IsNullOrEmpty(model.pass))
            {
                ViewData["ErrorMessage"] = "Preencha os campos todos!";
                return View(model);
            }

            var result = _userService.Get(model.email, model.pass);

            if (result.status == false || result.results == null || !result.results.Any())
            {
                ViewData["ErrorMessage"] = "Email ou password inválido. Tente novamente.";
                return View(model);
            }

            var user = result.results.First();

            if (user.pass != model.pass)
            {
                ViewData["ErrorMessage"] = "Email ou password inválido. Tente novamente.";
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.nome),
                new Claim(ClaimTypes.Email, user.email),
                new Claim("UserID", user.userID.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return RedirectToAction("Profile");
        }

        public IActionResult Register()
        {
            UserRegisterViewModel model = new UserRegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(UserRegisterViewModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.nome) || string.IsNullOrEmpty(model.email) || string.IsNullOrEmpty(model.pass))
            {
                return View(model);
            }

            utilizadorDTO dto = new utilizadorDTO
            {
                nome = model.nome,
                apelido = model.apelido,
                email = model.email,
                pass = model.pass
            };

            var result = _userService.Insert(dto);

            if (result.status == false)
            {
                return View(model);
            }

            return RedirectToAction("Login");
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View(GetProfileViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private UserProfileViewModel GetProfileViewModel()
        {
            var user = _httpContextAccessor.HttpContext.User;
            return new UserProfileViewModel
            {
                nome = user.Identity?.Name,
                email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
            };
        }
    }
}
