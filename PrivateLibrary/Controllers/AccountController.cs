using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PrivateLibrary.Data.Models;
using PrivateLibrary.Models;

namespace PrivateLibrary.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                TempData["error"] = "Unsuccessful login!";
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Unsuccessful login");
                TempData["error"] = "Unsuccessful login!";
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Unsuccessful login");
                TempData["error"] = "Unsuccessful login!";
                return View(model);
            }

            TempData["success"] = "Successful login!";
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RegisterEmployee()
        {
            EmployeeRegisterViewModel model = new();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterEmployee(EmployeeRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Unsuccessful register!";
                return View(model);
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
                Employee = new Employee()
                {
                    MiddleName = model.MiddleName,
                    EGN = model.EGN
                }
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Employee");
                TempData["success"] = "Successful login!";
                return RedirectToAction(nameof(Login));
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterReader()
        {
            ReaderRegisterViewModel model = new();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterReader(ReaderRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Unsuccessful register!";
                return View(model);
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Reader");
                TempData["success"] = "Successful login!";
                return RedirectToAction(nameof(Login));
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["success"] = "You have been successfully loged out!";
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
