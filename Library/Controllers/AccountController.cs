using System.Threading.Tasks;
using Library.Models;
using Library.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Library.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController( UserManager<User> userManager,
            SignInManager<User> signInManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // GET
        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View(new RegistrationVM());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegistrationVM registrationVM)
        {
             var user = await _userManager.FindByNameAsync(registrationVM.UserName);
             if (user == null)
             {
                 user = await _userManager.FindByEmailAsync(registrationVM.Email);
                 if (user == null)
                 {
                     user = new User
                     {
                         Email = registrationVM.Email,
                         UserName = registrationVM.UserName
                     };
                     user.EmailConfirmed = true;
                     await _userManager.CreateAsync(user, registrationVM.Password);
                     return RedirectToAction("Login");
                 }
             }
             return RedirectToAction("Registration");
        }

        [AllowAnonymous]

        public IActionResult Login()
        {
            return View(new LoginVM());
        }
        
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Login([FromForm]LoginVM loginVM)
        {
            var user = await _userManager.FindByNameAsync(loginVM.Login);
            await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, false);
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        
    }
}