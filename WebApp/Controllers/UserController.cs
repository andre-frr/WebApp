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

        private UserProfileViewModel GetProfileViewModel()
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userIDClaim = user.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

            if (string.IsNullOrEmpty(userIDClaim))
            {
                throw new Exception("ID não encontrado.");
            }

            if (!int.TryParse(userIDClaim, out int userID))
            {
                throw new Exception("Formato Inválido.");
            }

            var dados = _userService.GetUserById(userID);

            if (dados == null)
            {
                throw new Exception("Utilizador não está na base de dados.");
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
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProfile(UserProfileViewModel model)
        {
            if (model == null ||
    string.IsNullOrEmpty(model.nome) ||
    string.IsNullOrEmpty(model.apelido) ||
    string.IsNullOrEmpty(model.email) ||
    model.dt_nascimento == null ||
    string.IsNullOrEmpty(model.morada) ||
    model.nif <= 0 ||
    string.IsNullOrEmpty(model.cidade) ||
    string.IsNullOrEmpty(model.cod_postal))
            {
                ViewData["ErrorMessage"] = "Por favor, preencha todos os campos obrigatórios.";
                return View("Profile", model);
            }


            var user = _httpContextAccessor.HttpContext.User;
            var userIDClaim = user.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

            if (string.IsNullOrEmpty(userIDClaim) || !int.TryParse(userIDClaim, out int userID))
            {
                throw new Exception("ID não encontrado ou formato inválido.");
            }

            utilizadorDTO dto = new utilizadorDTO
            {
                userID = userID,
                nome = model.nome,
                apelido = model.apelido,
                email = model.email,
                dt_nascimento = model.dt_nascimento,
                morada = model.morada,
                nif = model.nif,
                cidade = model.cidade,
                cod_postal = model.cod_postal
            };

            var result = _userService.UpdateUser(dto);

            if (!result.status)
            {
                ViewData["ErrorMessage"] = "Ocorreu um erro ao atualizar os detalhes. Por favor, tente novamente.";
                return View("Profile", model);
            }

            ViewData["SuccessMessage"] = "Detalhes atualizados com sucesso.";
            return RedirectToAction("Profile");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(string currentPassword, string newPass, string confirmPass)
        {
            if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirmPass))
            {
                ViewData["ErrorMessage"] = "Todos os campos são obrigatórios.";
                return View("Profile", GetProfileViewModel());
            }

            if (newPass != confirmPass)
            {
                ViewData["ErrorMessage"] = "A nova password e a confirmação não coincidem.";
                return View("Profile", GetProfileViewModel());
            }

            var user = _httpContextAccessor.HttpContext.User;
            var userIDClaim = user.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

            if (string.IsNullOrEmpty(userIDClaim) || !int.TryParse(userIDClaim, out int userID))
            {
                throw new Exception("ID não encontrado ou formato inválido.");
            }

            var currentUser = _userService.GetUserById(userID);

            if (currentUser.pass != currentPassword)
            {
                ViewData["ErrorMessage"] = "A password atual está incorreta.";
                return View("Profile", GetProfileViewModel());
            }

            currentUser.pass = newPass;

            var result = _userService.UpdateUser(currentUser);

            if (!result.status)
            {
                ViewData["ErrorMessage"] = "Ocorreu um erro ao atualizar a password. Por favor, tente novamente.";
                return View("Profile", GetProfileViewModel());
            }

            ViewData["SuccessMessage"] = "Password atualizada com sucesso.";
            return RedirectToAction("Profile");
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
