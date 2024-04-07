using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateLibrary.Data;
using PrivateLibrary.Data.Models;
using PrivateLibrary.Models;
using PrivateLibrary.Models.Pagination;
using static System.Reflection.Metadata.BlobBuilder;

namespace PrivateLibrary.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            LoginViewModel model = new();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Неуспешно влизане!";
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Неуспешно влизане");
                TempData["error"] = "Неуспешно влизане!";
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Неуспешно влизане");
                TempData["error"] = "Неуспешно влизане!";
                return View(model);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
