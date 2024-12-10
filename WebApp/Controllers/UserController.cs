using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebApp.Helpers;
using WebApp.Models.DTOs;
using WebApp.Models.ViewModels.User;
using WebApp.Services;

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
            new Claim("UserID", user.userID.ToString()) // Ensure userID is stored as a string for claims
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

        private UserProfileViewModel GetProfileViewModel()
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new Exception("User ID not found in claims.");
            }

            if (!int.TryParse(userIdClaim, out int userId))
            {
                throw new Exception("Invalid User ID format.");
            }

            var dados = _userService.GetUserById(userId);

            if (dados == null)
            {
                throw new Exception("User not found in the database.");
            }

            return new UserProfileViewModel
            {
                email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                nome = dados.nome,
                apelido = dados.apelido,
                dt_nascimento = dados.dt_nascimento,
                morada = dados.morada,
                nif = dados.nif,
                cidade = dados.cidade,
                cod_postal = dados.cod_postal
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
