using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LanchesMac.Controllers
{         
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }
        [HttpPost]
       // [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            if(user !=null)
            {
                var result = await _signInManager.PasswordSignInAsync(user,loginVM.Password, false, false);

                if(result.Succeeded)
                {
                    if(string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");

                    }
                    return Redirect(loginVM.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Usuario/Senha inválidos ou não Cadastrados");
            return View(loginVM);
        }

        public IActionResult Register()// Por conta da atualização do framework , toda senha tem que ter Letra Maiuscula
            //letra minuscula, numero e um @. exemplo (Miko@123)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel loginVM)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser() { UserName = loginVM.UserName };

                var result = await _userManager.CreateAsync(user, loginVM.Password);

                if(result.Succeeded)
                {
                    //Adiciona o usuario padrão ao perfil Member
                    await _userManager.AddToRoleAsync(user, "Member");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("LoggedIn", "Account");

                }
              
            }
            return View(loginVM);
        }
        public ViewResult LoggedIn() => View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    } 
}